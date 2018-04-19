using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Newtonsoft.Json;

namespace CartolaUI.Controllers
{
	[Route("api/[controller]")]
	public class TimesController : Controller
	{
		TimeAPI _timeAPI = new TimeAPI();

		[HttpGet]
		public IActionResult Index()
		{
			var dto = new List<TimeDTO>();
			var client = _timeAPI.InitializeClient();
			var str = client.DownloadString(client.BaseAddress);

			dto = JsonConvert.DeserializeObject<List<TimeDTO>>(str);
			return View(dto);
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