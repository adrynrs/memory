using System.Drawing;
using System.Windows.Forms;

namespace memory
{
    // classe crée la fenêtre de fin de partie (ResultatForm)
    public class ResultatForm : Form
    {
        // constructeur
        // reçoit 3 info : si gagne (true/false), le nombre d'essais, et temps en secondes
        public ResultatForm(bool victoire, int essais, int secondes)
        {
            // envoie ses info a la méthode qui va dessiner la fenêtre
            InitialiserComposants(victoire, essais, secondes);
        }

        // création visuel
        private void InitialiserComposants(bool victoire, int essais, int secondes)
        {
            // config générale de la fenêtre
            // si victoire, le titre est "Victoire !", sinon "Game Over"
            this.Text = victoire ? "Victoire !" : "Game Over";
            this.ClientSize = new Size(420, 340); 
            this.BackColor = Color.FromArgb(15, 15, 40);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent; 

            Label labelTitre = new Label
            {
                // gagné : "Félicitations !", si perdu : "Temps écoulé !"
                Text = victoire ? "Félicitations !" : "Temps écoulé !",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                // si gagné : texte vert, si perdu : texte rouge.
                ForeColor = victoire ? Color.MediumSeaGreen : Color.IndianRed,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(380, 60),
                Location = new Point(20, 28)
            };

            // sépare le titre des scores
            Panel separateur = new Panel
            {
                BackColor = Color.FromArgb(55, 55, 90),
                Size = new Size(340, 2),
                Location = new Point(40, 100)
            };

            // texte du temps
            // si victoire, formate le temps
            // si défaite, affiche un tiret "—" car le temps max est connu
            string tempsTexte = victoire
                ? $"{secondes / 60:D2}:{secondes % 60:D2}"
                : "—";

            // étiquette "Essais"
            Label labelEssaisLib = new Label
            {
                Text = "Essais",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(160, 160, 210), 
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
                Size = new Size(170, 36),
                Location = new Point(50, 118)
            };

            // valeur des essais
            Label labelEssaisVal = new Label
            {
                Text = essais.ToString(), // transforme le nombre int en texte string
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleRight,
                Size = new Size(170, 36),
                Location = new Point(200, 118)
            };

            // étiquette "Temps"
            Label labelTempsLib = new Label
            {
                Text = "Temps",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(160, 160, 210),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleLeft,
                Size = new Size(170, 36),
                Location = new Point(50, 162)
            };

            // valeur du Temps
            Label labelTempsVal = new Label
            {
                Text = tempsTexte,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleRight,
                Size = new Size(170, 36),
                Location = new Point(200, 162)
            };

            // bouton retour, revenir au menu principal
            Button btnMenu = new Button
            {
                Text = "Retour au menu",
                Location  = new Point(110, 258),
                Size = new Size(200, 50),
                BackColor = Color.MediumSlateBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnMenu.FlatAppearance.BorderSize = 0;
            
            // quand clique, ferme la fenêtre
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