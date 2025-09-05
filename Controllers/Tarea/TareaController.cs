using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Runtime.Intrinsics.X86;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static OtroProyecto.Controllers.Tarea.TareaController;

namespace OtroProyecto.Controllers.Tarea
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        /*
          6. Buscar en Lista
          Endpoint GET /buscar/{item} que busque en una lista predefinida.
          Objetivo: Implementar búsqueda en colecciones.
          Conceptos: Métodos de listas (Contains, Find), respuestas
          HTTP.
          Tarea: Buscar un elemento en una lista predefinida y retornar
          si existe o no.
Aprendizaje: Búsqueda en colecciones y manejo de códigos
HTTP.*/
        private static readonly List<string> muebles =
        new List<string>() {
            "silla",
            "mesa",
            "cama",
            "escritorio",
        };

        [HttpGet("buscar/{item}")]
        public IActionResult EncontrarObjeto(string item)
        {
            bool okey = muebles.Contains(item);
            if (okey)
            {
                return Ok(okey);
            }
            else
            {
                return NotFound();
            }
        }
        /*
        8. Diccionario de Traducciones
Endpoint GET /traducir/{palabra} que use un Dictionary para traducciones
inglés-español.
Objetivo: Usar diccionarios para mapeo de datos.
Conceptos: Dictionary<TKey, TValue>, métodos TryGetValue.
Tarea: Implementar un traductor simple usando un diccionario.
Aprendizaje: Uso de diccionarios para relaciones clave-valor.
         */
        private static readonly Dictionary<string, string> traducciones = new Dictionary<string, string>()
        {
               { "hello", "hola" },
               { "goodbye", "adiós" },
               { "please", "por favor" },
               { "thank you", "gracias" },
               { "dog", "perro" },
               { "cat", "gato" }
        };
        [HttpGet("traducir/{palabra}")]
        public IActionResult Traducir(string palabra)
        {
            var traducido = traducciones.TryGetValue(palabra, out string value);// retorna bool
            if (traducido)
            {
                return Ok(new { palabra, traduccion = value });
            }
            else
            {
                return NotFound();
            }
        }
        /*
         * 9. Contador de Palabras
Endpoint POST /contar-palabras que cuente palabras en un texto enviado en el
body.
Objetivo: Procesamiento de texto.
Conceptos: Métodos de string (Split), manejo de arrays.
Tarea: Contar la cantidad de palabras en un texto recibido.
Aprendizaje: Manipulación de strings y procesamiento de
texto.
         */
        public class TextoRequest
        {
            public string Texto { get; set; } = "Hola mundo";
        }

        [HttpPost("contar-palabras")]
        public IActionResult contar_palabras([FromBody] TextoRequest texto)
        {
            var palabras = texto.Texto.Split(' ');
            int cantidad = palabras.Length;
            return Ok(cantidad);
        }
        /*
         * 10. Stack de Operaciones
Endpoint POST /stack que simule un stack (push/pop) con una lista.
Objetivo: Implementar una estructura LIFO.
Conceptos: Stack<T>, operaciones Push y Pop.
Tarea: Simular un stack con operaciones básicas.
Aprendizaje: Entender el concepto y uso de pilas (stack).
         */
        public class StackRequest
        {
            public string Operacion { get; set; } = string.Empty;
            public int? Valor { get; set; }  // only needed for push
        }
        private static List<int> _stack = new List<int>();

        [HttpPost("stack")]
        public IActionResult HandleStack([FromBody] StackRequest request)
        {
            if (request.Operacion?.ToLower() == "push")
            {
                if (!request.Valor.HasValue)
                {
                    return BadRequest(new { error = "Value is required for push operation" });
                }

                _stack.Add(request.Valor.Value);
                return Ok(new { message = $"Pushed {request.Valor}", stack = _stack });
            }
            else if (request.Operacion?.ToLower() == "pop")
            {
                if (_stack.Count == 0)
                {
                    return BadRequest(new { error = "Stack is empty" });
                }

                int value = _stack[_stack.Count - 1];
                _stack.RemoveAt(_stack.Count - 1);
                return Ok(new { message = $"Popped {value}", stack = _stack });
            }

            return BadRequest(new { error = "Invalid operation. Use 'push' or 'pop'." });
        }
        /*
         11. Clase Producto
Endpoint GET /productos que retorne una lista de objetos Producto (id,
nombre, precio).
Objetivo: Crear y usar clases personalizadas.
Conceptos: POO, propiedades, instanciación de objetos.
Tarea: Definir una clase Producto y retornar una lista de
productos.
Aprendizaje: Creación de clases y trabajo con objetos.
        */
        public class Producto
        {
            public int id { get; set; }
            public string? nombre { get; set; }

            public float precio { get; set; }
        }
        private static List<Producto> productos = new List<Producto>()
        {
            new Producto{id=1,nombre="Leche",precio=10},
            new Producto{id=2,nombre="Mantequilla",precio=15}
        };

        [HttpGet("objetos")]
        public IActionResult Lista_Objetos()
        {
            return Ok(productos);
        }
        /*12. Herencia: Empleado y Gerente
Endpoint GET /empleados que retorne diferentes tipos de empleados usando
herencia.
Objetivo: Implementar herencia en C#.
Conceptos: Herencia, clases abstractas, polimorfismo.
Tarea: Crear una jerarquía de clases de empleados usando
herencia.
Aprendizaje: Principios de herencia y polimorfismo
        */
        public abstract class Trabajador
        {
            public int id { get; set; }
            public string? nombre { get; set; }
            public abstract string getRol();
        }
        public class Empleado : Trabajador
        {
            public override string getRol()
            {
                return "Empleado";
            }
        }
        public class Gerente : Trabajador
        {
            public override string getRol()
            {
                return "Gerente";
            }
        }
        public static List<Trabajador> trabajadores = new List<Trabajador>()
        {
            new Gerente{ id=1,nombre="Marcelo"},
            new Empleado{id=2,nombre="Matias"},
            new Empleado{id=4,nombre="Camilo"}
        };

        [HttpGet("empleados")]
        public IActionResult getTrabajadores()
        {
            return Ok(trabajadores);
        }
        /*13. Interfaces: ICalculadora
Endpoint POST /calcular que use una interfaz para sumar/restar.
Objetivo: Usar interfaces para definir contratos.
Conceptos: Interfaces, implementación, inyección de
dependencias.
Tarea: Crear una interfaz ICalculadora y implementarla.
Aprendizaje: Uso de interfaces para desacoplar código.*/
        public interface ICalculadora
        {
            public int Sumar(int a, int b);
            public int Restar(int a, int b);
        }
        public class Calculadora : ICalculadora
        {
            public int Sumar(int a, int b) => a + b;
            public int Restar(int a, int b) => a - b;
        }

        [HttpGet("{operacion}/{a}/{b}")]

        public IActionResult Calcular_operacion(string operacion, int a, int b)
        {
            Calculadora calculadora = new Calculadora();
            int resultado = 0;
            switch (operacion)
            {
                case "suma":
                    resultado = calculadora.Sumar(a, b);
                    break;
                case "resta":
                    resultado = calculadora.Restar(a, b);
                    break;
                default:
                    return BadRequest();
            }
            return Ok(resultado);
        }
        /*14. Polimorfismo: Animales
Endpoint POST /animal que calcule el área de diferentes figuras (clases
Circulo, Rectangulo).
Objetivo: Aplicar polimorfismo en problemas.
Conceptos: Herencia y polimorfismo en C#, Controladores API,
Binding simple con dynamic objects
Serialización JSON básica
Tarea: Crear diferentes sonidos de animales a partir del
nombre.
Aprendizaje: Polimorfismo y clases abstractas.
*/
        public abstract class Figura
        {
            public abstract double Calcular_Area();
        }

        public class Circulo : Figura
        {
            public double Radio { get; set; }
            public override double Calcular_Area()
            {
                return Radio * Radio * 3.14;
            }
        }
        public class Rectangulo : Figura
        {
            public double Largo { get; set; }
            public double Ancho { get; set; }
            public override double Calcular_Area()
            {
                return Largo * Ancho;
            }
        }

        public class FiguraRequest
        {
            [Required(ErrorMessage = "Tipo es obligatorio")]
            public string Tipo { get; set; } = string.Empty; // "circulo" o "rectangulo"

            [Range(0.0001, double.MaxValue, ErrorMessage = "Radio debe ser mayor que 0")]
            public double Radio { get; set; } // usado si es círculo

            [Range(0.0001, double.MaxValue, ErrorMessage = "Ancho debe ser mayor que 0")]
            public double Ancho { get; set; } // usado si es rectángulo

            [Range(0.0001, double.MaxValue, ErrorMessage = "Alto debe ser mayor que 0")]
            public double Largo { get; set; } // usado si es rectángulo
        }


        [HttpPost("area")] // keeping your naming
        public IActionResult CalcularArea([FromBody] FiguraRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Figura? figura = request.Tipo.ToLower() switch
            {
                "circulo" => new Circulo { Radio = request.Radio },
                "rectangulo" => new Rectangulo { Ancho = request.Ancho, Largo = request.Largo },
                _ => null
            };

            if (figura == null)
                return BadRequest("Figura no reconocida. Usa 'circulo' o 'rectangulo'.");

            return Ok(new
            {
                Tipo = request.Tipo,
                Area = figura.Calcular_Area()
            });
        }

        /*15. Encapsulación: CuentaBancaria
Endpoint POST /depositar que modifique el saldo de una cuenta(validar saldo
≥ 0).
Objetivo: Aplicar encapsulación.
Conceptos: Modificadores de acceso, propiedades, validación.
Tarea: Implementar una clase CuentaBancaria con saldo
encapsulado.
Aprendizaje: Encapsulación y validación de datos.
        */
        public class CuentaBancaria
        {
            private decimal saldo; // campo privado

            public string codigo { get; set; } = string.Empty;

            // Propiedad de solo lectura para el saldo
            public decimal Saldo => saldo;

            // Método para depositar dinero con validación
            public bool Depositar(decimal cantidad)
            {
                if (cantidad < 0)
                    return false; // no se permite saldo negativo
                saldo += cantidad;
                return true;
            }
        }
        private static readonly List<CuentaBancaria> cuentas = new List<CuentaBancaria>
    {
        new CuentaBancaria { codigo = "123" },
        new CuentaBancaria { codigo = "456" }
    };

        [HttpPost("depositar/{codigo}/{saldo}")]
        public IActionResult Depositar(string codigo, decimal saldo)
        {
            var cuenta = cuentas.FirstOrDefault(c => c.codigo == codigo);
            if (cuenta == null)
                return NotFound("Cuenta no encontrada.");

            if (!cuenta.Depositar(saldo))
                return BadRequest("No se puede depositar una cantidad negativa.");

            return Ok(new
            {
                NumeroCuenta = cuenta.codigo,
                Saldo = cuenta.Saldo
            });

        }
        /*21. Validar Edad
Endpoint GET /validar-edad/{edad} que retorne error si edad< 18.
Objetivo: Implementar validaciones básicas.
Conceptos: Validación manual, códigos de error HTTP.
Tarea: Validar si una edad es mayor de 18 años.
Aprendizaje: Validación de datos y respuestas HTTP
apropiadas.*/
        [HttpGet("edad/{edad}")]
        public IActionResult ValidarEdad(int edad)
        {
            if (edad < 18)
            {
                return BadRequest("No esta permitido");
            }
            else
            {
                return Ok("Aprobado");
            }
        }
    }
}
