namespace memory
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonQuitter;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        private void InitializeComponent()
        {
            this.button1       = new System.Windows.Forms.Button();
            this.buttonQuitter = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // button1 — Rejouer
            this.button1.Location  = new System.Drawing.Point(20, 22);
            this.button1.Size      = new System.Drawing.Size(100, 38);
            this.button1.Text      = "Rejouer";
            this.button1.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Font   = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Click += new System.EventHandler(this.button1_Click);

            // buttonQuitter — Menu
            this.buttonQuitter.Location  = new System.Drawing.Point(130, 22);
            this.buttonQuitter.Size      = new System.Drawing.Size(100, 38);
            this.buttonQuitter.Text      = "Menu";
            this.buttonQuitter.BackColor = System.Drawing.Color.IndianRed;
            this.buttonQuitter.ForeColor = System.Drawing.Color.White;
            this.buttonQuitter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonQuitter.FlatAppearance.BorderSize = 0;
            this.buttonQuitter.Font   = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            this.buttonQuitter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonQuitter.Click += new System.EventHandler(this.buttonQuitter_Click);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(680, 540);
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonQuitter);
            this.Name = "Form1";
            this.Text = "Memory — Cybersécurité";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
