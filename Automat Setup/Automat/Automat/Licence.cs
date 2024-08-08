using Automat.Class;
using Microsoft.Data.Sqlite;
using Microsoft.Office.Interop.Excel;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Data.Entity.Infrastructure.Design.Executor;
using Application = System.Windows.Forms.Application;

namespace Automat
{
    public partial class Licence : Form
    {
        SqliteConnection con;
        public Licence()
        {
            InitializeComponent();
            txt_kljuc.MaxLength = 50;
            txt_objekt.MaxLength = 25;

            #region Serial Number of Licence
            //Computer
            string g = System.Windows.Forms.SystemInformation.ComputerName; //Computer Name
            string h = System.Windows.Forms.SystemInformation.UserName.ToString(); // Computer User

            /* Serial Number of Motherboard */
            String SerialNumber = "";
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * from Win32_BaseBoard");
            foreach (ManagementObject mo in mbs.Get())
            {
                SerialNumber = mo["SerialNumber"].ToString().Trim();
            }

            string i = SerialNumber;
            /* End Serial Number of Motherboard */

            string sSourceData;
            byte[] tmpSource;
            byte[] tmpHash;
            string tmpHash1;

            sSourceData = g + h + i;
            //Create a byte array from source data.
            tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
            //Compute hash based on source data.
            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            tmpHash1 = ByteArrayToString(tmpHash);

            txt_ser.Text = tmpHash1;

            //Name of company 
            try
            {
                string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                var con = new SqliteConnection(cs);
                con.Open();
                this.con = con;

                string str2 = "SELECT Name_company FROM Computer WHERE ID=1";
                var cmd2 = new SqliteCommand(str2, con);

                txt_objekt.Text = cmd2.ExecuteScalar().ToString();

                con.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            #endregion
        }

        #region ByteArrayToString Hash Value Licence number
        static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
        #endregion

        #region When form closing
        private void Licence_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region TXT KLJUC PRESS ENTER
        private void txt_kljuc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!File.Exists(@"C:/Automat/Automat/bin/Debug/Licence.txt"))
                {
                    if (String.IsNullOrEmpty(txt_objekt.Text) || String.IsNullOrEmpty(txt_kljuc.Text))
                    {
                        MessageBox.Show("Unesite sve podatke!");
                    }
                    else
                    {

                        //txt file create in directory
                        string fileName = @"C:/Automat/Automat/bin/Debug/Licence.txt";

                        // Create a new file     
                        using (FileStream fs = File.Create(fileName))
                        {
                            // Add some text to file    
                            Byte[] title = new UTF8Encoding(true).GetBytes(txt_kljuc.Text);
                            fs.Write(title, 0, title.Length);
                            // byte[] author = new UTF8Encoding(true).GetBytes("Bruno Lujanović");
                            // fs.Write(author, 0, author.Length);
                        }

                        string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                        var con = new SqliteConnection(cs);
                        con.Open();
                        this.con = con;


                        string str2 = "UPDATE Computer SET Name_company = '" + txt_objekt.Text + "' WHERE ID=1";
                        var cmd2 = new SqliteCommand(str2, con);
                        cmd2.ExecuteReader();

                        con.Close();
                    }
                   
                }
                else
                {
                    if (String.IsNullOrEmpty(txt_objekt.Text) || String.IsNullOrEmpty(txt_kljuc.Text))
                    {
                        MessageBox.Show("Unesite sve podatke!");
                    }
                    else
                    {
                        //txt file create in directory
                        string fileName = @"C:/Automat/Automat/bin/Debug/Licence.txt";
                        // Create a new file     
                        using (FileStream fs = File.Create(fileName))
                        {
                            // Add some text to file    
                            Byte[] title = new UTF8Encoding(true).GetBytes(txt_kljuc.Text.ToString());
                            fs.Write(title, 0, title.Length);
                            // byte[] author = new UTF8Encoding(true).GetBytes("Bruno Lujanović");
                            // fs.Write(author, 0, author.Length);
                        }

                        string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                        var con = new SqliteConnection(cs);
                        con.Open();
                        this.con = con;


                        string str2 = "UPDATE Computer SET Name_company = '" + txt_objekt.Text + "' WHERE ID=1";
                        var cmd2 = new SqliteCommand(str2, con);
                        cmd2.ExecuteReader();

                        con.Close();
                    }
                    
                }
            }
        }

        #endregion

        #region TXT Objekt only accept number,letter,white space
        private void txt_objekt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region Txt Objekt get first letter Upper and rest Lower
        private void txt_objekt_TextChanged(object sender, EventArgs e)
        {
            char[] c = txt_objekt.Text.ToCharArray();
            int j;
            for (j = 0; j < txt_objekt.Text.Length; j++)
            {
                if (j == 0) c[j] = c[j].ToString().ToUpper()[0];
                else c[j] = c[j].ToString().ToLower()[0];
            }
            txt_objekt.Text = new string(c);
            txt_objekt.Select(txt_objekt.Text.Length, 1);
        }
        #endregion
    }
}

