using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace CartolaUI
{

	public class TimeAPI
	{
		private string _apiBaseURI = "http://fernandokardel-001-site1.itempurl.com/api/values";
		public WebClient InitializeClient()
		{
			var client = new WebClient();
			client.BaseAddress = _apiBaseURI;
		

			return client;
		}
	}

	public class TimeDTO
	{
		private string _patrimonio;
		public string nome { get; set; }
		public string nome_cartola { get; set; }

		public string patrimonio
		{
			get => _patrimonio;
			set => _patrimonio = "C$ "+value;
		}
		

		public Ranking1 ranking { get; set; }
		public Pontos1 pontos { get; set; }

		public TimeDTO(string nome, string nomeCartola, string patrimonio, Ranking1 ranking, Pontos1 pontos)
		{
			this.nome = nome;
			nome_cartola = nomeCartola;
			this.patrimonio = patrimonio;
			this.ranking = ranking;
			this.pontos = pontos;
		}
	}
}