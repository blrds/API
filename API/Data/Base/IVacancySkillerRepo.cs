using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Base
{
    public interface IVacancySkillerRepo
    {
        bool SaveChanges();

        IEnumerable<VacancySkill> GetAllVacancySkills();
        IEnumerable<VacancySkill> GetVacancySkillsByIdSkill(string sid);
        IEnumerable<VacancySkill> GetVacancySkillsByIdVacancy(string vid);

        IEnumerable<VacancySkill> GetVacancySkillsByIds(string vid, string sid);

        void CreateVacancySkill(VacancySkill vacancySkill);

        void UpdateVacancySkill(VacancySkill vacancySkill);

        void DeleteVacancySkill(VacancySkill vacancySkill);
    }
}
