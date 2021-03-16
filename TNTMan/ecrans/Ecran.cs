using System;
using System.Collections.Generic;
using System.Drawing;
using SDL2;

namespace TNTMan.ecrans
{
    class Ecran
    {
        protected String titre;
        protected List<Bouton> boutons;
        protected IntPtr arrierePlan;
        protected Ecran ecranPrecedent;
        protected int boutonSel;

        protected Ecran() { }
        protected Ecran(String _titre, Ecran _ecranPrecedent)
        {
            titre = _titre;
            ecranPrecedent = _ecranPrecedent;
            arrierePlan = IntPtr.Zero;
            boutonSel = -1;
            boutons = new List<Bouton>();
        }

        public virtual void dessinerEcran(IntPtr rendu)
        {
            if (arrierePlan == IntPtr.Zero) return;
        }

        public void gererSouris()
        {
            int x, y;
            uint click = 0;

            // la fenêtre n'est pas active
            if (SDL.SDL_GetMouseFocus() == IntPtr.Zero)
                return;
            
            // récupére l'état des boutons de la souris appuyés
            click = SDL.SDL_GetGlobalMouseState(out x, out y);
            // récupére les coordonnées du curseur
            SDL.SDL_GetMouseState(out x, out y);
            // on itére à travers tout les boutons pour vérifier si notre curseur
            // est dans le cadre du bouton
            foreach (Bouton bouton in boutons)
            {
                if (bouton.curseurDans(new Point(x, y)))
                    boutonSel = bouton.getId();
            }
            // si un bouton est sélectionné
            if (boutonSel != -1)
            {
                // clic gauche détecté
                if ((uint)(click & 1) == 1)
                {
                    gererActionBouton(boutons.Find((b) => b.getId() == boutonSel));
                }
            }
        }

        public virtual void gererTouches(byte[] etats) { }

        public virtual void gererActionBouton(Bouton bouton) { }
    }
}
