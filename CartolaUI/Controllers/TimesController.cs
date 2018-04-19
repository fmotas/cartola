using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using CartolaUI.Data;
using CartolaUI.Entities;
using CartolaUI.Services;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CartolaUI.Controllers
{
	[Route("api/[controller]")]
	public class TimesController : Controller
	{
		TimeAPI _timeAPI = new TimeAPI();
		private Brasileirao2018DbContext _context;

		public TimesController(Brasileirao2018DbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var dto = new List<TimeDTO>();
			var client = _timeAPI.InitializeClient();
			var str = client.DownloadString(client.BaseAddress);

			dto = JsonConvert.DeserializeObject<List<TimeDTO>>(str);

			var rodadaInfoDb = new List<RodadaInfoDb>();

			foreach (var time in dto)
			{
				rodadaInfoDb.Add(new RodadaInfoDb(time));
			}

			var ae = new SqlRodadaData(_context);
			ae.Update();

			return View(rodadaInfoDb);
		}

		[HttpGet("mes")]
		public IActionResult IndexMes()
		{
			List<TimeDTO> dto = new List<TimeDTO>();
			var client = _timeAPI.InitializeClient();
			var str = client.DownloadString(client.BaseAddress);

			dto = JsonConvert.DeserializeObject<List<TimeDTO>>(str);
			return View(dto);
		}
	}
}