using System;
using System.Collections.Generic;
using System.Text;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        public Speciality? GetSpecialityById(Guid id);
        public List<Doctor>? GetDoctorsActive();
        public void InsertarDoctor(string name, string licenseNumber, Speciality speciality);
        public Doctor? GetDoctorActiveById(Guid id);
        public Doctor? BajaLogicaDoctorById(Guid id);
    }
}
