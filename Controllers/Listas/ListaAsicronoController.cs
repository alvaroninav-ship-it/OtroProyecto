using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtroProyecto.Models;
namespace OtroProyecto.Controllers.Listas
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaAsicronoController : ControllerBase
    {// static no crea un objeto
        private static List<Persona> personas = new()
        {
            new Persona {Id=1,Nombre="Juan",Edad=12},
            new Persona {Id=2,Nombre="Marco",Edad=43}
        };

        [HttpGet]
        public ActionResult<IEnumerable<Persona>> GetALL()// tipo lista amigable
        {
            return Ok(personas);
        }
    }
}
