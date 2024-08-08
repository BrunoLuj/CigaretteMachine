using SQLite;

namespace Automat.Class
{
    internal class Logs_Storage_dodavanje_proizvoda
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public int Barcode { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int Storage_Num { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

    }
}
