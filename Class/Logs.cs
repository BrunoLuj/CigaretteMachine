using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automat.Class
{
    internal class Logs
    {
        public int product_id { get; set; }
        public string product_title { get; set; }
        public string product_subtitle { get; set; }
        public int product_barcode { get; set; }
        public int product_cmd { get; set; }
        public int product_price { get; set; }
        public int product_amount { get; set; }
        public int product_box { get; set; }
        public string product_date { get; set; }
        public string product_time { get; set; }
        public string product_xml { get; set; }
        public string product_xml_file_name { get; set; }
        public int IsExistFile { get; set; }
    }

}
