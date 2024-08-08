using Automat.Class;
using Microsoft.Data.Sqlite;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography;
using System.Management;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Text.RegularExpressions;

namespace Automat
{

    public partial class Automat_za_cigarete : Form
    {
        SqliteConnection con;
        
        List<ButtonDynamics> products;
        List<Storage_User_Control> products_storage;

        public Automat_za_cigarete()
        {
            InitializeComponent();
            Refresh();
            loadform();
                
            #region Licence.txt dont exist
            //If Licence.txt dont exist
            if (!File.Exists(@"C:/Automat/Automat/bin/Debug/Licence.txt"))
            {

                Licence frm = new Licence();
                frm.ShowDialog();
            }
            #endregion

        }

        public string Licence;

        public void Automat_za_cigarete_Load(object sender, EventArgs e)
        {
            if (UtilityClass.IsCheckInternet()) //Version With Check Internet Connection
            {
                WhenLoad();
            }
            else
            {
                MessageBox.Show(@"Please check your internet connection", @"Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                Application.ExitThread();
            }

            //WhenLoad(); //Version Without Check Internet Connection
        }

        #region When load form
        private void WhenLoad()
        {
            if (System.IO.File.Exists(@"C:\Automat\Automat\bin\Debug\SQLiteData.db"))
            {
                GenerateButton();
                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel2.Visible = false;
                //flowLayoutPanel3.Visible = false;
                flowLayoutPanel4.Visible = false;
                flowLayoutPanel5.Visible = false;
                btn_back.Visible = false;
                btn_back1.Visible = false;
                button2.Visible = false;

                loadform();
                Refresh();

                #region Insert Licence key
                //Insert Licence key
                var db = new SQLiteConnection(@"C:\Automat\Automat\bin\Debug\SQLiteData.db");

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

                string str4 = "UPDATE Computer SET Licence = '" + tmpHash1 + "' WHERE ID=1";

                db.Execute(str4);

                #endregion

                #region Read Licence Key and Check if it is same
                // read Licence from Database
                string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";

                var con = new SqliteConnection(cs);
                
                con.Open();
                this.con = con;

                string str = "SELECT Licence FROM Computer WHERE ID=1";
                var cmd = new SqliteCommand(str, con);
                using (SqliteDataReader read = cmd.ExecuteReader())
                {
                    read.Read();
                    Licence = read["Licence"].ToString();
                }

                string sSourceData1;
                byte[] tmpSource1;
                byte[] tmpHash3;
                string tmpHash4;

                sSourceData1 = Licence.ToString();
                //Create a byte array from source data.
                tmpSource1 = ASCIIEncoding.ASCII.GetBytes(sSourceData1);
                //Compute hash based on source data.
                tmpHash3 = new MD5CryptoServiceProvider().ComputeHash(tmpSource1);
                tmpHash4 = ByteArrayToString(tmpHash3);

                string _key = tmpHash4.Substring(0, 19);

                string Key_Licence = Convert.ToString(Regex.Replace(_key, ".{4}", "$0-"));

                //If Licence.txt dont exist
                if (!File.Exists(@"C:/Automat/Automat/bin/Debug/Licence.txt"))
                {
                    Licence frm = new Licence();
                    frm.ShowDialog();
                }
                else
                {
                    // Open the stream and read it back.
                    // txt file create in directory
                    string fileName = @"C:/Automat/Automat/bin/Debug/Licence.txt";

                    using (StreamReader sr = File.OpenText(fileName))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            label2.Text = s;
                        }
                    }
                    
                    if (Key_Licence == label2.Text)
                    {
                        //Do nothing
                    }
                    else
                    {
                        //Open Form Licence

                        //throw new Exception("Invalid License");
                        // MessageBox.Show("Invalid Licence", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Application.ExitThread();

                        Licence frm = new Licence();
                        frm.ShowDialog();
                    }
                    #endregion
                }
            }
            else
            {
                //Do Nothing
            }
        }
        #endregion

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

        #region When Load Form Automat za cigarete / Populate panel from database
        private void loadform()
        {
            flowLayoutPanel1.Controls.Clear();

            //create folder for Export XML Automat file
            string currentPath = Directory.GetCurrentDirectory();
            if (!Directory.Exists(Path.Combine(currentPath, "XML")))
                Directory.CreateDirectory(Path.Combine(currentPath, "XML"));
            //at this point your folder should exist

            //create folder for Export XML Storage File
            string currentPath1 = Directory.GetCurrentDirectory();
            if (!Directory.Exists(Path.Combine(currentPath1, "XML_S")))
                Directory.CreateDirectory(Path.Combine(currentPath1, "XML_S"));
            //at this point your folder should exist


            //create file database
            if (System.IO.File.Exists(@"C:\Automat\Automat\bin\Debug\SQLiteData.db"))
            {
                //Do Nothing
            }
            else
            {
                var db = new SQLiteConnection(@"C:\Automat\Automat\bin\Debug\SQLiteData.db");

                db.CreateTable<COMPort>();
                db.CreateTable<Products>();
                db.CreateTable<Logs>();
                db.CreateTable<Config_storage>();
                db.CreateTable<Info>();
                db.CreateTable<Logs_Storage>();
                db.CreateTable<Logs_Storage_dodavanje_na_stanje>();
                db.CreateTable<Logs_Storage_dodavanje_proizvoda>();
                db.CreateTable<Logs_dodavanje_na_stanje>();
                db.CreateTable<Logs_dodavanje_proizvoda>();
                db.CreateTable<Product_storage>();
                db.CreateTable<Computer>();

                //ComPort
                string a = "COM1";
                string str1 = "INSERT INTO COMPort(PortName)"+"VALUES('" + a + "')";
                db.Execute(str1);

                //ConfigStorage
                int b = 1;
                int c = 1;
                string str2 = "INSERT INTO Config_storage(ID,Storage)" + "VALUES('" + b + "','" + c + "')";
                db.Execute(str2);

                //INFO
                string d = "Bruno";
                string e = "+(387)30 735-200";
                string f = "caljkusic@tel.net.ba";
                string str3 = "INSERT INTO Info(Name,Broj,Email)" + "VALUES('" + d + "','" + e + "','" + f + "')";

                db.Execute(str3);

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

                string naziv = "Čaljkušić d.o.o."; //naziv kompanije
                string str4 = "INSERT INTO Computer(Licence,Name_company)" + "VALUES('" + tmpHash1 + "','" + naziv + "')";
                
                db.Execute(str4);

                //txt file create in directory
                string fileName = @"C:/Automat/Automat/bin/Debug/Licence.txt";
                // Create a new file     
                using (FileStream fs = File.Create(fileName))
                {
                    // Add some text to file    
                    Byte[] title = new UTF8Encoding(true).GetBytes(tmpHash1);
                    fs.Write(title, 0, title.Length);
                   // byte[] author = new UTF8Encoding(true).GetBytes("Bruno Lujanović");
                   // fs.Write(author, 0, author.Length);
                }

                db.Close();

            }

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            // string str = "SELECT *  FROM Products ORDER BY product_title";
            string str = "SELECT DISTINCT product_title FROM Products ORDER BY product_title";
            var cmd = new SqliteCommand(str, con);


            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                /*int x = 40;
                int y = 30;
                int horizontalSpacing = 140;
                int verticalSpacing = 170;
                
                Size s = new Size(120, 150);
                */
                int counter = 0;
                while (read.Read())
                {
                    products = new List<ButtonDynamics>()
                    {
                        new ButtonDynamics()
                        {
                            Name = read["product_title"].ToString(),
                          /*Price = read["product_price"].ToString() + " " + "Km",
                            Amount=read["product_amount"].ToString() + " " + "kom.",*/
                            Color= Color.FromArgb(189, 213, 234)
                        }
                    };

                    foreach (ButtonDynamics item in products)
                    {
                        flowLayoutPanel1.Controls.Add(item);
                        item.Click += new System.EventHandler(this.ButtonDynamics_Click);
                    }

                    //Ovako se može generirati dinamički button
                    /*Button dynamicButton = new Button() { Location = new Point(x, y), Size = s }; 
                    {
                        dynamicButton.Text = read["product_subtitle"].ToString() + " \n" + read["product_price"].ToString() +" "+"Km";
                        //dynamicButton.Width = 120;
                        //dynamicButton.Height = 150;
                        //dynamicButton.Location = new Point(10, 50 );
                        //dynamicButton.Name = read["product_title"].ToString();

                    }*/

                    /*dynamicButton.Height = (int)read["height"];
                    dynamicButton.Width = (int)read["width"];

                    dynamicButton.Name = read["name"].ToString();
                    dynamicButton.Location = new Point(20, 150);*/

                    /*panel1.Controls.Add(dynamicButton);
                    x += horizontalSpacing;*/

                    /*counter++;
                    if (counter % 3 == 0)
                    {
                        y += verticalSpacing;
                        x = 40;
                    }*/

                    counter++;

                    if (counter > 12)
                    {
                        flowLayoutPanel1.AutoScroll = false;
                        flowLayoutPanel1.HorizontalScroll.Enabled = false;
                        flowLayoutPanel1.HorizontalScroll.Visible = false;
                        flowLayoutPanel1.HorizontalScroll.Maximum = 0;
                        flowLayoutPanel1.AutoScroll = true;
                    }

                }
            }
        }
        #endregion

        #region Open UserControl (ButtonDynamics) and show all Products
        void ButtonDynamics_Click(object sender, EventArgs e)
        {
            btn_back.Visible = true;
            flowLayoutPanel2.Controls.Clear();

            ButtonDynamics obj = (ButtonDynamics)sender;
            showpanel();

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            bool PanelVisible = false;

            con.Open();
            this.con = con;

            //string str = "SELECT product_title, product_subtitle, product_price, product_amount, product_price FROM Products WHERE product_price > 0  GROUP BY [product_title]";
            //string str = "SELECT DISTINCT product_title FROM Products ORDER BY product_title";
            string str = "SELECT * FROM Products WHERE product_title = '" + obj.Name.ToString() + "' AND product_amount > 0 ";

            var cmd = new SqliteCommand(str, con);

            //MessageBox.Show(this.ButtonDynamics_Click.Text.toString());

            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                int counter = 0;
                while (read.Read())
                {
                    //MemoryStream ms = new MemoryStream((byte[])read["product_img"]);

                    products = new List<ButtonDynamics>()
                    {

                    new ButtonDynamics()
                        {
                            Name1 = read["product_title"].ToString(),
                            Name = read["product_subtitle"].ToString(),
                            Price = read["product_price"].ToString() + " " + "Km",
                            Amount = read["product_amount"].ToString() + " " + "kom.",
                            CMD = read["product_cmd"].ToString(),
                            Barcode=read["product_barcode"].ToString(),
                            ID = read["product_id"].ToString(),
                            Box = read["product_box"].ToString(),
                           //Image1 = new Bitmap(ms).ToString(),
                           Color= Color.FromArgb(247, 247, 255)
                        }
                    };

                    foreach (ButtonDynamics item in products)
                    {
                        flowLayoutPanel2.Controls.Add(item);
                        //MessageBox.Show(read["product_subtitle"].ToString());
                        item.Click += new System.EventHandler(this.ButtonDynamics1_Click);
                    }

                    counter++;

                    if (counter > 12)
                    {
                        PanelVisible = true;
                    }
                }

                if (PanelVisible)
                {
                    flowLayoutPanel2.AutoScroll = true;
                    //flowLayoutPanel2.AutoScroll = false;
                    // flowLayoutPanel2.HorizontalScroll.Enabled = false;
                    //flowLayoutPanel2.HorizontalScroll.Visible = false;
                    //flowLayoutPanel2.HorizontalScroll.Maximum = 0;
                    //flowLayoutPanel2.AutoScroll = true;
                }

            }
        }

        #endregion

        #region COMPORT
        public string COMPort;
        public void GetComPort()
        {

            string str = "SELECT PortName FROM COMPort";
            var cmd = new SqliteCommand(str, con);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                read.Read();
                COMPort = read["PortName"].ToString();
            }

        }
        #endregion

        #region Open UserControl (ButtonDynamics) and Execute Cigarete

        // Create the serial port with basic settings
        // public SerialPort port = new SerialPort(portName: COMPort, 9600, Parity.None, 8, StopBits.One);

        void ButtonDynamics1_Click(object sender, EventArgs e)
        {

            Check_file_in_folder();
            GetComPort();

            ButtonDynamics obj = (ButtonDynamics)sender;
                
            SerialPort port = new SerialPort(COMPort, 9600, Parity.None, 8, StopBits.One);

            try
            {
                if (!(port.IsOpen) && (port != null))
                {
                    port.Open();
                    port.Write(obj.CMD);

                    int retrunValue = Int32.Parse(port.ReadLine());
                    int sendValue = Int32.Parse(obj.CMD.ToString());

                    if (retrunValue == sendValue)
                    {
                        string str = "SELECT product_amount FROM Products WHERE product_cmd = '" + obj.CMD + "' LIMIT 1";

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
                            if (Int32.Parse(read["product_amount"].ToString()) > 1)
                            {
                                string str1 = "UPDATE Products SET product_amount = (product_amount-1) WHERE product_cmd = '" + obj.CMD + "'";
                                var cmd1 = new SqliteCommand(str1, con);
                                cmd1.ExecuteReader();


                                //creating xml file
                                try
                                {
                                    using (XmlWriter writer = XmlWriter.Create($@"C:\Automat\Automat\bin\Debug\XML\{DATE}.xml"))
                                    {
                                        writer.WriteStartElement("Product");
                                        writer.WriteElementString("title", obj.Name1);
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
                                }

                                //reading xml file and insert to database
                                string xml = File.ReadAllText($@"C:\Automat\Automat\bin\Debug\XML\{DATE}.xml");
                                byte[] bytes = Encoding.ASCII.GetBytes(xml);

                                //Logs
                                int zero = 0;
                                int one = 1;

                                string str2 = "INSERT INTO Logs (product_id, product_title, product_subtitle, product_barcode, product_cmd ,product_price, product_amount, product_box, product_date, product_time, product_xml, product_xml_file_name, IsExistFile) " +
                                    "VALUES('" + obj.ID + "','" + obj.Name1 + "', '" + obj.Name + "', '" + obj.Barcode + "', '" + obj.CMD + "','" + obj.Price.Replace("Km", "") + "','" + one + "','" + obj.Box + "', '" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToShortTimeString() + "', '" + xml + "', '" + DATE + "', '" + zero + "')";
                                var cmd2 = new SqliteCommand(str2, con);
                                cmd2.ExecuteReader();

                                obj.Amount = Int32.Parse(read["product_amount"].ToString()) - 1 + " kom.";

                                //Timer for RX/TX
                                timer1.Interval = 500;
                                timer1.Enabled = true;
                                timer1.Tick += new EventHandler(timer1_Tick);
                                timer1.Start();
                                textBox1.BackColor = Color.Red;
                                //Thread.Sleep(500);
                                textBox2.BackColor = Color.Blue;

                            }
                            else
                            {
                                string str1 = "UPDATE Products SET product_amount = 0 WHERE product_cmd = '" + obj.CMD + "'";
                                var cmd1 = new SqliteCommand(str1, con);
                                cmd1.ExecuteReader();
                                //creating xml file
                                try
                                {
                                    using (XmlWriter writer = XmlWriter.Create($@"C:\Automat\Automat\bin\Debug\XML\{DATE}.xml"))
                                    {
                                        writer.WriteStartElement("Product");
                                        writer.WriteElementString("title", obj.Name1);
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
                                }

                                //reading xml file and insert to database
                                string xml = File.ReadAllText($@"C:\Automat\Automat\bin\Debug\XML\{DATE}.xml");
                                byte[] bytes = Encoding.ASCII.GetBytes(xml);

                                //Logs
                                int zero = 0;
                                int one = 1;
                                string str2 = "INSERT INTO Logs (product_id, product_title, product_subtitle, product_barcode, product_cmd, product_price, product_amount, product_box, product_date, product_time, product_xml, product_xml_file_name, IsExistFile) " +
                                    "VALUES('" + obj.ID + "','" + obj.Name1 + "', '" + obj.Name + "', '" + obj.Barcode + "', '" + obj.CMD + "','" + obj.Price.Replace("Km", "") + "','" + one + "' ,'" + obj.Box + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DateTime.Now.ToShortTimeString() + "', '" + xml + "', '" + DATE + "', '" + zero + "')";
                                var cmd2 = new SqliteCommand(str2, con);

                                cmd2.ExecuteReader();

                                obj.Amount = 0 + " kom.";

                                MessageBox.Show("Box " + obj.CMD + " je prazan!,\nMolimo napunite box za ponovno korištenje.");

                                flowLayoutPanel2.Visible = false;
                                flowLayoutPanel1.Visible = true;
                                btn_back.Visible = false;

                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Error : Nepoznata komadna");
                    }

                    //MessageBox.Show("Aktiviran box :" + port.ReadLine());
                    //skiniSaStanja(obj.CMD);
                    try
                    {
                        port.Close();
                    }
                    catch(Exception p)
                    {
                        MessageBox.Show(p.Message);
                    }
                    
                }
                else 
                {
                    System.Windows.Forms.MessageBox.Show("Make connection first");
                }

            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Greška otvaranja/pisanja u serial port(COM Port) :: " + "\n" + ex.Message, "Greška!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Greška otvaranja/pisanja u serial port(COM Port) :: " + "\n" + ex.Message, "Greška!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Greška otvaranja/pisanja u serial port(COM Port) :: " + "\n" + ex.Message, "Greška!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (IOException ex)
            {
                MessageBox.Show("Greška otvaranja/pisanja u serial port(COM Port) :: " + "\n" + ex.Message, "Greška!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Greška otvaranja/pisanja u serial port(COM Port) :: " + "\n" + ex.Message, "Greška!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        #endregion

        #region Show/Hide panel
                private void showpanel()
        {
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel4.Visible = false;
            flowLayoutPanel2.Visible = true; 
        }
        private void hidepanel()
        {
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel4.Visible = false;
        }
        #endregion

        #region Show/Hide panel Storage
        private void showpanel1()
        {
            flowLayoutPanel4.Visible = false;
            flowLayoutPanel5.Visible = true;
            button2.Visible = true;
        }
        private void hidepanel1()
        {
            flowLayoutPanel5.Visible = false;

        }
        #endregion

        #region Open Form Konfiguracija
        private void btn_Konfiguracija_Click(object sender, EventArgs e)
        {
            Konfiguracija f = new Konfiguracija();
            f.ShowDialog();
        }
        #endregion

        #region Open Form Pregled Stanja
        private void button1_Click(object sender, EventArgs e)
        {
            Pregled_stanja frm = new Pregled_stanja();
            frm.ShowDialog();
        }

        #endregion

        #region Do this when Application closing
        private void Automat_za_cigarete_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Ovo će zatvoriti cijelu aplikaciju. Potvrdite?", "Izlaz iz aplikacije.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
            serialPort1.Close();
        }
        #endregion

        #region Back Button
        private void btn_back_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = true;
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel4.Visible = false;
            btn_back.Visible = false;
            button2.Visible = false;
            button1.Visible = true;

            this.Text = "Automat za cigarete";
        }
        #endregion

        #region Back Button Storage
        private void btn_back1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel5.Visible = false;
            flowLayoutPanel4.Visible = true;
            btn_back.Visible = true;
            btn_back1.Visible = false;
            button1.Visible = false; 
            button2.Visible = true;
        }
        #endregion

        #region Refresh Form When Close Another Form
        private void Automat_za_cigarete_Activated(object sender, EventArgs e)
        {
            Refresh();
            loadform();
            GenerateButton();
        }

        #endregion

        #region Timer tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.GhostWhite;
            textBox2.BackColor = Color.GhostWhite;
            timer1.Stop();
        }
        #endregion

        #region Check file in folder
        private void Check_file_in_folder()
        {
            try
            {
                string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                var con = new SqliteConnection(cs);

                con.Open();
                this.con = con;

                string XMLName = string.Empty;

                string str = "SELECT product_xml_file_name FROM Logs WHERE IsExistFile = 0";

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
                                //MessageBox.Show(XMLName);

                                if (!File.Exists($@"C:\Automat\Automat\bin\Debug\XML\{XMLName}.xml"))
                                {
                                     //MessageBox.Show(XMLName + ".xml");
                                     string str4 = "UPDATE Logs SET IsExistFile = 1 WHERE product_xml_file_name = '" + XMLName + "'";
                                     //MessageBox.Show(str4);
                                     var cmd1 = new SqliteCommand(str4, con);
                                     cmd1.ExecuteNonQuery();
                                    XMLName = "";
                                    read.NextResult();
                                }
                                else
                                {
                                    //MessageBox.Show("File postoji!");
                                }
                                
                            }
                            
                        }
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
           
        }
        #endregion

        #region Get Number Of Storage and generate button from number in database

        public string Num_storage;
        void GenerateButton()
        {
            flowLayoutPanel3.Controls.Clear();

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT Storage FROM Config_storage";
            var cmd = new SqliteCommand(str, con);
            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                read.Read();
                Num_storage = read["Storage"].ToString();
                int num1 = Int32.Parse(Num_storage);

                //button number from database
                for (int i = 1; i <= num1; ++i)
                {
                    Button btn = new Button();
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.BorderColor = Color.FromArgb(123, 117, 134);
                    btn.Width = 35;
                    btn.Height = 35; //set padding or margin to appropriate values
                    

                    //button click event handler
                    switch (i)
                    {
                        case 1:
                            btn.Image = Image.FromFile(@"C:\Automat\Automat\images\hangar1.png");
                            btn.Click += new EventHandler(btn_Click);
                            btn.FlatAppearance.BorderColor = Color.FromArgb(55, 117, 134);
                            break;
                        case 2:
                            btn.Image = Image.FromFile(@"C:\Automat\Automat\images\hangar2.png");
                            btn.Click += new EventHandler(btn_Click1);
                            btn.FlatAppearance.BorderColor = Color.FromArgb(55, 117, 134);
                            break;
                        case 3:
                            btn.Image = Image.FromFile(@"C:\Automat\Automat\images\hangar3.png");
                            btn.Click += new EventHandler(btn_Click2);
                            btn.FlatAppearance.BorderColor = Color.FromArgb(55, 117, 134);
                            break;
                        case 4:
                            btn.Image = Image.FromFile(@"C:\Automat\Automat\images\hangar4.png");
                            btn.Click += new EventHandler(btn_Click3);
                            btn.FlatAppearance.BorderColor = Color.FromArgb(55, 117, 134);
                            break;
                        case 5:
                            btn.Image = Image.FromFile(@"C:\Automat\Automat\images\hangar5.png");
                            btn.Click += new EventHandler(btn_Click4);
                            btn.FlatAppearance.BorderColor = Color.FromArgb(55, 117, 134);
                            break;
                        case 6:
                            btn.Image = Image.FromFile(@"C:\Automat\Automat\images\hangar6.png");
                            btn.Click += new EventHandler(btn_Click5);
                            btn.FlatAppearance.BorderColor = Color.FromArgb(55, 117, 134);
                            break;
                        case 7:
                            btn.Image = Image.FromFile(@"C:\Automat\Automat\images\hangar7.png");
                            btn.Click += new EventHandler(btn_Click6);
                            btn.FlatAppearance.BorderColor = Color.FromArgb(55, 117, 134);
                            break;
                        case 8:
                            btn.Image = Image.FromFile(@"C:\Automat\Automat\images\hangar8.png");
                            btn.Click += new EventHandler(btn_Click7);
                            btn.FlatAppearance.BorderColor = Color.FromArgb(55, 117, 134);
                            break;
                        case 9:
                            btn.Image = Image.FromFile(@"C:\Automat\Automat\images\hangar9.png");
                            btn.Click += new EventHandler(btn_Click8);
                            btn.FlatAppearance.BorderColor = Color.FromArgb(55, 117, 134);
                            break;
                        case 10:
                            btn.Image = Image.FromFile(@"C:\Automat\Automat\images\hangar10.png");
                            btn.Click += new EventHandler(btn_Click9);
                            btn.FlatAppearance.BorderColor = Color.FromArgb(55, 117, 134);
                            break;
                            //...
                    }
                    this.Controls.Add(btn);

                    flowLayoutPanel3.Controls.Add(btn);
                }
                this.Controls.Add(flowLayoutPanel3);
            }

        }

        #endregion

        #region Generated button that serves to extract the names and warehouse number from the database and generates a button for ejecting larger quantities of cigarettes
        //10 button max 

        public static string skladiste1;
        public void btn_Click(object sender, EventArgs e)
        {
            skladiste1 = "1";

            flowLayoutPanel4.Controls.Clear();
            btn_back_Click(sender, e);
            //((Control)sender).BackColor = Color.Red;
            flowLayoutPanel4.Visible = true;
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel5.Visible = false;
            button1.Visible= false;
            btn_back.Visible = true;
            btn_back1.Visible = false;
            button2.Visible = true;

            this.Text = "Skladište automata za cigarete 1";

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT DISTINCT Title,Storage_Num FROM Product_storage WHERE Storage_Num = 1 ORDER BY Title ";
            var cmd = new SqliteCommand(str, con);

            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                int counter = 0;
                while (read.Read())
                {
                    products_storage = new List<Storage_User_Control>()
                    {
                        new Storage_User_Control()
                        {
                            //ID =read["id"].ToString(),
                            Name = read["Title"].ToString(),
                            Storage = read["Storage_Num"].ToString(),
                           /*// Price = read["product_price"].ToString() + " " + "Km",
                            Amount=read["Amount"].ToString() + " " + "kom.",*/
                            Color= Color.FromArgb(189, 213, 234)
                        }
                    };

                    foreach (Storage_User_Control item in products_storage)
                    {
                        flowLayoutPanel4.Controls.Add(item);
                        item.Click += new System.EventHandler(this.Storage_User_Control_Click);
                    }

                    counter++;

                    if (counter > 12)
                    {
                        flowLayoutPanel4.AutoScroll = false;
                        flowLayoutPanel4.HorizontalScroll.Enabled = false;
                        flowLayoutPanel4.HorizontalScroll.Visible = false;
                        flowLayoutPanel4.HorizontalScroll.Maximum = 0;
                        flowLayoutPanel4.AutoScroll = true;
                    }

                }
            }
        }

        private void btn_Click1(object sender, EventArgs e)
        {
            skladiste1 = "2";

            flowLayoutPanel4.Controls.Clear();
            btn_back_Click(sender, e);
            //((Control)sender).BackColor = Color.Red;
            flowLayoutPanel4.Visible = true;
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel5.Visible = false;
            button1.Visible = false;
            btn_back.Visible = true;
            btn_back1.Visible = false;
            button2.Visible = true;

            this.Text = "Skladište automata za cigarete 2";

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT DISTINCT Title,Storage_Num FROM Product_storage WHERE Storage_Num = 2 ORDER BY Title ";
            var cmd = new SqliteCommand(str, con);

            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                int counter = 0;
                while (read.Read())
                {
                    products_storage = new List<Storage_User_Control>()
                    {
                        new Storage_User_Control()
                        {
                            //ID =read["id"].ToString(),
                            Name = read["Title"].ToString(),
                            Storage = read["Storage_Num"].ToString(),
                           /*// Price = read["product_price"].ToString() + " " + "Km",
                            Amount=read["Amount"].ToString() + " " + "kom.",*/
                            Color= Color.FromArgb(189, 213, 234)
                        }
                    };

                    foreach (Storage_User_Control item in products_storage)
                    {
                        flowLayoutPanel4.Controls.Add(item);
                        item.Click += new System.EventHandler(this.Storage_User_Control_Click);
                    }

                    counter++;

                    if (counter > 12)
                    {
                        flowLayoutPanel4.AutoScroll = false;
                        flowLayoutPanel4.HorizontalScroll.Enabled = false;
                        flowLayoutPanel4.HorizontalScroll.Visible = false;
                        flowLayoutPanel4.HorizontalScroll.Maximum = 0;
                        flowLayoutPanel4.AutoScroll = true;
                    }

                }
            }
        }
        private void btn_Click2(object sender, EventArgs e)
        {
            skladiste1 = "3";

            flowLayoutPanel4.Controls.Clear();
            btn_back_Click(sender, e);
            //((Control)sender).BackColor = Color.Red;
            flowLayoutPanel4.Visible = true;
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel5.Visible = false;
            button1.Visible = false;
            btn_back.Visible = true;
            btn_back1.Visible = false;
            button2.Visible = true;

            this.Text = "Skladište automata za cigarete 3";

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT DISTINCT Title,Storage_Num FROM Product_storage WHERE Storage_Num = 3 ORDER BY Title ";
            var cmd = new SqliteCommand(str, con);

            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                int counter = 0;
                while (read.Read())
                {
                    products_storage = new List<Storage_User_Control>()
                    {
                        new Storage_User_Control()
                        {
                            //ID =read["id"].ToString(),
                            Name = read["Title"].ToString(),
                            Storage = read["Storage_Num"].ToString(),
                           /*// Price = read["product_price"].ToString() + " " + "Km",
                            Amount=read["Amount"].ToString() + " " + "kom.",*/
                            Color= Color.FromArgb(189, 213, 234)
                        }
                        
                    };

                    foreach (Storage_User_Control item in products_storage)
                    {
                        flowLayoutPanel4.Controls.Add(item);
                        item.Click += new System.EventHandler(this.Storage_User_Control_Click);
                    }

                    counter++;

                    if (counter > 12)
                    {
                        flowLayoutPanel4.AutoScroll = false;
                        flowLayoutPanel4.HorizontalScroll.Enabled = false;
                        flowLayoutPanel4.HorizontalScroll.Visible = false;
                        flowLayoutPanel4.HorizontalScroll.Maximum = 0;
                        flowLayoutPanel4.AutoScroll = true;
                    }

                }
            }
        }
        private void btn_Click3(object sender, EventArgs e)
        {
            skladiste1 = "4";

            flowLayoutPanel4.Controls.Clear();
            btn_back_Click(sender, e);
            //((Control)sender).BackColor = Color.Red;
            flowLayoutPanel4.Visible = true;
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel5.Visible = false;
            button1.Visible = false;
            btn_back.Visible = true;
            btn_back1.Visible = false;
            button2.Visible = true;

            this.Text = "Skladište automata za cigarete 4";

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT DISTINCT Title,Storage_Num FROM Product_storage WHERE Storage_Num = 4 ORDER BY Title ";
            var cmd = new SqliteCommand(str, con);

            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                int counter = 0;
                while (read.Read())
                {
                    products_storage = new List<Storage_User_Control>()
                    {
                        new Storage_User_Control()
                        {
                            //ID =read["id"].ToString(),
                            Name = read["Title"].ToString(),
                            Storage = read["Storage_Num"].ToString(),
                           /*// Price = read["product_price"].ToString() + " " + "Km",
                            Amount=read["Amount"].ToString() + " " + "kom.",*/
                            Color= Color.FromArgb(189, 213, 234)
                        }

                    };

                    foreach (Storage_User_Control item in products_storage)
                    {
                        flowLayoutPanel4.Controls.Add(item);
                        item.Click += new System.EventHandler(this.Storage_User_Control_Click);
                    }

                    counter++;

                    if (counter > 12)
                    {
                        flowLayoutPanel4.AutoScroll = false;
                        flowLayoutPanel4.HorizontalScroll.Enabled = false;
                        flowLayoutPanel4.HorizontalScroll.Visible = false;
                        flowLayoutPanel4.HorizontalScroll.Maximum = 0;
                        flowLayoutPanel4.AutoScroll = true;
                    }

                }
            }
        }
        private void btn_Click4(object sender, EventArgs e)
        {
            skladiste1 = "5";

            flowLayoutPanel4.Controls.Clear();
            btn_back_Click(sender, e);
            //((Control)sender).BackColor = Color.Red;
            flowLayoutPanel4.Visible = true;
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel5.Visible = false;
            button1.Visible = false;
            btn_back.Visible = true;
            btn_back1.Visible = false;
            button2.Visible = true;

            this.Text = "Skladište automata za cigarete 5";

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT DISTINCT Title,Storage_Num FROM Product_storage WHERE Storage_Num = 5 ORDER BY Title ";
            var cmd = new SqliteCommand(str, con);

            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                int counter = 0;
                while (read.Read())
                {
                    products_storage = new List<Storage_User_Control>()
                    {
                        new Storage_User_Control()
                        {
                            //ID =read["id"].ToString(),
                            Name = read["Title"].ToString(),
                            Storage = read["Storage_Num"].ToString(),
                           /*// Price = read["product_price"].ToString() + " " + "Km",
                            Amount=read["Amount"].ToString() + " " + "kom.",*/
                            Color= Color.FromArgb(189, 213, 234)
                        }

                    };

                    foreach (Storage_User_Control item in products_storage)
                    {
                        flowLayoutPanel4.Controls.Add(item);
                        item.Click += new System.EventHandler(this.Storage_User_Control_Click);
                    }

                    counter++;

                    if (counter > 12)
                    {
                        flowLayoutPanel4.AutoScroll = false;
                        flowLayoutPanel4.HorizontalScroll.Enabled = false;
                        flowLayoutPanel4.HorizontalScroll.Visible = false;
                        flowLayoutPanel4.HorizontalScroll.Maximum = 0;
                        flowLayoutPanel4.AutoScroll = true;
                    }

                }
            }
        }
        private void btn_Click5(object sender, EventArgs e)
        {
            skladiste1 = "6";

            flowLayoutPanel4.Controls.Clear();
            btn_back_Click(sender, e);
            //((Control)sender).BackColor = Color.Red;
            flowLayoutPanel4.Visible = true;
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel5.Visible = false;
            button1.Visible = false;
            btn_back.Visible = true;
            btn_back1.Visible = false;
            button2.Visible = true;

            this.Text = "Skladište automata za cigarete 6";

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT DISTINCT Title,Storage_Num FROM Product_storage WHERE Storage_Num = 6 ORDER BY Title ";
            var cmd = new SqliteCommand(str, con);

            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                int counter = 0;
                while (read.Read())
                {
                    products_storage = new List<Storage_User_Control>()
                    {
                        new Storage_User_Control()
                        {
                            //ID =read["id"].ToString(),
                            Name = read["Title"].ToString(),
                            Storage = read["Storage_Num"].ToString(),
                           /*// Price = read["product_price"].ToString() + " " + "Km",
                            Amount=read["Amount"].ToString() + " " + "kom.",*/
                            Color= Color.FromArgb(189, 213, 234)
                        }

                    };

                    foreach (Storage_User_Control item in products_storage)
                    {
                        flowLayoutPanel4.Controls.Add(item);
                        item.Click += new System.EventHandler(this.Storage_User_Control_Click);
                    }

                    counter++;

                    if (counter > 12)
                    {
                        flowLayoutPanel4.AutoScroll = false;
                        flowLayoutPanel4.HorizontalScroll.Enabled = false;
                        flowLayoutPanel4.HorizontalScroll.Visible = false;
                        flowLayoutPanel4.HorizontalScroll.Maximum = 0;
                        flowLayoutPanel4.AutoScroll = true;
                    }

                }
            }
        }
        private void btn_Click6(object sender, EventArgs e)
        {
            skladiste1 = "7";

            flowLayoutPanel4.Controls.Clear();
            btn_back_Click(sender, e);
            //((Control)sender).BackColor = Color.Red;
            flowLayoutPanel4.Visible = true;
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel5.Visible = false;
            button1.Visible = false;
            btn_back.Visible = true;
            btn_back1.Visible = false;
            button2.Visible = true;

            this.Text = "Skladište automata za cigarete 7";

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT DISTINCT Title,Storage_Num FROM Product_storage WHERE Storage_Num = 7 ORDER BY Title ";
            var cmd = new SqliteCommand(str, con);

            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                int counter = 0;
                while (read.Read())
                {
                    products_storage = new List<Storage_User_Control>()
                    {
                        new Storage_User_Control()
                        {
                            //ID =read["id"].ToString(),
                            Name = read["Title"].ToString(),
                            Storage = read["Storage_Num"].ToString(),
                           /*// Price = read["product_price"].ToString() + " " + "Km",
                            Amount=read["Amount"].ToString() + " " + "kom.",*/
                            Color= Color.FromArgb(189, 213, 234)
                        }

                    };

                    foreach (Storage_User_Control item in products_storage)
                    {
                        flowLayoutPanel4.Controls.Add(item);
                        item.Click += new System.EventHandler(this.Storage_User_Control_Click);
                    }

                    counter++;

                    if (counter > 12)
                    {
                        flowLayoutPanel4.AutoScroll = false;
                        flowLayoutPanel4.HorizontalScroll.Enabled = false;
                        flowLayoutPanel4.HorizontalScroll.Visible = false;
                        flowLayoutPanel4.HorizontalScroll.Maximum = 0;
                        flowLayoutPanel4.AutoScroll = true;
                    }

                }
            }
        }
        private void btn_Click7(object sender, EventArgs e)
        {
            skladiste1 = "8";

            flowLayoutPanel4.Controls.Clear();
            btn_back_Click(sender, e);
            //((Control)sender).BackColor = Color.Red;
            flowLayoutPanel4.Visible = true;
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel5.Visible = false;
            button1.Visible = false;
            btn_back.Visible = true;
            btn_back1.Visible = false;
            button2.Visible = true;

            this.Text = "Skladište automata za cigarete 8";

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT DISTINCT Title,Storage_Num FROM Product_storage WHERE Storage_Num = 8 ORDER BY Title ";
            var cmd = new SqliteCommand(str, con);

            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                int counter = 0;
                while (read.Read())
                {
                    products_storage = new List<Storage_User_Control>()
                    {
                        new Storage_User_Control()
                        {
                            //ID =read["id"].ToString(),
                            Name = read["Title"].ToString(),
                            Storage = read["Storage_Num"].ToString(),
                           /*// Price = read["product_price"].ToString() + " " + "Km",
                            Amount=read["Amount"].ToString() + " " + "kom.",*/
                            Color= Color.FromArgb(189, 213, 234)
                        }

                    };

                    foreach (Storage_User_Control item in products_storage)
                    {
                        flowLayoutPanel4.Controls.Add(item);
                        item.Click += new System.EventHandler(this.Storage_User_Control_Click);
                    }

                    counter++;

                    if (counter > 12)
                    {
                        flowLayoutPanel4.AutoScroll = false;
                        flowLayoutPanel4.HorizontalScroll.Enabled = false;
                        flowLayoutPanel4.HorizontalScroll.Visible = false;
                        flowLayoutPanel4.HorizontalScroll.Maximum = 0;
                        flowLayoutPanel4.AutoScroll = true;
                    }

                }
            }
        }
        private void btn_Click8(object sender, EventArgs e)
        {
            skladiste1 = "9";

            flowLayoutPanel4.Controls.Clear();
            btn_back_Click(sender, e);
            //((Control)sender).BackColor = Color.Red;
            flowLayoutPanel4.Visible = true;
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel5.Visible = false;
            button1.Visible = false;
            btn_back.Visible = true;
            btn_back1.Visible = false;
            button2.Visible = true;

            this.Text = "Skladište automata za cigarete 9";

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT DISTINCT Title,Storage_Num FROM Product_storage WHERE Storage_Num = 9 ORDER BY Title ";
            var cmd = new SqliteCommand(str, con);

            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                int counter = 0;
                while (read.Read())
                {
                    products_storage = new List<Storage_User_Control>()
                    {
                        new Storage_User_Control()
                        {
                            //ID =read["id"].ToString(),
                            Name = read["Title"].ToString(),
                            Storage = read["Storage_Num"].ToString(),
                           /*// Price = read["product_price"].ToString() + " " + "Km",
                            Amount=read["Amount"].ToString() + " " + "kom.",*/
                            Color= Color.FromArgb(189, 213, 234)
                        }

                    };

                    foreach (Storage_User_Control item in products_storage)
                    {
                        flowLayoutPanel4.Controls.Add(item);
                        item.Click += new System.EventHandler(this.Storage_User_Control_Click);
                    }

                    counter++;

                    if (counter > 12)
                    {
                        flowLayoutPanel4.AutoScroll = false;
                        flowLayoutPanel4.HorizontalScroll.Enabled = false;
                        flowLayoutPanel4.HorizontalScroll.Visible = false;
                        flowLayoutPanel4.HorizontalScroll.Maximum = 0;
                        flowLayoutPanel4.AutoScroll = true;
                    }

                }
            }
        }
        private void btn_Click9(object sender, EventArgs e)
        {
            skladiste1 = "10";

            flowLayoutPanel4.Controls.Clear();
            btn_back_Click(sender, e);
            //((Control)sender).BackColor = Color.Red;
            flowLayoutPanel4.Visible = true;
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel2.Visible = false;
            flowLayoutPanel5.Visible = false;
            button1.Visible = false;
            btn_back.Visible = true;
            btn_back1.Visible = false;
            button2.Visible = true;

            this.Text = "Skladište automata za cigarete 10";

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            string str = "SELECT DISTINCT Title,Storage_Num FROM Product_storage WHERE Storage_Num = 10 ORDER BY Title ";
            var cmd = new SqliteCommand(str, con);

            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                int counter = 0;
                while (read.Read())
                {
                    products_storage = new List<Storage_User_Control>()
                    {
                        new Storage_User_Control()
                        {
                            //ID =read["id"].ToString(),
                            Name = read["Title"].ToString(),
                            Storage = read["Storage_Num"].ToString(),
                           /*// Price = read["product_price"].ToString() + " " + "Km",
                            Amount=read["Amount"].ToString() + " " + "kom.",*/
                            Color= Color.FromArgb(189, 213, 234)
                        }

                    };

                    foreach (Storage_User_Control item in products_storage)
                    {
                        flowLayoutPanel4.Controls.Add(item);
                        item.Click += new System.EventHandler(this.Storage_User_Control_Click);
                    }

                    counter++;

                    if (counter > 12)
                    {
                        flowLayoutPanel4.AutoScroll = false;
                        flowLayoutPanel4.HorizontalScroll.Enabled = false;
                        flowLayoutPanel4.HorizontalScroll.Visible = false;
                        flowLayoutPanel4.HorizontalScroll.Maximum = 0;
                        flowLayoutPanel4.AutoScroll = true;
                    }

                }
            }
        }

        #endregion

        #region Open UserControl (Storage_User_Control_Click) and show all Products in Storage
        //Drugi klik da izvuče iz baze sve što se nalazi npr. u skladištu 1 i prosljeđuje na Storage_User_Control1_Click

        void Storage_User_Control_Click(object sender, EventArgs e)
        {
            Refresh();

            btn_back1.Visible = true;
            btn_back.Visible = false;
            flowLayoutPanel5.Controls.Clear();

            Storage_User_Control obj = (Storage_User_Control)sender;
            showpanel1();

            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            bool PanelVisible = false;

            con.Open();
            this.con = con;

           // MessageBox.Show(obj.Name.ToString());
           // MessageBox.Show(obj.Storage.ToString());

            string str = "SELECT * FROM Product_storage WHERE Title = '" + obj.Name.ToString() + "' AND Storage_Num = '" + obj.Storage.ToString() + "' AND Amount > 0 ";

            var cmd = new SqliteCommand(str, con);

            using (SqliteDataReader read = cmd.ExecuteReader())
            {
                int counter = 0;
                while (read.Read())
                {

                    products_storage = new List<Storage_User_Control>()
                    {

                    new Storage_User_Control()
                        {
                            //ID = read["id"].ToString(),
                            Name = read["Title"].ToString() +" "+ read["Subtitle"].ToString(),
                            Name1 = read["Title"].ToString(),
                            Storage = read["Storage_Num"].ToString(),
                            Price = read["Price"].ToString().Replace(",",".") + " " + "Km",
                            Amount=read["Amount"].ToString() + " " + "kom.",
                            ID=read["id"].ToString(),
                            //Barcode=read["Barcode"].ToString(),

                            Color = Color.FromArgb(247, 247, 255),
                            Color1 = Color.FromArgb( 254, 95, 85),
                            Color2 = Color.FromArgb(254, 95, 85),
                           // Color1= Color.FromArgb(2, 1, 34),
                           // Color2 = Color.FromArgb(38, 84, 124),
                        }
            };

                    foreach (Storage_User_Control item in products_storage)
                    {
                        flowLayoutPanel5.Controls.Add(item);
                        //MessageBox.Show(read["product_subtitle"].ToString());
                        item.Click += new System.EventHandler(this.Storage_User_Control1_Click);
                    }
                    counter++;

                    if (counter > 12)
                    {
                        PanelVisible = true;
                    }
                }

                if (PanelVisible)
                {
                    flowLayoutPanel5.AutoScroll = true;
                }

            }
        }
        #endregion

        #region Open UserControl (Storage_User_Control) and Execute Cigarete

        public static string cigarete;
        public static string kolicina;
        public static string skladiste;
        public static string ID;
        public static string Price;
        void Storage_User_Control1_Click(object sender, EventArgs e)
        {
            Refresh();
            Storage_User_Control obj = (Storage_User_Control)sender;
            cigarete = obj.Name;
            kolicina = obj.Amount;
            skladiste = obj.Storage;
            ID = obj.ID;
            Price = obj.Price;

            Izbacivanje_cigareta_iz_skladišta frm = new Izbacivanje_cigareta_iz_skladišta();
            frm.ShowDialog();
        }
        #endregion

        #region Enable Form Pregled stanja skladista with button2

        private void button2_Click(object sender, EventArgs e)
        {
            Pregled_stanja_skladiste frm = new Pregled_stanja_skladiste();
            frm.ShowDialog();
        }

        #endregion
    }
}
