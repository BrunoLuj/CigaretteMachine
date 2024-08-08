using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Automat.Class
{
    internal class Computer
    {
        [PrimaryKey, AutoIncrement]
        public int ID{ get; set; }

        public int Licence { get; set; }
    }
}
