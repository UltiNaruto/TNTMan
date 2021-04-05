using System;
using System.Drawing;
using TNTMan.map;
using TNTMan.map.blocs;

namespace TNTMan.ecrans
{
    class Ecran_Jeu : Ecran
    {
        Session session = null;

        public Ecran_Jeu(Session _session) : base("Jeu", null)
        {
            session = _session;
            arrierePlan = Gfx.images["fond_ecran"];
        }

        // taille bloc 32x32
        // grille 15x11 (bords inclus)
        public override void dessinerEcran(IntPtr rendu)
        {
            base.dessinerEcran(rendu);
            Size resolution = Gfx.getResolution();
            Size tailleGrille = new Size(Map.LARGEUR_GRILLE * Bloc.TAILLE_BLOC, Map.LONGUEUR_GRILLE * Bloc.TAILLE_BLOC);
            session.dessiner(rendu);
        }

        public override void gererTouches(byte[] etats)
        {
            session.gererTouches(etats);
            session.mettreAJour();
        }

        public void mettreAJour()
        {
            if (session.enPause)
            {
                Gfx.changerEcran(new Ecran_Pause(this));
            }
        }
    }
}
