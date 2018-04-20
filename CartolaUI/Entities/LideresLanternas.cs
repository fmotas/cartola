using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartolaUI.Entities
{
	public class LideresLanternas
	{
		public List<Lider> Lideres { get; set; }
		public List<Lanterna> Lanternas { get; set; }

		public LideresLanternas(List<ConquistasInfo> conquistas)
		{
			Lideres = new List<Lider>();
			Lanternas = new List<Lanterna>();

			conquistas.OrderByDescending(cqt => cqt.rodadas_como_lider);
			foreach (var conquista in conquistas)
			{
				Lideres.Add(new Lider(conquista.nome_cartola_real, conquista.rodadas_como_lider));
			}

			conquistas.OrderByDescending(cqt => cqt.rodadas_como_lanterna);
			foreach (var conquista in conquistas)
			{
				Lanternas.Add(new Lanterna(conquista.nome_cartola_real, conquista.rodadas_como_lanterna));
			}
		}
	}

	public class Lider
	{
		public string nome_cartola_real { get; set; }
		public int rodadas_como_lider { get; set; }

		public Lider(string nomeCartolaReal, int rodadasComoLider)
		{
			nome_cartola_real = nomeCartolaReal;
			rodadas_como_lider = rodadasComoLider;
		}
	}

	public class Lanterna
	{
		public string nome_cartola_real { get; set; }
		public int rodadas_como_lanterna { get; set; }

		public Lanterna(string nomeCartolaReal, int rodadasComoLanterna)
		{
			nome_cartola_real = nomeCartolaReal;
			rodadas_como_lanterna = rodadasComoLanterna;
		}
	}
}
