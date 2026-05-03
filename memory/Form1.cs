using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace memory
{
    public partial class Form1 : Form
    {
        private Jeu jeu;
        private List<PictureBox> pictureBoxes; // liste qui va contenir toutes les cartes sur la grille
        private Image imageDos;
        private Label labelEssais; // texte du score
        private Label labelTemps; // texte du chrono

        // Minuteurs
        private Timer timerDelai; // chrono pour le temps de mémorisation (1,5s)
        private Timer timerChronos; // compte à rebours global
        private int secondesEcoulees; // secondes se sont écoulées

        // Menu
        private Panel panelMenu;
        private bool  partieEnCours;
        // gestion chemins images
        
        // calcule le chemin du dossier "Images" (qui se trouve dans "Assets\Images" par rapport à là où le programme s'exécute)
        private static readonly string repImages =
            System.IO.Path.GetFullPath(
                System.IO.Path.Combine(Application.StartupPath, @"..\..\Assets\Images\"));

        private static readonly string cheminDos = repImages + "dos.png";

        // génère automatiquement la liste des chemins pour toutes les images des paires (image_1.png, image_2.png...)
        internal static string[] GenererChemins(int nombrePaires)
        {
            string[] chemins = new string[nombrePaires];
            for (int i = 0; i < nombrePaires; i++)
                chemins[i] = repImages + "image_" + (i + 1) + ".png"; // Construit le texte: "C:\...\image_1.png"
            return chemins;
        }

        // lancement app
        public Form1()
        {
            InitializeComponent();
            InitialiserTimers();
            InitialiserLabels();
        }

        // chargement fenêtre
        private void Form1_Load(object sender, System.EventArgs e)
        {
            this.BackColor  = Color.FromArgb(15, 15, 40); 
            this.ClientSize = new Size(660, 580); 

            // cache les éléments du jeu (comme compteurs et les boutons "Relancer"/"Quitter") 
            button1.Visible = false; 
            buttonQuitter.Visible = false;
            labelEssais.Visible = false;
            labelTemps.Visible = false;

            CreerPanelMenu();
        }

        // create menu principal
        private void CreerPanelMenu()
        {
            const int W = 660;

            panelMenu = new Panel
            {
                Location  = new Point(0, 0),
                Size      = new Size(W, 580),
                BackColor = Color.Transparent
            };

            // titre SecurIT Memory
            Label labelTitre = new Label
            {
                Text      = "SecurIT Memory",
                Font      = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.MediumSlateBlue,
                BackColor = Color.Transparent,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Size      = new Size(W - 40, 70),
                Location  = new Point(20, 70)
            };

            // tailles et position des boutons
            const int btnW = 240, btnH = 55;
            int btnX = (W - btnW) / 2;

            Button btnJouer   = ConstruireBoutonMenu("Jouer",   new Point(btnX, 220), Color.MediumSlateBlue);
            Button btnOptions = ConstruireBoutonMenu("Options", new Point(btnX, 300), Color.FromArgb(55, 55, 110));
            Button btnQuitter = ConstruireBoutonMenu("Quitter", new Point(btnX, 380), Color.IndianRed);

            btnJouer.Size = btnOptions.Size = btnQuitter.Size = new Size(btnW, btnH);

            // ce que font les boutons quand on clique dessus :
            btnJouer.Click   += (s, ev) => DemarrerJeu();
            btnOptions.Click += (s, ev) => { using (var o = new OptionsForm()) o.ShowDialog(this); };
            btnQuitter.Click += (s, ev) => Application.Exit();

            // colle les éléments sur le calque du menu
            panelMenu.Controls.AddRange(new Control[] { labelTitre, btnJouer, btnOptions, btnQuitter });
            
            // ajoute le calque à la fenêtre principale et on le met tout devant (BringToFront)
            this.Controls.Add(panelMenu);
            panelMenu.BringToFront();
        }

        private static Button ConstruireBoutonMenu(string texte, Point pos, Color couleur)
        {
            Button btn = new Button
            {
                Text = texte,
                Location = pos,
                BackColor = couleur,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        // transition jeu au menu
        private void DemarrerJeu()
        {
            panelMenu.Visible = false; // cache le grand calque du menu principal
            
            // rend visibles les éléments de jeu (les boutons et les compteurs)
            button1.Visible = true; 
            buttonQuitter.Visible = true;
            labelEssais.Visible = true;
            labelTemps.Visible = true;
            
            // lance la mécanique de la partie (mélange des cartes, création de la grille)
            LancerPartie(); 
        }

        // quand on veut revenir au menu principal (bouton quitter, ou à la fin d'une partie)
        internal void RetourAuMenu()
        {
            partieEnCours = false;
            timerDelai.Stop();
            timerChronos.Stop();

            // efface la grille de jeu, si il y a des cartes, les supprime de l'écran.
            if (pictureBoxes != null)
            {
                foreach (PictureBox pb in pictureBoxes)
                    this.Controls.Remove(pb);
                pictureBoxes = null;
            }

            // libère la mémoire utilisée par l'image de dos
            if (imageDos != null) { imageDos.Dispose(); imageDos = null; }

            // recache les éléments du jeu (boutons, chrono...)
            button1.Visible = false;
            buttonQuitter.Visible = false;
            labelEssais.Visible = false;
            labelTemps.Visible = false;

            // remet la fenêtre à sa taille d'origine et réaffiche le grand calque du menu
            this.ClientSize = new Size(660, 580);
            panelMenu.Size  = this.ClientSize;
            panelMenu.Visible = true;
            panelMenu.BringToFront(); // assure que le menu est bien au premier plan
        }

        // bouton in game
        // clic sur le bouton "Relancer" (button1) pendant une partie
        private void button1_Click(object sender, System.EventArgs e)
        {
            LancerPartie(); // écrase la partie en cours et recommence une nouvelle
        }

        // Clic sur le bouton "Quitter" pendant une partie
        private void buttonQuitter_Click(object sender, System.EventArgs e)
        {
            RetourAuMenu();
        }
    }
}