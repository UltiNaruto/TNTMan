using System;
using System.Drawing;

namespace TNTMan.ecrans
{
    class Bouton
    {
        int id;
        Point position;
        Size taille;
        protected static readonly Color couleur = Color.FloralWhite;
        protected static readonly Color couleurTexte = Color.Black;
        protected static readonly Color couleurSel = Color.LightGoldenrodYellow;
        protected static readonly Color couleurSelTexte = Color.Red;
        string texte;
        int taillePolice;

        protected Bouton() { }

        public Bouton(int _id, int x, int y, int w, int h, int px, String fmt, params Object[] args)
        {
            Size taille_rect_texte = new Size();

            id = _id;
            position = new Point(x, y);
            taillePolice = px;
            texte = String.Format(fmt, args);
            taille_rect_texte = Gfx.getTailleRectangleTexte(texte, taillePolice);
            if (w < taille_rect_texte.Width)
                w = taille_rect_texte.Width;
            if (h < taille_rect_texte.Height)
                h = taille_rect_texte.Height;
            taille = new Size(w, h);
        }

        public Bouton(int _id, int x, int y, int px, String fmt, params Object[] args)
            : this(_id, x, y, 200, 20, px, fmt, args)
        {
        }

        public int getId()
        {
            return id;
        }

        public bool curseurDans(Point curseur)
        {
            return curseur.X >= position.X 
                && curseur.X <= position.X + taille.Width
                && curseur.Y >= position.Y
                && curseur.Y <= position.Y + taille.Height;
        }

        public void dessiner(IntPtr rendu, bool sel)
        {
            Size taille_rect_texte = Gfx.getTailleRectangleTexte(texte, taillePolice);
            Gfx.remplirRectangle(position.X, position.Y, taille.Width, taille.Height, 1, sel ? couleurSel : couleur, sel ? couleurSelTexte : couleurTexte);
            Gfx.dessinerTexte(position.X + (taille.Width / 2 - taille_rect_texte.Width / 2), position.Y+(taille.Height / 2 - taille_rect_texte.Height / 2), taillePolice, sel ? couleurSelTexte : couleurTexte, texte);
        }
    }
}
