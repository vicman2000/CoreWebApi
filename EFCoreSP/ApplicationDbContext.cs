using EFCoreSP.Entidades;
using EFCoreSP.Entidades.SinLlaves;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSP
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<PersonasObtenerIdsResultado>().ToSqlQuery("exec PersonasObtenerIds");
		}


		public DbSet<Persona> Personas { get; set; }

	}
}
