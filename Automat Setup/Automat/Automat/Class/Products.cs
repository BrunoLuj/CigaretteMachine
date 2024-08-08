using SQLite;
using System.Collections.Generic;

namespace Automat.Class
{
    internal class Products
    {
        [PrimaryKey, AutoIncrement]
        public int product_id { get; set; }
        public string product_title { get; set; }
        public string product_subtitle { get; set; }
        public int product_barcode { get; set; }
        public int product_price { get; set; }
        public int product_amount { get; set; }
        public int product_cmd { get; set; }
        public int product_box { get; set; }
        public byte product_img { get; set; }

        public Products(string text) { }

        public Products(int C1, string C2, string C3, int C4, int C5, int C6, int C7, int C8, byte C9)
        {
            product_id = C1;
            product_title = C2;
            product_subtitle = C3;
            product_barcode = C4;
            product_price = C5;
            product_amount = C6;
            product_cmd = C7;
            product_box = C8;
            product_img = C9;
            
        }
    }
}
