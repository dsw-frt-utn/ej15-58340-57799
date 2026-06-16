using Dsw2026Ej15.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Controllers
{
    [ApiController]
    [Route("[controller]")] //indica que esta clase es un controlador de API y que la ruta para acceder a sus métodos será "/Doctor" (el nombre del controlador sin la palabra "Controller")
    [Route("api/Doctors")] //indica que esta clase es un controlador de API y que la ruta para acceder a sus métodos será "/api/Doctors"
    public class DoctorController : ControllerBase
    {
        private readonly IPersistence _persistence;

        public DoctorController(IPersistence persistence)
        {
            _persistance = persistence;
        }

        [HttpPost] //indica que este método se ejecutará cuando se realice una solicitud HTTP POST a la ruta "/Doctor/CreateDoctor"
        public async Task<IActionResult> CreateDoctor([FromBody]DoctorModel.Request request) //IActionResult es una interfaz que representa el resultado de una acción en un controlador, como devolver una vista, redirigir a otra acción, etc. //frombody es para decir recupera los datos del body
        {
            if(string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                return BadRequest("Nombre y matrícula son requeridos.");
            }

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality == null)
            {
                return BadRequest("Especialidad no existe.");
            }

            //return Ok("Hola mundo"); //estoy retornando un codigo 200
            return Created();
        }

        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
