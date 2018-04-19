using System.Collections.Generic;
using CartolaUI.Entities;

namespace CartolaUI.Services
{
	public interface IRodadaData
	{
		List<RodadaInfoDb> GetInfo();
		void Update();
	}
}
