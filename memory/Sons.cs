using System.IO;
using System.Media;
using System.Windows.Forms;

namespace memory
{
    internal static class Sons
    {
        private static readonly string repSons =
            Path.GetFullPath(
                Path.Combine(Application.StartupPath, @"..\..\Assets\Sounds\"));

        // Référence statique pour éviter que le GC libère le lecteur
        // pendant la lecture asynchrone
        private static SoundPlayer lecteurActif;

        private static void Jouer(string fichier)
        {
            try
            {
                string chemin = Path.Combine(repSons, fichier);
                if (!File.Exists(chemin)) return;
                lecteurActif = new SoundPlayer(chemin);
                lecteurActif.Play();
            }
            catch { }
        }

        public static void Clic()     => Jouer("clic.wav");
        public static void Paire()    => Jouer("paire.wav");
        public static void Victoire() => Jouer("victoire.wav");
        public static void GameOver() => Jouer("defaite.wav");
    }
}
