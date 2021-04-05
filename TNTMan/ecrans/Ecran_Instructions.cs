using SDL2;
using System;
using System.Drawing;

namespace TNTMan.ecrans
{
    class Ecran_Instructions : Ecran
    {
        int page = 0;
        public Ecran_Instructions(Ecran precedent) : base("Instructions", precedent)
        {
            Size resolution = Gfx.getResolution();
            Size taille_rect_page = Gfx.getTailleRectangleTexte(18, "{0}", page + 1);
            arrierePlan = Gfx.images["fond_ecran"];

            boutons.Add(new Bouton(0, (resolution.Width / 2 - taille_rect_page.Width / 2) - 36, resolution.Height - 60, 28, 28, 18, "<"));
            boutons.Add(new Bouton(1, (resolution.Width / 2 - taille_rect_page.Width / 2) + 18, resolution.Height - 60, 28, 28, 18, ">"));

            boutons.Add(new Bouton(100, resolution.Width / 3, resolution.Height - 30, 18, "Retour"));
        }

        public override void dessinerEcran(IntPtr rendu)
        {
            base.dessinerEcran(rendu);
            Size resolution = Gfx.getResolution();
            Size taille_rect_titre = Gfx.getTailleRectangleTexte(50, titre);
            Size taille_rect_page = Gfx.getTailleRectangleTexte(18, "{0}", page + 1);

            Gfx.dessinerTexte(resolution.Width / 2 - taille_rect_titre.Width / 2, 60, 50, Color.Red, titre);
            Gfx.dessinerTexte(resolution.Width / 2 - taille_rect_page.Width / 2, resolution.Height - 56, 18, Color.Black, "{0}", page + 1);
            if (page == 0)
            {
                Gfx.dessinerTexte(45, 150, 20, Color.Blue, "Joueur 1");
                Gfx.dessinerTexte(45, 180, 20, Color.Blue, "Se déplacer : Z (haut), S (bas), Q (gauche), D (droite)");
                Gfx.dessinerTexte(45, 210, 20, Color.Blue, "Poser une bombe : E");

                Gfx.dessinerTexte(45, 270, 20, Color.Red, "Joueur 2");
                Gfx.dessinerTexte(45, 300, 20, Color.Red, "Se déplacer : Haut, Bas, Gauche, Droite");
                Gfx.dessinerTexte(45, 330, 20, Color.Red, "Poser une bombe : Entrer");
            }

            if (page == 1)
            {
                Gfx.dessinerTexte(45, 150, 20, Color.DarkGreen, "Joueur 3");
                Gfx.dessinerTexte(45, 180, 20, Color.DarkGreen, "Se déplacer : U (haut), J (bas), H (gauche), K (droite)");
                Gfx.dessinerTexte(45, 210, 20, Color.DarkGreen, "Poser une bombe : I");

                Gfx.dessinerTexte(45, 270, 20, Color.Pink, "Joueur 4");
                Gfx.dessinerTexte(45, 300, 20, Color.Pink, "Se déplacer : Pavé numérique 5 (haut), Pavé numérique 2 (bas),");
                Gfx.dessinerTexte(45, 330, 20, Color.Pink, "Pavé numérique 1 (gauche), Pavé numérique 3 (droite)");
                Gfx.dessinerTexte(45, 360, 20, Color.Pink, "Poser une bombe : Pavé numérique 6");
            }

            foreach (Bouton bouton in boutons)
            {
                bouton.dessiner(rendu, bouton.getId() == boutonSel);
            }
        }

        public override void gererTouches(byte[] etats)
        {
            // Retour au menu précédent
            if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_ESCAPE] > 0)
            {
                Gfx.changerEcran(ecranPrecedent);
            }

            // page précédente
            if(etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_LEFT] > 0)
            {
                gererActionBouton(boutons.Find((b) => b.getId() == 0));
            }

            // page suivante
            if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_RIGHT] > 0)
            {
                gererActionBouton(boutons.Find((b) => b.getId() == 1));
            }

            // Equivalent du clic gauche pour sélectionner un bouton dans le menu
            if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_RETURN] > 0)
            {
                if (boutonSel != -1)
                {
                    Sfx.JouerSon("clic_bouton");
                    gererActionBouton(boutons.Find((b) => b.getId() == boutonSel));
                }
            }

        }

        public override void gererActionBouton(Bouton bouton)
        {
            // page précédente
            if (bouton.getId() == 0)
            {
                if (page > 0)
                    page--;
            }
            // page suivant
            if (bouton.getId() == 1)
            {
                if (page < 1)
                    page++;
            }
            // Retourner au menu principal
            if (bouton.getId() == 100)
            {
                Gfx.changerEcran(ecranPrecedent);
            }
        }
    }
}
