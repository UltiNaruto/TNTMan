using SDL2;
using System;
using System.Drawing;

namespace TNTMan.ecrans
{
    class Bouton
    {
        int id;
        Point position;
        Size taille;
        Color couleur;
        Color couleurTexte;
        Color couleurSel;
        Color couleurSelTexte;
        string texte;
        bool survole;
        Ecran ecran_dirige;

        public Bouton() { }

        public Bouton(int x, int y, int w, int h, Color c, Color ct, Color cs, Color cst, String t, Ecran e)
        {
            position = new Point(x, y);
            taille = new Size(w, h);
            couleur = c;
            couleurTexte = ct;
            couleurSel = cs;
            couleurSelTexte = cst;
            texte = t;
            survole = false;
            ecran_dirige = e;
        }
        public void curseurDans(Point curseur)
        {
            // Le curseur survole le bouton
            if ((curseur.X >= position.X) && (curseur.X <= position.X + taille.Width) && (curseur.Y >= position.Y) && curseur.Y <= position.Y + taille.Height)
            {
                survole = true;
            }

            // Le curseur ne survole pas le bouton
            else
            {
                survole = false;
            }
        }

        public void dessiner(IntPtr rendu)
        {
            Gfx.remplirRectangle(position.X, position.Y, 200, 35, 1, couleur, couleurTexte);
            Gfx.dessinerTexte(position.X + 20, position.Y, 18, couleurTexte, texte);
        }

        public void mettreAJour()
        {
            if (survole)
            {
                Gfx.remplirRectangle(position.X, position.Y, 200, 35, 1, couleurSel, couleurSelTexte);
                Gfx.dessinerTexte(position.X, position.Y, 1, couleurSelTexte, texte);
            }
        }

        public void gererEvenements()
        {
            //coordonées de la souris
            Point curseur = new Point();

            SDL.SDL_Event evenement;
            SDL.SDL_WaitEvent(out evenement);
            if (evenement.type == SDL.SDL_EventType.SDL_MOUSEMOTION)
            {
                curseur.X = evenement.motion.x;
                curseur.Y = evenement.motion.y;
                curseurDans(curseur);
            }
            if (evenement.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN && survole)
            {
                if (evenement.button.state == SDL.SDL_BUTTON_LEFT)
                {
                    Gfx.changerEcran(ecran_dirige);
                }
            }
            mettreAJour();
        }
    }
}
