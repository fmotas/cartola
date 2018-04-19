namespace CartolaUI.Entities
{
	public class RodadaInfo
	{
		public string nome { get; set; }
		public string nome_cartola { get; set; }
		public string patrimonio { get; set; }
		public Ranking1 ranking { get; set; }
		public Pontos1 pontos { get; set; }

		public string slug { get; set; }
		public string pontuacaoParcial { get; set; }

		public RodadaInfo(string nome, string nome_cartola, string patrimonio, Ranking1 ranking, Pontos1 pontos, string slug)
		{
			this.nome = nome;
			this.nome_cartola = nome_cartola;
			this.patrimonio = patrimonio;
			this.ranking = ranking;
			this.pontos = pontos;
			this.slug = slug;
		}
	}
}
