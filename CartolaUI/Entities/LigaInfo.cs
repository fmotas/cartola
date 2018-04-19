namespace CartolaUI
{

	public class LigaInfo
	{
		public Amigo[] amigos { get; set; }
		public Liga liga { get; set; }
		public bool membro { get; set; }
		public int pagina { get; set; }
		public Time_Dono time_dono { get; set; }
		public Time_Usuario time_usuario { get; set; }
		public Time[] times { get; set; }

		public LigaInfo()
		{

		}

		public LigaInfo(Amigo[] amigos, Liga liga, bool membro, int pagina, Time_Dono timeDono, Time_Usuario timeUsuario, Time[] times)
		{
			this.amigos = amigos;
			this.liga = liga;
			this.membro = membro;
			this.pagina = pagina;
			time_dono = timeDono;
			time_usuario = timeUsuario;
			this.times = times;
		}
	}

	public class Liga
	{
		public int liga_id { get; set; }
		public int time_dono_id { get; set; }
		public object clube_id { get; set; }
		public string nome { get; set; }
		public string descricao { get; set; }
		public string slug { get; set; }
		public string tipo { get; set; }
		public bool mata_mata { get; set; }
		public bool editorial { get; set; }
		public bool patrocinador { get; set; }
		public string criacao { get; set; }
		public int tipo_flamula { get; set; }
		public int tipo_estampa_flamula { get; set; }
		public int tipo_adorno_flamula { get; set; }
		public string cor_primaria_estampa_flamula { get; set; }
		public string cor_secundaria_estampa_flamula { get; set; }
		public string cor_borda_flamula { get; set; }
		public string cor_fundo_flamula { get; set; }
		public string url_flamula_svg { get; set; }
		public string url_flamula_png { get; set; }
		public object tipo_trofeu { get; set; }
		public object cor_trofeu { get; set; }
		public object url_trofeu_svg { get; set; }
		public object url_trofeu_png { get; set; }
		public object inicio_rodada { get; set; }
		public object fim_rodada { get; set; }
		public object quantidade_times { get; set; }
		public bool sorteada { get; set; }
		public string imagem { get; set; }
		public object mes_ranking_num { get; set; }
		public object mes_variacao_num { get; set; }
		public object camp_ranking_num { get; set; }
		public object camp_variacao_num { get; set; }
		public int total_times_liga { get; set; }
		public object vagas_restantes { get; set; }
		public int total_amigos_na_liga { get; set; }
	}

	public class Time_Dono
	{
		public int time_id { get; set; }
		public string nome { get; set; }
		public string nome_cartola { get; set; }
		public string slug { get; set; }
		public long facebook_id { get; set; }
		public string url_escudo_png { get; set; }
		public string url_escudo_svg { get; set; }
		public string foto_perfil { get; set; }
		public bool assinante { get; set; }
	}

	public class Time_Usuario
	{
		public string url_escudo_png { get; set; }
		public string url_escudo_svg { get; set; }
		public int time_id { get; set; }
		public string nome { get; set; }
		public string nome_cartola { get; set; }
		public string slug { get; set; }
		public long facebook_id { get; set; }
		public string foto_perfil { get; set; }
		public bool assinante { get; set; }
		public string patrimonio { get; set; }
		public Ranking ranking { get; set; }
		public Pontos pontos { get; set; }
		public Variacao variacao { get; set; }
	}

	public class Ranking
	{
		public object rodada { get; set; }
		public object mes { get; set; }
		public object turno { get; set; }
		public object campeonato { get; set; }
		public object patrimonio { get; set; }
	}

	public class Pontos
	{
		public object rodada { get; set; }
		public object mes { get; set; }
		public object turno { get; set; }
		public object campeonato { get; set; }
	}

	public class Variacao
	{
		public object mes { get; set; }
		public object turno { get; set; }
		public object campeonato { get; set; }
		public object patrimonio { get; set; }
	}

	public class Amigo
	{
		public int time_id { get; set; }
		public string nome { get; set; }
		public string nome_cartola { get; set; }
		public string slug { get; set; }
		public long facebook_id { get; set; }
		public string url_escudo_png { get; set; }
		public string url_escudo_svg { get; set; }
		public string foto_perfil { get; set; }
		public bool assinante { get; set; }
	}

	public class Time
	{
		public string url_escudo_png { get; set; }
		public string url_escudo_svg { get; set; }
		public int time_id { get; set; }
		public string nome { get; set; }
		public string nome_cartola { get; set; }
		public string slug { get; set; }
		public long? facebook_id { get; set; }
		public string foto_perfil { get; set; }
		public bool assinante { get; set; }
		public string patrimonio { get; set; }
		public Ranking1 ranking { get; set; }
		public Pontos1 pontos { get; set; }
		public Variacao1 variacao { get; set; }
	}

	public class Ranking1
	{
		public string rodada { get; set; }
		public string mes { get; set; }
		public string turno { get; set; }
		public string campeonato { get; set; }
		public string patrimonio { get; set; }

		public string naoparticipou = "Não participou dessa rodada.";
		public string nada = "-";
	}

	public class Pontos1
	{
		public string rodada { get; set; }
		public string mes { get; set; }
		public string turno { get; set; }
		public string campeonato { get; set; }

		public string mongolou = "Mongolei e não escalei meu time nessa rodada.";
		public string naoparticipou = "Não participou dessa rodada.";
		public string nada = "-";

		public string GetPontos(string pontos)
		{
			var result = new double();
			var resultbool = double.TryParse(pontos, out result);
			return result.ToString("N2");
		}
	}

	public class Variacao1
	{
		public string mes { get; set; }
		public string turno { get; set; }
		public string campeonato { get; set; }
		public string patrimonio { get; set; }
	}

}
