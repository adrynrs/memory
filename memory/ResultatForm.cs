using System.Drawing;
using System.Windows.Forms;

namespace memory
{
    public class ResultatForm : Form
    {
        public ResultatForm(bool victoire, int essais, int secondes)
        {
            InitialiserComposants(victoire, essais, secondes);
        }

        private void InitialiserComposants(bool victoire, int essais, int secondes)
        {
            this.Text            = victoire ? "Victoire !" : "Game Over";
            this.ClientSize      = new Size(420, 340);
            this.BackColor       = Color.FromArgb(15, 15, 40);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.StartPosition   = FormStartPosition.CenterParent;

            Label labelTitre = new Label
            {
                Text      = victoire ? "Félicitations !" : "Temps écoulé !",
                Font      = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = victoire ? Color.MediumSeaGreen : Color.IndianRed,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                Size      = new Size(380, 60),
                Location  = new Point(20, 28)
            };

            Panel separateur = new Panel
            {
                BackColor = Color.FromArgb(55, 55, 90),
                Size      = new Size(340, 2),
                Location  = new Point(40, 100)
            };

            string tempsTexte = victoire
                ? $"{secondes / 60:D2}:{secondes % 60:D2}"
                : "—";

            Label labelEssaisLib = new Label
            {
                Text      = "Essais",
                Font      = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(160, 160, 210),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
                Size      = new Size(170, 36),
                Location  = new Point(50, 118)
            };

            Label labelEssaisVal = new Label
            {
                Text      = essais.ToString(),
                Font      = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleRight,
                Size      = new Size(170, 36),
                Location  = new Point(200, 118)
            };

            Label labelTempsLib = new Label
            {
                Text      = "Temps",
                Font      = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(160, 160, 210),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
                Size      = new Size(170, 36),
                Location  = new Point(50, 162)
            };

            Label labelTempsVal = new Label
            {
                Text      = tempsTexte,
                Font      = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleRight,
                Size      = new Size(170, 36),
                Location  = new Point(200, 162)
            };

            Button btnMenu = new Button
            {
                Text      = "Retour au menu",
                Location  = new Point(110, 258),
                Size      = new Size(200, 50),
                BackColor = Color.MediumSlateBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor    = Cursors.Hand
            };
            btnMenu.FlatAppearance.BorderSize = 0;
            btnMenu.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[]
            {
                labelTitre, separateur,
                labelEssaisLib, labelEssaisVal,
                labelTempsLib,  labelTempsVal,
                btnMenu
            });
        }
    }
}
