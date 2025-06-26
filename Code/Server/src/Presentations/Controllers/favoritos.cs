using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IMT_Reservas.Server.src.Presentations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class favoritos : ControllerBase
    {
        private readonly IExecuteQuery execute;
       public favoritos(IExecuteQuery executeQuery)
        {
            execute = executeQuery;
        }



        // GET api/<favoritos>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var dataTable = execute.EjecutarFuncion($"SELECT carnet, id_grupo_equipo FROM public.favoritos where carnet='{id}'", null);

            var result = new List<object>();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                result.Add(new
                {
                    carnet = row["carnet"],
                    id_grupo_equipo = row["id_grupo_equipo"]
                });
            }

            return Ok(result);
        }

       [HttpGet("{carnet}/{id_grupo_equipo}")]
        public IActionResult Get(string carnet, int id_grupo_equipo)
        {
            try
            {


                var dataTable = execute.EjecutarFuncion(
                    $"SELECT 1 FROM public.favoritos WHERE carnet='{carnet}' AND id_grupo_equipo={id_grupo_equipo}", null);

                bool exists = dataTable.Rows.Count > 0;
                return Ok(new { exists });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al consultar favoritos.");
            }

        }
        



        [HttpPost("/api/agregar_favorito")]
        public void Post([FromBody] AgregarFavorito entrada)
        {
            
            var query = $"INSERT INTO public.favoritos( carnet, id_grupo_equipo) VALUES ('{entrada.carnet}', {entrada.id_grupo_equipo});";
            execute.EjecutarSpNR(query, null);
            
        }



        // DELETE api/<favoritos>/5
        [HttpDelete("{id}/{id_grupo_equipo}")]
        public void Delete(string id, int id_grupo_equipo)
        {
            var query = $"DELETE FROM public.favoritos WHERE id_grupo_equipo = {id_grupo_equipo} AND carnet = '{id}';";
            execute.EjecutarSpNR(query, null);

        }
    }
}
