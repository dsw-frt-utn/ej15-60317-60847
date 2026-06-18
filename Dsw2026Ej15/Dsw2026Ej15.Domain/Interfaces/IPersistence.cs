using System;
using System.Collections.Generic;
using System.Text;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        List<Doctor> doctors { get; set; }
        List<Speciality> specialities { get; set; }

        Speciality? GetSpecialityById(Guid id);

    }
}
