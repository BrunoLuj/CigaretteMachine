using System;
using System.IO.Ports;
using System.Windows.Forms;
using Automat.Class;
using SQLite;
using Microsoft.Data.Sqlite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Net.NetworkInformation;


namespace Automat
{
    public partial class PostavkeCOMPorta : Form
    {
        SqliteConnection con;
        public PostavkeCOMPorta()
        {
            InitializeComponent();

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            try
            {
                foreach (string port in SerialPort.GetPortNames())
                {
                    cmb_COMPort.Items.Add(port);
                }

                string str = "SELECT PortName FROM COMPort WHERE ID=1";
                var cmd = new SqliteCommand(str, con);
                SqliteDataReader read = cmd.ExecuteReader();

                while (read.Read())
                {
                    cmb_COMPort.SelectedItem = read.GetString(0);
                }

                lbl_CMP.Text = cmb_COMPort.Text;
            }
            catch (Exception e)
            {
                MessageBox.Show("Dogodila se greška, napravite reset programa. " + "\n" + e.Message, "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            con.Close();

        }

        private void PostavkeCOMPorta_Load(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(@"C:\Automat\Automat\bin\Debug\SQLiteData.db"))
            {
                //Do Nothing
            }
            else
            {
                var db = new SQLiteConnection(@"C:\Automat\Automat\bin\Debug\SQLiteData.db");

                db.CreateTable<COMPort>();

                db.Close();
            }
        }

        #region Save COM Port
        private void btn_COMPortSpremi_Click(object sender, EventArgs e)
        {
            try
            {

                if (cmb_COMPort.SelectedItem != null)
                {
                    //COMPort temp = new COMPort(cmb_COMPort.Text);

                    var db = new SQLiteConnection(@"C:\Automat\Automat\bin\Debug\SQLiteData.db");

                    string str = "UPDATE COMPort SET PortName = '" + cmb_COMPort.SelectedItem.ToString() + "' WHERE ID = 1";

                    db.Execute(str);
                    db.Close();

                    DialogResult dialogResult = MessageBox.Show("Da li želite spremiti novi COM Port!.", "Informacija", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                    MessageBox.Show("Uspješno ste spremili novi COM Port!.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        //DO Nothing
                    }
                }

                else
                {
                    MessageBox.Show("Odaberite neki od ponuđenih COM port-ova. Ako nema niti jednog, provjerite kabele od automata za cigarete.","Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
               
            catch (Exception ex)
            {
                MessageBox.Show("[Greška pri spremanju COM Porta] \n" + ex.Message, "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        #endregion

        #region Refresh COM Port
        private void btn_refreshCOM_Click(object sender, EventArgs e)
        {
            cmb_COMPort.Items.Clear();
            try
            {
                foreach (string port in SerialPort.GetPortNames())
                {
                    cmb_COMPort.Items.Add(port);
                }
                cmb_COMPort.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ESC Key Press
        private void PostavkeCOMPorta_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Escape 
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }
        #endregion

        #region PostavkeComPorta Closing
        private void PostavkeCOMPorta_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Vaš COM Port je : " + cmb_COMPort.Text, "Informacija", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Cancel)
            {
                e.Cancel = true;
            }

        }
        #endregion

        #region KeyDown TextBox Enter like Tab
        private void cmb_COMPort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }
        #endregion

    }
}
