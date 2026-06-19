//using Microsoft.AspNetCore.Components;
using Dsw2026Ej15.Api.models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;
        public DoctorsController(IPersistence persistance)
        {
            _persistence = persistance;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorModel.Request request)

        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenceNumber))
            {
                throw new ValidationException("Nombre y matricula son requeridos");
            }

            var speciality = _persistence.GetSpecialityById(request.SpecialityId);
            if (speciality == null)
            {
                throw new ValidationException("La especialidad no existe");
            }
            var newDoctor = new Doctor
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                LicenceNumber = request.LicenceNumber,
                IsActive = true,
                Speciality = speciality
            };
            _persistence.doctors.Add(newDoctor);
            return Created();
        }
        [HttpGet]
           public async Task<IActionResult> GetDoctors()
           {
                var doctors = _persistence.doctors;
                return Ok(doctors);
           }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(Guid id)
        {
            var doctor = _persistence.doctors.SingleOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound("No se encontró ningún médico con el ID ingresado");
            }
            return Ok(doctor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var doctor = _persistence.doctors.SingleOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound("No se encontró ningún médico con el ID ingresado para dar de baja");
            }
            doctor.IsActive = false;
            return NoContent();
        }
    }
}
