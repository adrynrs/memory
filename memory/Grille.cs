using System;
using System.Drawing;
using System.Windows.Forms;

namespace memory
{
    public partial class Form1
    {
        // start partie
        // quand on clique sur "Jouer"
        private void LancerPartie()
        {
            // si des cartes d'une partie précédente, les effaces toutes
            if (pictureBoxes != null)
            {
                foreach (PictureBox pb in pictureBoxes)
                    this.Controls.Remove(pb);
            }
            // prépare une liste vide pour stocker les futures cartes
            pictureBoxes = new System.Collections.Generic.List<PictureBox>();

            // arrête les compteurs de temps et remet tous à zéro, temps et essais
            timerDelai.Stop();
            timerChronos.Stop();
            secondesEcoulees = 0;
            labelTemps.Text      = "Temps : " + FormatTemps(ConfigPartie.TempsLimite);
            labelTemps.ForeColor = System.Drawing.Color.White;
            labelEssais.Text = "Essais : 0";

            // crée le "cerveau" du jeu et on dit combien de paires préparer
            jeu = new Jeu();
            jeu.InitialiserPartie(ConfigPartie.NombrePaires);

            // charge l'image du dos de la carte, si elle existe pas dans les dossiers, le jeu crie help
            if (!System.IO.File.Exists(cheminDos))
                throw new System.IO.FileNotFoundException("Image manquante : " + cheminDos);
            imageDos = Image.FromFile(cheminDos);

            // indique que la partie est lancée, dessine le plateau et start le chronomètre
            partieEnCours = true;
            GenererGrille();
            timerChronos.Start();
        }

        // cration du plateau jeu
        // partie visuelle, fabrique la grille de cartes
        private void GenererGrille()
        {
            // vérifie combien de colonnes, 4 pour facile et 6 pour difficile
            int cols = ConfigPartie.NombreColonnes;
            
            // si 4x4, les cartes = 100px, sinon plus petites = 78px
            int taille = cols <= 4 ? 100 : 78;
            int espacement = cols <= 4 ? 10  : 8;
            
            // marges, laisse un peu d'espace à gauche et en haut (110)
            const int offsetX = 20;
            const int offsetY = 110;

            // calcul pour redimensionner la fenêtre entière selon la taille du plateau
            int lignes = (int)Math.Ceiling((double)jeu.Cartes.Count / cols);
            int gridWidth = offsetX + cols * (taille + espacement);
            const int labelW = 185;
            int formW = Math.Max(gridWidth + labelW + 30, 560);
            int formH = offsetY + lignes * (taille + espacement) + 20;
            this.ClientSize = new Size(formW, formH); // fenêtre s'ajuste seule

            // place les textes "Essais" et "Temps" sur la droite
            int labelX = formW - labelW - 10;
            labelEssais.Location = new Point(labelX, 15);
            labelEssais.Size = new Size(labelW, 32);
            labelTemps.Location = new Point(labelX, 48);
            labelTemps.Size = new Size(labelW, 32);

            // boucle pour dessiner les cartes
            for (int i = 0; i < jeu.Cartes.Count; i++)
            {
                // calcule sur qu'elle ligne et colonne doit se placer la carte actuelle
                int ligne = i / cols;
                int colonne = i % cols;

                // fabrique l'objet visuel "PictureBox" qui représentera la carte sur l'écran
                PictureBox pb = new PictureBox
                {
                    Size = new Size(taille, taille),
                    Location = new Point(
                        offsetX + colonne * (taille + espacement), // position de gauche a droite
                        offsetY + ligne   * (taille + espacement)), // position de haut en bas
                    SizeMode = PictureBoxSizeMode.StretchImage, // permet à l'image de s'écraser pour tenir dans la boîte
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.DarkSlateBlue,
                    Tag = i
                };

                // applique l'image de dos (face cachée)
                pb.Image = imageDos;

                // dit au programme "si clique sur cette carte, déclenche la méthode PictureBox_Click"
                pb.Click += PictureBox_Click;
                
                // ajoute la carte sur l'écran, et l'enregistre dans liste
                this.Controls.Add(pb);
                pictureBoxes.Add(pb);
            }
        }

        // retourner cartes face cahcé
        // méthode sert à remettre les cartes de dos (quand se trompe de paire)
        private void RafraichirGrille()
        {
            // inspecte toutes les cartes visuelles une par une
            for (int i = 0; i < pictureBoxes.Count; i++)
            {
                // Si "cerveau" du jeu dit que cette carte est censée être cachée...
                if (jeu.Cartes[i].Etat == EtatCarte.Cachee)
                {
                    // ...alors on lui remet l'image de dos
                    pictureBoxes[i].Image     = imageDos;
                    pictureBoxes[i].BackColor = Color.White;
                }
            }
        }

        // dévoile une carte
        // méthode affiche l'icône de carte
        private void AfficherFaceVisible(PictureBox pb, int index)
        {
            // récupère le nom du fichier image depuis la logique du jeu
            string nomFichier = System.IO.Path.GetFileName(jeu.Cartes[index].ImageChemin);
            string chemin     = repImages + nomFichier; // construit chemin complet du dossier images

            // vérifie que l'image existe dans le dossier pour ne pas crash le jeu
            if (!System.IO.File.Exists(chemin))
                throw new System.IO.FileNotFoundException("Image manquante : " + chemin);

            // remplace le dos de la carte par la vraie image 
            pb.Image     = Image.FromFile(chemin);
            pb.BackColor = Color.White;
        }

    }
}