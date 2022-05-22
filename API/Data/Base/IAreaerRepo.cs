using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Base
{
    public interface IAreaerRepo
    {
        bool SaveChanges();

        IEnumerable<Area> GetAllAreas();
        Area GetAreaById(int id);

        void CreateArea(Area skill);

        void UpdateArea(Area skill);

        void DeleteArea(Area skill);
    }
}
