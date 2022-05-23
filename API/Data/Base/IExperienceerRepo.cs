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
        IEnumerable<Experience> GetExperiencesByName(string name);
        IEnumerable<Experience> GetExperiencesById(string id);

        void CreateExperience(Experience experience);

        void UpdateExperience(Experience experience);

        void DeleteExperience(Experience experience);
    }
}
