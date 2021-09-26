using Entertainment.DataAccess.DataAccess;
using Entertainment.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entertainment.DataAccess.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<UserDto> GetUserByUsernameAndPassword(string username, string password)
        {
            UserDto userDto = new UserDto();

            SqlParameter[] arrSqlParam = new SqlParameter[2]
            {
                new SqlParameter()
                {
                    ParameterName = "@p_user_name",
                    SqlDbType = SqlDbType.VarChar,
                    Value = username
                },
                new SqlParameter()
                {
                    ParameterName = "@p_pass_word",
                    SqlDbType = SqlDbType.VarChar,
                    Value = password
                }
            };

            AppDbContext<UserDto> dbContext = new AppDbContext<UserDto>();
            try
            {
                userDto = (await dbContext.GetRecord("CheckMainUserLogin", CommandType.StoredProcedure, arrSqlParam));
                return userDto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<MenuListDto>> GetUserMenuList(long userGroupID)
        {
            List<MenuListDto> menuListDtos = new List<MenuListDto>();

            SqlParameter[] arrSqlParam = new SqlParameter[1]
            {
                new SqlParameter()
                {
                    ParameterName = "@p_usergrp_id",
                    SqlDbType = SqlDbType.BigInt,
                    Value = userGroupID
                }
            };

            AppDbContext<MenuListDto> dbContext = new AppDbContext<MenuListDto>();
            try
            {
                menuListDtos = (await dbContext.GetRecords("GetUserWiseMenuList", CommandType.StoredProcedure, arrSqlParam)).ToList();
                return menuListDtos;
            }
            catch
            {
                return null;
            }
        }
    }
}
