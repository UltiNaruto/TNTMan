using System;
using System.Drawing;
using TNTMan.map;

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
