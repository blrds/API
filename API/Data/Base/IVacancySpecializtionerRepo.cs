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
        VacancySpecialization GetVacancySpecializationById(int vid, int sid);
        VacancySpecialization GetVacancySpecializationByVacancyId(int vid);
        VacancySpecialization GetVacancySpecializationBySpecializationId(int sid);

        void CreateVacancySpecialization(VacancySpecialization skill);

        void UpdateVacancySpecialization(VacancySpecialization skill);

        void DeleteVacancySpecialization(VacancySpecialization skill);
    }
}
