using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string Name { get; init; }
        public string LicenseNumber { get; init; }
        [JsonInclude]
        public bool IsActive { get; private set; } = true;
        public Guid? SpecialityId { get; init; } //es la FK 
        public Speciality Speciality { get; init; } //está vinculado por agregación, se mantiene y en el contexto de OR/M pasa a llamarse propiedad de navegación

        public Doctor(string name, string licenseNumber, Speciality speciality, Guid? id = null) : base(id)
        {
            Name = name;
            LicenseNumber = licenseNumber;
            Speciality = speciality;
            IsActive = true;
        }

        public Doctor(Guid id, string name, string licenseNumber, Speciality speciality): base (id)
        {
            Name = name;
            LicenseNumber = licenseNumber;
            Speciality = speciality;
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public Doctor() //solo para trabajar con el OR/M y no romper con decisiones de diseño, no trae problemas tener un constructor privado
        {
        }
    }
}
