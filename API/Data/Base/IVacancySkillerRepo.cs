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
        VacancySkill GetVacancySkillById(int vid, int sid);

        void CreateVacancySkill(VacancySkill skill);

        void UpdateVacancySkill(VacancySkill skill);

        void DeleteVacancySkill(VacancySkill skill);
    }
}
