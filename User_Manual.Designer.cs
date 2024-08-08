namespace Automat
{
    partial class User_Manual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(User_Manual));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Ser_Num = new System.Windows.Forms.Label();
            this.User = new System.Windows.Forms.Label();
            this.Firma = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Serijski broj :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Korisnik :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Naziv objekta :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Serijski broj :";
            // 
            // Ser_Num
            // 
            this.Ser_Num.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Ser_Num.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Ser_Num.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Ser_Num.ForeColor = System.Drawing.Color.White;
            this.Ser_Num.Location = new System.Drawing.Point(112, 14);
            this.Ser_Num.Name = "Ser_Num";
            this.Ser_Num.Size = new System.Drawing.Size(256, 23);
            this.Ser_Num.TabIndex = 6;
            this.Ser_Num.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // User
            // 
            this.User.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.User.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.User.ForeColor = System.Drawing.Color.White;
            this.User.Location = new System.Drawing.Point(112, 45);
            this.User.Name = "User";
            this.User.Size = new System.Drawing.Size(256, 23);
            this.User.TabIndex = 6;
            this.User.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Firma
            // 
            this.Firma.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Firma.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Firma.ForeColor = System.Drawing.Color.White;
            this.Firma.Location = new System.Drawing.Point(112, 78);
            this.Firma.Name = "Firma";
            this.Firma.Size = new System.Drawing.Size(256, 23);
            this.Firma.TabIndex = 6;
            this.Firma.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // User_Manual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(88)))), ((int)(((byte)(103)))));
            this.ClientSize = new System.Drawing.Size(389, 127);
            this.Controls.Add(this.Firma);
            this.Controls.Add(this.User);
            this.Controls.Add(this.Ser_Num);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "User_Manual";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Korisničke informacije";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Ser_Num;
        private System.Windows.Forms.Label User;
        private System.Windows.Forms.Label Firma;
    }
}