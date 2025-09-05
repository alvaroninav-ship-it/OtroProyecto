using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtroProyecto.Models;

namespace OtroProyecto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListAsi : ControllerBase
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

        [HttpGet("{id:int}")]
        public ActionResult<Persona> 
            Get_BY_id(int id){
            var persona = personas.FirstOrDefault(x => x.Id == id);
            if (persona is not null)
            {
                return Ok(persona);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<Persona> Create(Persona nuevo)
        {// Any significa si existe un elemento
            try
            {

                nuevo.Id = personas.Any() ? personas.Max(a => a.Id) + 1 : 1;
                personas.Add(nuevo);
                return CreatedAtAction(nameof(Get_BY_id), new { id = nuevo.Id }, nuevo);
                //puedes colocar un throw como por una condicional 
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Persona mod)
        {
            var persona = personas.FirstOrDefault(x => x.Id == id);
            if (persona is null) return NotFound();
            persona.Nombre = mod.Nombre;
            persona.Edad = mod.Edad;
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public IActionResult Elimminar(int id)
        {
            var persona = personas.FirstOrDefault(a => a.Id == id);
            if (persona is null) return NotFound();
            else
            {
                personas.Remove(persona);
                return Ok();
            }
        }
    }

   

}
