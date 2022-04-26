using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.User
{
    public interface IUserInterface
    {
        Task<IEnumerable<Student>> GetUsers();
        Task AddUsers(string value);
    }
}
