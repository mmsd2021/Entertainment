using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entertainment.DataAccess.Dtos
{
    public class MenuListDto
    {
        public int menu_key { get; set; }
        public long menu_code { get; set; }
        public decimal menu_group_code { get; set; }
        public string menu_name { get; set; }
        public string menu_group_type { get; set; }
        public long menu_doc_id { get; set; }
        public int menu_sl_no { get; set; }
        public string menu_url { get; set; }
        public string menu_key_code { get; set; }
        public int menu_level { get; set; }
        public int menu_expand_flag { get; set; }
        public int menu_is_app { get; set; }
        public int menu_height { get; set; }
    }
}
