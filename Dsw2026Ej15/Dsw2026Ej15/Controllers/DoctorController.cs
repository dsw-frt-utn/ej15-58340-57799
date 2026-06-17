using Dsw2026Ej15.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Domain.Interfaces;

namespace Dsw2026Ej15.Controllers
{
    [ApiController]
    //[Route("[controller]")] //indica que esta clase es un controlador de API y que la ruta para acceder a sus métodos será "/Doctor" (el nombre del controlador sin la palabra "Controller")
    [Route("api/Doctors")] //indica que esta clase es un controlador de API y que la ruta para acceder a sus métodos será "/api/Doctors"
    public class DoctorController : ControllerBase
    {
        private readonly IPersistence _persistence;
        //private readonly ProductsManagmentService _service;

        public DoctorController(IPersistence persistence)
        {
            _persistence = persistence;
        }

        [HttpPost] //indica que este método se ejecutará cuando se realice una solicitud HTTP POST a la ruta "/Doctor/CreateDoctor"
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorModel.Request request) //IActionResult es una interfaz que representa el resultado de una acción en un controlador, como devolver una vista, redirigir a otra acción, etc. //frombody es para decir recupera los datos del body
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                return BadRequest("Nombre y matrícula son requeridos.");
            }

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality == null)
            {
                return BadRequest("Especialidad no existe.");
            }

            _persistence.InsertarDoctor(request.Name, request.LicenseNumber, speciality);


            //return Ok("Hola mundo"); //estoy retornando un codigo 200
            return Created(string.Empty, "Doctor creado exitosamente."); //retorna un codigo 201
        }

        [HttpGet] //indica que este método se ejecutará cuando se realice una solicitud HTTP GET a la ruta "/Doctor/GetDoctor"
        public async Task<IActionResult> GetDoctorsActive() //IActionResult es una interfaz que representa el resultado de una acción en un controlador, como devolver una vista, redirigir a otra acción, etc. //fromquery es para decir recupera los datos de la query string
        {
            var doctors = _persistence.GetDoctorsActive();
            return Ok(doctors);
        }

        [HttpGet("{id}")] //indica que este método se ejecutará cuando se realice una solicitud HTTP GET a la ruta "/Doctor/GetDoctor"
        public async Task<IActionResult> GetDoctorActiveById(/*DoctorModel.RequestId request*/Guid id) //IActionResult es una interfaz que representa el resultado de una acción en un controlador, como devolver una vista, redirigir a otra acción, etc. //fromquery es para decir recupera los datos de la query string
        {
            var doctors = _persistence.GetDoctorActiveById(/*request.Id*/id);
            return Ok(doctors);
        }
    }
}
