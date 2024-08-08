using SQLite;

namespace Automat.Class
{
    internal class Product_storage
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public int Barcode { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int Storage_Num { get; set; }
    }
}
