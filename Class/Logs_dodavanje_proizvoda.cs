using SQLite;

namespace Automat.Class
{
    internal class Logs_dodavanje_proizvoda
    {
        public string product_title { get; set; }
        public string product_subtitle { get; set; }
        public int product_barcode{ get; set; }
        public int product_price { get; set; }
        public int product_amount { get; set; }
        public int product_cmd { get; set; }
        public int product_box { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
