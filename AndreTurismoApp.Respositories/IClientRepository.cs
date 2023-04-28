using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndreTurismoApp.Models;

namespace AndreTurismoApp.Repositories
{
    public interface IClientRepository
    {
        bool InsertDapper(Client client);

        List<Client> GetAllDapper();

        bool UpdateDapper(Client client);

        bool DeleteDapper(int Id);
    }
}
