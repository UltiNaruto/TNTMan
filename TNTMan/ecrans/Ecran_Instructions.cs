using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TNTMan.ecrans
{
    class Ecran_Instructions : Ecran
    {
        public Ecran_Instructions() : base("Instructions", null)
        {
            Size resolution = Gfx.getResolution();
            boutons.Add(new Bouton(0, 45, resolution.Height - 30, 18, "Retour"));
        }

        public override void dessinerEcran(IntPtr rendu)
        {
            base.dessinerEcran(rendu);
            Size resolution = Gfx.getResolution();
            Gfx.nettoyerEcran(Color.SkyBlue);
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
            base.gererTouches(etats);

        }

        public override void gererActionBouton(Bouton bouton)
        {
            // Retourner au menu principal
            if (bouton.getId() == 0)
            {
                Gfx.changerEcran(new Ecran_Titre());
            }
        }
    }
}
