using System;
using System.Drawing;
using System.Windows.Forms;

namespace Automat
{
    public partial class ButtonDynamics : UserControl
    {
        public ButtonDynamics()
        {
            InitializeComponent();
            WireAllControls(this);

            lbl_Name1.Hide();
            lbl_barcode.Hide();
            lbl_id.Hide();
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

        public string Name1
        {
            get => lbl_Name1.Text;
            set => lbl_Name1.Text = value;
        }

        public string Name
        {
            get => lbl_Name.Text;
            set => lbl_Name.Text = value;   
        }

        public string Price
        {
            get => lbl_Price.Text;
            set => lbl_Price.Text = value;
        }
        public string Amount
        {
            get => lbl_amount.Text;
            set => lbl_amount.Text = value;
        }
        public string CMD
        {
            get => lbl_cmd.Text;
            set => lbl_cmd.Text = value;
        }
        public string Image1
        {
            get => pictureBox1.Text;
            set => pictureBox1.Text = value;
        }
        public string Barcode
        {
            get => lbl_barcode.Text;
            set => lbl_barcode.Text = value;
        }
        public string Box
        {
            get => lbl_box_number.Text;
            set => lbl_box_number.Text = value;
        }

        public Color Color
        {
            get => panel1.BackColor;
            set => panel1.BackColor = value;
        }
    }
}
