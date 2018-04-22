using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CartolaWeb.Entities;


namespace CartolaWeb.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		// GET api/values
		[HttpGet]
		public IEnumerable<UsersInfo> Get()
		{
			var Times = GetTimesInfo().Result;

			//por rodada, ajeitar depois TODO
			var Escalacao = GetEscalacao(Times);

			var http = (HttpWebRequest)WebRequest.Create("https://api.cartolafc.globo.com/atletas/pontuados");
			http.ContentType = "application/json;charset=UTF-8";
			http.UserAgent =
				"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2227.1 Safari/537.36";
			http.Headers.Add("X-GLB-TOKEN", Globals.Token);

			var response = http.GetResponse();

			var stream = response.GetResponseStream();
			var sr = new StreamReader(stream);
			var content = sr.ReadToEnd();


			foreach (var escalados in Escalacao)
			{
				var pontosParciais = 0.00;

				if (escalados.atletas == null)
				{
					Times.First(time => time.nome_cartola == escalados.DonodoTime).pontuacaoParcial = "Mongolei e não escalei meu time nessa rodada.";
					break;
				}

				
					foreach (var atleta in escalados.atletas)
					{
						var parse = double.TryParse(GetPontuacaoParcial(content,
							atleta.atleta_id,
							atleta.apelido
						),NumberStyles.Number, CultureInfo.InvariantCulture, out var pontos);
						if (parse)
						{
							pontosParciais += Math.Round(pontos, 2);
						}
					}


					Times.First(t => t.nome_cartola == escalados.DonodoTime).pontuacaoParcial = pontosParciais.ToString("N2");
			}
			return Times.ToArray();
		}

		private static List<TimeInfo> GetEscalacao(List<UsersInfo> Times)
		{
			var ListEscalacao = new List<TimeInfo>();
			foreach(var time in Times)
			{
				var http = (HttpWebRequest)WebRequest.Create("https://api.cartolafc.globo.com/time/slug/" + time.slug);
				http.ContentType = "application/json;charset=UTF-8";
				http.UserAgent =
					"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2227.1 Safari/537.36";
				http.Headers.Add("X-GLB-TOKEN",
					"17f947b9200b2a803a412555b56b8ea166574596f41384d593974795a30586433395662557561316b4b52336a306b486e75576772456d4a6539694b57303439524257724d373932443658574a354873416b4f79644c54796e6968637871785a43674375625f673d3d3a303a706f63616f323030352e32303136");


				var response = http.GetResponse();

				var stream = response.GetResponseStream();
				var sr = new StreamReader(stream);
				var content = sr.ReadToEnd();

				var escalacao = JsonConvert.DeserializeObject<TimeInfo>(content);
				escalacao.DonodoTime = time.nome_cartola;
				ListEscalacao.Add(escalacao);
			};
			return ListEscalacao;
		}

		private static async Task<List<UsersInfo>> GetTimesInfo()
		{
			var http = (HttpWebRequest)WebRequest.Create("https://api.cartolafc.globo.com/auth/liga/liga-carioca-capixaba");
			http.ContentType = "application/json;charset=UTF-8";
			http.UserAgent =
				"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2227.1 Safari/537.36";
			http.Headers.Add("X-GLB-TOKEN", Globals.Token);

			try
			{
				var response = http.GetResponse();

				var stream = response.GetResponseStream();
				var sr = new StreamReader(stream);
				var content = sr.ReadToEnd();

				var Liga = JsonConvert.DeserializeObject<LigaInfo>(content);


				var Times = new List<UsersInfo>();

				foreach (var time in Liga.times)
				{
					Times.Add(new UsersInfo(time.nome, time.nome_cartola, time.patrimonio, time.ranking, time.pontos, time.slug));
				}

				return Times;

			}

			catch
			{
				GetAuthenticationToken().Wait();
				return await GetTimesInfo();
			}
		}

		private static async Task GetAuthenticationToken()
		{
			var httpNew = (HttpWebRequest)WebRequest.Create("https://login.globo.com/api/authentication");
			httpNew.ContentType = "application/json;charset=UTF-8";
			httpNew.UserAgent =
				"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2227.1 Safari/537.36";
			httpNew.Method = "POST";

			ServicePointManager.SecurityProtocol =
				SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


			var payload = new AuthenticationPayload
			{
				payload = new Payload
				{
					email = "pocao2005@gmail.com",
					password = "asdf1234",
					serviceId = 438
				}
			};

			string json = JsonConvert.SerializeObject(payload);

			var client = new HttpClient();
			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await client.PostAsync("https://login.globo.com/api/authentication", httpContent);

			if (response.Content != null)
			{
				var responseContent = await response.Content.ReadAsStringAsync();
				Globals.Token = JsonConvert.DeserializeObject<AuthenticationInfo>(responseContent).glbId;
			}

		}


		public string GetPontuacaoParcial(string content, int id, string apelido)
		{
			try
			{
				var jogador = string.Format("\"{0}\":{{\"apelido\":\"{1}\",\"pontuacao\":", id, apelido);
				var limiteInicial = content.IndexOf(jogador) + jogador.Length;
				var limiteFinal = content.IndexOf(',', limiteInicial);

				var pontuacao = content.Substring(limiteInicial, limiteFinal - limiteInicial).Trim();

				if (pontuacao != null)
				{
					return pontuacao;
				}

				return "0.00";
			}
			catch
			{
				return "0.00";
			}
		}
	}
}
