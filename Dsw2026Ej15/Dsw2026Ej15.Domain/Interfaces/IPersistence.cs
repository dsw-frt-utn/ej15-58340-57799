using System;
using System.Collections.Generic;
using System.Text;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        public Task <Speciality?> GetSpecialityById(Guid id);
        public Task<IEnumerable<Doctor>> GetDoctorsActive();
        public Task InsertarDoctor(string name, string licenseNumber, Speciality speciality);
        public Task <Doctor?> GetDoctorActiveById(Guid id);
        public Task BajaLogicaDoctorById(Guid id);
    }
}
