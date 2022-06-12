using BAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public interface IUserInterface
    {
        Task<IEnumerable<Student>> GetUsers();
        Task<string> AddUsers(UserDto userObj);
        Task UpdateUsers(int id, UserDto userObj, int userId);
        Task AddProducts(ProductDto model);
    }
}
