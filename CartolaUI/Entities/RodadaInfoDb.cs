using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using CartolaUI.Controllers;
using Microsoft.WindowsAzure.Storage.Table;
using ServiceStack;

namespace CartolaUI.Entities
{
	public class RodadaInfoDb
	{
		public long Id { get; set; }
		public string nome { get; set; }
		public string nome_cartola { get; set; }
		public string patrimonio { get; set; }
		//[Display(Name = "ranking_na_rodada")]
		public string ranking { get; set; }
		//[Display(Name = "pontos_na_rodada")]
		public string pontos { get; set; }

		//[IgnoreProperty]
		public string slug { get; set; }
		//[IgnoreProperty]
		public string pontuacaoParcial { get; set; }
		public string naoparticipou = "Não participou dessa rodada.";
		public string mongolou = "Mongolei e não escalei meu time nessa rodada.";

		public RodadaInfoDb()
		{
				
		}

		public RodadaInfoDb(string nome, string nomeCartola, string patrimonio, string ranking, string pontos)
		{
			this.nome = nome;
			nome_cartola = nomeCartola;
			this.patrimonio = patrimonio;
			this.ranking = ranking;
			this.pontos = pontos;
		}

		public RodadaInfoDb(TimeDTO rodadaInfo)
		{
			nome = rodadaInfo.nome;
			nome_cartola = rodadaInfo.nome_cartola;
			patrimonio = rodadaInfo.patrimonio;
			ranking = rodadaInfo.ranking.rodada;
			pontos = rodadaInfo.pontos.rodada;
		}
	}
}
