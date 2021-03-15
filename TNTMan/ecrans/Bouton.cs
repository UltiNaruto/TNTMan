using System;
using System.Drawing;

namespace TNTMan.ecrans
{
    class Bouton
    {
        int id;
        Point position;
        protected static readonly Color couleur = Color.FloralWhite;
        protected static readonly Color couleurTexte = Color.Black;
        protected static readonly Color couleurSel = Color.LightGoldenrodYellow;
        protected static readonly Color couleurSelTexte = Color.Red;
        string texte;
        int taillePolice;

        protected Bouton() { }

        public Bouton(int _id, int x, int y, int px, String fmt, params Object[] args)
        {
            id = _id;
            position = new Point(x, y);
            taillePolice = px;
            texte = String.Format(fmt, args);
        }

        public int getId()
        {
            return id;
        }

        public bool curseurDans(Point curseur)
        {
            Size taille = Gfx.getTailleRectangleTexte(texte, taillePolice);
            return curseur.X >= position.X 
                && curseur.X <= position.X + taille.Width
                && curseur.Y >= position.Y
                && curseur.Y <= position.Y + taille.Height;
        }

        public void dessiner(IntPtr rendu, bool sel)
        {
            Size taille = Gfx.getTailleRectangleTexte(texte, taillePolice);
            Gfx.remplirRectangle(position.X, position.Y, taille.Width, taille.Height, 1, sel ? couleurSel : couleur, sel ? couleurSelTexte : couleurTexte);
            Gfx.dessinerTexte(position.X+1, position.Y+1, taillePolice, sel ? couleurSelTexte : couleurTexte, texte);
        }
    }
}
