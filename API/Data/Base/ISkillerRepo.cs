using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Base
{
    public interface ISkillerRepo
    {
        bool SaveChanges();

        IEnumerable<Skill> GetAllSkills();
        Skill GetSkillById(int id);

        IEnumerable<Skill> GetSkillsByName(string name);
        IEnumerable<Skill> GetSkillsById(string id);
        void CreateSkill(Skill skill);

        void UpdateSkill(Skill skill);

        void DeleteSkill(Skill skill);
    }
}
