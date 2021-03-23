﻿using SDL2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using TNTMan.ecrans;

namespace TNTMan
{
    class Gfx
    {
        static IntPtr fenetre = IntPtr.Zero;
        static IntPtr rendu = IntPtr.Zero;
        static IntPtr[] police = null;
        static Ecran ecranActuel = null;
        static DateTime temps_derniere_pression_touche = DateTime.Now;
        static public Dictionary<String, IntPtr> images = new Dictionary<string, IntPtr>();

        internal static int initialiser_2d()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_AUDIO) != 0)
            {
                Program.MessageErr("SDL_Init: ERREUR ({0})\n", SDL.SDL_GetError());
                return 1;
            }

            fenetre = SDL.SDL_CreateWindow("TNTMan", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, 640, 480, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            if (fenetre == IntPtr.Zero)
            {
                Program.MessageErr("SDL_CreateWindow: ERREUR ({0})\n", SDL.SDL_GetError());
                SDL.SDL_Quit();
                return 2;
            }

            rendu = SDL.SDL_CreateRenderer(fenetre, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
            if (rendu == IntPtr.Zero)
            {
                Program.MessageErr("SDL_CreateRenderer: ERREUR ({0})\n", SDL.SDL_GetError());
                SDL.SDL_DestroyWindow(fenetre);
                SDL.SDL_Quit();
                return 3;
            }

            if (SDL_ttf.TTF_Init() == -1)
            {
                Program.MessageErr("TTF_Init: ERREUR ({0})", SDL.SDL_GetError());
                SDL.SDL_DestroyRenderer(rendu);
                SDL.SDL_DestroyWindow(fenetre);
                SDL.SDL_Quit();
                return 4;
            }

            police = new IntPtr[73];
            for (int i = 1; i < 73; i++)
            {
                police[i] = SDL_ttf.TTF_OpenFont("res/font.ttf", i);
                if(police[i] == IntPtr.Zero)
                {
                    Program.MessageErr("TTF_OpenFont: TAILLE ({0}), ERREUR ({1})", i, SDL.SDL_GetError());
                    SDL_ttf.TTF_Quit();
                    SDL.SDL_DestroyRenderer(rendu);
                    SDL.SDL_DestroyWindow(fenetre);
                    SDL.SDL_Quit();
                    return 5;
                }
            }

            if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == -1)
            {
                Program.MessageErr("IMG_Init: ERREUR ({0})\n", SDL.SDL_GetError());
                SDL.SDL_DestroyRenderer(rendu);
                SDL.SDL_DestroyWindow(fenetre);
                SDL.SDL_Quit();
                return 4;
            }

            chargerAllImages();

            changerEcran(new Ecran_Titre());

            return 0;
        }

        internal static Size getResolution()
        {
            int w, h;
            if(rendu == IntPtr.Zero)
            {
                Program.MessageErr("Impossible d'obtenir la taille de la fenêtre");
                deinitialiser_2d();
                return new Size(-1, -1);
            }
            SDL.SDL_GetRendererOutputSize(rendu, out w, out h);
            return new Size(w, h);
        }

        internal static void deinitialiser_2d()
        {
            SDL_image.IMG_Quit();
            for(int i = 0;i<73;i++)
            {
                if(police[i] != IntPtr.Zero)
                {
                    SDL_ttf.TTF_CloseFont(police[i]);
                    police[i] = IntPtr.Zero;
                }
            }
            SDL_ttf.TTF_Quit();
            if (rendu != IntPtr.Zero)
            {
                SDL.SDL_DestroyRenderer(rendu);
                rendu = IntPtr.Zero;
            }
            if (fenetre != IntPtr.Zero)
            {
                SDL.SDL_DestroyWindow(fenetre);
                fenetre = IntPtr.Zero;
            }
            SDL.SDL_Quit();
        }

        internal static void nettoyerEcran(Color color)
        {
            SDL.SDL_SetRenderDrawColor(rendu, color.R, color.G, color.B, color.A);
            SDL.SDL_RenderClear(rendu);
        }

        // Début du dessin de l'écran actuel
        internal static void debut2D()
        {
            if (ecranActuel == null) return;
            ecranActuel.dessinerEcran(rendu);
        }

        // Affiche l'écran actuel
        internal static void presenterEcran()
        {
            SDL.SDL_RenderPresent(rendu);
        }

        // Fin du dessin de l'écran actuel et gestion des événements
        internal static void fin2D()
        {
            SDL.SDL_Event _event;
            while(SDL.SDL_PollEvent(out _event) != 0)
            {
                if(_event.type == SDL.SDL_EventType.SDL_QUIT)
                {
                    deinitialiser_2d();
                    return;
                }
            }
        }

        internal static void gererTouches()
        {
            DateTime temps_actuel = DateTime.Now;
            int nb_etats = 0;
            byte[] etats = Utils.PtrToArray(SDL.SDL_GetKeyboardState(out nb_etats), nb_etats);
            if (ecranActuel == null || etats == null) return;
            ecranActuel.gererSouris();
            if (ecranActuel.GetType() == typeof(Ecran_Jouer) || (temps_actuel - temps_derniere_pression_touche).TotalMilliseconds >= 120)
            {
                ecranActuel.gererTouches(etats);
                temps_derniere_pression_touche = temps_actuel;
            }
        }

        internal static void changerEcran(Ecran nouvelEcran)
        {
            ecranActuel = nouvelEcran;
            temps_derniere_pression_touche = DateTime.Now;
            Thread.Sleep(60);
        }

        internal static String chercherCheminImage(String nomImage)
        {
            // Récupération des chemins des fichiers
            string cheminComplet = Directory.GetCurrentDirectory();
            string cheminProjet = cheminComplet.Remove(cheminComplet.IndexOf(@"\bin\Debug"));
            return cheminProjet + nomImage;
        }

        internal static IntPtr chargerImage(String format, params Object[] args)
        {
            String chemin = String.Format(format, args);
            if (rendu == IntPtr.Zero)
            {
                return IntPtr.Zero;
            }
            IntPtr sw_tex = SDL_image.IMG_Load(chemin);
            IntPtr hw_tex = SDL.SDL_CreateTextureFromSurface(rendu, sw_tex);
            SDL.SDL_FreeSurface(sw_tex);
            return hw_tex;
        }

        internal static void dessinerImage(int x, int y, int w, int h, IntPtr image)
        {
            SDL.SDL_Rect rect = new SDL.SDL_Rect();
            rect.x = x;
            rect.y = y;
            rect.w = w;
            rect.h = h;
            if (rendu == IntPtr.Zero) return;
            if (image == IntPtr.Zero) return;
            SDL.SDL_RenderCopy(rendu, image, IntPtr.Zero, ref rect);
        }

        internal static void dessinerImageCentreH(int y, int w, int h, IntPtr image)
        {
            Size resolution = getResolution();
            if (rendu == IntPtr.Zero) return;
            if (image == IntPtr.Zero) return;
            dessinerImage(resolution.Width / 2 - w / 2, y, w, h, image);
        }

        internal static void dessinerImageCentreV(int x, int w, int h, IntPtr image)
        {
            Size resolution = getResolution();
            if (rendu == IntPtr.Zero) return;
            if (image == IntPtr.Zero) return;
            dessinerImage(x, resolution.Height / 2 - h / 2, w, h, image);
        }

        internal static void chargerAllImages()
        {
            // Chargement du fond 
            images.Add("fond_gazon", chargerImage(chercherCheminImage(@"\images\gamescreen_bg.jpg")));
            // Chargement des blocs
            images.Add("bloc_incassable", chargerImage(chercherCheminImage(@"\images\blocs\bloc_incassable.png")));
            images.Add("bloc_terre", chargerImage(chercherCheminImage(@"\images\blocs\bloc_terre.png")));
            // Chargement des bombes
            images.Add("bombe_noire", chargerImage(chercherCheminImage(@"\images\bombes\bombe_noire.png")));
            // Chargement de l'explosion
            images.Add("feu", chargerImage(chercherCheminImage(@"\images\explosion.png")));
            // Chargement du joueur 1
            images.Add("j1_haut", chargerImage(chercherCheminImage(@"\images\joueurs\joueur1_haut.png")));
            images.Add("j1_bas", chargerImage(chercherCheminImage(@"\images\joueurs\joueur1_bas.png")));
            images.Add("j1_gauche", chargerImage(chercherCheminImage(@"\images\joueurs\joueur1_gauche.png")));
            images.Add("j1_droite", chargerImage(chercherCheminImage(@"\images\joueurs\joueur1_droite.png")));
            // Chargement du joueur 2
            images.Add("j2_haut", chargerImage(chercherCheminImage(@"\images\joueurs\joueur2_haut.png")));
            images.Add("j2_bas", chargerImage(chercherCheminImage(@"\images\joueurs\joueur2_bas.png")));
            images.Add("j2_gauche", chargerImage(chercherCheminImage(@"\images\joueurs\joueur2_gauche.png")));
            images.Add("j2_droite", chargerImage(chercherCheminImage(@"\images\joueurs\joueur2_droite.png")));
            // Chargement du joueur 3
            images.Add("j3_haut", chargerImage(chercherCheminImage(@"\images\joueurs\joueur3_haut.png")));
            images.Add("j3_bas", chargerImage(chercherCheminImage(@"\images\joueurs\joueur3_bas.png")));
            images.Add("j3_gauche", chargerImage(chercherCheminImage(@"\images\joueurs\joueur3_gauche.png")));
            images.Add("j3_droite", chargerImage(chercherCheminImage(@"\images\joueurs\joueur3_droite.png")));
            // Chargement du joueur 4
            images.Add("j4_haut", chargerImage(chercherCheminImage(@"\images\joueurs\joueur4_haut.png")));
            images.Add("j4_bas", chargerImage(chercherCheminImage(@"\images\joueurs\joueur4_bas.png")));
            images.Add("j4_gauche", chargerImage(chercherCheminImage(@"\images\joueurs\joueur4_gauche.png")));
            images.Add("j4_droite", chargerImage(chercherCheminImage(@"\images\joueurs\joueur4_droite.png")));
        }

        internal static void dessinerRectangle(int x, int y, int w, int h, int px, Color couleur)
        {
            SDL.SDL_Rect rect = new SDL.SDL_Rect();
            rect.x = x;
            rect.y = y;
            rect.w = w;
            rect.h = h;
            if (rendu == IntPtr.Zero) return;
            SDL.SDL_RenderSetScale(rendu, (float)px, (float)px);
            SDL.SDL_SetRenderDrawColor(rendu, couleur.R, couleur.G, couleur.B, couleur.A);
            SDL.SDL_RenderDrawRect(rendu, ref rect);
            SDL.SDL_RenderSetScale(rendu, 1.0f, 1.0f);
        }

        internal static void remplirRectangle(int x, int y, int w, int h, int px, Color couleur_remplissage, Color couleur)
        {
            SDL.SDL_Rect rect = new SDL.SDL_Rect();
            rect.x = x;
            rect.y = y;
            rect.w = w;
            rect.h = h;
            if (rendu == IntPtr.Zero) return;
            SDL.SDL_SetRenderDrawColor(rendu, couleur_remplissage.R, couleur_remplissage.G, couleur_remplissage.B, couleur_remplissage.A);
            SDL.SDL_RenderFillRect(rendu, ref rect);
            dessinerRectangle(x, y, w, h, px, couleur);
        }

        internal static Size getTailleRectangleTexte(String texte, int px)
        {
            int w = 0, h = 0;
            if (police[px] == IntPtr.Zero) return new Size(-1, -1);
            SDL_ttf.TTF_SizeText(police[px], texte, out w, out h);
            return new Size(w, h);
        }

        internal static void dessinerTexte(int x, int y, int px, Color couleur, String format, params Object[] args)
        {
            String message = String.Format(format, args);
            IntPtr surfaceMessage;
            IntPtr texMessage;
            SDL.SDL_Rect boiteMessage;
            Size tailleBoiteMessage;

            if (rendu == IntPtr.Zero || police[px] == IntPtr.Zero) return;

            tailleBoiteMessage = getTailleRectangleTexte(message, px);
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(police[px], message, new SDL.SDL_Color() { a = couleur.A, r = couleur.R, g = couleur.G, b = couleur.B});

            if (surfaceMessage == IntPtr.Zero) return;

            texMessage = SDL.SDL_CreateTextureFromSurface(rendu, surfaceMessage);

            if (texMessage == IntPtr.Zero)
            {
                SDL.SDL_FreeSurface(surfaceMessage);
                surfaceMessage = IntPtr.Zero;
                return;
            }
            boiteMessage = new SDL.SDL_Rect() { x = x, y = y, w = tailleBoiteMessage.Width, h = tailleBoiteMessage.Height };
            SDL.SDL_RenderCopy(rendu, texMessage, IntPtr.Zero, ref boiteMessage);
            SDL.SDL_FreeSurface(surfaceMessage);
            surfaceMessage = IntPtr.Zero;
            SDL.SDL_DestroyTexture(texMessage);
            texMessage = IntPtr.Zero;
        }
    }
}
