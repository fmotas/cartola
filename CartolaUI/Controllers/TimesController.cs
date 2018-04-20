using System;
using CartolaUI.Data;
using CartolaUI.Entities;
using CartolaUI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using ServiceStack.OrmLite;

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
			var sqlRodada = new SqlRodadaData(_context);
			sqlRodada.UpdateRodadaAtual();
			var info = sqlRodada.GetInfoRodadaAtual();

			return View(info);
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

		[HttpGet("/api/admin/finalizarrodada/{id}")]
		public IActionResult FinalizarRodada(int id)
		{
			var sqlRodada = new SqlRodadaData(_context);
			sqlRodada.UpdateRodadaAtual();
			sqlRodada.UpdateRodadaId(id);
			return Redirect("/api/times");
		}
	}
}