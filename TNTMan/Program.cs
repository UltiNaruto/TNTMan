using System;
using System.Drawing;
using System.Threading;
using SDL2;

namespace TNTMan
{
    class Program
    {
        public static Random random;

        public static void MessageErr(String format, params Object[] args)
        {
            SDL.SDL_ShowSimpleMessageBox(
                SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR,
                "TNTMan",
                String.Format(format, args),
                IntPtr.Zero
            );
        }

        static void Main(string[] args)
        {
            random = new Random((int)DateTime.Now.Ticks);

            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) != 0)
            {
                MessageErr("SDL_Init: ERREUR ({0})\n", SDL.SDL_GetError());
                return;
            }

            if (Sfx.initialiser_son() != 0)
            {
                MessageErr("Son non initialisé! Fermeture...");
                return;
            }

            if (Gfx.initialiser_2d() != 0)
            {
                MessageErr("2D non initialisé! Fermeture...");
                return;
            }

            while (SDL.SDL_WasInit(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_AUDIO) != 0)
            {
                Gfx.nettoyerEcran(Color.Black);
                Gfx.debut2D();
                Gfx.presenterEcran();
                Gfx.fin2D();
                Gfx.gererTouches();
                // IPS fixées à 60
                Thread.Sleep(16);
            }
        }
    }
}
