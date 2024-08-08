using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Automat
{
    public partial class Pregled_stanja : Form
    {
        SqliteConnection con;
      
        public Pregled_stanja()
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

            string str = "SELECT product_id, product_title, product_subtitle, product_barcode, product_price, product_amount, product_cmd, product_box FROM Products WHERE product_amount > 0";
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
                    lv.SubItems.Add(read.GetString(7));

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
            listView1.Columns.Add("Naziv", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("Podnaziv", 85, HorizontalAlignment.Left);
            listView1.Columns.Add("Barcode", 87, HorizontalAlignment.Left);
            listView1.Columns.Add("Cijena", 45, HorizontalAlignment.Left);
            listView1.Columns.Add("Stanje", 45, HorizontalAlignment.Left);
            listView1.Columns.Add("Box", 35, HorizontalAlignment.Left);
            listView1.Columns.Add("Vel", 30, HorizontalAlignment.Left);
        }

        #endregion

        #region Select Sum Value of all amount in Aparate
        private void SUMProduct()
        {
            btn_back.Visible = false;

            Stanje_u_automatu.Text = null;

            ListViewTitle();

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT SUM (product_amount) FROM Products WHERE product_price > 0";
            var cmd = new SqliteCommand(str, con);
            // SqliteDataReader read = cmd.ExecuteReader();


            Stanje_u_automatu.Text = cmd.ExecuteScalar().ToString();
            con.Close();
        }
        #endregion

        #region Do this when Load Form Pregled_stanja
        private void Pregled_stanja_Load(object sender, EventArgs e)
        {
            SUMProduct();
        }

        #endregion

        #region Show Empty Box from DataBase
        private void btn_praznibox_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            btn_back.Visible = Enabled;
            this.Text = "Pregled praznih Box-ova";

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT product_id, product_title, product_subtitle, product_barcode, product_price, product_amount, product_cmd, product_box FROM Products WHERE product_amount=0";
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
                    lv.SubItems.Add(read.GetString(7));

                    listView1.Items.Add(lv);
                }
            }
        }
        #endregion

        #region Back Button When Click Show Empty Box
        private void btn_back_Click(object sender, EventArgs e)
        {
            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT product_id, product_title, product_subtitle, product_barcode, product_price, product_amount, product_cmd, product_box FROM Products WHERE product_amount > 0";
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
                    lv.SubItems.Add(read.GetString(7));

                    listView1.Items.Add(lv);
                }
            }
            btn_back.Visible = false;
            this.Text = "Pregled stanja";
        }
        #endregion

        #region On Double Click Open Form Dodavanje_artikla_na_stanje
        private void OnlistView1ItemDoubleClick(object sender, EventArgs e)
        {
            try
            {
               if (listView1.FocusedItem != null) 
                { 
                    var form2 = new Dodavanje_artikla_na_stanje
                    {
                        ID = listView1.FocusedItem.Text,
                        Title = listView1.FocusedItem.SubItems[1].Text,
                        Subtitle = listView1.FocusedItem.SubItems[2].Text,
                        Barcode = listView1.FocusedItem.SubItems[3].Text,
                        Price = listView1.FocusedItem.SubItems[4].Text,
                        Amount = listView1.FocusedItem.SubItems[5].Text,
                        Box = listView1.FocusedItem.SubItems[6].Text,
                        Velicina = listView1.FocusedItem.SubItems[7].Text,
                    };
                    form2.ShowDialog();
                }
                /* Dodavanje_artikla_na_stanje f = new Dodavanje_artikla_na_stanje();
                 f.ShowDialog();*/
            }
            catch(Exception ex)
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
                    var form2 = new Dodavanje_artikla_na_stanje
                    {
                        ID = listView1.FocusedItem.Text,
                        Title = listView1.FocusedItem.SubItems[1].Text,
                        Subtitle = listView1.FocusedItem.SubItems[2].Text,
                        Barcode = listView1.FocusedItem.SubItems[3].Text,
                        Price = listView1.FocusedItem.SubItems[4].Text,
                        Amount = listView1.FocusedItem.SubItems[5].Text,
                        Box = listView1.FocusedItem.SubItems[6].Text,
                        Velicina = listView1.FocusedItem.SubItems[7].Text,
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
        private void Pregled_stanja_Activated(object sender, EventArgs e)
        {
            Refresh();
            DataToListView();

            btn_back.Visible = false;

            Stanje_u_automatu.Text = null;

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT SUM (product_amount) FROM Products WHERE product_price > 0";
            var cmd = new SqliteCommand(str, con);
            // SqliteDataReader read = cmd.ExecuteReader();


            Stanje_u_automatu.Text = cmd.ExecuteScalar().ToString();
            con.Close();
        }
        #endregion

    }
}
