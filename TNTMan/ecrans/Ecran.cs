using System;
using System.Collections.Generic;
using System.Drawing;
using SDL2;

namespace TNTMan.ecrans
{
    class Ecran
    {
        public String titre;
        public List<Bouton> boutons;
        IntPtr arrierePlan;
        Ecran ecranPrecedent;

        protected Ecran() { }
        protected Ecran(String _titre, Ecran _ecranPrecedent)
        {
            titre = _titre;
            ecranPrecedent = _ecranPrecedent;
            arrierePlan = IntPtr.Zero;
            boutons = new List<Bouton>();
        }

        public virtual void dessinerEcran(IntPtr rendu)
        {
            if (arrierePlan == IntPtr.Zero) return;
        }

        public virtual void gererSouris()
        {

        }

        public virtual void gererTouches(byte[] etats)
        {

        }

        public virtual void dessinerBoutons(IntPtr rendu)
        {
            foreach (Bouton b in boutons)
            {
                b.dessiner(rendu);
            }
        }

        public virtual void mettreAJourBoutons(Point curseur)
        {
            foreach (Bouton b in boutons)
            {
                b.gererEvenements();
            }
        }
    }
}
