using Dsw2026Ej15.Data.Utils;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEF : IPersistence
    {
        private readonly Dsw2026Ej16DbContext _context;

        public PersistenceEF(Dsw2026Ej16DbContext context)
        {
            _context = context;
            InitializeData();
        }

        public async Task BajaLogicaDoctorById(Guid id)
        {
            var doctor = await GetDoctorActiveById(id);

            if (doctor != null)
            {
                doctor.Deactivate();
                _context.Update(doctor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Doctor?> GetDoctorActiveById(Guid id)
        {
            return await _context.Doctors
            .Include(d => d.Speciality)
            .SingleOrDefaultAsync(d => d.Id == id && d.IsActive);
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsActive()
        {
            return await _context.Doctors
            .Include(nameof(Doctor.Speciality)) //la otra sobrecarga es un delegado al que se le puede pasar una expresion (d => d.Speciality)
            .Where(d => d.IsActive).ToListAsync();
        }

        public async Task<Speciality?> GetSpecialityById(Guid id)
        {
            return await _context.Specialities
            .SingleOrDefaultAsync(s => s.Id == id);
        }
        
        public async Task InsertarDoctor(string name, string licenseNumber, Speciality speciality)
        {
            _context.Doctors.Add(new Doctor(name, licenseNumber, speciality));
            _context.SaveChangesAsync();
        }

        public void InitializeData()
        {
            _context.Seedwork<Speciality>("specialities");
            _context.Seedwork<Doctor>("doctors");

        }

    }
    
}
