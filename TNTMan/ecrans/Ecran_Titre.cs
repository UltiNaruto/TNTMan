using System;
using System.Drawing;

namespace TNTMan.ecrans
{
    class Ecran_Titre : Ecran
    {
        public Ecran_Titre() : base("TNTMan", null)
        {
            Size resolution = Gfx.getResolution();
            boutons.Add(new Bouton(0, resolution.Width / 2 - 100, resolution.Height / 2 - 100, 18, "Nouvelle Partie"));
            boutons.Add(new Bouton(1, resolution.Width / 2 - 100, resolution.Height / 2 - 50, 18, "Instructions"));
            boutons.Add(new Bouton(2, resolution.Width / 2 - 100, resolution.Height / 2, 18, "Options"));
            boutons.Add(new Bouton(100, resolution.Width / 2 - 100, resolution.Height / 2 + 150, 18, "Quitter"));
        }

        public override void dessinerEcran(IntPtr rendu)
        {
            base.dessinerEcran(rendu);
            Size resolution = Gfx.getResolution();
            Gfx.nettoyerEcran(Color.SkyBlue);
            Gfx.dessinerTexte(resolution.Width / 2 - 100, 60, 50, Color.Red, titre);
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
            // Jouer
            if (bouton.getId() == 0)
            {
                Gfx.changerEcran(new Ecran_Jouer());
            }

            // Quitter
            if (bouton.getId() == 100)
            {
                Gfx.deinitialiser_2d();
            }
        }
    }
}