using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Data.Sources
{
    public class PersistenceInMemory : IPersistence
    {
        public List<Doctor> doctors { get; set; } = new List<Doctor>();
        public List<Speciality> specialities { get; set; } = new List<Speciality>();

        public PersistenceInMemory() 
        {
            LoadSpecialities();
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            return specialities.SingleOrDefault(e => e.Id == id);
        }
        private void LoadSpecialities()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
                var json = File.ReadAllText(jsonPath);
                var specialitiesDto = JsonSerializer.Deserialize<List<SpecialityDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? [];
                specialities = [.. specialitiesDto.Select(s => new Speciality(s.Name, s.Description, s.Id))];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al cargar el archivo JSON: {ex.Message}");
            }
        }
        }
    }
