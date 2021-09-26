using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entertainment.DataAccess.Dtos
{
    public class UserDto
    {
        public int login_user_id { get; set; }

        public string login_user_name { get; set; }

        public string login_user_full_name { get; set; }

        public string group_id { get; set; }

        public string group_name { get; set; }
    }
}
