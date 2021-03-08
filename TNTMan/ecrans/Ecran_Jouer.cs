using SDL2;
using System;
using System.Collections.Generic;
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
            joueur = new Joueur(4, 1.25f, 1.25f);
            map = new Map();
            map.chargerMapParDefaut();
        }

        // taille bloc 32x32
        // grille 15x11 (bords inclus)
        public override void dessinerEcran(IntPtr rendu)
        {
            base.dessinerEcran(rendu);
            Bombe[] bombes = map.getToutLesBombes();
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

            joueur.dessiner(rendu);
            Gfx.dessinerTexte(5, 5, 18, Color.Black, "J1 - ({0:0.0}, {1:0.0})", joueur.getPosition().X, joueur.getPosition().Y);
            Gfx.dessinerTexte(5, 460, 18, Color.Black, "Bombes restantes : {0}", joueur.getNbBombes());
            // Affichage des bombes posées à l'écran
            if (bombes != null)
            {
                foreach (Bombe b in bombes)
                {
                    b.dessine(rendu);
                }
                // TEMPORAIRE - Affichage du temps restant avant explosion de la 1ère bombe posée
                Gfx.dessinerTexte(5, 30, 18, Color.Black, "Temps avant explosion de la plus ancienne bombe : {0} ms ", bombes[0].getTempsExplosion());
            }
        }

        public override void gererSouris()
        {
        }

        public override void gererTouches(byte[] etats)
        {
            Bombe[] bombes = null;

            if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_W] > 0)
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
            if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_SPACE] > 0)
            {
                map.ajoutEntite(joueur.poserBombe());
            }
            bombes = map.getToutLesBombes();
            if (bombes != null)
            {
                foreach (Bombe b in bombes)
                {
                    if (!b.getStatut())
                    {
                        map.supprimerEntite(b);
                    }
                    else
                    {
                        b.mettreAJour(map);
                    }
                }
            }
        }
    }
}
