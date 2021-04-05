using SDL2;
using System;
using System.Drawing;

namespace TNTMan.ecrans
{
    class Ecran_Pause : Ecran
    {
        DateTime tempsDebutPause;
        public Ecran_Pause(Ecran_Jeu precedent) : base("TNTMan - Pause", precedent)
        {
            Size resolution = Gfx.getResolution();
            Size taille_rect_quitter = Gfx.getTailleRectangleTexte(18, "Quitter la partie");
            arrierePlan = Gfx.images["fond_ecran"];
            boutons.Add(new Bouton(0, resolution.Width / 2 - taille_rect_quitter.Width / 2, resolution.Height / 2 + 20, taille_rect_quitter.Width, 20, 18, "Continuer"));
            boutons.Add(new Bouton(1, resolution.Width / 2 - taille_rect_quitter.Width / 2, resolution.Height / 2 + 40, taille_rect_quitter.Width, 20, 18, "Quitter la partie"));
            boutons.Add(new Bouton(100, resolution.Width / 2 - taille_rect_quitter.Width / 2, resolution.Height / 2 + 80, taille_rect_quitter.Width, 20, 18, "Quitter le jeu"));
            tempsDebutPause = DateTime.Now;
            boutonSel = 0;
        }

        public override void dessinerEcran(IntPtr rendu)
        {
            base.dessinerEcran(rendu);
            Size resolution = Gfx.getResolution();
            Gfx.remplirRectangle(resolution.Width / 2 - 225, resolution.Height / 2 - 125, 450, 250, 1, Color.White, Color.Red);
            Gfx.dessinerTexte(resolution.Width / 2 - 150, resolution.Height / 2 - 100, 30, Color.Red, "PAUSE !");
            Gfx.dessinerTexte(resolution.Width / 2 - 175, resolution.Height / 2 - 25, 20, Color.Red, "Que voulez-vous faire ?");

            foreach (Bouton bouton in boutons)
            {
                bouton.dessiner(rendu, bouton.getId() == boutonSel);
            }
        }

        public override void gererActionBouton(Bouton bouton)
        {
            // Continuer la partie
            if (bouton.getId() == 0)
            {
                ((Ecran_Jeu)ecranPrecedent).retourEnJeu((long)(DateTime.Now - tempsDebutPause).TotalMilliseconds);
                Gfx.changerEcran(ecranPrecedent);
            }

            // Quitter la session
            if (bouton.getId() == 1)
            {
                Gfx.changerEcran(new Ecran_Titre());
            }

            // Quitter le jeu
            if (bouton.getId() == 100)
            {
                Sfx.deinitialiser_son();
                Gfx.deinitialiser_2d();
                SDL.SDL_Quit();
            }
        }
    }
}
