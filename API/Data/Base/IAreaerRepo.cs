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
        IEnumerable<Area> GetAreasByName(string name);
        IEnumerable<Area> GetAreasById(string id);

        void CreateArea(Area area);

        void UpdateArea(Area area);

        void DeleteArea(Area area);
    }
}
