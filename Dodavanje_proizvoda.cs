using Automat.Class;
using System;
using System.Windows.Forms;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using Microsoft.Data.Sqlite;

namespace Automat
{
    public partial class Dodavanje_proizvoda : Form
    {
        SqliteConnection con;
        List<Products> products = new List<Products>();

        public Dodavanje_proizvoda()
        {
            InitializeComponent();

            //Automat
            txt_cmd.MaxLength = 2;
            txt_price.MaxLength = 4;
            txt_barcode.MaxLength = 20;
            txt_title.MaxLength = 15;
            txt_subtitle.MaxLength = 15;
            btn_additems.Enabled = false;
            txt_amount.Enabled = false;

            //Storage
            txt_title_storage.MaxLength = 15;
            txt_subtitle_storage.MaxLength = 15;
            txt_numstorage.MaxLength = 2;
            txt_price_storage.MaxLength = 4;
            txt_barcode_storage.MaxLength = 20;
            txt_amount_storage.MaxLength = 3;
        }

        #region Save Products in Database
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_title.Text) || string.IsNullOrEmpty(txt_subtitle.Text) || string.IsNullOrEmpty(txt_cmd.Text) || string.IsNullOrEmpty(txt_price.Text) || string.IsNullOrEmpty(txt_amount.Text) || string.IsNullOrEmpty(txt_barcode.Text))
            {
                MessageBox.Show("Neke od informacija ne mogu biti prazne,\nMolimo unesite podatke!", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
               // var db = new SQLiteConnection(@"C:\Automat\Automat\bin\Debug\SQLiteData.db");

                string str1 = "SELECT * FROM Products WHERE product_cmd = '" + txt_cmd.Text + "' LIMIT 1";

                string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                var con = new SqliteConnection(cs);

                con.Open();
                this.con = con;

                var cmd = new SqliteCommand(str1, con);

                using (SqliteDataReader read = cmd.ExecuteReader())
                {
                    if (read.HasRows)
                    {
                        MessageBox.Show("Odabrani box je zauzet, molimo izaberite neki drugi box!","Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        try
                        {
                            //To convert immage into bytes
                            /* Image img = pb_image.Image;
                             MemoryStream ms = new MemoryStream();
                             pb_image.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                             byte[] dataByte = ms.ToArray();*/

                            var db = new SQLiteConnection(@"C:\Automat\Automat\bin\Debug\SQLiteData.db");

                            //Verzija box-a
                            int box;
                            if(che_10.Checked == true)
                            {
                                box = 1;
                            }
                            else
                            {
                                box = 2;
                            }

                            //product_img
                            string str = "INSERT INTO Products(product_title, product_subtitle,product_barcode,product_price,product_amount, product_box, product_cmd) " +
                                "values('" + txt_title.Text.ToString() + "','" + txt_subtitle.Text.ToString() + "','" + txt_barcode.Text.ToString() + "'" +
                                ",'" + txt_price.Text.ToString() + "','" + txt_amount.Text.ToString() + "','" + box + "','" + txt_cmd.Text.ToString() + "')";

                            db.Execute(str);

                            //Logs_dodavanje_proizvoda
                            string str3 = "INSERT INTO Logs_dodavanje_proizvoda(product_title, product_subtitle,product_barcode,product_price,product_amount, product_cmd,product_box, Date, Time) " +
                               "values('" + txt_title.Text.ToString() + "','" + txt_subtitle.Text.ToString() + "','" + txt_barcode.Text.ToString() + "'" +
                               ",'" + txt_price.Text.ToString() + "','" + txt_amount.Text.ToString() + "','" + txt_cmd.Text.ToString() + "' ,'" + box + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortTimeString() + "')";

                            db.Execute(str3);

                            db.Close();

                            MessageBox.Show("Uspješno ste spremili proizvod!", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Dogodila se pogreška!" + ex.Message, "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        //nakon spremanja očisti textbox
                        txt_title.Clear();
                        txt_subtitle.Clear();
                        txt_price.Clear();
                        txt_barcode.Clear();
                        txt_cmd.Clear();
                        txt_amount.Clear();

                        txt_title.Focus();

                    }
                }
            }
        }
        #endregion

        #region Upload image
        private void btn_additems_Click(object sender, EventArgs e)
        {
            //Napraviti ako unese krivu sliku npr svg

            openFileDialog1.ShowDialog();

            string filepath = openFileDialog1.FileName;
            pb_image.Image = Image.FromFile(filepath);
        }

        #endregion

        #region Ograničen unos količine cigareta po box-u
        private void txt_amount_TextChanged(object sender, EventArgs e)
        {
            if (che_10.Checked == true && che_20.Checked == false)
            {
                txt_amount.Enabled = true;
                int box_int = 0;
                Int32.TryParse(txt_amount.Text, out box_int);

                if (box_int < 1 && txt_amount.Text != "")
                {
                    txt_amount.Text = "1";
                    MessageBox.Show("Minimalan broj kutija cigareta u box-u je 1 ");
                }
                else if (box_int > 10 && txt_amount.Text != "")
                {
                    txt_amount.Text = "10";
                    MessageBox.Show("Maksimalni broj kutija cigareta u box-u je 10");
                }
            }
            else if(che_20.Checked == true && che_10.Checked == false)
            {
                txt_amount.Enabled = true;
                int box_int = 0;
                Int32.TryParse(txt_amount.Text, out box_int);

                if (box_int < 1 && txt_amount.Text != "")
                {
                    txt_amount.Text = "1";
                    MessageBox.Show("Minimalan broj kutija cigareta u box-u je 1 ");
                }
                else if (box_int > 20 && txt_amount.Text != "")
                {
                    txt_amount.Text = "20";
                    MessageBox.Show("Maksimalni broj kutija cigareta u box-u je 20");
                }
            }
            else if(che_20.Checked == true && che_10.Checked == true)
            {
                che_20.Checked = false;
                che_10.Checked = false;
                txt_amount.Enabled = false;
            }

        }

        #endregion
        
        #region Price Textbox in form only accept Number and one dot and one comma
        private void txt_price_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') /*&& (e.KeyChar != ',')*/)
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            /*
            // only allow one decimal point
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }*/
        }

        #endregion

        #region Cmd/Box Textbox only accept 2 number lenght and can only accept number
        private void txt_cmd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            /*
            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }*/
        }

        #endregion

        #region Textbox Price Don't allow first enter dot
        private void txt_price_TextChanged(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.StartsWith(".")) box.Text = "";
        }

        #endregion

        #region Barcode accept only number and  max lenght=20
        private void txt_barcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region Textbox title get first letter Upper and rest Lower
        private void txt_title_TextChanged(object sender, EventArgs e)
        {
            // THIS ONLY UPPER FIRST LETTER
            /*  if (txt_title.Text.Length <= 0) return;
              string s = txt_title.Text.Substring(0, 1);
              if (s != s.ToUpper())
              {
                  int curSelStart = txt_title.SelectionStart;
                  int curSelLength = txt_title.SelectionLength;
                  txt_title.SelectionStart = 0;
                  txt_title.SelectionLength = 1;
                  txt_title.SelectedText = s.ToUpper();
                  txt_title.SelectionStart = curSelStart;
                  txt_title.SelectionLength = curSelLength;
              }*/

            char[] c = txt_title.Text.ToCharArray();
            int j;
            for (j = 0; j < txt_title.Text.Length; j++)
            {
                if (j == 0) c[j] = c[j].ToString().ToUpper()[0];
                else c[j] = c[j].ToString().ToLower()[0];
            }
            txt_title.Text = new string(c);
            txt_title.Select(txt_title.Text.Length, 1);
        }

        #endregion

        #region Textbox title accept Letter and Space max lenght=15
        private void txt_title_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region Textbox subtitle get first letter Upper and rest Lower
        private void txt_subtitle_TextChanged(object sender, EventArgs e)
        {
            char[] c = txt_subtitle.Text.ToCharArray();
            int j;
            for (j = 0; j < txt_subtitle.Text.Length; j++)
            {
                if (j == 0) c[j] = c[j].ToString().ToUpper()[0];
                else c[j] = c[j].ToString().ToLower()[0];
            }
            txt_subtitle.Text = new string(c);
            txt_subtitle.Select(txt_subtitle.Text.Length, 1);
        }


        #endregion

        #region Textbox subtitle accept Letter and Space max lenght=15
        private void txt_subtitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region Enter like Tab in form Automat
        private void txt_title_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txt_subtitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txt_barcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txt_price_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txt_amount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txt_cmd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        #endregion

        #region Key Press Event (ESC)
        private void Dodavanje_proizvoda_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Escape 
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region Button Automat i Storage
        private void btn_automat_Click(object sender, EventArgs e)
        {
            panel1.Show();
            panel2.Hide();
            panel3.Hide();
            this.Text = "Dodavanje proizvoda u Automat";
            this.ActiveControl = txt_title;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            panel2.Show();
            panel1.Hide();
            panel3.Hide();
            this.Text = "Dodavanje proizvoda";
        }

        private void btn_storage_Click(object sender, EventArgs e)
        {
            panel3.Show();
            panel1.Hide();
            panel2.Hide();
            this.Text = "Dodavanje proizvoda u Skladište";
            this.ActiveControl = txt_title_storage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Show();
            panel1.Hide();
            panel3.Hide();
            this.Text = "Dodavanje proizvoda";
        }
        #endregion

        #region Save Product in Storage
        private void btn_save_storage_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_title_storage.Text) || string.IsNullOrEmpty(txt_subtitle_storage.Text) || string.IsNullOrEmpty(txt_numstorage.Text) || string.IsNullOrEmpty(txt_price_storage.Text) || string.IsNullOrEmpty(txt_amount_storage.Text) || string.IsNullOrEmpty(txt_barcode_storage.Text))
            {
                MessageBox.Show("Neke od informacija ne mogu biti prazne,\nMolimo unesite podatke!", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string str1 = "SELECT Storage FROM Config_storage WHERE ID=1";

                string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                var con = new SqliteConnection(cs);

                con.Open();
                this.con = con;

                var cmd = new SqliteCommand(str1, con);

                using (SqliteDataReader read = cmd.ExecuteReader())
                {
                    read.Read();
                    string Broj_skladista = read["Storage"].ToString();

                    int Skladiste = Int32.Parse(Broj_skladista);

                    int Unos_skladista = Int32.Parse(txt_numstorage.Text);

                    con.Close();

                    if ( Skladiste < Unos_skladista)
                    {
                        MessageBox.Show("Nemate toliko skladišta u automatu!", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        try
                        {
                            var db = new SQLiteConnection(@"C:\Automat\Automat\bin\Debug\SQLiteData.db");

                            string str = "INSERT INTO Product_storage(Title, Subtitle,Barcode,Price,Amount, Storage_Num) " +
                                "values('" + txt_title_storage.Text.ToString() + "','" + txt_subtitle_storage.Text.ToString() + "','" + txt_barcode_storage.Text.ToString() + "'" +
                                ",'" + txt_price_storage.Text.ToString() + "','" + txt_amount_storage.Text.ToString() + "','" + txt_numstorage.Text.ToString() + "')";

                            db.Execute(str);

                            //Logs_dodavanje_proizvoda
                            string str3 = "INSERT INTO Logs_Storage_dodavanje_proizvoda(Title, Subtitle,Barcode,Price,Amount,Storage_Num, Date, Time) " +
                                "values('" + txt_title_storage.Text.ToString() + "','" + txt_subtitle_storage.Text.ToString() + "','" + txt_barcode_storage.Text.ToString() + "'" +
                                ",'" + txt_price_storage.Text.ToString() + "','" + txt_amount_storage.Text.ToString() + "','" + txt_numstorage.Text.ToString() + "' ,'" +
                                DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortTimeString() + "')";

                            db.Execute(str3);

                            db.Close();

                            MessageBox.Show("Uspješno ste spremili proizvod!", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Dogodila se pogreška!" + ex.Message, "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        //nakon spremanja očisti textbox
                        txt_title_storage.Clear();
                        txt_subtitle_storage.Clear();
                        txt_price_storage.Clear();
                        txt_barcode_storage.Clear();
                        txt_numstorage.Clear();
                        txt_amount_storage.Clear();

                        txt_title_storage.Focus();

                    }
                }
            }
        }
        #endregion

        #region Storage entry in Texbox restrictions (key press, textchanged)
        private void txt_numstorage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_price_storage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') /*&& (e.KeyChar != ',')*/)
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            /*
            // only allow one decimal point
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
            */
        }

        private void txt_barcode_storage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_amount_storage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_numstorage_TextChanged(object sender, EventArgs e)
        {
            int box_int = 0;
            Int32.TryParse(txt_numstorage.Text, out box_int);

            if (box_int < 1 && txt_numstorage.Text != "")
            {
                txt_numstorage.Text = "1";
                MessageBox.Show("Minimalan broj skladišta mora biti 1, provjerite broj na automatu!");
            }
            else if (box_int > 10 && txt_numstorage.Text != "")
            {
                txt_numstorage.Text = "10";
                MessageBox.Show("Maksimalni broj skladišta je 10, provjerite broj na automatu!");
            }
        }

        private void txt_title_storage_TextChanged(object sender, EventArgs e)
        {
            char[] c = txt_title_storage.Text.ToCharArray();
            int j;
            for (j = 0; j < txt_title_storage.Text.Length; j++)
            {
                if (j == 0) c[j] = c[j].ToString().ToUpper()[0];
                else c[j] = c[j].ToString().ToLower()[0];
            }
            txt_title_storage.Text = new string(c);
            txt_title_storage.Select(txt_title_storage.Text.Length, 1);
        }

        private void txt_subtitle_storage_TextChanged(object sender, EventArgs e)
        {
            char[] c = txt_subtitle_storage.Text.ToCharArray();
            int j;
            for (j = 0; j < txt_subtitle_storage.Text.Length; j++)
            {
                if (j == 0) c[j] = c[j].ToString().ToUpper()[0];
                else c[j] = c[j].ToString().ToLower()[0];
            }
            txt_subtitle_storage.Text = new string(c);
            txt_subtitle_storage.Select(txt_subtitle_storage.Text.Length, 1);
        }

        private void txt_subtitle_storage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_title_storage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region Enter like Tab in Storage
        private void txt_title_storage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txt_subtitle_storage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txt_barcode_storage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txt_price_storage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txt_amount_storage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txt_numstorage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }
        #endregion

        #region Active color of textbox (lightBlue) and leave color (White)
        private void txt_title_storage_Enter(object sender, EventArgs e)
        {
            txt_title_storage.BackColor = Color.LightBlue;
        }

        private void txt_title_storage_Leave(object sender, EventArgs e)
        {
            txt_title_storage.BackColor = Color.White;
        }

        private void txt_subtitle_storage_Enter(object sender, EventArgs e)
        {
            txt_subtitle_storage.BackColor = Color.LightBlue;
        }

        private void txt_subtitle_storage_Leave(object sender, EventArgs e)
        {
            txt_subtitle_storage.BackColor = Color.White;
        }

        private void txt_barcode_storage_Enter(object sender, EventArgs e)
        {
            txt_barcode_storage.BackColor= Color.LightBlue;
        }

        private void txt_barcode_storage_Leave(object sender, EventArgs e)
        {
            txt_barcode_storage.BackColor = Color.White;
        }

        private void txt_price_storage_Enter(object sender, EventArgs e)
        {
            txt_price_storage.BackColor = Color.LightBlue;
        }

        private void txt_price_storage_Leave(object sender, EventArgs e)
        {
            txt_price_storage.BackColor = Color.White;
        }

        private void txt_amount_storage_Enter(object sender, EventArgs e)
        {
            txt_amount_storage.BackColor = Color.LightBlue;
        }

        private void txt_amount_storage_Leave(object sender, EventArgs e)
        {
            txt_amount_storage.BackColor = Color.White;
        }

        private void txt_numstorage_Enter(object sender, EventArgs e)
        {
            txt_numstorage.BackColor = Color.LightBlue;
        }

        private void txt_numstorage_Leave(object sender, EventArgs e)
        {
            txt_numstorage.BackColor= Color.White;
        }

        private void txt_title_Enter(object sender, EventArgs e)
        {
            txt_title.BackColor = Color.LightBlue;
        }

        private void txt_title_Leave(object sender, EventArgs e)
        {
            txt_title.BackColor= Color.White;
        }

        private void txt_subtitle_Enter(object sender, EventArgs e)
        {
            txt_subtitle.BackColor = Color.LightBlue;
        }

        private void txt_subtitle_Leave(object sender, EventArgs e)
        {
            txt_subtitle.BackColor= Color.White;
        }

        private void txt_barcode_Enter(object sender, EventArgs e)
        {
            txt_barcode.BackColor = Color.LightBlue;
        }

        private void txt_barcode_Leave(object sender, EventArgs e)
        {
            txt_barcode.BackColor= Color.White;
        }

        private void txt_price_Enter(object sender, EventArgs e)
        {
            txt_price.BackColor = Color.LightBlue;
        }

        private void txt_price_Leave(object sender, EventArgs e)
        {
            txt_price.BackColor= Color.White;
        }

        private void txt_amount_Enter(object sender, EventArgs e)
        {
            txt_amount.BackColor = Color.LightBlue;
        }

        private void txt_amount_Leave(object sender, EventArgs e)
        {
            txt_amount.BackColor= Color.White;
        }

        private void txt_cmd_Enter(object sender, EventArgs e)
        {
            txt_cmd.BackColor = Color.LightBlue;
        }

        private void txt_cmd_Leave(object sender, EventArgs e)
        {
            txt_cmd.BackColor= Color.White;
        }
        #endregion

        #region Check Status Changed between Box 10 and Box 20
        private void che_10_CheckedChanged(object sender, EventArgs e)
        {
            if (che_10.Checked == true && che_20.Checked == false)
            {
                txt_amount.Enabled = true;
                int box = 1;
                
            }
            else if (che_20.Checked == true && che_10.Checked == false)
            {
                txt_amount.Enabled = true;
                int box = 2;
               
            }
            else if (che_20.Checked == true && che_10.Checked == true)
            {
                che_20.Checked = false;
                che_10.Checked = false;
                txt_amount.Enabled = false;
            }
            else if (che_20.Checked == false && che_10.Checked == false)
            {
                txt_amount.Enabled = false;
            }
        }

        private void che_20_CheckedChanged(object sender, EventArgs e)
        {
            if (che_10.Checked == true && che_20.Checked == false)
            {
                txt_amount.Enabled = true;
                int box = 1;
            }
            else if (che_20.Checked == true && che_10.Checked == false)
            {
                txt_amount.Enabled = true;
                int box = 2;
            }
            else if (che_20.Checked == true && che_10.Checked == true)
            {
                che_20.Checked = false;
                che_10.Checked = false;
                txt_amount.Enabled = false;
            }
            else if (che_20.Checked == false && che_10.Checked == false)
            {
                txt_amount.Enabled = false;
            }
            #endregion

        }
    }
}
