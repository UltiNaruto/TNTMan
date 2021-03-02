using System;
using System.Drawing;
using TNTMan.entitees;
using TNTMan.map;
using TNTMan.map.blocs;

namespace TNTMan.ecrans
{
    class Ecran_Jouer : Ecran
    {
        //Session session = null;
        Map map = null;
        Joueur joueur = null;

        public Ecran_Jouer() : base("Jeu", null)
        {
            joueur = new Joueur(1, 0.0f, 0.0f);
            map = new Map();
            map.chargerMapParDefaut();
        }

        // taille bloc 32x32
        // grille 15x11 (bords inclus)
        public override void dessinerEcran(IntPtr rendu)
        {
            base.dessinerEcran(rendu);
            Size resolution = Gfx.getResolution();
            Size taille_grille = new Size(Map.LARGEUR_GRILLE * Bloc.TAILLE_BLOC, Map.LONGUEUR_GRILLE * Bloc.TAILLE_BLOC);
            Gfx.nettoyerEcran(Color.Green);
            for (int x = 0; x < Map.LARGEUR_GRILLE; x++)
                for (int y = 0; y < Map.LONGUEUR_GRILLE; y++)
                {
                    Bloc bloc = map.getBlocA(x, y);
                    if (bloc != null)
                    {
                        if (bloc.GetType() == typeof(BlocIncassable))
                            Gfx.remplirRectangle((resolution.Width - taille_grille.Width) / 2 + x * Bloc.TAILLE_BLOC, (resolution.Height - taille_grille.Height) / 2 + y * Bloc.TAILLE_BLOC, Bloc.TAILLE_BLOC, Bloc.TAILLE_BLOC, 1, Color.Gray, Color.Black);
                        if (bloc.GetType() == typeof(BlocTerre))
                            Gfx.remplirRectangle((resolution.Width - taille_grille.Width) / 2 + x * Bloc.TAILLE_BLOC, (resolution.Height - taille_grille.Height) / 2 + y * Bloc.TAILLE_BLOC, Bloc.TAILLE_BLOC, Bloc.TAILLE_BLOC, 1, Color.Brown, Color.Black);
                    }
                }
        }

        public override void gererSouris()
        {
        }

        public override void gererTouches(byte[] etats)
        {
        }
    }
}
