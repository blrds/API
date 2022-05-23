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

        public void CreateVacancySpecialization(VacancySpecialization vacancySpecialization)
        {
            if (vacancySpecialization == null)
            {
                throw new ArgumentNullException(nameof(vacancySpecialization));
            }

            _context.VacancySpecializations.Add(vacancySpecialization);
        }

        public void DeleteVacancySpecialization(VacancySpecialization vacancySpecialization)
        {
            if (vacancySpecialization == null)
            {
                throw new ArgumentNullException(nameof(vacancySpecialization));
            }
            _context.VacancySpecializations.Remove(vacancySpecialization);
        }

        public IEnumerable<VacancySpecialization> GetAllVacancySpecializations()
        {
            return _context.VacancySpecializations.ToList();
        }

        public IEnumerable<VacancySpecialization> GetVacancySpecializationsByIds(string sid, string vid)
        {
            return _context.VacancySpecializations.Where(x => x.IdSpecialization == sid && x.IdVacancy.ToString()==vid).ToList();
        }

        public IEnumerable<VacancySpecialization> GetVacancySpecializationsByIdSpecialization(string sid)
        {
            return _context.VacancySpecializations.Where(x => x.IdSpecialization == sid).ToList();
        }

        public IEnumerable<VacancySpecialization> GetVacancySpecializationsByIdVacancy(string vid)
        {
            return _context.VacancySpecializations.Where(x => x.IdVacancy.ToString() == vid).ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);
        }

        public void UpdateVacancySpecialization(VacancySpecialization vacancySpecialization)
        {
            //
        }
    }
}
