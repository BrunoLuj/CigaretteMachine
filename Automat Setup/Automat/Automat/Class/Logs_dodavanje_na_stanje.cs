using SQLite;

namespace Automat.Class
{
    internal class Logs_dodavanje_na_stanje
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Subname { get; set; }
        public int CMD { get; set; }
        public int Barcode { get; set; }
        public int Na_stanju { get; set; }
        public int Dodano_na_stanje { get; set; }
        public int Ukupno_ostavljeno_nakon_dodavanja { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
