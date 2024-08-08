namespace Automat
{
    partial class Pregled_stanja_skladiste
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pregled_stanja_skladiste));
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_praznibox = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Stanje_skladista = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_back
            // 
            this.btn_back.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(117)))), ((int)(((byte)(134)))));
            this.btn_back.FlatAppearance.BorderSize = 0;
            this.btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_back.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(117)))), ((int)(((byte)(134)))));
            this.btn_back.Image = ((System.Drawing.Image)(resources.GetObject("btn_back.Image")));
            this.btn_back.Location = new System.Drawing.Point(55, 12);
            this.btn_back.Margin = new System.Windows.Forms.Padding(2, 2, 4, 4);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(35, 30);
            this.btn_back.TabIndex = 17;
            this.btn_back.UseVisualStyleBackColor = true;
            // 
            // btn_praznibox
            // 
            this.btn_praznibox.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(117)))), ((int)(((byte)(134)))));
            this.btn_praznibox.FlatAppearance.BorderSize = 0;
            this.btn_praznibox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_praznibox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_praznibox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(117)))), ((int)(((byte)(134)))));
            this.btn_praznibox.Image = ((System.Drawing.Image)(resources.GetObject("btn_praznibox.Image")));
            this.btn_praznibox.Location = new System.Drawing.Point(12, 9);
            this.btn_praznibox.Margin = new System.Windows.Forms.Padding(2, 2, 4, 4);
            this.btn_praznibox.Name = "btn_praznibox";
            this.btn_praznibox.Size = new System.Drawing.Size(37, 37);
            this.btn_praznibox.TabIndex = 16;
            this.btn_praznibox.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 57);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(400, 400);
            this.listView1.TabIndex = 13;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listView1_KeyPress);
            // 
            // Stanje_skladista
            // 
            this.Stanje_skladista.AutoSize = true;
            this.Stanje_skladista.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Stanje_skladista.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Stanje_skladista.Location = new System.Drawing.Point(363, 36);
            this.Stanje_skladista.Name = "Stanje_skladista";
            this.Stanje_skladista.Size = new System.Drawing.Size(20, 18);
            this.Stanje_skladista.TabIndex = 15;
            this.Stanje_skladista.Text = "tu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(116, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(241, 18);
            this.label2.TabIndex = 14;
            this.label2.Text = "Ukupno stanje cigareta u skladistu :";
            // 
            // Pregled_stanja_skladiste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(88)))), ((int)(((byte)(103)))));
            this.ClientSize = new System.Drawing.Size(425, 469);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_praznibox);
            this.Controls.Add(this.Stanje_skladista);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Pregled_stanja_skladiste";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pregled stanja skladiste";
            this.Activated += new System.EventHandler(this.Pregled_stanja_skladiste_Activated);
            this.Load += new System.EventHandler(this.Pregled_stanja_skladiste_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_praznibox;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label Stanje_skladista;
        private System.Windows.Forms.Label label2;
    }
}