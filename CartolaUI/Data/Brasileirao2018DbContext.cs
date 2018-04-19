using CartolaUI.Entities;
using Microsoft.EntityFrameworkCore;

namespace CartolaUI.Data
{
	public class Brasileirao2018DbContext : DbContext
    {
	    public Brasileirao2018DbContext(DbContextOptions options)
			: base(options)
	    {
		    
	    }

		public DbSet<RodadaInfoDb> RodadaAtual { get; set; }
    }
}
