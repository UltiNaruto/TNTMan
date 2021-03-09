using SDL2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TNTMan.ecrans
{
    class Ecran_Titre : Ecran
    {
        public Ecran_Titre() : base("TNTMan", null)
        {
            Size resolution = Gfx.getResolution();
            boutons.Add(new Bouton(resolution.Width / 2 - 100, resolution.Height / 2 - 100, 200, 30, Color.FloralWhite, Color.Black, Color.LightGoldenrodYellow, Color.Red, "Nouvelle Partie", new Ecran_Jouer()));
            boutons.Add(new Bouton(resolution.Width / 2 - 100, resolution.Height / 2 - 50, 200, 30, Color.FloralWhite, Color.Black, Color.LightGoldenrodYellow, Color.Red, "Instructions", new Ecran_Jouer()));
            boutons.Add(new Bouton(resolution.Width / 2 - 100, resolution.Height / 2, 200, 30, Color.FloralWhite, Color.Black, Color.LightGoldenrodYellow, Color.Red, "Options", new Ecran_Jouer()));
        }

        public override void dessinerEcran(IntPtr rendu)
        {
            base.dessinerEcran(rendu);
            Size resolution = Gfx.getResolution();
            Gfx.nettoyerEcran(Color.SkyBlue);
            Gfx.dessinerTexte(resolution.Width / 2 - 100, 60, 50, Color.Red, titre);
            dessinerBoutons(rendu);
        }

        public override void gererSouris()
        {
            Point curseur = new Point();
            mettreAJourBoutons(curseur);
        }
    }
}
