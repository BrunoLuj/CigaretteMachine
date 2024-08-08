using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Automat
{
    public partial class Pregled_stanja_skladiste : Form
    {
        SqliteConnection con;

        string skladiste = Automat_za_cigarete.skladiste1;
        
        public Pregled_stanja_skladiste()
        {
            InitializeComponent();
            DataToListView();
        }

        #region GetDataToListView
        private void DataToListView()
        {
            
            listView1.DoubleClick += OnlistView1ItemDoubleClick;

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT ID, Title , Subtitle, Barcode, Price, Amount, Storage_Num FROM Product_storage WHERE Amount > 0 AND Storage_Num = '"+skladiste+"'";
            var cmd = new SqliteCommand(str, con);

            listView1.Items.Clear();

            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                while (read.Read())
                {
                    ListViewItem lv = new ListViewItem(read.GetString(0));
                    lv.SubItems.Add(read.GetString(1));
                    lv.SubItems.Add(read.GetString(2));
                    lv.SubItems.Add(read.GetString(3));
                    lv.SubItems.Add(read.GetString(4));
                    lv.SubItems.Add(read.GetString(5));
                    lv.SubItems.Add(read.GetString(6));

                    listView1.Items.Add(lv);

                }
            }
            con.Close();
        }
        #endregion

        #region ListViewTitle
        private void ListViewTitle()
        {
            listView1.View = View.Details;

            listView1.Columns.Add("ID", 30, HorizontalAlignment.Left);
            listView1.Columns.Add("Naziv",65, HorizontalAlignment.Left);
            listView1.Columns.Add("Podnaziv", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("Barcode", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Cijena", 45, HorizontalAlignment.Left);
            listView1.Columns.Add("Stanje", 45, HorizontalAlignment.Left);
            listView1.Columns.Add("Skladište", 50, HorizontalAlignment.Left);
        }

        #endregion

        #region Select Sum Value of all amount in Aparate
        private void SUMProduct()
        {
            btn_back.Visible = false;
            label2.Text = "Ukupno stanje cigareta u skladistu" + " " + skladiste + " " + ":";

            this.Text = "Ukupno stanje cigareta u skladistu:" +" "+ skladiste;
           // Stanje_u_automatu.Text = null;

            ListViewTitle();

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT SUM (Amount) FROM Product_storage WHERE Storage_Num = '" + skladiste + "'";
            var cmd = new SqliteCommand(str, con);
            // SqliteDataReader read = cmd.ExecuteReader();

            Stanje_skladista.Text = cmd.ExecuteScalar().ToString();
            con.Close();

            
        }
        #endregion

        #region Do this when Load Form Pregled_stanja
        private void Pregled_stanja_skladiste_Load(object sender, EventArgs e)
        {
            SUMProduct();
        }
        #endregion

        #region On Double Click Open Form Dodavanje_artikla_na_stanje !!Pogledati treba li 
        private void OnlistView1ItemDoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (listView1.FocusedItem != null)
                {
                    var form2 = new Dodavanje_artikla_na_stanje_skladiste
                    {
                        ID = listView1.FocusedItem.Text,
                        Title = listView1.FocusedItem.SubItems[1].Text,
                        Subtitle = listView1.FocusedItem.SubItems[2].Text,
                        Barcode = listView1.FocusedItem.SubItems[3].Text,
                        Price = listView1.FocusedItem.SubItems[4].Text,
                        Amount = listView1.FocusedItem.SubItems[5].Text,
                        Box = listView1.FocusedItem.SubItems[6].Text
                    };
                    form2.ShowDialog();
                }
                /* Dodavanje_artikla_na_stanje f = new Dodavanje_artikla_na_stanje();
                 f.ShowDialog();*/
            }
            catch (Exception ex)
            {
                Console.WriteLine("");
            }

        }

        #endregion

        #region Enter open new form, and ESC Close Form
        private void listView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Escape 
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }

            else if (listView1.FocusedItem != null)
            {
                //Enter open form Dodavanje artikla na stanje
                if (e.KeyChar == (char)13)
                {
                    var form2 = new Dodavanje_artikla_na_stanje_skladiste
                    {
                        ID = listView1.FocusedItem.Text,
                        Title = listView1.FocusedItem.SubItems[1].Text,
                        Subtitle = listView1.FocusedItem.SubItems[2].Text,
                        Barcode = listView1.FocusedItem.SubItems[3].Text,
                        Price = listView1.FocusedItem.SubItems[4].Text,
                        Amount = listView1.FocusedItem.SubItems[5].Text,
                        Box = listView1.FocusedItem.SubItems[6].Text
                    };

                    form2.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Odaberite artikal koji želite izmjeniti!");
            }
        }

        #endregion

        #region Refresh Data When Close Form Dodavanje artikla na stanje
        private void Pregled_stanja_skladiste_Activated(object sender, EventArgs e)
        {
            Refresh();
            DataToListView();

            btn_back.Visible = false;

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT SUM (Amount) FROM Product_storage WHERE Storage_Num = '" + skladiste + "'";
            var cmd = new SqliteCommand(str, con);
            // SqliteDataReader read = cmd.ExecuteReader();

            Stanje_skladista.Text = cmd.ExecuteScalar().ToString();
            con.Close();
        }
        #endregion
    }
}
