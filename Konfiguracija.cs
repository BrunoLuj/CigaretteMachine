using Automat.Class;
using Microsoft.Data.Sqlite;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Automat
{
    public partial class Konfiguracija : Form
    {
        List<Products> products = new List<Products>();

        SqliteConnection con;
        public Konfiguracija()
        {
            InitializeComponent();
            //this.dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void btn_COM_Port_Click(object sender, EventArgs e)
        {
            PostavkeCOMPorta frm = new PostavkeCOMPorta();
            frm.ShowDialog();
  
        }

        private void Konfiguracija_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(@"C:\Automat\Automat\bin\Debug\SQLiteData.db"))
            {
                //Do Nothing
            }
            else
            {
                var db = new SQLiteConnection(@"C:\Automat\Automat\bin\Debug\SQLiteData.db");

                db.CreateTable<Products>();

                db.Close();
            }
        }

        #region Open Form Dodavanje_proizvoda
        private void btn_additems_Click(object sender, EventArgs e)
        {
            Dodavanje_proizvoda frm = new Dodavanje_proizvoda();
            frm.ShowDialog();
        }
        #endregion

        #region ESC Key Press
        private void Konfiguracija_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Escape 
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region Refresh Form When Form is Activated
        private void Konfiguracija_Activated(object sender, EventArgs e)
        {
            Load_Data();
        }
        #endregion

        #region When Load Form
        private void Load_Data()
        {
            /*this.dataGridView1.Rows.Clear();

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            try
            {
                string str = "SELECT * FROM Products";
                var cmd = new SqliteCommand(str, con);
                //SqliteDataReader read = cmd.ExecuteReader();

                using (SqliteDataReader read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                       // dataGridView1.Rows.Add(new object[] {
                        read.GetValue(0),  // U can use column index
                        read.GetValue(read.GetOrdinal("product_title")),  // Or column name like this
                        read.GetValue(read.GetOrdinal("product_subtitle")),
                        read.GetValue(read.GetOrdinal("product_barcode")),
                        read.GetValue(read.GetOrdinal("product_price")),
                        read.GetValue(read.GetOrdinal("product_amount")),

                        });
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Dogodila se pogreška!" + ex.Message, "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        #endregion

        #region Mouse Hover (Informacije u postavkama za svaki button)
        private void btn_additems_MouseHover(object sender, EventArgs e)
        {
            MyToolTip.Show("Dodavanje novog proizvoda koji nije u bazi!", btn_additems);
        }

        private void btn_COM_Port_MouseHover(object sender, EventArgs e)
        {
            MyToolTip.Show("Postavke izlaznog COM-Porta, pazite da je uvijek kabel(usb) uključen iz automata u kompjuter!", btn_COM_Port);
        }
        private void btn_postavke_skladista_MouseHover(object sender, EventArgs e)
        {
            MyToolTip.Show("Postavke skladišta!", btn_postavke_skladista);

        }
        private void btn_Prodaja_MouseHover(object sender, EventArgs e)
        {
            MyToolTip.Show("Pregled prodaje cigareta u automatu!", btn_Prodaja);
        }
        private void btn_UserManual_MouseHover(object sender, EventArgs e)
        {
            MyToolTip.Show("Korisničke informacije!", btn_UserManual);
        }
        #endregion

        #region Button Questionmark on form Konfiguracija
        private void button2_Click(object sender, EventArgs e)
        {
            string name = string.Empty;
            string broj = string.Empty;
            string email = string.Empty;

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT * FROM Info";
            var cmd = new SqliteCommand(str, con);

            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    name = read.GetString(0);
                    broj = read.GetString(1);
                    email = read.GetString(2);
                }
            }
            con.Close();
            MessageBox.Show("Za sve informacije nazovite korisničku službu na broj:" + " " + broj + " " + "(" + name + ")" + "," + " " + "ili putem Email-a:" + " " + email + "," + "\nHvala što koristite naše usluge.","Informacija",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region Open form Postavke_skladišta
        private void btn_postavke_skladista_Click(object sender, EventArgs e)
        {
            Postavke_skladista frm = new Postavke_skladista();
            frm.ShowDialog();
        }

        #endregion

        #region Open Form Pregled Prodaje
        private void btn_Prodaja_Click(object sender, EventArgs e)
        {
            Pregled_prodaje frm = new Pregled_prodaje();
            frm.ShowDialog();
        }
        #endregion

        #region Open User_Manual form
        private void btn_UserManual_Click(object sender, EventArgs e)
        { 
            User_Manual frm = new User_Manual();
            frm.ShowDialog();
        }
        #endregion


    }
}
