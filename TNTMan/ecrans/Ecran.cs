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
            arrierePlan = IntPtr.Zero;
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
    }
}
