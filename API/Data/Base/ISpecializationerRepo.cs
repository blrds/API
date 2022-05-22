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

        void CreateSpecialization(Specialization skill);

        void UpdateSpecialization(Specialization skill);

        void DeleteSpecialization(Specialization skill);
    }
}
