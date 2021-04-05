using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TNTMan.entitees;

namespace TNTMan.ecrans
{
    class Ecran_Score : Ecran
    {
        List<Joueur> joueurs;

        public Ecran_Score(List<Joueur> copie_liste_joueurs) : base("TNTMan - Scores", null)
        {
            Size resolution = Gfx.getResolution();
            arrierePlan = Gfx.images["fond_ecran"];
            boutons.Add(new Bouton(1, resolution.Width / 2 - 100, resolution.Height - 80, 18, "Revenir au menu principal"));
            boutons.Add(new Bouton(2, resolution.Width / 2 - 100, resolution.Height - 40, 18, "Quitter le jeu"));
            boutonSel = 0;
            joueurs = copie_liste_joueurs;
        }

        public override void dessinerEcran(IntPtr rendu)
        {
            base.dessinerEcran(rendu);
            Size resolution = Gfx.getResolution();
            int ecart = 30;
            // On crée un panneau des scores
            Gfx.remplirRectangle(resolution.Width / 2 - 235, 10, 500, resolution.Height / 2 + 100, 1, Color.White, Color.Red);
            Gfx.dessinerTexte(resolution.Width / 2 - 120, 30, 30, Color.Red, "FIN DE LA PARTIE !");
            // On affiche l'en-tête du tableau des scores
            Gfx.dessinerTexte(resolution.Width / 2 - 210, 70, 20, Color.DarkGreen, "Joueur");
            Gfx.dessinerTexte(resolution.Width / 2 - 130, 70, 20, Color.DarkGreen, "Nombre de victoires");
            Gfx.dessinerTexte(resolution.Width / 2 + 80, 70, 20, Color.DarkGreen, "Nombre de tués");
            // On affiche les scores pour tous les joueurs
            foreach (Joueur j in joueurs)
            {
                Gfx.dessinerTexte(resolution.Width / 2 - 210, 70 + ecart, 20, Color.Black, "{0}", j.getId());
                Gfx.dessinerTexte(resolution.Width / 2 - 130, 70 + ecart, 20, Color.Black, "{0}", j.getScore().getNbVictoires());
                Gfx.dessinerTexte(resolution.Width / 2 + 80, 70 + ecart, 20, Color.Black, "{0}", j.getScore().getNbTues());
                ecart += 20;
            }
            // On affiche le numéro du gagnant
            Gfx.dessinerTexte(resolution.Width / 2 - 150, 100 + ecart, 20, Color.DarkGreen, "Le gagnant est ...");
            Gfx.dessinerTexte(resolution.Width / 2 - 70, 120 + ecart, 30, Color.DarkGreen, "Joueur n°{0} !", getGagnantID());
        }

        public override void gererActionBouton(Bouton bouton)
        {
            // Retourner au menu principal
            if (bouton.getId() == 1)
            {
                Gfx.changerEcran(new Ecran_Titre());
            }

            // Quitter le jeu
            if (bouton.getId() == 2)
            {
                Gfx.deinitialiser_2d();
            }
        }

        public int getGagnantID()
        {
            Joueur gagnant = joueurs[0];
            for (int i = 1; i < joueurs.Count; i++)
            {
                if (gagnant.getScore().getNbVictoires() < joueurs[i].getScore().getNbVictoires())
                    gagnant = joueurs[i];
                else if (gagnant.getScore().getNbVictoires() == joueurs[i].getScore().getNbVictoires() && gagnant.getScore().getNbTues() < joueurs[i].getScore().getNbTues())
                    gagnant = joueurs[i];
            }
            return gagnant.getId();
        }
    }
}
