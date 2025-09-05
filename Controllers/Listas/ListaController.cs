using System.Collections;
using System.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtroProyecto.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace OtroProyecto.Controllers.Listas
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaController : Controller
    {
        #region Listas
        [HttpGet("generica")]
        public IActionResult ListaGenerica()
        {
            var personas = new List<Persona>()
            {
                new Persona { Id = 1, Nombre = "Juan", Edad = 20 },
                new Persona { Id = 2, Nombre = "Maria", Edad = 40 }
            };

            return Ok(personas);
        }
        #endregion

        [HttpGet("diccionario")]
        public IActionResult ObtenerDiccionario()
        {
            var diccionario = new Dictionary<string,string>()
            {
                {"clave1","valor1" },
                {"clave2","valor2" },
                {"clave3","valor3" }
            };
            return Ok(diccionario);
        }
        [HttpGet("dinamico")]
        public IActionResult ObtenerDinamico()
        {
            // variable para incrementar
            dynamic dinamico = new ExpandoObject();
            dinamico.Marca = "Toyota";
            dinamico.Modelo = "2000";
            dinamico.Precio = 2343;
            return Ok(dinamico);
        }

        #region HastTable
        [HttpGet("hash")]
        public IActionResult ObtenerHastTable()
        {
            var hastTable = new Hashtable
        {
            { "uno", 1 },
            { "dos", 2 }
        };

            return Ok(hastTable);
        }
        #endregion
        #region Cola
        [HttpGet("cola")]
        public IActionResult ObtenerCola()
        {
            var cola = new Queue<string>();
            cola.Enqueue("1ro");
            cola.Enqueue("2doo");
            cola.Enqueue("3ro");

            return Ok(cola);
        }
        #endregion


        #region Pila
        [HttpGet("pila")]
        public IActionResult ObtenerPila()
        {
            var pila = new Stack<int>();
            pila.Push(100);
            pila.Push(200);
            pila.Push(300);

            return Ok(pila);
        }
        #endregion
        #region Hash
        [HttpGet("hash2")]
        public IActionResult ObtenerHash()
        {
            var conjunto = new HashSet<string>
            { "uno", "dos", "tres", "uno" };

            return Ok(conjunto);
        }
        #endregion

        #region Anidada
        [HttpGet("anidada")]
        public IActionResult ObtenerAnidada()
        {
            var grupo1 = new List<Persona>()
            {
                new Persona { Id = 1, Nombre = "Juan", Edad = 20 },
                new Persona { Id = 2, Nombre = "Maria", Edad = 40 }
            };

            var grupo2 = new List<Persona>()
            {
                new Persona { Id = 3, Nombre = "Pedro", Edad = 21 },
                new Persona { Id = 4, Nombre = "Maribel", Edad = 35 }
            };

            var dicCompleja = new
                List<Dictionary<string, List<Persona>>>
            {
                new Dictionary<string, List<Persona>>
                { { "Grupo A", grupo1  } },
                 new Dictionary<string, List<Persona>>
                 { { "Grupo B", grupo2  } },
            };

            return Ok(dicCompleja);
        }
        #endregion


    }
}
