using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public interface IAddressRepository
    {
        bool InsertDapper(Address address);

        List<Address> GetAllDapper();

        bool UpdateDapper(Address address);

        bool DeleteDapper(int id);
    }
}
