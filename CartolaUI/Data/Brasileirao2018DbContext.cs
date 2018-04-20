using System.ComponentModel.DataAnnotations.Schema;
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

	    //public DbSet<RodadaInfoDb> Rodada1 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada2 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada3 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada4 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada5 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada6 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada7 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada8 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada9 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada10 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada11 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada12 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada13 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada14 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada15 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada16 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada17 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada18 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada19 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada20 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada21 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada22 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada23 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada24 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada25 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada26 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada27 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada28 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada29 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada30 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada31 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada32 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada33 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada34 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada35 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada36 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada37 { get; set; }
		//public DbSet<RodadaInfoDb> Rodada38 { get; set; }
	}
	
}
