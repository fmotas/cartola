using CartolaUI.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using CartolaUI.Controllers;
using CartolaUI.Entities;
using Microsoft.Extensions.Logging.AzureAppServices.Internal;
using ServiceStack.OrmLite;

namespace CartolaUI.Services
{
	public class SqlRodadaData : IRodadaData
    {
	    private Brasileirao2018DbContext _context;
	    TimeAPI _timeAPI = new TimeAPI();

		public SqlRodadaData(Brasileirao2018DbContext context)
	    {
		    _context = context;
	    }

	    public List<RodadaInfoDb> GetInfo()
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
			return rodadaInfoDb;
	    }

	    public void Update()
	    {

			var table = GetInfo();
		    var i = 1;
		    foreach (var row in table)
		    {
			    row.Id = i;
			    _context.RodadaAtual.Add(row);
			    i++;
		    }
		    _context.SaveChanges();
		}

	}
}
