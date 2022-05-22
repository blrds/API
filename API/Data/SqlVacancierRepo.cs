using API.Data.Base;
using API.Data.Contexts;
using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Data
{
    public class SqlVacancierRepo : IVacancierRepo
    {
        private VacancierContext _context;

        public SqlVacancierRepo(VacancierContext context)
        {
            _context = context;
        }

        public void CreateVacancy(Vacancy skill)
        {
            if (skill == null)
            {
                throw new ArgumentNullException(nameof(skill));
            }

            _context.Vacancies.Add(skill);
        }

        public void DeleteVacancy(Vacancy skill)
        {
            if (skill == null)
            {
                throw new ArgumentNullException(nameof(skill));
            }
            _context.Vacancies.Remove(skill);
        }

        public IEnumerable<Vacancy> GetAllVacancies()
        {
            return _context.Vacancies.ToList();
        }

        public Vacancy GetVacancyById(int id)
        {
            return _context.Vacancies.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);
        }

        public void UpdateVacancy(Vacancy skill)
        {
            //
        }
    }
}
