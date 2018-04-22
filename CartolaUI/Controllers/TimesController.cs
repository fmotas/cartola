using System;
using CartolaUI.Data;
using CartolaUI.Entities;
using CartolaUI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Options;
using ServiceStack.OrmLite;

namespace CartolaUI.Controllers
{
	[Route("api/[controller]")]
	public class TimesController : Controller
	{
		TimeAPI _timeAPI = new TimeAPI();
		private Brasileirao2018DbContext _context;
		private readonly IOptions<Startup.MyConfig> config;
		private readonly string connectionString;

		public TimesController(Brasileirao2018DbContext context, IOptions<Startup.MyConfig> config)
		{
			_context = context;
			this.config = config;
			this.connectionString = config.Value.ConnectionStrings.Brasileirao2018;
		}

		[HttpGet("/")]
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

		[HttpGet("/lideres-lanternas")]
		public IActionResult Conquistas()
		{
			var sqlAchievements = new SqlAchievementsData(config);
			var conquistas = new List<ConquistasInfo>(sqlAchievements.GetConquistasInfo());

			var lideres_lanternas = new List<LideresLanternas>();
			lideres_lanternas.Add(new LideresLanternas(conquistas));

			return View(lideres_lanternas);
		}

		[HttpGet("rodadasvalidas")]
		public IEnumerable<string> GetRodadasValidas()
		{
			var sqlAchievements = new SqlAchievementsData(config);
			var listaRodadasValidas = sqlAchievements.GetRodadasValidas();

			return listaRodadasValidas.ToArray();
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