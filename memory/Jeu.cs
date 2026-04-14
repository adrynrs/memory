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

        public Jeu()
        {
            Cartes = new List<Carte>();
            random = new Random();
        }

        public void InitialiserPartie(int nombrePaires)
        {
            Cartes.Clear();
            NbEssais = 0;
            PremiereCarte = null;
            DeuxiemeCarte = null;

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
    }
}