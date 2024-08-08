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
    public partial class Storage_User_Control : UserControl
    {
        public Storage_User_Control()
        {
            InitializeComponent();
            WireAllControls(this);
            lbl_id.Hide();
            lbl_storage.Hide();
            lbl_barcode.Hide();
            lbl_price.Hide();
            lbl_Name1.Hide();
        }

        private void WireAllControls(Control cont)
        {
            foreach (Control ctl in cont.Controls)
            {
                ctl.Click += ctl_Click;
                if (ctl.HasChildren)
                {
                    WireAllControls(ctl);
                }
            }
        }
        private void ctl_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, EventArgs.Empty);
        }
        public string ID
        {
            get => lbl_id.Text;
            set => lbl_id.Text = value;
        }

        public string Name
        {
            get => lbl_Name.Text;
            set => lbl_Name.Text = value;
        }
        public string Name1
        {
            get => lbl_Name1.Text;
            set => lbl_Name1.Text = value;
        }
        public string Amount
        {
            get => lbl_amount.Text;
            set => lbl_amount.Text = value;
        }
        public string Storage
        {
            get => lbl_storage.Text;
            set => lbl_storage.Text = value;
        }
        public string Barcode
        {
            get => lbl_barcode.Text;
            set => lbl_barcode.Text = value;
        }
        public string Price
        {
            get => lbl_price.Text;
            set => lbl_price.Text = value;
        }
        public Color Color
        {
            get => panel1.BackColor;
            set => panel1.BackColor = value;
        }
        public Color Color1
        {
            get => panel2.BackColor;
            set => panel2.BackColor = value;
        }
        public Color Color2
        {
            get => panel3.BackColor;
            set => panel3.BackColor = value;
        }
    }
}
