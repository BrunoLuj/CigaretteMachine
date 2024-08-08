namespace Automat
{
    partial class Pregled_stanja
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pregled_stanja));
            this.listView1 = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.Stanje_u_automatu = new System.Windows.Forms.Label();
            this.btn_praznibox = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 55);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(400, 400);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listView1_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(120, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ukupno stanje cigareta u automatu :";
            // 
            // Stanje_u_automatu
            // 
            this.Stanje_u_automatu.AutoSize = true;
            this.Stanje_u_automatu.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Stanje_u_automatu.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Stanje_u_automatu.Location = new System.Drawing.Point(371, 34);
            this.Stanje_u_automatu.Name = "Stanje_u_automatu";
            this.Stanje_u_automatu.Size = new System.Drawing.Size(20, 18);
            this.Stanje_u_automatu.TabIndex = 3;
            this.Stanje_u_automatu.Text = "tu";
            // 
            // btn_praznibox
            // 
            this.btn_praznibox.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(117)))), ((int)(((byte)(134)))));
            this.btn_praznibox.FlatAppearance.BorderSize = 0;
            this.btn_praznibox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_praznibox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_praznibox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(117)))), ((int)(((byte)(134)))));
            this.btn_praznibox.Image = ((System.Drawing.Image)(resources.GetObject("btn_praznibox.Image")));
            this.btn_praznibox.Location = new System.Drawing.Point(12, 11);
            this.btn_praznibox.Margin = new System.Windows.Forms.Padding(2, 2, 4, 4);
            this.btn_praznibox.Name = "btn_praznibox";
            this.btn_praznibox.Size = new System.Drawing.Size(37, 37);
            this.btn_praznibox.TabIndex = 8;
            this.btn_praznibox.UseVisualStyleBackColor = true;
            this.btn_praznibox.Click += new System.EventHandler(this.btn_praznibox_Click);
            // 
            // btn_back
            // 
            this.btn_back.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(117)))), ((int)(((byte)(134)))));
            this.btn_back.FlatAppearance.BorderSize = 0;
            this.btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_back.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(117)))), ((int)(((byte)(134)))));
            this.btn_back.Image = ((System.Drawing.Image)(resources.GetObject("btn_back.Image")));
            this.btn_back.Location = new System.Drawing.Point(55, 14);
            this.btn_back.Margin = new System.Windows.Forms.Padding(2, 2, 4, 4);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(35, 30);
            this.btn_back.TabIndex = 12;
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // Pregled_stanja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(88)))), ((int)(((byte)(103)))));
            this.ClientSize = new System.Drawing.Size(425, 469);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_praznibox);
            this.Controls.Add(this.Stanje_u_automatu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Pregled_stanja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pregled stanja";
            this.Activated += new System.EventHandler(this.Pregled_stanja_Activated);
            this.Load += new System.EventHandler(this.Pregled_stanja_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Stanje_u_automatu;
        private System.Windows.Forms.Button btn_praznibox;
        private System.Windows.Forms.Button btn_back;
    }
}