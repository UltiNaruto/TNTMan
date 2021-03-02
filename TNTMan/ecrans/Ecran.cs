using System;
using System.Collections.Generic;
using SDL2;

namespace TNTMan.ecrans
{
    class Ecran
    {
        String titre;
        List<Bouton> boutons;
        IntPtr arrierePlan;
        Ecran ecranPrecedent;

        protected Ecran() { }
        protected Ecran(String _titre, Ecran _ecranPrecedent)
        {
            titre = _titre;
            ecranPrecedent = _ecranPrecedent;
        }

        public void dessinerEcran(IntPtr rendu)
        {

        }

        public void gererSouris()
        {

        }

        public void gererTouches(byte[] etats)
        {

        }
    }
}
