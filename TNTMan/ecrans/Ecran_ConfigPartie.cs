using SDL2;
using System;
using System.Drawing;
using System.Linq;
using TNTMan.map;

namespace TNTMan.ecrans
{
    class Ecran_ConfigPartie : Ecran
    {
        int id_map = 0;
        // 2-4 joueurs
        int nb_joueurs = 2;
        // 1-9 manches
        int nb_manches = 3;
        // 1-9 min
        int temps_manche = 180;

        public Ecran_ConfigPartie(Ecran _ecranPrecedent) : base("Configuration de la partie", _ecranPrecedent)
        {
            Size resolution = Gfx.getResolution();
            Size taille_rect_nb_joueurs_val = Gfx.getTailleRectangleTexte(20, "{0}", nb_joueurs);
            Size taille_rect_nb_manches_val = Gfx.getTailleRectangleTexte(20, "{0}", nb_manches);
            Size taille_rect_temps_manche_val = Gfx.getTailleRectangleTexte(20, "{0}:{1:00}", temps_manche / 60, temps_manche % 60);

            arrierePlan = Gfx.images["fond_ecran"];

            boutons.Add(new Bouton(0, (resolution.Width / 2 - taille_rect_nb_joueurs_val.Width / 2) - 40, 150, 30, 30, 20, "<"));
            boutons.Add(new Bouton(1, (resolution.Width / 2 - taille_rect_nb_joueurs_val.Width / 2) + 20, 150, 30, 30, 20, ">"));

            boutons.Add(new Bouton(2, (resolution.Width / 2 - taille_rect_nb_manches_val.Width / 2) - 40, 220, 30, 30, 20, "<"));
            boutons.Add(new Bouton(3, (resolution.Width / 2 - taille_rect_nb_manches_val.Width / 2) + 20, 220, 30, 30, 20, ">"));

            boutons.Add(new Bouton(4, (resolution.Width / 2 - taille_rect_temps_manche_val.Width / 2) - 40, 290, 30, 30, 20, "<"));
            boutons.Add(new Bouton(5, (resolution.Width / 2 - taille_rect_temps_manche_val.Width / 2) + 50, 290, 30, 30, 20, ">"));

            boutons.Add(new Bouton(6, resolution.Width / 3, resolution.Height - 60, 20, "Jouer"));
            boutons.Add(new Bouton(100, resolution.Width / 3, resolution.Height - 30, 20, "Retour"));
        }

        public override void dessinerEcran(IntPtr rendu)
        {
            base.dessinerEcran(rendu);

            Size resolution = Gfx.getResolution();
            Size taille_rect_titre = Gfx.getTailleRectangleTexte(38, titre);

            String lbl_nb_joueurs = "Nombre de joueurs";
            Size taille_rect_nb_joueurs = Gfx.getTailleRectangleTexte(24, lbl_nb_joueurs);
            Size taille_rect_nb_joueurs_val = Gfx.getTailleRectangleTexte(20, "{0}", nb_joueurs);

            String lbl_nb_manches = "Nombre de manches";
            Size taille_rect_nb_manches = Gfx.getTailleRectangleTexte(24, lbl_nb_manches);
            Size taille_rect_nb_manches_val = Gfx.getTailleRectangleTexte(20, "{0}", nb_manches);

            String lbl_temps_manche = "Durée de la manche";
            Size taille_rect_temps_manche = Gfx.getTailleRectangleTexte(24, lbl_nb_manches);
            Size taille_rect_temps_manche_val = Gfx.getTailleRectangleTexte(20, "{0}:{1:00}", temps_manche / 60, temps_manche % 60);

            Gfx.dessinerTexte(resolution.Width / 2 - taille_rect_titre.Width / 2, 60, 38, Color.Red, titre);

            Gfx.dessinerTexte(resolution.Width / 2 - taille_rect_nb_joueurs.Width / 2, 120, 24, Color.Black, lbl_nb_joueurs);
            Gfx.dessinerTexte(resolution.Width / 2 - taille_rect_nb_joueurs_val.Width / 2, 155, 20, Color.Black, "{0}", nb_joueurs);

            Gfx.dessinerTexte(resolution.Width / 2 - taille_rect_nb_manches.Width / 2, 190, 24, Color.Black, lbl_nb_manches);
            Gfx.dessinerTexte(resolution.Width / 2 - taille_rect_nb_manches_val.Width / 2, 225, 20, Color.Black, "{0}", nb_manches);

            Gfx.dessinerTexte(resolution.Width / 2 - taille_rect_temps_manche.Width / 2, 260, 24, Color.Black, lbl_temps_manche);
            Gfx.dessinerTexte(resolution.Width / 2 - taille_rect_temps_manche_val.Width / 2, 295, 20, Color.Black, "{0}:{1:00}", temps_manche / 60, temps_manche % 60);
            foreach (Bouton bouton in boutons)
            {
                bouton.dessiner(rendu, bouton.getId() == boutonSel);
            }
        }

        public override void gererTouches(byte[] etats)
        {
            // Sélection du bouton précédent dans le menu
            if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_UP] > 0)
            {
                // si le bouton est le premier on retourne au dernier bouton
                // sinon on cherche le dernier bouton avant le bouton sélectionné
                if (boutonSel == boutons.First().getId())
                {
                    boutonSel = boutons.Last().getId();
                }
                else
                {
                    boutonSel = boutons.Last((b) => b.getId() < boutonSel).getId();
                }
            } // Sélection du bouton suivant dans le menu
            else if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_DOWN] > 0)
            {
                // si le bouton est le dernier on retourne au premier bouton
                // sinon on cherche le premier bouton après le bouton sélectionné
                if (boutonSel == boutons.Last().getId())
                {
                    boutonSel = boutons.First().getId();
                }
                else
                {
                    boutonSel = boutons.Find((b) => b.getId() > boutonSel).getId();
                }
            } // Equivalent du clic gauche pour sélectionner un bouton dans le menu
            else if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_RETURN] > 0)
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
            // valeur de nombre de joueurs diminue
            if (bouton.getId() == 0)
            {
                if (nb_joueurs > 2)
                    nb_joueurs--;
            }
            // valeur de nombre de joueurs augmente
            if (bouton.getId() == 1)
            {
                if (nb_joueurs < 4)
                    nb_joueurs++;
            }
            // valeur de nombre de manches diminue
            if (bouton.getId() == 2)
            {
                if (nb_manches > 1)
                    nb_manches--;
            }
            // valeur de nombre de manches augmente
            if (bouton.getId() == 3)
            {
                if (nb_manches < 9)
                    nb_manches++;
            }
            // valeur de temps de la manche diminue
            if (bouton.getId() == 4)
            {
                if (temps_manche > 60)
                    temps_manche -= 60;
            }
            // valeur de temps de la manche augmente
            if (bouton.getId() == 5)
            {
                if (temps_manche < 9 * 60)
                    temps_manche += 60;
            }

            // lancer la partie
            if (bouton.getId() == 6)
            {
                Gfx.changerEcran(new Ecran_Jeu(new Session(nb_joueurs, id_map, nb_manches, temps_manche, temps_manche / 6)));
            }

            // retour au menu principal
            if (bouton.getId() == 100)
            {
                Gfx.changerEcran(ecranPrecedent);
            }
        }
    }
}
