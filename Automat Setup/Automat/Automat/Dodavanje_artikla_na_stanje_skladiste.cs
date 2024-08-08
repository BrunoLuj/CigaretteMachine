using Microsoft.Data.Sqlite;
using SQLite;
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
    public partial class Dodavanje_artikla_na_stanje_skladiste : Form
    {
        SqliteConnection con;

        #region Data from Listiew
        public string ID { get; set; } = "";
        public string Title { get; set; } = "";
        public string Subtitle { get; set; } = "";
        public string Barcode { get; set; } = "";
        public string Price { get; set; } = "";
        public string Amount { get; set; } = "";
        public string Box { get; set; } = "";
        #endregion

        public Dodavanje_artikla_na_stanje_skladiste()
        {
            InitializeComponent();
            txt_amount_add.MaxLength = 3;
            txt_id.ReadOnly = true;
        }

        private void Dodavanje_artikla_na_stanje_skladiste_Load(object sender, EventArgs e)
        {
            DataFromListView();
        }

        #region Data From ListView To TextBox
        private void DataFromListView()
        {
            //Populate Textbox-s with data from ListView
            txt_id.Text = ID;
            txt_title.Text = Title;
            txt_subtitle.Text = Subtitle;
            txt_barcode.Text = Barcode;
            txt_price.Text = Price;
            txt_amount.Text = Amount;
            txt_cmd.Text = Box;
        }
        #endregion

        #region Key Press Event
        private void Dodavanje_artikla_na_stanje_skladiste_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Escape 
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion

        #region KeyDown TextBox Enter like Tab
        private void txt_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

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

        private void txt_cmd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txt_amount_add_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }


        #endregion

        #region Update DataBase with new Amount
        private void btn_save_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txt_amount_sum.Text))
            {
                MessageBox.Show("Ukupna vrijednost stanja ne može biti prazna", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    var db = new SQLiteConnection(@"C:\Automat\Automat\bin\Debug\SQLiteData.db");

                    string str = "UPDATE Product_storage SET Amount = '" + txt_amount_sum.Text + "', Title = '" + txt_title.Text + "', Subtitle = '" + txt_subtitle.Text + "', Barcode = '" + txt_barcode.Text + "'," +
                        "Price = '" + txt_price.Text + "', Storage_Num= '" + txt_cmd.Text + "' WHERE id =  '" + txt_id.Text + "' ";

                    db.Execute(str);

                    string str2 = "INSERT INTO Logs_Storage_dodavanje_na_stanje (ID, Title, Subtitle, Barcode, Storage_Num,  Na_stanju, Dodano_na_stanje, Ukupno_ostavljeno_nakon_dodavanja, Date,  Time) " +
                                "VALUES('" + txt_id.Text + "','" + txt_title.Text + "', '" + txt_subtitle.Text + "', '" + txt_barcode.Text + "', '" + txt_cmd.Text + "', '" + txt_amount.Text + "', '" + txt_amount_add.Text + "', '" + txt_amount_sum.Text + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortTimeString() + "')";

                    db.Execute(str2);

                    db.Close();

                    MessageBox.Show("Uspješno ste spremili proizvod!", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Dogodila se pogreška!" + ex.Message, "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                btn_save.Enabled = false;
            }

        }
        #endregion

        #region TextBox Amount Add And Sum Trenutno stanje i Dodaje se na stanje (ukupno ne veće od 1000 po skladištu)
        private void txt_amount_add_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_amount.Text) && !string.IsNullOrEmpty(txt_amount_add.Text))
            {
                txt_amount_sum.Text = ((Convert.ToInt32(txt_amount.Text) + Convert.ToInt32(txt_amount_add.Text)).ToString());

                if ((Convert.ToInt32(txt_amount_sum.Text)) >= 1000)
                {
                    MessageBox.Show("Ukupno stanje u Box-u ne može biti veće od 1000");

                    txt_amount_add.Text = "";
                    txt_amount_sum.Text = "";
                    txt_amount_add.Focus();
                }
            }
        }
        #endregion

        #region TextBox Amount Add only accept Number
        private void txt_amount_add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region Active color of textbox (lightBlue) and leave color (White)
        private void txt_title_Enter(object sender, EventArgs e)
        {
            txt_title.BackColor = Color.LightBlue;
        }

        private void txt_title_Leave(object sender, EventArgs e)
        {
            txt_title.BackColor = Color.White;
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

        private void txt_cmd_Enter(object sender, EventArgs e)
        {
            txt_cmd.BackColor = Color.LightBlue;
        }

        private void txt_cmd_Leave(object sender, EventArgs e)
        {
            txt_cmd.BackColor= Color.White;
        }

        private void txt_amount_add_Enter(object sender, EventArgs e)
        {
            txt_amount_add.BackColor = Color.LightBlue;
        }

        private void txt_amount_add_Leave(object sender, EventArgs e)
        {
            txt_amount_add.BackColor= Color.White;
        }
        #endregion
    }
}
