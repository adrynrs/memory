using System;
using System.Drawing;
using System.Windows.Forms;

namespace memory
{
    // classe, représente la fenêtre d'option jeu
    public class OptionsForm : Form
    {
        // déclarations, éléments visuels de la fenêtre

        // grand texte de titre en haut
        private Label labelTitre;
        // petit texte "Taille de la grille :"
        private Label labelGrille;
        // choix à cocher pour Facile (4x4)
        private RadioButton radio4x4;
        // choix à cocher pour Difficile (6x6)
        private RadioButton radio6x6;
        // bouton pour save les choix
        private Button      btnValider;
        // bouton pour quitter sans rien changer
        private Button      btnAnnuler;
        // constructeur (first execute)
        public OptionsForm()
        {
            InitialiserComposants(); // start la création du visuel
        }

        // creation visuelle
        private void InitialiserComposants()
        {
            // paramètres généraux de la fenêtre
            this.Text            = "Options";
            this.ClientSize      = new Size(360, 300);
            this.BackColor       = Color.FromArgb(15, 15, 40);
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // empêche de redimensionner la fenêtre
            this.MaximizeBox     = false;
            this.MinimizeBox     = false;
            this.StartPosition   = FormStartPosition.CenterParent; // Ouvre cette fenêtre au centre du menu principal

            // paramétrage du titre
            labelTitre = new Label
            {
                Text      = "Options de jeu",
                Font      = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.MediumSlateBlue,
                BackColor = Color.Transparent,
                Size      = new Size(320, 40),
                Location  = new Point(20, 20) // position dans la fenêtre
            };

            // paramétrage du sous-titre
            labelGrille = new Label
            {
                Text      = "Taille de la grille :",
                Font      = new Font("Segoe UI", 11, FontStyle.Underline),
                ForeColor = Color.FromArgb(200, 200, 230),
                BackColor = Color.Transparent,
                Size      = new Size(300, 28),
                Location  = new Point(20, 80)
            };

            // paramétrage du 1er choix (4x4)
            radio4x4 = new RadioButton
            {
                Text      = "4 × 4   (8 paires — Facile)",
                Location  = new Point(35, 118),
                Size      = new Size(280, 28),
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 11),
                BackColor = Color.Transparent,
                // si config de la partie est déjà sur 4 colonnes, coche par défaut
                Checked   = ConfigPartie.NombreColonnes == 4 
            };

            // paramétrage du 2ème choix (6x6)
            radio6x6 = new RadioButton
            {
                Text      = "6 × 6   (18 paires — Difficile)",
                Location  = new Point(35, 155),
                Size      = new Size(280, 28),
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 11),
                BackColor = Color.Transparent,
                // si config de la partie est déjà sur 6 colonnes, on le coche par défaut
                Checked   = ConfigPartie.NombreColonnes == 6 
            };

            // paramétrage du bouton Valider
            btnValider = new Button
            {
                Text      = "Valider",
                Location  = new Point(40, 225),
                Size      = new Size(120, 44),
                BackColor = Color.MediumSlateBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor    = Cursors.Hand // souris devient une petite main
            };
            btnValider.FlatAppearance.BorderSize = 0; // retire la bordure autour du bouton

            // paramétrage du bouton annuler
            btnAnnuler = new Button
            {
                Text      = "Annuler",
                Location  = new Point(200, 225),
                Size      = new Size(120, 44),
                BackColor = Color.FromArgb(70, 70, 70),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font      = new Font("Segoe UI", 11),
                Cursor    = Cursors.Hand
            };
            btnAnnuler.FlatAppearance.BorderSize = 0;

            // évenements (actions lors des clics)
            
            // si clique sur valider, lance la méthode "BtnValider_Click" plus bas
            btnValider.Click += BtnValider_Click; 
            
            // Si clique sur annuler, ferme juste la fenêtre
            btnAnnuler.Click += (s, e) => this.Close(); 

            // affichage
            // colle tous les éléments paramétrer sur la fenêtre pour les voir
            this.Controls.AddRange(new Control[]
            {
                labelTitre, labelGrille,
                radio4x4, radio6x6,
                btnValider, btnAnnuler
            });
        }

        // logique de save (quand clique sur Valider)
        private void BtnValider_Click(object sender, EventArgs e)
        {
            // si joueur a sélectionné le bouton "facile"
            if (radio4x4.Checked)
            {
                // met à jour la config du jeu
                ConfigPartie.NombrePaires   = 8; 
                ConfigPartie.NombreColonnes = 4;
            }
            // forcément sélectionné "difficile"
            else
            {
                // mas la config du jeu avec des valeurs plus grandes
                ConfigPartie.NombrePaires   = 18;
                ConfigPartie.NombreColonnes = 6;
            }
            this.Close();
        }
    }
}