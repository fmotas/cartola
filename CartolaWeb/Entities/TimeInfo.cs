namespace CartolaWeb.Entities
{
	public class TimeInfo
	{
		public Atleta[] atletas { get; set; }

		public string DonodoTime { get; set; }
	}

	public class Atleta
	{
		public string nome { get; set; }
		public string apelido { get; set; }
		public string foto { get; set; }
		public int atleta_id { get; set; }
		public int rodada_id { get; set; }
		public int clube_id { get; set; }
		public int posicao_id { get; set; }
		public int status_id { get; set; }
		public string pontos_num { get; set; }
		public string preco_num { get; set; }
		public string variacao_num { get; set; }
		public string media_num { get; set; }
		public string jogos_num { get; set; }
		public object scout { get; set; }
	}

}
