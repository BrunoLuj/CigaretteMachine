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
    public partial class Postavke_skladista : Form
    {
        SqliteConnection con;

        public Postavke_skladista()
        {
            InitializeComponent();
        }

        private void btn_SkladisteSpremi_Click(object sender, EventArgs e)
        {
            try
            {

                if (String.IsNullOrEmpty(Br_skladista.Text))
                {
                    MessageBox.Show("Broj skladišta ne može biti prazan broj!", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    //COMPort temp = new COMPort(cmb_COMPort.Text);

                    var db = new SQLiteConnection(@"C:\Automat\Automat\bin\Debug\SQLiteData.db");

                    string str = "UPDATE Config_storage SET Storage = '" + Br_skladista.Text.ToString() + "' WHERE ID = 1";

                    db.Execute(str);
                    db.Close();

                    DialogResult dialogResult = MessageBox.Show("Da li želite spremiti novi broj skladišta!.", "Informacija", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        MessageBox.Show("Uspješno ste spremili novi broj skladišta!.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //DO Nothing
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Broj skladišta ne može biti prazan broj!", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region ESC
        private void Postavke_skladista_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Escape 
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region KeyDown TextBox Enter like Tab
        private void Br_skladista_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }
        #endregion

        #region Active color of textbox (lightBlue) and leave color (White)
        private void Br_skladista_Enter(object sender, EventArgs e)
        {
            Br_skladista.BackColor = Color.LightBlue;
        }

        private void Br_skladista_Leave(object sender, EventArgs e)
        {
            Br_skladista.BackColor = Color.White;
        }
        #endregion
    }
}
