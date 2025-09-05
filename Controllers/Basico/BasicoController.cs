using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OtroProyecto.Controllers.Basico
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasicoController : ControllerBase
    {


        #region Hola_Mundo 
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hola Mundo");
        }
        #endregion

        #region Saludar

        [HttpGet("{nombre}")]
        public IActionResult Saludar(string nombre)
        {
            return Ok($"Hola {nombre}");
        }

        #endregion

        #region Sumar 2 Numero
        [HttpGet("sumar/{a}/{b}")]
        public IActionResult Sumar_numeros(int a, int b)
        {
            return Ok($"Suma: {a + b}");
        }

        #endregion
        [HttpGet("classnum/{a}")]
        public IActionResult Par_Impar(int a)
        {
            string mensaje = (a % 2 == 0) ? "Par" : "Impar";
            return Ok($"El numero {a} es {mensaje}");
        }


        [HttpGet("Tamanio/{numero}")]
        public IActionResult MostrarTamanio(int numero)
        {
            return Ok(int.MaxValue);
        }

        #region Lista De frutas
        private static readonly List<string> frutas = 
        new List<string>() {
            "manzana",
            "fresa",
            "naranja",
            "sandia",
        };

        [HttpGet("frutas")]
        public IActionResult ObtenerFrutar()
        {
            return Ok(frutas);
        }
        #endregion

        #region Numeros_Filtrar_clases

        [HttpGet("numeros")]
        public IActionResult ObtenerFrutar([FromBody] List<int> numeros)
        {
            List<int> pares = new();
            foreach(int num in numeros)
            {
                if (num % 2 == 0)
                {
                    pares.Add(num);
                }
            }
            return Ok(pares);
        }
        #endregion
    }

}
