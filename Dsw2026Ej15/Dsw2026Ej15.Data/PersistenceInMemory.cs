using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
//using System.Linq.Expressions;
using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {

        private readonly List<Doctor> _doctors = new List<Doctor>();
        private List<Speciality> _specialities = new List<Speciality>();

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            return _specialities.SingleOrDefault(s => s.Id == id); //single xq la id debe ser unica, en vez de firstordefault
        }

        public List<Doctor>? GetDoctorsActive()
        {
            return _doctors.Where(d => d.IsActive == true).ToList();
        }

        public void InsertarDoctor(string name, string licenseNumber,Speciality speciality)
        {
            _doctors.Add(new Doctor(name, licenseNumber, speciality));
        }

        public Doctor? GetDoctorActiveById(Guid id)
        {
            return _doctors.Where(d => d.Id == id && d.IsActive == true).ToList();
        }

        private void LoadSpecialities()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
                string jsonContent = File.ReadAllText(jsonPath);
                var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(jsonContent, new JsonSerializerOptions //deserializer es convertir el json en un objeto
                {
                    PropertyNameCaseInsensitive = true,
                }) ?? [];

                _specialities = (specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))).ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
