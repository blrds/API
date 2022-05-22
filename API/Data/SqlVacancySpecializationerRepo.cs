using API.Data.Base;
using API.Data.Contexts;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Data
{
    public class SqlVacancySpecializationerRepo : IVacancySpecializationerRepo
    {
        private VacancySpecializationerContext _context;

        public SqlVacancySpecializationerRepo(VacancySpecializationerContext context)
        {
            _context = context;
        }

        public void CreateVacancySpecialization(VacancySpecialization vacancyspecialization)
        {
            if (vacancyspecialization == null)
            {
                throw new ArgumentNullException(nameof(vacancyspecialization));
            }

            _context.VacancySpecializations.Add(vacancyspecialization);
        }

        public void DeleteVacancySpecialization(VacancySpecialization vacancyspecialization)
        {
            if (vacancyspecialization == null)
            {
                throw new ArgumentNullException(nameof(vacancyspecialization));
            }
            _context.VacancySpecializations.Remove(vacancyspecialization);
        }

        public IEnumerable<VacancySpecialization> GetAllVacancySpecializations()
        {
            return _context.VacancySpecializations.ToList();
        }

        public VacancySpecialization GetVacancySpecializationById(int vid, int sid)
        {
            return _context.VacancySpecializations.FirstOrDefault(p => p.IdVacancy == vid && p.IdSpecialization == sid);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);
        }

        public void UpdateVacancySpecialization(VacancySpecialization vacancyspecialization)
        {
            //
        }
    }
}
