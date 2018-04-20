using CartolaUI.Controllers;
using CartolaUI.Data;
using CartolaUI.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace CartolaUI.Services
{
	public class SqlRodadaData : IRodadaData
	{
		private Brasileirao2018DbContext _context;
		TimeAPI _timeAPI = new TimeAPI();

		public SqlRodadaData(Brasileirao2018DbContext context)
		{
			_context = context;
		}

		public IEnumerable<RodadaInfoDb> GetInfoRodadaAtual()
		{

			var rodadalist = RodadaInfo("RodadaAtual");

			foreach (var time in rodadalist)
			{
				var parse = double.TryParse(time.pontos, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var pontos);
				if (parse)
				{
					time.pontos = Math.Round(pontos, 2).ToString(CultureInfo.InvariantCulture);
				}
			}

			return rodadalist;
		}

		private static List<RodadaInfoDb> RodadaInfo(string rodada)
		{
			SqlConnection conn =
				new SqlConnection(
					"Data Source=SQL7001.site4now.net;Initial Catalog=DB_A39CB1_fernandokardel;MultipleActiveResultSets=true;User Id=DB_A39CB1_fernandokardel_admin;Password=asdf1234;");
			conn.Open();

			var rodadalist = new List<RodadaInfoDb>();

			SqlCommand command = new SqlCommand("SELECT * FROM [DB_A39CB1_fernandokardel].[dbo]."+rodada, conn);
			// int result = command.ExecuteNonQuery();
			using (SqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					rodadalist.Add(new RodadaInfoDb(reader["nome"].ToString(), reader["nome_cartola"].ToString(),
						reader["patrimonio"].ToString(), reader["ranking"].ToString(), reader["pontos"].ToString()));
				}
			}

			conn.Close();
			return rodadalist;
		}

		private static void InsertRodadaInfo(string id)
		{
			string cmdString = "INSERT INTO Rodada"+id+ " (id,nome,nome_cartola,patrimonio,ranking,pontos) VALUES (@val1, @val2, @val3, @val4, @val5,@val6)";
			string connString = "Data Source=SQL7001.site4now.net;Initial Catalog=DB_A39CB1_fernandokardel;MultipleActiveResultSets=true;User Id=DB_A39CB1_fernandokardel_admin;Password=asdf1234;";
			var times = RodadaInfo("RodadaAtual");
			using (SqlConnection conn = new SqlConnection(connString))
			{
				using (SqlCommand comm = new SqlCommand())
				{
					comm.Connection = conn;
					comm.CommandText = cmdString;
					conn.Open();
					var i = 0;
					foreach (var time in times)
					{
						comm.Parameters.Clear();
						i++;
						comm.Parameters.AddWithValue("@val1", i);
						comm.Parameters.AddWithValue("@val2", time.nome);
						comm.Parameters.AddWithValue("@val3", time.nome_cartola);
						comm.Parameters.AddWithValue("@val4", time.patrimonio);
						comm.Parameters.AddWithValue("@val5", time.ranking);
						comm.Parameters.AddWithValue("@val6", time.pontos);
						try
						{
							comm.ExecuteNonQuery();
						}
						catch (SqlException e)
						{
							// do something with the exception
							// don't hide it
						}
					}
				}
			}

		}


		public List<RodadaInfoDb> GetApiInfo()
		{
			var dto = new List<TimeDTO>();
			var client = _timeAPI.InitializeClient();
			var str = client.DownloadString(client.BaseAddress);

			dto = JsonConvert.DeserializeObject<List<TimeDTO>>(str);

			var rodadaInfoDb = new List<RodadaInfoDb>();

			foreach (var time in dto)
			{
				rodadaInfoDb.Add(new RodadaInfoDb(time));
			}
			return rodadaInfoDb;
		}

		public void UpdateRodadaAtual()
		{
			var table = GetApiInfo();
			var i = 1;
			foreach (var row in table)
			{
				row.Id = i;
				_context.RodadaAtual.Update(row);
				i++;
			}
			_context.SaveChanges();
		}

		public void UpdateRodadaId(int id)
		{
			InsertRodadaInfo(id.ToString());
		}
		
	}
}
