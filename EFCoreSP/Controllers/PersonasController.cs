using EFCoreSP.Entidades;
using EFCoreSP.Entidades.SinLlaves;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSP.Controllers
{

	[ApiController]
	[Route("api/personas")]
	public class PersonasController : ControllerBase
	{
		private readonly ApplicationDbContext context;

		public PersonasController( ApplicationDbContext context)
		{
			this.context = context;
		}

		[HttpPost]
		public async Task<ActionResult<int>> Post(Persona persona)
		{
			var parametroId = new SqlParameter("@Id", System.Data.SqlDbType.Int);
			parametroId.Direction = System.Data.ParameterDirection.Output;

			try
			{
				await context.Database.ExecuteSqlInterpolatedAsync($@"exec PersonasInsertar @nombre = {persona.Nombre}, @Id = {parametroId}  OUTPUT");

				var id = (int)parametroId.Value;
				return Ok(id);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());	
				return BadRequest(ex.ToString());
			}

		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<Persona>> Get(int id)
		{
			var personas = context.Personas
				.FromSqlInterpolated($@"Exec PersonasObtenerPorId @Id={id}")
				.AsAsyncEnumerable();

			await foreach (var persona in personas)
			{
				return persona;
			}

			return NotFound();
		}



		[HttpGet]
		public async Task<IEnumerable<PersonasObtenerIdsResultado>> Get()
		{
			return await context.Set<PersonasObtenerIdsResultado>().ToListAsync();
		}





	
	}
}
