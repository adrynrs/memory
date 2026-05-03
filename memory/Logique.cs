using System.Drawing;
using System.Windows.Forms;
using memory;

namespace memory
{
    public partial class Form1
    {
        //clique sur 1 carte
        // le code s'exécute chaque fois qu'on clique sur une carte
        private void PictureBox_Click(object sender, System.EventArgs e)
        {
            // identifie quelle carte visuelle exacte a été cliquée
            PictureBox pb    = (PictureBox)sender;
            
            // récupère son numéro d'identification (l'index caché dans la propriété "Tag" lors de la création)
            int        index = (int)pb.Tag;

            // demande à la logique du jeu (la classe Jeu) d'essayer de retourner cette carte
            // refuse (parce que la carte est déjà face visible ou que 2 cartes sont déjà retournées), arrête tout (return)
            bool retournee = jeu.RetournerCarte(index);
            if (!retournee) return;

            // autorise le retournement, affiche la face visible de la carte et joue un son de "clic"
            AfficherFaceVisible(pb, index);
            Sons.Clic();

            // demande au jeu : "Est-ce qu'on a exactement 2 cartes retournées maintenant ?"
            // si non (c'est seulement la première carte), s'arrête là et attend le prochain clic.
            if (!jeu.DeuxCartesSelectionnees()) return;

            // si oui (2 cartes visibles), vérifie si forment une paire et maj le compteur de tentatives
            bool paire = jeu.VerifierPaire();
            MettreAJourEssais();

            // si c'est une paire valide (les deux cartes sont identiques)
            if (paire)
            {
                // vérifie si c'était la toute dernière paire du jeu
                if (jeu.PartieGagnee())
                    AfficherVictoire(); // gagné, on lance la fin
                else
                    Sons.Paire(); // pas fini, mais joue son de réussite
            }
            // si pas une paire
            else
            {
                // démarre le minuteur de délai
                // patiente pour laisser le temps de mémoriser les cartes avant de les recacher
                timerDelai.Start();
            }
        }

        // victoire
        private void AfficherVictoire()
        {
            // stoppe tous les compteurs de temps et joue musique
            timerDelai.Stop();
            timerChronos.Stop();
            Sons.Victoire();

            // ouvre la fenêtre de résultats avec le nombre d'essais et le temps
            using (ResultatForm r = new ResultatForm(true, jeu.NbEssais, secondesEcoulees))
                r.ShowDialog(this);

            RetourAuMenu();
        }

        // défaite
        private void AfficherGameOver()
        {
            // arrête le jeu et son défaite
            timerDelai.Stop();
            Sons.GameOver();

            // ouvre fenêtre de résultats avec défaite
            using (ResultatForm r = new ResultatForm(false, jeu.NbEssais, secondesEcoulees))
                r.ShowDialog(this);

            RetourAuMenu();
        }

        // texte scores
        private void InitialiserLabels()
        {
            // paramétrage du texte pour le nombre d'essais
            labelEssais = new Label
            {
                Text      = "Essais : 0",
                Location  = new Point(460, 20), 
                Size      = new Size(220, 30),
                Font      = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent 
            };

            // texte pour le chronomètre
            labelTemps = new Label
            {
                Text      = "Temps écoulé : 00:00",
                Location  = new Point(460, 60),
                Size      = new Size(220, 30),
                Font      = new System.Drawing.Font("Segoe UI", 11, System.Drawing.FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };

            this.Controls.Add(labelEssais);
            this.Controls.Add(labelTemps);
        }

        // maj du score
        private void MettreAJourEssais()
        {
            // actualise le texte avec la nouvelle valeur stockée dans la logique de jeu
            labelEssais.Text = "Essais : " + jeu.NbEssais;
        }

        private string FormatTemps(int secondes)
        {
            // (secondes / 60) calcule les minutes complètes.
            // (secondes % 60) récupère le reste des secondes.
            // ":D2" force l'affichage à avoir toujours 2 chiffres (ex: "05" au lieu de "5").
            return $"{secondes / 60:D2}:{secondes % 60:D2}";
        }
    }
}