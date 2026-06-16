using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Data
{
    internal class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> _doctors = new List<Doctor>();
        private readonly List<Speciality> _specialities = new List<Speciality>();

        public PersistenceInMemory()
        {
            LoadSpecialities();
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            return _specialities.SingleOrDefault(s => s.Id == id); //single xq la id debe ser unica, en vez de firstordefault
        }

        private void LoadSpecialities()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
                string jsonContent = File.ReadAllText(jsonPath);
                _specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json, new JsonSerializer()){
                    PropertyNameCaseInsensitive = true //deserializer es convertir el json en un objeto
                }) ?? [];
                 
                return JsonSerializer.Deserialize<List<T>>(jsonContent);
            }
            catch(Exception e)
            {

            }
        
        }
    }
}
