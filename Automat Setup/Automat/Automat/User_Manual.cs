using Microsoft.Data.Sqlite;
using System.Windows.Forms;
using SQLite;
using System;
using System.IO.Ports;

namespace Automat
{
    public partial class User_Manual : Form
    {
        SqliteConnection con;
        public User_Manual()
        {
            InitializeComponent();

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;
            try
            {
                string str = "SELECT Licence FROM Computer WHERE ID=1";
                var cmd = new SqliteCommand(str, con);

                Ser_Num.Text = cmd.ExecuteScalar().ToString(); //Serial Number

                string str1 = "SELECT Name_company FROM Computer WHERE ID=1";
                var cmd1 = new SqliteCommand(str1, con);

                Firma.Text = cmd1.ExecuteScalar().ToString(); //Company name

                User.Text = System.Windows.Forms.SystemInformation.UserName.ToString(); // Computer User
            }
            catch (Exception e)
            {
                MessageBox.Show("Dogodila se greška, napravite reset programa. " + "\n" + e.Message, "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            con.Close();
        }
    }
}
