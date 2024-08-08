using SQLite;

namespace Automat.Class
{
    internal class Logs_Storage
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int Storage { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string XML { get; set; }
        public string XML_Name { get; set; }
        public int IsExistFile { get; set; }
    }
}
