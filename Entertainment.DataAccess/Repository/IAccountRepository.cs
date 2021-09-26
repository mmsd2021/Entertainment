using Entertainment.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entertainment.DataAccess.Repository
{
    public interface IAccountRepository
    {
        Task<UserDto> GetUserByUsernameAndPassword(string username, string password);
        Task<List<MenuListDto>> GetUserMenuList(long userGroupID);
    }
}
