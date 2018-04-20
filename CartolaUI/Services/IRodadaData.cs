using System.Collections.Generic;
using CartolaUI.Entities;

namespace CartolaUI.Services
{
	public interface IRodadaData
	{
		IEnumerable<RodadaInfoDb> GetInfoRodadaAtual();
		void UpdateRodadaAtual();
	}
}
