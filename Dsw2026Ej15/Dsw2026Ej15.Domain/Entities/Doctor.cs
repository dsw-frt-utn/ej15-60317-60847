using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string Name { get; set; }
        public string LicenceNumber { get; set; }
        public bool IsActive { get; set; }
        public Speciality Speciality { get; set; }

        public Doctor(string name, string licenceNumber, Speciality speciality)
        {
            Name = name;
            LicenceNumber = licenceNumber;
            Speciality = speciality;
        }
        public Doctor() { }
    }
}
