using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory
{
    public class Carte
    {
        public int IdPaire { get; set; }
        public string ImageChemin { get; set; }
        public EtatCarte Etat { get; set; }

        public Carte(int idPaire, string imageChemin)
        {
            IdPaire = idPaire;
            ImageChemin = imageChemin;
            Etat = EtatCarte.Cachee;
        }
    }
}
