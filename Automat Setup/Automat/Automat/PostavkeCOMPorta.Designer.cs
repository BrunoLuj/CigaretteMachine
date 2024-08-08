namespace Automat
{
    partial class PostavkeCOMPorta
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PostavkeCOMPorta));
            this.btn_COMPortSpremi = new System.Windows.Forms.Button();
            this.btn_refreshCOM = new System.Windows.Forms.Button();
            this.cmb_COMPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_CMP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_COMPortSpremi
            // 
            this.btn_COMPortSpremi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(95)))), ((int)(((byte)(85)))));
            this.btn_COMPortSpremi.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_COMPortSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(88)))), ((int)(((byte)(103)))));
            this.btn_COMPortSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_COMPortSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_COMPortSpremi.ForeColor = System.Drawing.Color.White;
            this.btn_COMPortSpremi.Location = new System.Drawing.Point(113, 170);
            this.btn_COMPortSpremi.Margin = new System.Windows.Forms.Padding(4);
            this.btn_COMPortSpremi.Name = "btn_COMPortSpremi";
            this.btn_COMPortSpremi.Size = new System.Drawing.Size(161, 34);
            this.btn_COMPortSpremi.TabIndex = 5;
            this.btn_COMPortSpremi.Text = "Spremi";
            this.btn_COMPortSpremi.UseVisualStyleBackColor = false;
            this.btn_COMPortSpremi.Click += new System.EventHandler(this.btn_COMPortSpremi_Click);
            // 
            // btn_refreshCOM
            // 
            this.btn_refreshCOM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(95)))), ((int)(((byte)(85)))));
            this.btn_refreshCOM.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_refreshCOM.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(88)))), ((int)(((byte)(103)))));
            this.btn_refreshCOM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_refreshCOM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_refreshCOM.ForeColor = System.Drawing.Color.White;
            this.btn_refreshCOM.Location = new System.Drawing.Point(113, 63);
            this.btn_refreshCOM.Margin = new System.Windows.Forms.Padding(4);
            this.btn_refreshCOM.Name = "btn_refreshCOM";
            this.btn_refreshCOM.Size = new System.Drawing.Size(161, 34);
            this.btn_refreshCOM.TabIndex = 6;
            this.btn_refreshCOM.TabStop = false;
            this.btn_refreshCOM.Text = "Refresh";
            this.btn_refreshCOM.UseVisualStyleBackColor = false;
            this.btn_refreshCOM.Click += new System.EventHandler(this.btn_refreshCOM_Click);
            // 
            // cmb_COMPort
            // 
            this.cmb_COMPort.FormattingEnabled = true;
            this.cmb_COMPort.Location = new System.Drawing.Point(113, 11);
            this.cmb_COMPort.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_COMPort.Name = "cmb_COMPort";
            this.cmb_COMPort.Size = new System.Drawing.Size(160, 24);
            this.cmb_COMPort.TabIndex = 4;
            this.cmb_COMPort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_COMPort_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(25, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "COM Port :";
            // 
            // lbl_CMP
            // 
            this.lbl_CMP.ForeColor = System.Drawing.Color.White;
            this.lbl_CMP.Location = new System.Drawing.Point(225, 127);
            this.lbl_CMP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_CMP.Name = "lbl_CMP";
            this.lbl_CMP.Size = new System.Drawing.Size(56, 27);
            this.lbl_CMP.TabIndex = 7;
            this.lbl_CMP.Text = "label2";
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(16, 127);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 28);
            this.label2.TabIndex = 8;
            this.label2.Text = "Trenutno odabrani COM Port je:";
            // 
            // PostavkeCOMPorta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(88)))), ((int)(((byte)(103)))));
            this.ClientSize = new System.Drawing.Size(291, 223);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_CMP);
            this.Controls.Add(this.btn_COMPortSpremi);
            this.Controls.Add(this.btn_refreshCOM);
            this.Controls.Add(this.cmb_COMPort);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PostavkeCOMPorta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COM Port";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PostavkeCOMPorta_FormClosing);
            this.Load += new System.EventHandler(this.PostavkeCOMPorta_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PostavkeCOMPorta_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_COMPortSpremi;
        private System.Windows.Forms.Button btn_refreshCOM;
        private System.Windows.Forms.ComboBox cmb_COMPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_CMP;
        private System.Windows.Forms.Label label2;
    }
}