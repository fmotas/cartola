using CartolaUI.Entities;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CartolaUI.Services
{
	public class SqlAchievementsData
	{
		private readonly IOptions<Startup.MyConfig> config;
		private readonly string connectionString;

		public SqlAchievementsData(IOptions<Startup.MyConfig> config)
		{
			this.config = config;
			this.connectionString = config.Value.ConnectionStrings.Brasileirao2018;
		}

		public List<ConquistasInfo> GetConquistasInfo()
		{
			var rodadasValidas = GetRodadasValidas();

			var lideres = GetLideres(rodadasValidas);
			var lanternas = GetLanternas(rodadasValidas);
			AtualizarPontuacaoLideres(lideres);
			AtualizarPontuacaoLanternas(lanternas);

			var comm = "SELECT * FROM [DB_A39CB1_fernandokardel].[dbo].[Conquistas]";
			var reader = GetSqlDataReader(comm);

			var conquistas = new List<ConquistasInfo>();

			while (reader.Read())
			{
				int.TryParse(reader["rodadas_como_lider"].ToString(), out var rodadas_como_lider);
				int.TryParse(reader["rodadas_como_lanterna"].ToString(), out var rodadas_como_lanterna);
				int.TryParse(reader["id"].ToString(), out var id);
				conquistas.Add(new ConquistasInfo(reader["nome"].ToString(), reader["nome_cartola_real"].ToString(),
					rodadas_como_lider, rodadas_como_lanterna, id));
			}

			return conquistas;
		}

		public List<string> GetRodadasValidas()
		{
			var comm = "SELECT Rodada FROM [DB_A39CB1_fernandokardel].[dbo].[Rodadas] where Valida = 'sim'";
			var reader = GetSqlDataReader(comm);

			var list = new List<string>();

			while (reader.Read())
			{
				list.Add(reader["Rodada"].ToString());
			}
			
			return list;
		}

		public List<string> GetLideres(List<string> rodadasValidas)
		{
			var Lideres = new List<string>();

			foreach (var rodada in rodadasValidas)
			{
				var comm = $"SELECT nome FROM [DB_A39CB1_fernandokardel].[dbo].[Rodada{rodada}] where ranking = '1'";
				var reader = GetSqlDataReader(comm);
				if (reader.Read())
				{
					Lideres.Add(reader["nome"].ToString());
				}
			}
			return Lideres;
		}

		public List<string> GetLanternas(List<string> rodadasValidas)
		{
			var Lanternas = new List<string>();

			foreach (var rodada in rodadasValidas)
			{
				var comm = "";
				if (rodada == "1")
				{
					comm = $"SELECT nome FROM [DB_A39CB1_fernandokardel].[dbo].[Rodada{rodada}] where ranking = '16'";
				}
				else
				{
					comm = $"SELECT nome FROM [DB_A39CB1_fernandokardel].[dbo].[Rodada{rodada}] where ranking = '17'";
				}

				var reader = GetSqlDataReader(comm);
				if (reader.Read())
				{
					Lanternas.Add(reader["nome"].ToString());
				}
			}
			return Lanternas;
		}

		public void ZerarPontuacaoLideres()
		{
			var cmm = "UPDATE [DB_A39CB1_fernandokardel].[dbo].[Conquistas] SET rodadas_como_lider = 0 WHERE nome !=''";
			GetSqlDataReader(cmm);
		}

		public void ZerarPontuacaoLanternas()
		{
			var cmm = "UPDATE [DB_A39CB1_fernandokardel].[dbo].[Conquistas] SET rodadas_como_lanterna = 0 WHERE nome !=''";
			GetSqlDataReader(cmm);
		}

		public void AtualizarPontuacaoLideres(List<string> lideres)
		{
			ZerarPontuacaoLideres();

			foreach (var lider in lideres)
			{
				var cmm = $"SELECT * FROM [DB_A39CB1_fernandokardel].[dbo].[Conquistas] WHERE nome = '{lider}'";
				var reader = GetSqlDataReader(cmm);

				reader.Read();

				var pontuacaoAtualbool = double.TryParse(reader["rodadas_como_lider"].ToString(), out var pontuacaoAtual);
				if (pontuacaoAtualbool)
				{
					cmm =
						$"UPDATE [DB_A39CB1_fernandokardel].[dbo].[Conquistas] SET rodadas_como_lider = '{pontuacaoAtual + 1}' WHERE nome = '{lider}'";
					GetSqlDataReader(cmm);
				}
			}
		}

		public void AtualizarPontuacaoLanternas(List<string> lanternas)
		{
			ZerarPontuacaoLanternas();

			foreach (var lanterna in lanternas)
			{
				var cmm = $"SELECT * FROM [DB_A39CB1_fernandokardel].[dbo].[Conquistas] WHERE nome = '{lanterna}'";
				var reader = GetSqlDataReader(cmm);

				reader.Read();

				var pontuacaoAtualbool = double.TryParse(reader["rodadas_como_lanterna"].ToString(), out var pontuacaoAtual);
				if (pontuacaoAtualbool)
				{
					cmm =
						$"UPDATE [DB_A39CB1_fernandokardel].[dbo].[Conquistas] SET rodadas_como_lanterna = '{pontuacaoAtual + 1}' WHERE nome = '{lanterna}'";
					GetSqlDataReader(cmm);
				}
			}
		}

		public SqlDataReader GetSqlDataReader(string command)
		{
			SqlConnection conn = new SqlConnection(connectionString);
			SqlCommand comm = new SqlCommand();
			SqlDataReader reader;

			comm.CommandText = command;
			comm.CommandType = CommandType.Text;
			comm.Connection = conn;

			conn.Open();

			reader = comm.ExecuteReader();

			return reader;
		}
	}
}
