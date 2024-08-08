namespace Automat
{
    partial class Postavke_skladista
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Postavke_skladista));
            this.lbl_CMP = new System.Windows.Forms.Label();
            this.Br_skladista = new System.Windows.Forms.TextBox();
            this.btn_SkladisteSpremi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_CMP
            // 
            this.lbl_CMP.AllowDrop = true;
            this.lbl_CMP.ForeColor = System.Drawing.Color.White;
            this.lbl_CMP.Location = new System.Drawing.Point(1, 11);
            this.lbl_CMP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_CMP.Name = "lbl_CMP";
            this.lbl_CMP.Size = new System.Drawing.Size(201, 62);
            this.lbl_CMP.TabIndex = 8;
            this.lbl_CMP.Text = "Unesite broj skladišta automata:";
            this.lbl_CMP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Br_skladista
            // 
            this.Br_skladista.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Br_skladista.Location = new System.Drawing.Point(203, 29);
            this.Br_skladista.Margin = new System.Windows.Forms.Padding(4);
            this.Br_skladista.Name = "Br_skladista";
            this.Br_skladista.Size = new System.Drawing.Size(83, 24);
            this.Br_skladista.TabIndex = 9;
            this.Br_skladista.Enter += new System.EventHandler(this.Br_skladista_Enter);
            this.Br_skladista.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Br_skladista_KeyDown);
            this.Br_skladista.Leave += new System.EventHandler(this.Br_skladista_Leave);
            // 
            // btn_SkladisteSpremi
            // 
            this.btn_SkladisteSpremi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(95)))), ((int)(((byte)(85)))));
            this.btn_SkladisteSpremi.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_SkladisteSpremi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(88)))), ((int)(((byte)(103)))));
            this.btn_SkladisteSpremi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SkladisteSpremi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btn_SkladisteSpremi.ForeColor = System.Drawing.Color.White;
            this.btn_SkladisteSpremi.Location = new System.Drawing.Point(125, 86);
            this.btn_SkladisteSpremi.Margin = new System.Windows.Forms.Padding(4);
            this.btn_SkladisteSpremi.Name = "btn_SkladisteSpremi";
            this.btn_SkladisteSpremi.Size = new System.Drawing.Size(161, 34);
            this.btn_SkladisteSpremi.TabIndex = 10;
            this.btn_SkladisteSpremi.Text = "Spremi";
            this.btn_SkladisteSpremi.UseVisualStyleBackColor = false;
            this.btn_SkladisteSpremi.Click += new System.EventHandler(this.btn_SkladisteSpremi_Click);
            // 
            // Postavke_skladista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(88)))), ((int)(((byte)(103)))));
            this.ClientSize = new System.Drawing.Size(299, 133);
            this.Controls.Add(this.btn_SkladisteSpremi);
            this.Controls.Add(this.Br_skladista);
            this.Controls.Add(this.lbl_CMP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Postavke_skladista";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Postavke skladišta";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Postavke_skladista_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_CMP;
        private System.Windows.Forms.TextBox Br_skladista;
        private System.Windows.Forms.Button btn_SkladisteSpremi;
    }
}