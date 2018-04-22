using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CartolaUI.Entities;
using Microsoft.Extensions.Options;

namespace CartolaUI.Services
{
	public class SqlExpulsoesData
	{
		private readonly IOptions<Startup.MyConfig> config;
		private readonly string connectionString;

		public SqlExpulsoesData(IOptions<Startup.MyConfig> config)
		{
			this.config = config;
			this.connectionString = config.Value.ConnectionStrings.Brasileirao2018;
		}

		public IEnumerable<ExpulsoesInfoDb> GetExpulsoesInfo()
		{

			var comm = "SELECT * FROM [DB_A39CB1_fernandokardel].[dbo].[Expulsoes]";
			var reader = GetSqlDataReader(comm);

			var expulsoes = new List<ExpulsoesInfoDb>();

			while (reader.Read())
			{
				int.TryParse(reader["quantidade_de_expulsoes"].ToString(), out var quantidade_de_expulsoes);
				expulsoes.Add(new ExpulsoesInfoDb(reader["nome"].ToString(), quantidade_de_expulsoes));
			}

			return expulsoes;
		}

		public string Expulsar(string nome)
		{
			var comm = $"SELECT quantidade_de_expulsoes FROM [DB_A39CB1_fernandokardel].[dbo].[Expulsoes] WHERE nome = '{nome}'";
			var reader = GetSqlDataReader(comm);
			reader.Read();
			var qtd_exp = int.TryParse(reader["quantidade_de_expulsoes"].ToString(), out var qtdResult);
			if (qtd_exp)
			{
				var qtd_nova = qtdResult + 1;
				comm = $"UPDATE [DB_A39CB1_fernandokardel].[dbo].[Expulsoes] SET quantidade_de_expulsoes = {qtd_nova} WHERE nome = '{nome}'";
				var reader2 = GetSqlDataReader(comm);
				return $"O participante {nome} tinha {qtdResult} expulsões e agora tem {qtd_nova} expulsões.";
			}
			return "Erro.";
		}

		public string Desexpulsar(string nome)
		{
			var comm = $"SELECT quantidade_de_expulsoes FROM [DB_A39CB1_fernandokardel].[dbo].[Expulsoes] WHERE nome = '{nome}'";
			var reader = GetSqlDataReader(comm);
			reader.Read();
			var qtd_exp = int.TryParse(reader["quantidade_de_expulsoes"].ToString(), out var qtdResult);
			if (qtd_exp)
			{
				var qtd_nova = qtdResult - 1;
				comm = $"UPDATE [DB_A39CB1_fernandokardel].[dbo].[Expulsoes] SET quantidade_de_expulsoes = {qtd_nova} WHERE nome = '{nome}'";
				var reader2 = GetSqlDataReader(comm);
				return $"O participante {nome} tinha {qtdResult} expulsões e agora tem {qtd_nova} expulsões.";
			}
			return "Erro.";
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
