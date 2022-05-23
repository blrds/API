using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Base
{
    public interface IVacancySpecializationerRepo
    {
        bool SaveChanges();

        IEnumerable<VacancySpecialization> GetAllVacancySpecializations();
        IEnumerable<VacancySpecialization> GetVacancySpecializationsByIdVacancy(string vid);
        IEnumerable<VacancySpecialization> GetVacancySpecializationsByIdSpecialization(string sid);

        IEnumerable<VacancySpecialization> GetVacancySpecializationsByIds(string sid, string vid);

        void CreateVacancySpecialization(VacancySpecialization vacancySpecialization);

        void UpdateVacancySpecialization(VacancySpecialization vacancySpecialization);

        void DeleteVacancySpecialization(VacancySpecialization vacancySpecialization);
    }
}
