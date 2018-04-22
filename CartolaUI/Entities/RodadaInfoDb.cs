using System;
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
		public string pontuacaoParcial { get; set; }
		public string patrimonio { get; set; }
		public string ranking { get; set; }
		public string pontos { get; set; }

		
		public string slug { get; set; }
		public string naoparticipou = "Não participou dessa rodada.";
		public string mongolou = "Mongolei e não escalei meu time nessa rodada.";

		public RodadaInfoDb()
		{
				
		}

		public RodadaInfoDb(string id, string nome, string nome_cartola,string pontuacaoParcial, string patrimonio, string ranking, string pontos, string slug)
		{
			this.Id = long.Parse(id);
			this.nome = nome;
			this.nome_cartola = nome_cartola;
			this.pontuacaoParcial = pontuacaoParcial;
			this.patrimonio = patrimonio;
			this.ranking = ranking;
			this.pontos = pontos;
			this.slug = slug;
		}

		public RodadaInfoDb(TimeDTO rodadaInfo)
		{
			nome = rodadaInfo.nome;
			nome_cartola = rodadaInfo.nome_cartola;
			pontuacaoParcial = rodadaInfo.pontuacaoParcial;
			patrimonio = rodadaInfo.patrimonio;
			ranking = rodadaInfo.ranking.rodada;
			pontos = rodadaInfo.pontos.rodada;
			slug = rodadaInfo.slug;
		}
	}
}
