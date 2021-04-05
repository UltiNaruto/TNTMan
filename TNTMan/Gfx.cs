using SDL2;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        internal static Dictionary<String, IntPtr> images;

        internal static int prechargerImages()
        {
            try
            {
                images = new Dictionary<String, IntPtr>()
                {
                    { "explosion", chargerImage(@"images\explosion.png") },
                    { "fond_ecran", chargerImage(@"images\fond_ecran.png") },
                    // blocs
                    { "bloc_1", chargerImage(@"images\blocs\bloc_1.png") },
                    { "bloc_2", chargerImage(@"images\blocs\bloc_2.png") },
                    // bombes
                    { "bombe", chargerImage(@"images\bombes\bombe.png") },
                    // bonus
                    { "bonus_bombe_plus", chargerImage(@"images\bonus\bonus_bombe_plus.png") },
                    { "bonus_bombe_moins", chargerImage(@"images\bonus\bonus_bombe_moins.png") },
                    { "bonus_portee_plus", chargerImage(@"images\bonus\bonus_portee_plus.png") },
                    { "bonus_portee_moins", chargerImage(@"images\bonus\bonus_portee_moins.png") },
                    { "bonus_vitesse_plus", chargerImage(@"images\bonus\bonus_vitesse_plus.png") },
                    { "bonus_vitesse_moins", chargerImage(@"images\bonus\bonus_vitesse_moins.png") },
                    // joueurs
                    { "j1_haut", chargerImage(@"images\joueurs\1\haut.png") },
                    { "j1_bas", chargerImage(@"images\joueurs\1\bas.png") },
                    { "j1_gauche", chargerImage(@"images\joueurs\1\gauche.png") },
                    { "j1_droite", chargerImage(@"images\joueurs\1\droite.png") },
                    { "j2_haut", chargerImage(@"images\joueurs\2\haut.png") },
                    { "j2_bas", chargerImage(@"images\joueurs\2\bas.png") },
                    { "j2_gauche", chargerImage(@"images\joueurs\2\gauche.png") },
                    { "j2_droite", chargerImage(@"images\joueurs\2\droite.png") },
                    { "j3_haut", chargerImage(@"images\joueurs\3\haut.png") },
                    { "j3_bas", chargerImage(@"images\joueurs\3\bas.png") },
                    { "j3_gauche", chargerImage(@"images\joueurs\3\gauche.png") },
                    { "j3_droite", chargerImage(@"images\joueurs\3\droite.png") },
                    { "j4_haut", chargerImage(@"images\joueurs\4\haut.png") },
                    { "j4_bas", chargerImage(@"images\joueurs\4\bas.png") },
                    { "j4_gauche", chargerImage(@"images\joueurs\4\gauche.png") },
                    { "j4_droite", chargerImage(@"images\joueurs\4\droite.png") }
                };

                return 0;
            } catch {
                return 1;
            }
        }

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

            if(prechargerImages() > 0)
            {
                Program.MessageErr("prechargerImages: ERREUR ({0})\n", SDL.SDL_GetError());
                SDL_image.IMG_Quit();
                SDL.SDL_DestroyRenderer(rendu);
                SDL.SDL_DestroyWindow(fenetre);
                SDL.SDL_Quit();
                return 5;
            }

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
            if (ecranActuel.GetType() == typeof(Ecran_Jeu) || (temps_actuel - temps_derniere_pression_touche).TotalMilliseconds >= 100)
            {
                ecranActuel.gererSouris();
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

        internal static Size getTailleRectangleTexte(int px, String fmt, params Object[] args)
        {
            int w = 0, h = 0;
            String texte = String.Format(fmt, args);
            if (police[px] == IntPtr.Zero) return new Size(-1, -1);
            SDL_ttf.TTF_SizeText(police[px], texte, out w, out h);
            return new Size(w, h);
        }

        internal static void dessinerTexte(int x, int y, int px, Color couleur, String format, params Object[] args)
        {
            IntPtr surfaceMessage;
            IntPtr texMessage;
            SDL.SDL_Rect boiteMessage;
            Size tailleBoiteMessage;

            if (rendu == IntPtr.Zero || police[px] == IntPtr.Zero) return;

            tailleBoiteMessage = getTailleRectangleTexte(px, format, args);
            surfaceMessage = SDL_ttf.TTF_RenderText_Solid(police[px], String.Format(format, args), new SDL.SDL_Color() { a = couleur.A, r = couleur.R, g = couleur.G, b = couleur.B});

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
