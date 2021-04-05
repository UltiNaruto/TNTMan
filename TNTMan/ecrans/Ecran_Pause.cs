using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TNTMan.map;

namespace TNTMan.ecrans
{
    class Ecran_Pause : Ecran
    {
        public Ecran_Pause(Ecran_Jeu precedent) : base("TNTMan - Scores", precedent)
        {
            Size resolution = Gfx.getResolution();
            Size taille_rect_continuer = Gfx.getTailleRectangleTexte(18, "Continuer");
            Size taille_rect_quitter = Gfx.getTailleRectangleTexte(18, "Quitter");
            arrierePlan = Gfx.images["fond_ecran"];
            boutons.Add(new Bouton(0, resolution.Width / 2 - taille_rect_continuer.Width / 2, resolution.Height / 20 + 20, taille_rect_continuer.Width, taille_rect_continuer.Height, 18, "Continuer"));
            boutons.Add(new Bouton(100, resolution.Width / 2 - taille_rect_quitter.Width / 2, resolution.Height / 20 + 40, taille_rect_quitter.Width, taille_rect_quitter.Height, 18, "Quitter"));
            boutonSel = 0;
        }

        public override void dessinerEcran(IntPtr rendu)
        {
            base.dessinerEcran(rendu);
            Size resolution = Gfx.getResolution();
            Gfx.remplirRectangle(resolution.Width / 2 - 225, resolution.Height / 2 - 125, 450, 250, 1, Color.White, Color.Red);
            Gfx.dessinerTexte(resolution.Width / 2 - 150, resolution.Height / 2 - 100, 30, Color.Red, "PAUSE !");
            Gfx.dessinerTexte(resolution.Width / 2 - 175, resolution.Height / 2 - 25, 20, Color.Red, "Que voulez-vous faire ?");
        }

        public override void gererActionBouton(Bouton bouton)
        {
            // Continuer la partie
            if (bouton.getId() == 0)
            {
                //ecranPrecedent.session.enPause = false;
                Gfx.changerEcran(ecranPrecedent);
            }

            // Quitter la session
            if (bouton.getId() == 100)
                Gfx.changerEcran(new Ecran_Titre());
        }
    }
}
