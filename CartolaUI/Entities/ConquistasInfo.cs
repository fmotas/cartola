using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartolaUI.Entities
{
    public class ConquistasInfo
    {
	    public string nome { get; set; }
	    public string nome_cartola_real { get; set; }
	    public int rodadas_como_lider { get; set; }
	    public int rodadas_como_lanterna { get; set; }
	    public int id { get; set; }

	    public ConquistasInfo(string nome, string nomeCartolaReal, int rodadasComoLider, int rodadasComoLanterna, int id)
	    {
		    this.nome = nome;
		    nome_cartola_real = nomeCartolaReal;
		    rodadas_como_lider = rodadasComoLider;
		    rodadas_como_lanterna = rodadasComoLanterna;
		    this.id = id;
	    }
    }
}
