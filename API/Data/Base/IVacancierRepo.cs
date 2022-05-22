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

        void CreateVacancy(Vacancy skill);

        void UpdateVacancy(Vacancy skill);

        void DeleteVacancy(Vacancy skill);
    }
}
