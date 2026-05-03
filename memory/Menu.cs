using System;
using System.Drawing;
using System.Windows.Forms;

namespace memory
{
    public class MenuForm : Form
    {
        private Label  labelTitre;
        private Label  labelSousTitre;
        private Button btnJouer;
        private Button btnOptions;
        private Button btnQuitter;

        public MenuForm()
        {
            InitialiserComposants();
        }

        private void InitialiserComposants()
        {
            const int W = 660;
            const int H = 580;

            this.Text            = "Memory — Cyber";
            this.ClientSize      = new Size(W, H);
            this.BackColor       = Color.FromArgb(15, 15, 40);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.StartPosition   = FormStartPosition.CenterScreen;

            labelTitre = new Label
            {
                Text      = "SecurIT Memory",
                Font      = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.MediumSlateBlue,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                Size      = new Size(W - 40, 70),
                Location  = new Point(20, 70)
            };

            const int btnW = 240;
            const int btnH = 55;
            int btnX = (W - btnW) / 2;

            btnJouer = ConstruireBouton(
                "Jouer",
                new Point(btnX, 220),
                new Size(btnW, btnH),
                Color.MediumSlateBlue
            );

            btnOptions = ConstruireBouton(
                "Options",
                new Point(btnX, 300),
                new Size(btnW, btnH),
                Color.FromArgb(55, 55, 110)
            );

            btnQuitter = ConstruireBouton(
                "Quitter",
                new Point(btnX, 380),
                new Size(btnW, btnH),
                Color.IndianRed
            );

            btnJouer.Click   += BtnJouer_Click;
            btnOptions.Click += BtnOptions_Click;
            btnQuitter.Click += BtnQuitter_Click;

            this.Controls.AddRange(new Control[]
            {
                labelTitre,
                btnJouer, btnOptions, btnQuitter
            });
        }

        private static Button ConstruireBouton(string texte, Point pos, Size taille, Color couleur)
        {
            Button btn = new Button
            {
                Text      = texte,
                Location  = pos,
                Size      = taille,
                BackColor = couleur,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 13, FontStyle.Bold),
                Cursor    = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void BtnJouer_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 jeu = new Form1();
            jeu.FormClosed += (s, ev) => this.Show();
            jeu.Show();
        }

        private void BtnOptions_Click(object sender, EventArgs e)
        {
            using (OptionsForm options = new OptionsForm())
                options.ShowDialog(this);
        }

        private void BtnQuitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
