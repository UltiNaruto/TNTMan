using SDL2;
using System;
using System.Drawing;
using System.Threading;

namespace TNTMan
{
    class Gfx
    {
        static IntPtr fenetre = IntPtr.Zero;
        static IntPtr rendu = IntPtr.Zero;

        internal static int initialiser_2d()
        {
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_AUDIO) != 0)
            {
                Console.WriteLine("ERREUR ({0})\n", SDL_error.SDL_GetError());
                Thread.Sleep(1000);
                return 1;
            }

            fenetre = SDL_video.SDL_CreateWindow("TNTMan", SDL_video.SDL_WINDOWPOS_UNDEFINED_DISPLAY(0), SDL_video.SDL_WINDOWPOS_UNDEFINED_DISPLAY(0), 640, 480, (uint)SDL_video.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            if (fenetre == null)
            {
                Console.WriteLine("ERREUR ({0})\n", SDL_error.SDL_GetError());
                Thread.Sleep(1000);
                return 2;
            }

            rendu = SDL_render.SDL_CreateRenderer(fenetre, -1, (uint)(SDL_render.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL_render.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC));
            if (rendu == null)
            {
                Console.WriteLine("ERREUR ({0})\n", SDL_error.SDL_GetError());
                Thread.Sleep(1000);
                return 3;
            }

            // chargement de la police d'écriture
            // chargement des images
            // initialisation du premier écran

            return 0;
        }

        internal static void deinitialiser_2d()
        {
            // décharger les images
            // décharger la police d'écriture
            if (rendu != IntPtr.Zero)
            {
                SDL_render.SDL_DestroyRenderer(rendu);
                rendu = IntPtr.Zero;
            }
            if (fenetre != IntPtr.Zero)
            {
                SDL_video.SDL_DestroyWindow(fenetre);
                fenetre = IntPtr.Zero;
            }
            SDL.SDL_Quit();
        }

        internal static void nettoyerEcran(Color color)
        {
            SDL_render.SDL_SetRenderDrawColor(rendu, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
            SDL_render.SDL_RenderClear(rendu);
        }

        // Début du dessin de l'écran actuel
        internal static void debut2D()
        {

        }

        // Affiche l'écran actuel
        internal static void presenterEcran()
        {
            SDL_render.SDL_RenderPresent(rendu);
        }

        // Fin du dessin de l'écran actuel et gestion des événements
        internal static void fin2D()
        {
            SDL_events.SDL_Event _event;
            while(SDL_events.SDL_PollEvent(out _event) != 0)
            {
                if(_event.type == (uint)SDL_events.SDL_EventType.SDL_QUIT)
                {
                    deinitialiser_2d();
                    return;
                }
            }
        }

        internal static void gererTouches()
        {

        }
    }
}
