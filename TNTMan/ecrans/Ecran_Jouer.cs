using SDL2;
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
<<<<<<< HEAD
            joueur = new Joueur(4, 1.5f, 1.5f);
            map = new Map();
            map.chargerMapParDefaut();
=======

>>>>>>> 38b087b (AJOUTS & MODIFICATIONS DE CLASSES)
        }

        public new void dessinerEcran(IntPtr rendu)
        {
            base.dessinerEcran(rendu);
<<<<<<< HEAD
            Size resolution = Gfx.getResolution();
            Size tailleGrille = new Size(Map.LARGEUR_GRILLE * Bloc.TAILLE_BLOC, Map.LONGUEUR_GRILLE * Bloc.TAILLE_BLOC);
            Gfx.nettoyerEcran(Color.Green);
            for (int x = 0; x < Map.LARGEUR_GRILLE; x++)
                for (int y = 0; y < Map.LONGUEUR_GRILLE; y++)
                {
                    Bloc bloc = map.getBlocA(x, y);
                    if (bloc != null)
                    {
                        if (bloc.GetType() == typeof(BlocIncassable))
                            Gfx.remplirRectangle((resolution.Width - tailleGrille.Width) / 2 + x * Bloc.TAILLE_BLOC, (resolution.Height - tailleGrille.Height) / 2 + y * Bloc.TAILLE_BLOC, Bloc.TAILLE_BLOC, Bloc.TAILLE_BLOC, 1, Color.Gray, Color.Black);
                        if (bloc.GetType() == typeof(BlocTerre))
                            Gfx.remplirRectangle((resolution.Width - tailleGrille.Width) / 2 + x * Bloc.TAILLE_BLOC, (resolution.Height - tailleGrille.Height) / 2 + y * Bloc.TAILLE_BLOC, Bloc.TAILLE_BLOC, Bloc.TAILLE_BLOC, 1, Color.Brown, Color.Black);
                    }
                }

            Gfx.remplirRectangle((resolution.Width - tailleGrille.Width) / 2 + (int)(joueur.getPosition().X * Bloc.TAILLE_BLOC) - 8, (resolution.Height - tailleGrille.Height) / 2 + (int)(joueur.getPosition().Y * Bloc.TAILLE_BLOC) - 8, 16, 16, 1, joueur.getCouleur(), joueur.getCouleur());
            Gfx.dessinerTexte(5, 5, 18, Color.Black, "J1 - ({0:0.0}, {1:0.0})", joueur.getPosition().X, joueur.getPosition().Y);
=======
            Gfx.nettoyerEcran(Color.Green);
>>>>>>> 38b087b (AJOUTS & MODIFICATIONS DE CLASSES)
        }

        public new void gererSouris()
        {
            base.gererSouris();
        }

        public new void gererTouches(byte[] etats)
        {
<<<<<<< HEAD
            if(etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_W] > 0)
            {
                joueur.deplacer(0.0f, -1.0f);
            }
            else if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_S] > 0)
            {
                joueur.deplacer(0.0f, 1.0f);
            }
            else if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_A] > 0)
            {
                joueur.deplacer(-1.0f, 0.0f);
            }
            else if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_D] > 0)
            {
                joueur.deplacer(1.0f, 0.0f);
            }
            joueur.mettreAJour(map);
=======
            base.gererTouches(etats);
>>>>>>> 38b087b (AJOUTS & MODIFICATIONS DE CLASSES)
        }
    }
}
