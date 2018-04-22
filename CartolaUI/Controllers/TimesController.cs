using System;
using CartolaUI.Data;
using CartolaUI.Entities;
using CartolaUI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using ServiceStack;

namespace CartolaUI.Controllers
{
	[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
	public class TimesController : Controller
	{
		TimeAPI _timeAPI = new TimeAPI();
		private Brasileirao2018DbContext _context;
		private readonly Microsoft.Extensions.Options.IOptions<Startup.MyConfig> config;
		private readonly string connectionString;

		public TimesController(Brasileirao2018DbContext context, Microsoft.Extensions.Options.IOptions<Startup.MyConfig> config)
		{
			_context = context;
			this.config = config;
			this.connectionString = config.Value.ConnectionStrings.Brasileirao2018;
		}

		[HttpGet("/rodadaatual")]
		public async Task<IActionResult> RodadaAtual()
		{
			var sqlRodada = new SqlRodadaData(_context);
			if (Math.Abs(DateTime.Now.Second - Globals.UpdateSecond) > 15)
			{
				Globals.UpdateSecond = DateTime.Now.Second;
				sqlRodada.UpdateRodadaAtual().Wait();
				Globals.UpdateSecond = DateTime.Now.Second;
			}
			var info = sqlRodada.GetInfoRodadaAtual();
			return View("rodadaatual", info);
		}

		[HttpGet("/")]
		public IActionResult Index()
		{
			var sqlRodada = new SqlRodadaData(_context);
			if (DateTime.Today.Day.ToString() != Globals.UpdateDay)
			{
				sqlRodada.UpdateCampeonato().Wait();
				Globals.UpdateDay = DateTime.Today.Day.ToString();
			}
			var info = sqlRodada.GetInfoCampeonato();
			return View("campeonato", info);
		}

		[HttpGet("/lideres-lanternas")]
		public IActionResult Conquistas()
		{
			var sqlAchievements = new SqlAchievementsData(config);
			var conquistas = new List<ConquistasInfo>(sqlAchievements.GetConquistasInfo());

			var lideres_lanternas = new List<LideresLanternas> { new LideresLanternas(conquistas) };

			return View("conquistas", lideres_lanternas);
		}

		[HttpGet("rodadasvalidas")]
		public IEnumerable<string> GetRodadasValidas()
		{
			var sqlAchievements = new SqlAchievementsData(config);
			var listaRodadasValidas = sqlAchievements.GetRodadasValidas();

			return listaRodadasValidas.ToArray();
		}


		[HttpGet("/api/admin/finalizarrodada/{id}")]
		public async Task<IActionResult> FinalizarRodada(int id)
		{
			var sqlRodada = new SqlRodadaData(_context);
			await sqlRodada.UpdateRodadaAtual();
			sqlRodada.UpdateRodadaId(id);
			return Redirect("/");
		}

		[HttpGet("/expulsoes")]
		public IActionResult Expulsoes()
		{
			var sqlExpulsoes = new SqlExpulsoesData(config);
			var expulsoes = new List<ExpulsoesInfoDb>(sqlExpulsoes.GetExpulsoesInfo());
			return View("expulsoes", expulsoes);
		}

		[HttpGet("/francine/expulsar/{nome}")]
		public string Expulsar(string nome)
		{
			var sqlExpulsoes = new SqlExpulsoesData(config);
			return sqlExpulsoes.Expulsar(nome); 
		}
		[HttpGet("/francine/desexpulsar/{nome}")]
		public string Desexpulsar(string nome)
		{
			var sqlExpulsoes = new SqlExpulsoesData(config);
			return sqlExpulsoes.Desexpulsar(nome);
		}
	}
}