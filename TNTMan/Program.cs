using System;
using System.Drawing;
using System.Threading;
using SDL2;

namespace TNTMan
{
    class Program
    {
        public static Random random;

        static void Main(string[] args)
        {
            random = new Random(DateTime.Now.DayOfYear * DateTime.Now.Day);
            if (Gfx.initialiser_2d() != 0)
            {
                Console.WriteLine("2D non initialisé! Fermeture...");
                Thread.Sleep(1000);
                return;
            }

            while(SDL.SDL_WasInit(SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_AUDIO) != 0)
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
