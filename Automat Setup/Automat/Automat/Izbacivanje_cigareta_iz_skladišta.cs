using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Automat
{
    public partial class Izbacivanje_cigareta_iz_skladišta : Form
    {
        SqliteConnection con;

        public Izbacivanje_cigareta_iz_skladišta()
        {
            InitializeComponent();
            cigarete_naziv.Text = Automat_za_cigarete.cigarete;
            kolicina.Text = Automat_za_cigarete.kolicina;
            skladiste.Text = Automat_za_cigarete.skladiste;
            ID.Text = Automat_za_cigarete.ID;
            Price.Text = Automat_za_cigarete.Price; 

            Kolicina_izbacivanja.Text = "0";
        }

        #region Button for increase and decrease value of amount
        private void btn_plus_jedan_Click(object sender, EventArgs e)
        {
            string p = kolicina.Text.ToString().Replace("kom.", "");
            //MessageBox.Show(p);
            int j = Convert.ToInt32(p);

            string s = Kolicina_izbacivanja.Text.ToString();
            int i = Convert.ToInt32(s);

            if (j <= i)
            {
                MessageBox.Show("Količina cigareta ne može biti veća od stanja cigareta u skladištu!");
            }
            else
            {
                i = i + 1;
                Kolicina_izbacivanja.Text = i.ToString();
            }
        }

        private void btn_minus_jedan_Click(object sender, EventArgs e)
        {
            string s = Kolicina_izbacivanja.Text.ToString();
            int i = Convert.ToInt32(s);

            if (i<=0)
            {
                MessageBox.Show("Količina ne može biti 0 ili manja!");
            }
            else
            {
                i = i - 1;
                Kolicina_izbacivanja.Text = i.ToString();
            }
        }

        private void btn_plus_deset_Click(object sender, EventArgs e)
        {
            string p = kolicina.Text.ToString().Replace("kom.", "");
            //MessageBox.Show(p);
            int j = Convert.ToInt32(p);

            string s = Kolicina_izbacivanja.Text.ToString();
            int i = Convert.ToInt32(s);

            if (j <= i || (j-i<10))
            {
                MessageBox.Show("Količina cigareta ne može biti veća od stanja cigareta u skladištu!");
                //Kolicina_izbacivanja.Text = j.ToString();
            }
            else
            {
                i = i + 10;
                Kolicina_izbacivanja.Text = i.ToString();
            }
            
        }

        private void btn_minus_deset_Click(object sender, EventArgs e)
        {
            string s = Kolicina_izbacivanja.Text.ToString();
            int i = Convert.ToInt32(s);

            string p = kolicina.Text.ToString().Replace("kom.","");
            int j = Convert.ToInt32(p);

            if (i <= 0 || i<10)
            {
                MessageBox.Show("Količina ne može biti 0 ili manja!");
            }
            else
            {
                i = i - 10;
                Kolicina_izbacivanja.Text = i.ToString();
            }
        }

        #endregion

        #region Button Enter
        private void Enter_Click(object sender, EventArgs e)
        {
            string t = Kolicina_izbacivanja.Text.ToString();
            int p = Convert.ToInt32(t);

            if (p == 0)
            {
                //Do nothing
            }
            else
            {
                try
                {
                    Check_File_In_Folder();

                    //Storage_User_Control obj = (Storage_User_Control)sender;

                    string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                    var con = new SqliteConnection(cs);

                    con.Open();
                    this.con = con;

                    string str = "SELECT Amount FROM Product_storage WHERE id = '" + ID.Text.ToString() + "' AND Amount > 0 LIMIT 1";

                    MessageBox.Show(ID.Text.ToString());
                    MessageBox.Show(Kolicina_izbacivanja.Text.ToString());
                    MessageBox.Show(cigarete_naziv.Text.ToString());

                    var cmd = new SqliteCommand(str, con);

                    using (SqliteDataReader read = cmd.ExecuteReader())
                    {
                        //creating name of xml file in folder XML
                        string DAT = Convert.ToString(DateTime.Now);
                        StringBuilder sb = new StringBuilder(DAT);

                        sb.Replace(" ", "_");
                        sb.Replace(":", "_");

                        var DATE = sb.ToString();
                        //end creating name of xml file

                        read.Read();
                        if (Int32.Parse(read["Amount"].ToString()) > 1)
                        {
                            string str1 = "UPDATE Product_storage SET Amount = (Amount - '" + Kolicina_izbacivanja.Text.ToString() + "') WHERE id = '" + ID.Text.ToString() + "'";
                            var cmd1 = new SqliteCommand(str1, con);
                            cmd1.ExecuteReader();

                            //creating xml file
                            try
                            {
                                using (XmlWriter writer = XmlWriter.Create($@"C:\Automat\Automat\bin\Debug\XML_S\{DATE}.xml"))
                                {
                                    writer.WriteStartElement("Product");
                                    writer.WriteElementString("title", cigarete_naziv.Text.ToString());
                                    writer.WriteElementString("subtitle", cigarete_naziv.Text.ToString());
                                    writer.WriteElementString("price", Price.Text.ToString());
                                    // writer.WriteElementString("barcode", obj.Barcode);
                                    //writer.WriteElementString("price", obj.Price.Replace("Km", ""));
                                    // writer.WriteElementString("currency", "Km");
                                    writer.WriteElementString("amount", Kolicina_izbacivanja.Text.ToString());
                                    writer.WriteElementString("currency", "kom");
                                    writer.WriteEndElement();
                                    writer.Flush();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                            //reading xml file and insert to database
                            string xml = File.ReadAllText($@"C:\Automat\Automat\bin\Debug\XML_S\{DATE}.xml");
                            byte[] bytes = Encoding.ASCII.GetBytes(xml);

                            //Logs Storage
                            int zero = 0;
                            string str2 = "INSERT INTO Logs_Storage (ID, Title, Price, Amount, Storage, Date, Time, XML, Xml_Name, IsExistFile) " +
                                "VALUES('" + ID.Text.ToString() + "','" + cigarete_naziv.Text.ToString() + "','" + Price.Text.ToString().Replace("Km","") + "', '" + Kolicina_izbacivanja.Text.ToString() +
                                "', '" + skladiste.Text.ToString() + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToShortTimeString() + "', '" + xml + "', '" + DATE + "', '" + zero + "')";
                            var cmd2 = new SqliteCommand(str2, con);
                            cmd2.ExecuteReader();


                            string s = Kolicina_izbacivanja.Text.ToString();
                            int i = Convert.ToInt32(s);

                            kolicina.Text = Int32.Parse(read["Amount"].ToString()) - i + " kom.";

                            this.Close();


                            /* timer1.Interval = 500;
                             timer1.Enabled = true;
                             timer1.Tick += new EventHandler(timer1_Tick);
                             timer1.Start();
                             textBox1.BackColor = Color.Red;
                             //Thread.Sleep(500);
                             textBox2.BackColor = Color.Blue;*/

                        }
                        else
                        {
                            /*string str1 = "UPDATE Product_storage SET Amount = 0 WHERE id = '" + obj.ID + "'";
                            var cmd1 = new SqliteCommand(str1, con);
                            cmd1.ExecuteReader();
                            //creating xml file
                            try
                            {
                                using (XmlWriter writer = XmlWriter.Create($@"C:\Automat\Automat\bin\Debug\XML\{DATE}.xml"))
                                {
                                    writer.WriteStartElement("Product");
                                    writer.WriteElementString("title", obj.Name);
                                    writer.WriteElementString("subtitle", obj.Name);
                                    writer.WriteElementString("barcode", obj.Barcode);
                                    writer.WriteElementString("price", obj.Price.Replace("Km", ""));
                                    writer.WriteElementString("currency", "Km");
                                    writer.WriteEndElement();
                                    writer.Flush();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }*/
                            /*
                            //reading xml file and insert to database
                            string xml = File.ReadAllText($@"C:\Automat\Automat\bin\Debug\XML\{DATE}.xml");
                            byte[] bytes = Encoding.ASCII.GetBytes(xml);

                            //Logs
                            int zero = 0;
                            string str2 = "INSERT INTO Logs (product_id, product_title, product_subtitle, product_barcode, product_cmd, product_date, product_time, product_xml, product_xml_file_name, IsExistFile) " +
                                "VALUES('" + obj.ID + "','" + obj.Name1 + "', '" + obj.Name + "', '" + obj.Barcode + "', '" + obj.CMD + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortTimeString() + "', '" + xml + "', '" + DATE + "', '" + zero + "')";
                            var cmd2 = new SqliteCommand(str2, con);

                            cmd2.ExecuteReader();
                            */

                            /* obj.Amount = 0 + " kom.";

                             MessageBox.Show("Cigarete:" + obj.Name + "u skladištu" + obj.Storage + "nema ih! ,\nMolimo napunite skladište za ponovno korištenje.");
                            */

                            /* flowLayoutPanel5.Visible = false;
                             flowLayoutPanel4.Visible = true;
                             btn_back.Visible = true;
                             btn_back1.Visible = false;*/

                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    /*MessageBox.Show("COM Port je isključen ili niste odabrali dobar COM Port. Pokušajte isključiti/uključiti kabel ili odaberite novi COM port u postavkama automata!",
                                    "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);*/
                }
            }
            
        }
        #endregion

        #region ESC (KeyPress)
        private void Izbacivanje_cigareta_iz_skladišta_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Escape 
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion

        #region Check File in Folder

        void Check_File_In_Folder()
        {
            try
            {
                string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                var con = new SqliteConnection(cs);

                con.Open();
                this.con = con;

                string XMLName = string.Empty;

                string str = "SELECT Xml_Name FROM Logs_Storage WHERE IsExistFile = 0";

                var cmd = new SqliteCommand(str, con);

                using (SqliteDataReader read = cmd.ExecuteReader())
                {
                    if (read.HasRows)
                    {
                        while (read.Read())
                        {
                            for (int i = 0; i < read.FieldCount; i++)
                            {

                                XMLName = read.GetString(i);
                                MessageBox.Show(XMLName);

                                if (!File.Exists($@"C:\Automat\Automat\bin\Debug\XML_S\{XMLName}.xml"))
                                {
                                    MessageBox.Show(XMLName + ".xml");
                                    string str4 = "UPDATE Logs_Storage SET IsExistFile = 1 WHERE Xml_Name = '" + XMLName + "'";
                                    MessageBox.Show(str4);
                                    var cmd1 = new SqliteCommand(str4, con);
                                    cmd1.ExecuteNonQuery();
                                    XMLName = "";
                                    read.NextResult();
                                }
                                else
                                {
                                    MessageBox.Show("File postoji!");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        #endregion
    }
}
