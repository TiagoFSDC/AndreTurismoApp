using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public interface ICityRepository
    {
        bool InsertDapper(City city);

        List<City> GetAllDapper();

        bool UpdateDapper(City city);

        bool DeleteDapper(City city);
    }
}
