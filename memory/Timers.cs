using System.Drawing;
using System.Windows.Forms;

namespace memory
{
    // - Responsabilité : initialisation des timers et gestion de leurs événements Tick.
    // - timerDelai   : attend 1.5s puis retourne les cartes non-appariées face cachée.
    // - timerChronos : décompte le temps restant et déclenche le game over.
    
    public partial class Form1
    {
        // Crée timerDelai pause 1,5s avant de cacher les cartes
        // et timerChronos décompte affiché chaque seconde
        private void InitialiserTimers()
        {
            timerDelai = new Timer { Interval = 1500 };
            timerDelai.Tick += TimerDelai_Tick;

            timerChronos = new Timer { Interval = 1000 };
            timerChronos.Tick += TimerChronos_Tick;
        }

        // Déclenché après 1,5s : remet les deux cartes face cachée
        // et rend la main au joueur (PeutJouer redevient true dans Jeu.cs).
        private void TimerDelai_Tick(object sender, System.EventArgs e)
        {
            // Tick en attente possible après RetourAuMenu()
            if (!partieEnCours) return;
            timerDelai.Stop();
            jeu.CacherCartesSelectionnees();
            RafraichirGrille();
        }

        // Déclenché chaque seconde : décrémente le temps restant.
        // Passe en Rouge sous les 10s ptite dose de stress, déclenche GameOver à 0.
        private void TimerChronos_Tick(object sender, System.EventArgs e)
        {
            // Tick en attente possible après RetourAuMenu()
            if (!partieEnCours) return;
            secondesEcoulees++;
            int restant = ConfigPartie.TempsLimite - secondesEcoulees;

            if (restant <= 0)
            {
                timerChronos.Stop();
                labelTemps.Text      = "Temps : 00:00";
                labelTemps.ForeColor = Color.OrangeRed;
                AfficherGameOver();
                return;
            }

            labelTemps.Text      = "Temps : " + FormatTemps(restant);
            labelTemps.ForeColor = restant <= 10 ? Color.OrangeRed : Color.White;
        }
    }
}
