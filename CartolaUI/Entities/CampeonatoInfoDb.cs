using CartolaUI.Controllers;

namespace CartolaUI.Entities
{
	public class CampeonatoInfoDb
	{
		public long Id { get; set; }

		public string nome { get; set; }
		public string nome_cartola { get; set; }
		public string patrimonio { get; set; }
		public string ranking { get; set; }
		public string pontos { get; set; }


		public string slug { get; set; }
		public string naoparticipou = "Não participou dessa rodada.";
		public string mongolou = "Mongolei e não escalei meu time nessa rodada.";

		public CampeonatoInfoDb()
		{

		}
		

		public CampeonatoInfoDb(TimeDTO rodadaInfo)
		{
			nome = rodadaInfo.nome;
			nome_cartola = rodadaInfo.nome_cartola;
			patrimonio = rodadaInfo.patrimonio;
			ranking = rodadaInfo.ranking.campeonato;
			pontos = rodadaInfo.pontos.campeonato;
			slug = rodadaInfo.slug;
		}
	}
}
