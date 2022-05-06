using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.UserService
{
    public interface IUserInterface
    {
        Task<IEnumerable<Student>> GetUsers();
        Task<string> AddUsers(UserDto userObj,int userId);
        Task UpdateUsers(int id, UserDto userObj, int userId);
    }
}
