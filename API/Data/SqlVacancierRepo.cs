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

        public void CreateVacancy(Vacancy vacancy)
        {
            if (vacancy == null)
            {
                throw new ArgumentNullException(nameof(vacancy));
            }

            _context.Vacancies.Add(vacancy);
        }

        public void DeleteVacancy(Vacancy vacancy)
        {
            if (vacancy == null)
            {
                throw new ArgumentNullException(nameof(vacancy));
            }
            _context.Vacancies.Remove(vacancy);
        }

        public IEnumerable<Vacancy> GetAllVacancies()
        {
            return _context.Vacancies.ToList();
        }

        public Vacancy GetVacancyById(int id)
        {
            return _context.Vacancies.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Vacancy> GetVacanciesById(string id)
        {
            return _context.Vacancies.Where(x => x.Id.ToString() == id).ToList();
        }

        public IEnumerable<Vacancy> GetVacanciesByName(string name)
        {
            return _context.Vacancies.Where(x => x.Name == name).ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);
        }

        public void UpdateVacancy(Vacancy vacancy)
        {
            //
        }

        public IEnumerable<Vacancy> GetVacanciesByIdarea(string idarea)
        {
            return _context.Vacancies.Where(x => x.IdArea.ToString() == idarea).ToList();
        }

        public IEnumerable<Vacancy> GetVacanciesBySalaryfrom(string salaryFrom)
        {
            return _context.Vacancies.Where(x => x.SalaryFrom.ToString() == salaryFrom).ToList();
        }

        public IEnumerable<Vacancy> GetVacanciesBySalaryto(string salaryTo)
        {
            return _context.Vacancies.Where(x => x.SalaryTo.ToString() == salaryTo).ToList();
        }

        public IEnumerable<Vacancy> GetVacanciesBySalarycurency(string salaryCurrency)
        {
            return _context.Vacancies.Where(x => x.SalaryCurrency == salaryCurrency).ToList();
        }

        public IEnumerable<Vacancy> GetVacanciesByPublishedat(string publishedAt)
        {
            return _context.Vacancies.Where(x => x.PublishedDate.ToString("yyyy-MM-dd HH:mm:ss.fff") == publishedAt).ToList();
        }

        public IEnumerable<Vacancy> GetVacanciesBySnippetrequirement(string req)
        {
            return _context.Vacancies.Where(x => x.SnippetRequirement == req).ToList();
        }

        public IEnumerable<Vacancy> GetVacanciesBySnippetresponsibility(string res)
        {
            return _context.Vacancies.Where(x => x.SnippetResponsibility ==res).ToList();
        }

        public IEnumerable<Vacancy> GetVacanciesByDescription(string des)
        {
            return _context.Vacancies.Where(x => x.Description == des).ToList();
        }

        public IEnumerable<Vacancy> GetVacanciesByIdexperience(string exp)
        {
            return _context.Vacancies.Where(x => x.IdExperience == exp).ToList();
        }
    }
}
