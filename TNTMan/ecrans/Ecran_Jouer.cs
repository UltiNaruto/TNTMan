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
            map = new Map();
            map.chargerMapParDefaut();
            joueur = new Joueur(1, 1.25f, 1.25f, map);
            map.ajoutEntite(joueur);
        }

        // taille bloc 32x32
        // grille 15x11 (bords inclus)
        public override void dessinerEcran(IntPtr rendu)
        {
            base.dessinerEcran(rendu);
            Size resolution = Gfx.getResolution();
            Size tailleGrille = new Size(Map.LARGEUR_GRILLE * Bloc.TAILLE_BLOC, Map.LONGUEUR_GRILLE * Bloc.TAILLE_BLOC);
            Gfx.nettoyerEcran(Color.Green);
            for (int x = 0; x < Map.LARGEUR_GRILLE; x++)
                for (int y = 0; y < Map.LONGUEUR_GRILLE; y++)
                {
                    Bloc bloc = map.getBlocA(x, y);
                    if (bloc != null)
                    {
                        bloc.dessiner(rendu, x, y);
                    }
                }

            map.executerPourToutEntite((e) =>
            {
                if (!e.estMort())
                {
                    e.dessiner(rendu);
                }
            });
            Gfx.dessinerTexte(5, 5, 18, Color.Black, "J1 - ({0:0.00}, {1:0.00})", joueur.getPosition().X, joueur.getPosition().Y);
            Gfx.dessinerTexte(5, 25, 18, Color.Black, "Vel - ({0:0.00}, {1:0.00})", joueur.vitesse.X, joueur.vitesse.Y);
            Gfx.dessinerTexte(5, 460, 18, Color.Black, "Bombes restantes : {0}", joueur.getNbBombes());
        }

        public override void gererTouches(byte[] etats)
        {
            map.executerPourToutEntite((e) =>
            {
                if (e.estMort())
                {
                    map.supprimerEntite(e);
                }
                else
                {
                    if (e.GetType() != typeof(Joueur))
                        e.mettreAJour();
                    else
                        ((Joueur)e).mettreAJour(etats);
                }
            });
        }
    }
}
