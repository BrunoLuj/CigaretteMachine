using SQLite;

namespace Automat.Class
{
    class COMPort
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string PortName { get; set; }

        public COMPort() { }

        public COMPort(string C1)
        {
            PortName = C1;
        }
    }
}
