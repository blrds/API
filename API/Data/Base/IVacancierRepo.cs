using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Base
{
    public interface IVacancierRepo
    {
        bool SaveChanges();

        IEnumerable<Vacancy> GetAllVacancies();
        Vacancy GetVacancyById(int id);
        IEnumerable<Vacancy> GetVacanciesByName(string name);
        IEnumerable<Vacancy> GetVacanciesById(string id);
        IEnumerable<Vacancy> GetVacanciesByIdarea(string idarea);
        IEnumerable<Vacancy> GetVacanciesBySalaryfrom(string salaryFrom);
        IEnumerable<Vacancy> GetVacanciesBySalaryto(string salaryTo);
        IEnumerable<Vacancy> GetVacanciesBySalarycurency(string salaryCurency);
        IEnumerable<Vacancy> GetVacanciesByPublishedat(string publishedAt);
        IEnumerable<Vacancy> GetVacanciesBySnippetrequirement(string req);
        IEnumerable<Vacancy> GetVacanciesBySnippetresponsibility(string res);
        IEnumerable<Vacancy> GetVacanciesByDescription(string des);
        IEnumerable<Vacancy> GetVacanciesByIdexperience(string exp);

        void CreateVacancy(Vacancy vacancy);

        void UpdateVacancy(Vacancy vacancy);

        void DeleteVacancy(Vacancy vacancy);
    }
}
