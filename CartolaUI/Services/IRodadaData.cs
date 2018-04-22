using System.Collections.Generic;
using System.Threading.Tasks;
using CartolaUI.Entities;

namespace CartolaUI.Services
{
	public interface IRodadaData
	{
		IEnumerable<RodadaInfoDb> GetInfoRodadaAtual();
	}
}
