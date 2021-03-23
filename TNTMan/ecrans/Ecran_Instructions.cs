using SDL2;
using System;
using System.Drawing;
using System.IO;

namespace TNTMan.ecrans
{
    class Ecran_Instructions : Ecran
    {
        public Ecran_Instructions(Ecran precedent) : base("Instructions", precedent)
        {
            Size resolution = Gfx.getResolution();
            boutons.Add(new Bouton(0, 45, resolution.Height - 30, 18, "Retour"));
        }

        public override void dessinerEcran(IntPtr rendu)
        {
            base.dessinerEcran(rendu);
            Size resolution = Gfx.getResolution();
            Gfx.dessinerImageCentreH(0, resolution.Width, resolution.Height, Gfx.images["fond_gazon"]);
            Gfx.dessinerTexte(resolution.Width / 2 - 100, 60, 50, Color.Red, titre);
            Gfx.dessinerTexte(45, 150, 20, Color.Red, "Se déplacer : Z (haut), S (bas), Q (gauche), D (droite)");
            Gfx.dessinerTexte(45, 200, 20, Color.Red, "Poser une bombe : ESPACE");
            foreach (Bouton bouton in boutons)
            {
                bouton.dessiner(rendu, bouton.getId() == boutonSel);
            }
        }

        public override void gererTouches(byte[] etats)
        {
            if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_ESCAPE] > 0)
            {
                Gfx.changerEcran(ecranPrecedent);
            }
            
            // Equivalent du clic gauche pour sélectionner un bouton dans le menu
            if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_RETURN] > 0)
            {
                gererActionBouton(boutons.Find((b) => b.getId() == boutonSel));
            }

        }

        public override void gererActionBouton(Bouton bouton)
        {
            // Retourner au menu principal
            if (bouton.getId() == 0)
            {
                Gfx.changerEcran(ecranPrecedent);
            }
        }
    }
}
