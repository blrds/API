using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Base
{
    public interface ISpecializationerRepo
    {
        bool SaveChanges();

        IEnumerable<Specialization> GetAllSpecializations();
        Specialization GetSpecializationById(string id);
        IEnumerable<Specialization> GetSpecializationsByName(string name);
        IEnumerable<Specialization> GetSpecializationsById(string id);

        void CreateSpecialization(Specialization specialization);

        void UpdateSpecialization(Specialization specialization);

        void DeleteSpecialization(Specialization specialization);
    }
}
