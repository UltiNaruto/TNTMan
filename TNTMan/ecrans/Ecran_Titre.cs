﻿using SDL2;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

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
            boutonSel = 0;
        }

        public override void dessinerEcran(IntPtr rendu)
        {
            base.dessinerEcran(rendu);
            Size resolution = Gfx.getResolution();
            Gfx.dessinerImageCentreH(0, resolution.Width, resolution.Height, Gfx.images["fond_gazon"]);
            // Affichage du texte
            Gfx.dessinerTexte(resolution.Width / 2 - 100, 60, 50, Color.Red, titre);
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
                gererActionBouton(boutons.Find((b) => b.getId() == boutonSel));
            }
        }

        public override void gererActionBouton(Bouton bouton)
        {
            // Jouer
            if (bouton.getId() == 0)
            {
                Gfx.changerEcran(new Ecran_Jouer());
            }

            // Consulter les instructions
            if (bouton.getId() == 1)
            {
                Gfx.changerEcran(new Ecran_Instructions(this));
            }

            // Quitter
            if (bouton.getId() == 100)
            {
                Gfx.deinitialiser_2d();
            }
        }
    }
}