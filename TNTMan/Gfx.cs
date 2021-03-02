using SDL2;
using System;
using System.Drawing;
using TNTMan.ecrans;

namespace TNTMan
{
    class Gfx
    {
        static IntPtr fenetre = IntPtr.Zero;
        static IntPtr rendu = IntPtr.Zero;
        static Ecran ecranActuel = null;
        static long temps_derniere_pression_touche;

        internal static int initialiser_2d()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_AUDIO) != 0)
            {
                Program.MessageErr("ERREUR ({0})\n", SDL.SDL_GetError());
                return 1;
            }

            fenetre = SDL.SDL_CreateWindow("TNTMan", SDL.SDL_WINDOWPOS_UNDEFINED, SDL.SDL_WINDOWPOS_UNDEFINED, 640, 480, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            if (fenetre == IntPtr.Zero)
            {
                Program.MessageErr("ERREUR ({0})\n", SDL.SDL_GetError());
                SDL.SDL_Quit();
                return 2;
            }

            rendu = SDL.SDL_CreateRenderer(fenetre, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
            if (rendu == IntPtr.Zero)
            {
                Program.MessageErr("ERREUR ({0})\n", SDL.SDL_GetError());
                SDL.SDL_DestroyWindow(fenetre);
                SDL.SDL_Quit();
                return 3;
            }

            // chargement de la police d'écriture
            if(SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == -1)
            {
                Program.MessageErr("ERREUR ({0})\n", SDL.SDL_GetError());
                SDL.SDL_DestroyRenderer(rendu);
                SDL.SDL_DestroyWindow(fenetre);
                SDL.SDL_Quit();
                return 4;
            }
            changerEcran(new Ecran_Jouer());

            return 0;
        }

        internal static void deinitialiser_2d()
        {
            SDL_image.IMG_Quit();
            // décharger la police d'écriture
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
            SDL.SDL_SetRenderDrawColor(rendu, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
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
            long temps_actuel = DateTime.Now.ToFileTime();
            int nb_etats = 0;
            byte[] etats = Utils.PtrToArray(SDL.SDL_GetKeyboardState(out nb_etats), nb_etats);
            if (ecranActuel == null || etats == null) return;
            ecranActuel.gererSouris();
            if (ecranActuel.GetType() == typeof(Ecran_Jouer) || temps_actuel - temps_derniere_pression_touche >= 120)
            {
                ecranActuel.gererTouches(etats);
                temps_derniere_pression_touche = temps_actuel;
            }
        }

        internal static void changerEcran(Ecran nouvelEcran)
        {
            ecranActuel = nouvelEcran;
        }

        internal static IntPtr chargerImage(String format, params Object[] args)
        {
            Program.MessageErr("chargerImage() : Non implémenté");
            deinitialiser_2d();
            return IntPtr.Zero;
        }
    }
}
