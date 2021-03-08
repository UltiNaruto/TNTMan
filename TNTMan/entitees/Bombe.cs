using System;
using System.Drawing;
using TNTMan.map;
using TNTMan.map.blocs;

namespace TNTMan.entitees
{
    class Bombe : Entite
    {
        // Attributs

        //int id;
        string nom;
        int portee;
        Joueur proprietaire;
        protected int tempsExplosion;

        //Constructeur
        public Bombe(Joueur joueur)
        {
            proprietaire = joueur;
            position = proprietaire.getPosition();
            tempsExplosion = 3000; // 3 secondes par défaut
            statut = true;
            portee = joueur.getPortee();
        }

        // Méthodes
        string getNom()
        {
            return this.nom;
        }

        int getPortee()
        {
            return this.portee;
        }

        Joueur getProprietaire()
        {
            return this.proprietaire;
        }

        public int getTempsExplosion()
        {
            return tempsExplosion;
        }

        public void dessine(IntPtr rendu)
        {
            Size resolution = Gfx.getResolution();
            Size tailleGrille = new Size(Map.LARGEUR_GRILLE * Bloc.TAILLE_BLOC, Map.LONGUEUR_GRILLE * Bloc.TAILLE_BLOC);
            Gfx.remplirRectangle((resolution.Width - tailleGrille.Width) / 2 + (int)(this.position.X * Bloc.TAILLE_BLOC) - 8, (resolution.Height - tailleGrille.Height) / 2 + (int)(this.position.Y * Bloc.TAILLE_BLOC) - 8, 16, 16, 1, Color.Orange, Color.DarkRed);
        }

        void explose()
        {
            /* Explosion enclenchée - On dessine quatre rectangles représentant l'explosion
            Gfx.remplirRectangle((int)position.X, (int)((float)portee - position.Y), 1, portee, portee, Color.LightGoldenrodYellow, Color.LightGoldenrodYellow);
            Gfx.remplirRectangle((int)position.X, (int)((float)portee + position.Y), 1, portee, portee, Color.LightGoldenrodYellow, Color.LightGoldenrodYellow);
            Gfx.remplirRectangle((int)((float)portee - position.X), (int)position.Y, 1, portee, portee, Color.LightGoldenrodYellow, Color.LightGoldenrodYellow);
            Gfx.remplirRectangle((int)((float)portee + position.X), (int)position.Y, 1, portee, portee, Color.LightGoldenrodYellow, Color.LightGoldenrodYellow);
            */
            // Disparition de la bombe
            tuer();
            proprietaire.ajouterBombe();
        }

        public override void mettreAJour(Map map)
        {
            if(tempsExplosion > 0)
                tempsExplosion -= 16; // décrémente de 16 ms par image
            else
                explose();
        }
    }
}
