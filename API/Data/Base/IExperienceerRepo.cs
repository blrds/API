using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Base
{
    public interface IExperienceerRepo
    {
        bool SaveChanges();

        IEnumerable<Experience> GetAllExperiences();
        Experience GetExperienceById(string id);

        void CreateExperience(Experience skill);

        void UpdateExperience(Experience skill);

        void DeleteExperience(Experience skill);
    }
}
