//using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Api.models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/doctors")] 
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;
        public DoctorsController (IPersistence persistance)
        {
            _persistence = persistance;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor([FromBody]DoctorModel.Request request)
            
        {
            if (string.IsNullOrWhiteSpace(request.Name)||string.IsNullOrWhiteSpace(request.LicenceNumber))
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
    }
}
