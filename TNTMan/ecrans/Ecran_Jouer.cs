using System;
using System.Drawing;
using TNTMan.entitees;

namespace TNTMan.ecrans
{
    class Ecran_Jouer : Ecran
    {
        Joueur joueur = null;

        public Ecran_Jouer() : base("Jeu", null)
        {

        }

        public new void dessinerEcran(IntPtr rendu)
        {
            base.dessinerEcran(rendu);
            Gfx.nettoyerEcran(Color.Green);
        }

        public new void gererSouris()
        {
            base.gererSouris();
        }

        public new void gererTouches(byte[] etats)
        {
            base.gererTouches(etats);
        }
    }
}
