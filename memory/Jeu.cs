using System;
using System.Collections.Generic;

namespace memory
{
    public class Jeu
    {
        public List<Carte> Cartes { get; private set; }
        public int NbEssais { get; private set; }
        public Carte PremiereCarte { get; private set; }
        public Carte DeuxiemeCarte { get; private set; }
        private Random random;
        public bool PeutJouer { get; private set; }

        public Jeu()
        {
            Cartes = new List<Carte>();
            random = new Random();
            PeutJouer = true;
        }

        public void InitialiserPartie(int nombrePaires)
        {
            Cartes.Clear();
            NbEssais = 0;
            PremiereCarte = null;
            DeuxiemeCarte = null;
            PeutJouer = true;

            CreerPaires(nombrePaires);
            MelangerCartes();
        }

        private void CreerPaires(int nombrePaires)
        {
            for (int i = 1; i <= nombrePaires; i++)
            {
                string image = "Assets/Images/image_" + i + ".png";

                Cartes.Add(new Carte(i, image));
                Cartes.Add(new Carte(i, image));
            }
        }

        private void MelangerCartes()
        {
            for (int i = 0; i < Cartes.Count; i++)
            {
                int indexAleatoire = random.Next(i, Cartes.Count);

                Carte temp = Cartes[i];
                Cartes[i] = Cartes[indexAleatoire];
                Cartes[indexAleatoire] = temp;
            }
        }

        public bool RetournerCarte (int i) 
        {
            if (!PeutJouer)
            {
                return false;
            }

            if ( i < 0 || i >= Cartes.Count)
            {
                return false;
            }

            Carte carteSelectionnee = Cartes[i];

            if (carteSelectionnee.Etat == EtatCarte.Trouvee || carteSelectionnee.Etat == EtatCarte.Revelee)
            {
                return false;
            }

            if (PremiereCarte != null && DeuxiemeCarte != null)
            {
                return false;
            }

            carteSelectionnee.Etat = EtatCarte.Revelee;

            if (PremiereCarte == null)
            {
                PremiereCarte = carteSelectionnee;
            }
            else
            {
                DeuxiemeCarte = carteSelectionnee;
            }
            return true;
        }
        public bool DeuxCartesSelectionnees()
        {
            return PremiereCarte != null && DeuxiemeCarte != null;
        }

        public bool VerifierPaire()
        {
            if (!DeuxCartesSelectionnees())
            {
                return false;
            }

            NbEssais++;

            if ( PremiereCarte.IdPaire == DeuxiemeCarte.IdPaire)
            {
                PremiereCarte.Etat = EtatCarte.Trouvee;
                DeuxiemeCarte.Etat = EtatCarte.Trouvee;

                PremiereCarte = null;
                DeuxiemeCarte = null;

                return true;
            }

            PeutJouer = false;
            return false;
        }

        public void CacherCartesSelectionnees()
        {
            if (PremiereCarte != null && DeuxiemeCarte != null)
            {
                PremiereCarte.Etat = EtatCarte.Cachee;
                DeuxiemeCarte.Etat = EtatCarte.Cachee;

                PremiereCarte = null;
                DeuxiemeCarte = null;

                PeutJouer = true;
            }
        }

        public bool PartieGagnee()
        {
            foreach (Carte carte in Cartes)
            {
                if (carte.Etat != EtatCarte.Trouvee)
                {
                    return false;
                }
            }
            return true;
        }
    }
}