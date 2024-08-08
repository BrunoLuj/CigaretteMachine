using Microsoft.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Automat
{
    public partial class Pregled_prodaje : Form
    {
        SqliteConnection con;

        public Pregled_prodaje()
        {
            InitializeComponent();
        }

        private void Pregled_prodaje_Load(object sender, EventArgs e)
        {
           //set up format
            string DateTimeFormat = "yyyy-MM-dd";

            //start Date
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = DateTimeFormat;
            dateTimePicker1.Value = DateTime.Now;

            //end Date
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = DateTimeFormat;
            dateTimePicker2.Value = DateTime.Now;

            TotalIncome();

            LoadChart();
        }

        #region TotalIncome
        void TotalIncome()
        {
            string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
            var con = new SqliteConnection(cs);

            con.Open();
            this.con = con;

            //Ukupno prodano u automatu
            string str1 = "SELECT SUM (product_price) FROM Logs ";

            var cmd1 = new SqliteCommand(str1, con);

            Total_income.Text = cmd1.ExecuteScalar().ToString() + " " + "Km";
           
            //Ukupna količina prodanih cigareta u automatu
            string str3 = "SELECT SUM (product_amount) FROM Logs ";

            var cmd3 = new SqliteCommand(str3, con);

            Sell_amount.Text = cmd3.ExecuteScalar().ToString() + " " + "kom.";


            //Ukupnp stanje automata (količina)
            string str2 = "SELECT SUM (product_amount) FROM Products ";

            var cmd2 = new SqliteCommand(str2, con);

            Ukupno_stanje.Text = cmd2.ExecuteScalar().ToString() + " " + "kom.";

            //Ukupnp stanje automata (cijena)
            string str4 = "SELECT SUM (product_price * product_amount) FROM Products ";

            var cmd4 = new SqliteCommand(str4, con);

            stanjeautomata.Text = cmd4.ExecuteScalar().ToString() + " " + "Km";

            //label text change
            label14.Text = "Stanje automata";
            label11.Text = "Stanje automata";
        }

        #endregion

        #region PopulateListView
        private void GetDataToListView()
        {
            if (checkBox1.Checked)
            {
                listView1.Clear();
                //Skladište
                listView1.View = View.Details;

                listView1.Columns.Add("ID", 40, HorizontalAlignment.Left);
                listView1.Columns.Add("Naziv", 150, HorizontalAlignment.Left);
                listView1.Columns.Add("Cijena", 60, HorizontalAlignment.Left);
                listView1.Columns.Add("Količina", 70, HorizontalAlignment.Left);
                listView1.Columns.Add("Skladište", 70, HorizontalAlignment.Left);
                listView1.Columns.Add("Datum", 90, HorizontalAlignment.Left);
                listView1.Columns.Add("Vrijeme", 70, HorizontalAlignment.Left);
                //Skladište


                string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                var con = new SqliteConnection(cs);

                con.Open();
                this.con = con;

                string str = "SELECT ID, Title, Price, Amount, Storage, Date, Time FROM Logs_storage WHERE Date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' ORDER BY Date ASC ";

                var cmd = new SqliteCommand(str, con);

                string str1 = "SELECT SUM (Price * Amount) FROM Logs_storage WHERE Date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' ";

                var cmd1 = new SqliteCommand(str1, con);

                Sum.Text = cmd1.ExecuteScalar().ToString();

                string str2 = "SELECT SUM (Amount) FROM Logs_storage WHERE Date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' ";

                var cmd2 = new SqliteCommand(str2, con);

                Sum_amount.Text = cmd2.ExecuteScalar().ToString();

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
                       // lv.SubItems.Add(read.GetString(7));
                       // lv.SubItems.Add(read.GetString(8));

                        listView1.Items.Add(lv);

                    }
                }

                //Load Chart between date
                try
                {

                    string str4 = "SELECT Title, SUM(Amount) AS Amount FROM Logs_Storage WHERE Date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' GROUP BY ID ORDER BY SUM(Amount) DESC LIMIT 5";

                    var cmd4 = new SqliteCommand(str4, con);

                    using (SqliteDataReader read = cmd4.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        // chart1.Series["columns"].Points.Clear();

                        dt.Load(read);

                        string[] x = new string[dt.Rows.Count];
                        int[] y = new int[dt.Rows.Count];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            x[i] = dt.Rows[i][0].ToString();
                            y[i] = Convert.ToInt32(dt.Rows[i][1].ToString());

                        }

                        chart1.Series[0].Points.DataBindXY(x, y);
                        chart1.Series[0].ChartType = SeriesChartType.Pie;
                        chart1.Legends[0].Enabled = true;

                    }
                    con.Close();

                }

                catch (Exception ex) { MessageBox.Show(ex.Message); }

                con.Close();

            }
            else
            {
                //Automat
                listView1.Clear();

                listView1.View = View.Details;

                listView1.Columns.Add("ID", 30, HorizontalAlignment.Left);
                listView1.Columns.Add("Naziv", 100, HorizontalAlignment.Left);
                listView1.Columns.Add("Podnaziv", 120, HorizontalAlignment.Left);
                listView1.Columns.Add("Barcode", 100, HorizontalAlignment.Left);
                listView1.Columns.Add("Box", 40, HorizontalAlignment.Left);
                listView1.Columns.Add("Cijena", 50, HorizontalAlignment.Left);
                listView1.Columns.Add("Količina", 60, HorizontalAlignment.Left);
                listView1.Columns.Add("Datum", 80, HorizontalAlignment.Left);
                listView1.Columns.Add("Vrijeme", 60, HorizontalAlignment.Left);

                //Automat

                /* var dateAndTime1 = dateTimePicker1.Value;
                var date1 = dateAndTime1.ToShortDateString();

                var dateAndTime2 = dateTimePicker2.Value;
                var date2 = dateAndTime2.ToShortDateString();*/

                string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                var con = new SqliteConnection(cs);

                con.Open();
                this.con = con;

                string str = "SELECT product_id, product_title, product_subtitle, product_barcode, product_cmd, product_price, product_amount, product_date, product_time FROM Logs WHERE product_date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' ORDER BY product_date ASC ";

                var cmd = new SqliteCommand(str, con);
                /*cmd.Parameters.Add("@s_Date", SqliteType.Text).Value = date1;
                cmd.Parameters.Add("@e_Date", SqliteType.Text).Value = date2;

                MessageBox.Show(date1.ToString());
                MessageBox.Show(date2.ToString());*/

                string str1 = "SELECT SUM (product_price * product_amount) FROM Logs WHERE product_date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' ";

                var cmd1 = new SqliteCommand(str1, con);

                Sum.Text = cmd1.ExecuteScalar().ToString();

                string str2 = "SELECT SUM (product_amount) FROM Logs WHERE product_date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' ";

                var cmd2 = new SqliteCommand(str2, con);

                Sum_amount.Text = cmd2.ExecuteScalar().ToString();

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
                        lv.SubItems.Add(read.GetString(8));

                        listView1.Items.Add(lv);

                    }
                }

                //Load Chart between date

                try
                {
                    // con.Open();
                    this.con = con;

                    string str4 = "SELECT product_subtitle, SUM(product_amount) AS product_amount FROM Logs WHERE product_date BETWEEN '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "' AND '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' GROUP BY product_barcode ORDER BY SUM(product_amount) DESC LIMIT 5";

                    var cmd4 = new SqliteCommand(str4, con);

                    using (SqliteDataReader read = cmd4.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        // chart1.Series["columns"].Points.Clear();

                        dt.Load(read);

                        string[] x = new string[dt.Rows.Count];
                        int[] y = new int[dt.Rows.Count];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            x[i] = dt.Rows[i][0].ToString();
                            y[i] = Convert.ToInt32(dt.Rows[i][1].ToString());

                        }

                        chart1.Series[0].Points.DataBindXY(x, y);
                        chart1.Series[0].ChartType = SeriesChartType.Pie;
                        chart1.Legends[0].Enabled = true;

                    }
                    con.Close();
                }

                catch (Exception ex) { MessageBox.Show(ex.Message); }
                
                con.Close();
            }
          
        }
        #endregion

        #region Button Pretraga
        private void Pretraga_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            GetDataToListView();
        }

        #endregion

        #region Button Generirati_Izvjesce
        private void Generirati_izvjesce_Click(object sender, EventArgs e)
        {

            //Automat
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(1);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Worksheets[1];

            //Color HEADER
            Microsoft.Office.Interop.Excel.Range headerRange1 = ws.get_Range("A8", "I8");
            headerRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            headerRange1.Font.Bold = true;
            //headerRange1.ColumnWidth = 20;
            headerRange1.Borders.Color = System.Drawing.Color.Red;
            //headerRange1.Interior.Color = 1;

            //Color HEADER 2
            Microsoft.Office.Interop.Excel.Range headerRange2 = ws.get_Range("A3", "C6");
            headerRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            headerRange2.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            headerRange2.Font.Bold = true;
            headerRange2.Merge();
            headerRange2.Interior.Color = System.Drawing.Color.FromArgb(220, 220, 220);
            headerRange2.WrapText = true;

            headerRange2.Value = "Izvješće o prodaji za period:" + "  " + dateTimePicker1.Value.ToString("dd.MM.yyyy.") + " - " + dateTimePicker2.Value.ToString("dd.MM.yyyy.");
            //headerRange1.ColumnWidth = 20;
            headerRange2.Borders.Color = System.Drawing.Color.Black;
            //headerRange1.Interior.Color = 1;

            //Color HEADER 2
            Microsoft.Office.Interop.Excel.Range headerRange3 = ws.get_Range("D3", "I6");
            headerRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            headerRange3.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            headerRange3.Font.Bold = true;
            headerRange3.Merge();
            headerRange3.Interior.Color = System.Drawing.Color.FromArgb(220, 220, 220);
            headerRange3.WrapText = true;

            headerRange3.Value = "";
            //headerRange1.ColumnWidth = 20;
            headerRange3.Borders.Color = System.Drawing.Color.Black;
            //headerRange1.Interior.Color = 1;

            int i = 1;
            int i2 = 10;
            int x = 1;
            int x2 = 8;
            int j = 0;
            int colNum = listView1.Columns.Count;

            // Set first ROW as Column Headers Text
            foreach (ColumnHeader ch in listView1.Columns)
            {
                ws.Cells[x2, x] = ch.Text;
                x++;
            }

            foreach (ListViewItem lvi in listView1.Items)
            {
                i = 1;
                foreach (ListViewItem.ListViewSubItem lvs in lvi.SubItems)
                {
                    ws.Cells[i2, i] = lvs.Text;
                    i++;
                }
                i2++;

            }

            if (checkBox1.Checked)
            {
                //Color HEADER 5
                Microsoft.Office.Interop.Excel.Range headerRange5 = ws.get_Range("A1", "I2");
                headerRange5.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                headerRange5.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                headerRange5.Font.Bold = true;
                headerRange5.Merge();
                headerRange5.WrapText = true;
                headerRange5.Font.Size = 20;
                headerRange5.Interior.Color = System.Drawing.Color.Yellow;
                headerRange5.Borders.Color = System.Drawing.Color.Black;
                headerRange5.Value = "Skladište automata za cigarete";
                

                ws.Cells[i2 + 1, 1] = "Ukupno";

                ws.Cells[i2 + 1, 1].Borders.Color = System.Drawing.Color.Red;
                ws.Cells[i2 + 1, 2].Borders.Color = System.Drawing.Color.Red;

                ws.Cells[i2 + 1, 3].Borders.Color = System.Drawing.Color.Red;
                ws.Cells[i2 + 1, 3].Interior.Color = System.Drawing.Color.Yellow;

                ws.Cells[i2 + 1, 4].Borders.Color = System.Drawing.Color.Red;
                ws.Cells[i2 + 1, 4].Interior.Color = System.Drawing.Color.Yellow;

                ws.Cells[i2 + 1, 5].Borders.Color = System.Drawing.Color.Red;

                ws.Cells[i2 + 1, 6].Borders.Color = System.Drawing.Color.Red;
               // ws.Cells[i2 + 1, 6].Interior.Color = System.Drawing.Color.Yellow;

                ws.Cells[i2 + 1, 7].Borders.Color = System.Drawing.Color.Red;
              //  ws.Cells[i2 + 1, 7].Interior.Color = System.Drawing.Color.Yellow;

                ws.Cells[i2 + 1, 8].Borders.Color = System.Drawing.Color.Red;
                ws.Cells[i2 + 1, 9].Borders.Color = System.Drawing.Color.Red;

                ws.Cells[i2 + 1, 3] = Sum.Text + " " + label7.Text;

                ws.Cells[i2 + 1, 4] = Sum_amount.Text + " " + label8.Text;
            }
            else
            {
                //Color HEADER 5
                Microsoft.Office.Interop.Excel.Range headerRange5 = ws.get_Range("A1", "I2");
                headerRange5.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                headerRange5.VerticalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                headerRange5.Font.Bold = true;
                headerRange5.Merge();
                headerRange5.WrapText = true;
                headerRange5.Font.Size = 20;
                headerRange5.Interior.Color = System.Drawing.Color.Yellow;
                headerRange5.Borders.Color = System.Drawing.Color.Black;
                headerRange5.Value = "Automat za cigarete";
                

                ws.Cells[i2 + 1, 1] = "Ukupno";

                ws.Cells[i2 + 1, 1].Borders.Color = System.Drawing.Color.Red;
                ws.Cells[i2 + 1, 2].Borders.Color = System.Drawing.Color.Red;
                ws.Cells[i2 + 1, 3].Borders.Color = System.Drawing.Color.Red;
                ws.Cells[i2 + 1, 4].Borders.Color = System.Drawing.Color.Red;
                ws.Cells[i2 + 1, 5].Borders.Color = System.Drawing.Color.Red;

                ws.Cells[i2 + 1, 6].Borders.Color = System.Drawing.Color.Red;
                ws.Cells[i2 + 1, 6].Interior.Color = System.Drawing.Color.Yellow;

                ws.Cells[i2 + 1, 7].Borders.Color = System.Drawing.Color.Red;
                ws.Cells[i2 + 1, 7].Interior.Color = System.Drawing.Color.Yellow;

                ws.Cells[i2 + 1, 8].Borders.Color = System.Drawing.Color.Red;
                ws.Cells[i2 + 1, 9].Borders.Color = System.Drawing.Color.Red;

                ws.Cells[i2 + 1, 6] = Sum.Text + " "+ label7.Text;

                ws.Cells[i2 + 1, 7] = Sum_amount.Text + " " + label8.Text;
            }
            

            // AutoSet Cell Widths to Content Size
            ws.Cells.Select();
            ws.Cells.EntireColumn.AutoFit();

            MessageBox.Show("Export Completed", "Export to Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        #endregion

        #region CheckBox1 For Storage Counter
        private void checkBox1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                LoadChartStorage();

                string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                var con = new SqliteConnection(cs);

                con.Open();
                this.con = con;

                //Dio velikih labela na vrhu
                //Ukupno prodano u skladistu automata
                string str4 = "SELECT SUM (Price * Amount) FROM Logs_storage ";

                var cmd4 = new SqliteCommand(str4, con);

                Total_income.Text = cmd4.ExecuteScalar().ToString() + " " + "Km";

                //Ukupna količina prodanih cigareta
                string str6 = "SELECT SUM (Amount) FROM Logs_storage ";

                var cmd6 = new SqliteCommand(str6, con);

                Sell_amount.Text = cmd6.ExecuteScalar().ToString() + " " + "kom.";

                //Ukupno stanje skladista automata
                string str5 = "SELECT SUM (Amount) FROM Product_storage ";

                var cmd5 = new SqliteCommand(str5, con);

                Ukupno_stanje.Text = cmd5.ExecuteScalar().ToString() + " " + "kom.";

                //Ukupno stanje skladista automata
                string str7 = "SELECT SUM (Price * Amount) FROM Product_storage ";

                var cmd7 = new SqliteCommand(str7, con);

                stanjeautomata.Text = cmd7.ExecuteScalar().ToString() + " " + "Km";

                //label text change
                label14.Text = "Stanje skladišta";
                label11.Text = "Stanje skladišta";
            }
            else if (checkBox1.Checked == false)
            {
                LoadChart();

                TotalIncome();
            }
        }

        #endregion

        #region Enter Click
        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        #endregion

        #region Load Chart
        public void LoadChart()
        {
            try
            {
                string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                var con = new SqliteConnection(cs);

                con.Open();
                this.con = con;

                string str1 = "SELECT product_subtitle, SUM(product_amount) AS product_amount FROM Logs GROUP BY product_barcode ORDER BY SUM(product_amount) DESC LIMIT 5";

                var cmd1 = new SqliteCommand(str1, con);

                using (SqliteDataReader read = cmd1.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    // chart1.Series["columns"].Points.Clear();

                    dt.Load(read);

                    string[] x = new string[dt.Rows.Count];
                    int[] y = new int[dt.Rows.Count];

                    for (int i=0; i < dt.Rows.Count; i++)
                    {
                        x[i] = dt.Rows[i][0].ToString();
                        y[i] = Convert.ToInt32(dt.Rows[i][1].ToString());

                    }

                    chart1.Series[0].Points.DataBindXY(x, y);
                    chart1.Series[0].ChartType = SeriesChartType.Pie;
                    chart1.Legends[0].Enabled = true;

                }

                //Najbolje prodavan proizvod
                string str5 = "SELECT product_subtitle, SUM(product_amount) AS product_amount FROM Logs GROUP BY product_barcode ORDER BY SUM(product_amount) DESC LIMIT 1";

                var cmd5 = new SqliteCommand(str5, con);

                using (SqliteDataReader read = cmd5.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    // chart1.Series["columns"].Points.Clear();

                    dt.Load(read);

                    if (dt.Rows.Count > 0)
                    {
                        label10.Text = cmd5.ExecuteScalar().ToString();
                    }
                    else
                    {
                        label10.Text = "";
                    }

                }

                con.Close();

            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        #endregion

        #region Load Chart Storage
        public void LoadChartStorage()
        {
            try
            {
                string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                var con = new SqliteConnection(cs);

                con.Open();
                this.con = con;

                string str1 = "SELECT Title, SUM(Amount) AS Amount FROM Logs_Storage GROUP BY ID ORDER BY SUM(Amount) DESC LIMIT 5";
                
                var cmd1 = new SqliteCommand(str1, con);

                using (SqliteDataReader read = cmd1.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    // chart1.Series["columns"].Points.Clear();

                    dt.Load(read);

                    string[] x = new string[dt.Rows.Count];
                    int[] y = new int[dt.Rows.Count];

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        x[i] = dt.Rows[i][0].ToString();
                        y[i] = Convert.ToInt32(dt.Rows[i][1].ToString());

                    }

                    chart1.Series[0].Points.DataBindXY(x, y);
                    chart1.Series[0].ChartType = SeriesChartType.Pie;
                    chart1.Legends[0].Enabled = true;

                }

                //Najbolje prodavan proizvod
                string str2 = "SELECT Title, SUM(Amount) AS Amount FROM Logs_Storage GROUP BY ID ORDER BY SUM(Amount) DESC LIMIT 1";

                var cmd2 = new SqliteCommand(str2, con);

                using (SqliteDataReader read = cmd2.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    // chart1.Series["columns"].Points.Clear();

                    dt.Load(read);

                    if(dt.Rows.Count > 0 )
                    {
                        label10.Text = cmd2.ExecuteScalar().ToString();
                    }
                    else
                    {
                        label10.Text = "";
                    }
                    
                }
                con.Close();

            }

            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        #endregion

        #region Best Sell Product (Button 10, 15, 20, 25)
        private void najprodavanijih_5_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                try
                {
                    string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                    var con = new SqliteConnection(cs);

                    con.Open();
                    this.con = con;

                    string str1 = "SELECT Title, SUM(Amount) AS Amount FROM Logs_Storage GROUP BY ID ORDER BY SUM(Amount) DESC LIMIT 5";

                    var cmd1 = new SqliteCommand(str1, con);

                    using (SqliteDataReader read = cmd1.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        // chart1.Series["columns"].Points.Clear();

                        dt.Load(read);

                        string[] x = new string[dt.Rows.Count];
                        int[] y = new int[dt.Rows.Count];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            x[i] = dt.Rows[i][0].ToString();
                            y[i] = Convert.ToInt32(dt.Rows[i][1].ToString());

                        }

                        chart1.Series[0].Points.DataBindXY(x, y);
                        chart1.Series[0].ChartType = SeriesChartType.Pie;
                        chart1.Legends[0].Enabled = true;

                    }
                    con.Close();

                }

                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                try
                {
                    string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                    var con = new SqliteConnection(cs);

                    con.Open();
                    this.con = con;

                    string str1 = "SELECT product_subtitle, SUM(product_amount) AS product_amount FROM Logs GROUP BY product_barcode ORDER BY SUM(product_amount) DESC LIMIT 5";

                    var cmd1 = new SqliteCommand(str1, con);

                    using (SqliteDataReader read = cmd1.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        // chart1.Series["columns"].Points.Clear();

                        dt.Load(read);

                        string[] x = new string[dt.Rows.Count];
                        int[] y = new int[dt.Rows.Count];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            x[i] = dt.Rows[i][0].ToString();
                            y[i] = Convert.ToInt32(dt.Rows[i][1].ToString());

                        }

                        chart1.Series[0].Points.DataBindXY(x, y);
                        chart1.Series[0].ChartType = SeriesChartType.Pie;
                        chart1.Legends[0].Enabled = true;

                    }
                    con.Close();

                }

                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        private void najprodavanijih_10_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                try
                {
                    string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                    var con = new SqliteConnection(cs);

                    con.Open();
                    this.con = con;

                    string str1 = "SELECT Title, SUM(Amount) AS Amount FROM Logs_Storage GROUP BY ID ORDER BY SUM(Amount) DESC LIMIT 10";

                    var cmd1 = new SqliteCommand(str1, con);

                    using (SqliteDataReader read = cmd1.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        // chart1.Series["columns"].Points.Clear();

                        dt.Load(read);

                        string[] x = new string[dt.Rows.Count];
                        int[] y = new int[dt.Rows.Count];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            x[i] = dt.Rows[i][0].ToString();
                            y[i] = Convert.ToInt32(dt.Rows[i][1].ToString());

                        }

                        chart1.Series[0].Points.DataBindXY(x, y);
                        chart1.Series[0].ChartType = SeriesChartType.Pie;
                        chart1.Legends[0].Enabled = true;

                    }
                    con.Close();

                }

                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                try
                {
                    string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                    var con = new SqliteConnection(cs);

                    con.Open();
                    this.con = con;

                    string str1 = "SELECT product_subtitle, SUM(product_amount) AS product_amount FROM Logs GROUP BY product_barcode ORDER BY SUM(product_amount) DESC LIMIT 10";

                    var cmd1 = new SqliteCommand(str1, con);

                    using (SqliteDataReader read = cmd1.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        // chart1.Series["columns"].Points.Clear();

                        dt.Load(read);

                        string[] x = new string[dt.Rows.Count];
                        int[] y = new int[dt.Rows.Count];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            x[i] = dt.Rows[i][0].ToString();
                            y[i] = Convert.ToInt32(dt.Rows[i][1].ToString());

                        }

                        chart1.Series[0].Points.DataBindXY(x, y);
                        chart1.Series[0].ChartType = SeriesChartType.Pie;
                        chart1.Legends[0].Enabled = true;

                    }
                    con.Close();

                }

                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
                
        }

        private void najprodavanijih_15_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                try
                {
                    string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                    var con = new SqliteConnection(cs);

                    con.Open();
                    this.con = con;

                    string str1 = "SELECT Title, SUM(Amount) AS Amount FROM Logs_Storage GROUP BY ID ORDER BY SUM(Amount) DESC LIMIT 15";

                    var cmd1 = new SqliteCommand(str1, con);

                    using (SqliteDataReader read = cmd1.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        // chart1.Series["columns"].Points.Clear();

                        dt.Load(read);

                        string[] x = new string[dt.Rows.Count];
                        int[] y = new int[dt.Rows.Count];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            x[i] = dt.Rows[i][0].ToString();
                            y[i] = Convert.ToInt32(dt.Rows[i][1].ToString());

                        }

                        chart1.Series[0].Points.DataBindXY(x, y);
                        chart1.Series[0].ChartType = SeriesChartType.Pie;
                        chart1.Legends[0].Enabled = true;

                    }
                    con.Close();

                }

                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                try
                {
                    string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                    var con = new SqliteConnection(cs);

                    con.Open();
                    this.con = con;

                    string str1 = "SELECT product_subtitle, SUM(product_amount) AS product_amount FROM Logs GROUP BY product_barcode ORDER BY SUM(product_amount) DESC LIMIT 15";

                    var cmd1 = new SqliteCommand(str1, con);

                    using (SqliteDataReader read = cmd1.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        // chart1.Series["columns"].Points.Clear();

                        dt.Load(read);

                        string[] x = new string[dt.Rows.Count];
                        int[] y = new int[dt.Rows.Count];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            x[i] = dt.Rows[i][0].ToString();
                            y[i] = Convert.ToInt32(dt.Rows[i][1].ToString());

                        }

                        chart1.Series[0].Points.DataBindXY(x, y);
                        chart1.Series[0].ChartType = SeriesChartType.Pie;
                        chart1.Legends[0].Enabled = true;

                    }
                    con.Close();

                }

                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void najprodavanijih_20_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                try
                {
                    string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                    var con = new SqliteConnection(cs);

                    con.Open();
                    this.con = con;

                    string str1 = "SELECT Title, SUM(Amount) AS Amount FROM Logs_Storage GROUP BY ID ORDER BY SUM(Amount) DESC LIMIT 20";

                    var cmd1 = new SqliteCommand(str1, con);

                    using (SqliteDataReader read = cmd1.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        // chart1.Series["columns"].Points.Clear();

                        dt.Load(read);

                        string[] x = new string[dt.Rows.Count];
                        int[] y = new int[dt.Rows.Count];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            x[i] = dt.Rows[i][0].ToString();
                            y[i] = Convert.ToInt32(dt.Rows[i][1].ToString());

                        }

                        chart1.Series[0].Points.DataBindXY(x, y);
                        chart1.Series[0].ChartType = SeriesChartType.Pie;
                        chart1.Legends[0].Enabled = true;

                    }
                    con.Close();

                }

                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                try
                {
                    string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                    var con = new SqliteConnection(cs);

                    con.Open();
                    this.con = con;

                    string str1 = "SELECT product_subtitle, SUM(product_amount) AS product_amount FROM Logs GROUP BY product_barcode ORDER BY SUM(product_amount) DESC LIMIT 20";

                    var cmd1 = new SqliteCommand(str1, con);

                    using (SqliteDataReader read = cmd1.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        // chart1.Series["columns"].Points.Clear();

                        dt.Load(read);

                        string[] x = new string[dt.Rows.Count];
                        int[] y = new int[dt.Rows.Count];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            x[i] = dt.Rows[i][0].ToString();
                            y[i] = Convert.ToInt32(dt.Rows[i][1].ToString());

                        }

                        chart1.Series[0].Points.DataBindXY(x, y);
                        chart1.Series[0].ChartType = SeriesChartType.Pie;
                        chart1.Legends[0].Enabled = true;

                    }
                    con.Close();

                }

                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void najprodavanijih_25_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                try
                {
                    string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                    var con = new SqliteConnection(cs);

                    con.Open();
                    this.con = con;

                    string str1 = "SELECT Title, SUM(Amount) AS Amount FROM Logs_Storage GROUP BY ID ORDER BY SUM(Amount) DESC LIMIT 25";

                    var cmd1 = new SqliteCommand(str1, con);

                    using (SqliteDataReader read = cmd1.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        // chart1.Series["columns"].Points.Clear();

                        dt.Load(read);

                        string[] x = new string[dt.Rows.Count];
                        int[] y = new int[dt.Rows.Count];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            x[i] = dt.Rows[i][0].ToString();
                            y[i] = Convert.ToInt32(dt.Rows[i][1].ToString());

                        }

                        chart1.Series[0].Points.DataBindXY(x, y);
                        chart1.Series[0].ChartType = SeriesChartType.Pie;
                        chart1.Legends[0].Enabled = true;

                    }
                    con.Close();

                }

                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else
            {
                try
                {
                    string cs = "Data Source = C:/Automat/Automat/bin/Debug/SQLiteData.db";
                    var con = new SqliteConnection(cs);

                    con.Open();
                    this.con = con;

                    string str1 = "SELECT product_subtitle, SUM(product_amount) AS product_amount FROM Logs GROUP BY product_barcode ORDER BY SUM(product_amount) DESC LIMIT 25";

                    var cmd1 = new SqliteCommand(str1, con);

                    using (SqliteDataReader read = cmd1.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        // chart1.Series["columns"].Points.Clear();

                        dt.Load(read);

                        string[] x = new string[dt.Rows.Count];
                        int[] y = new int[dt.Rows.Count];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            x[i] = dt.Rows[i][0].ToString();
                            y[i] = Convert.ToInt32(dt.Rows[i][1].ToString());

                        }

                        chart1.Series[0].Points.DataBindXY(x, y);
                        chart1.Series[0].ChartType = SeriesChartType.Pie;
                        chart1.Legends[0].Enabled = true;

                    }
                    con.Close();

                }

                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            
        }
        #endregion
    }
}
