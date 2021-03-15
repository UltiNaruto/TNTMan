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
            position = new PointF((int)proprietaire.getPosition().X + 0.5f, (int)proprietaire.getPosition().Y + 0.5f);
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

        public override void dessiner(IntPtr rendu)
        {
            Point _position = Map.getPositionEcranDepuis(position.X, position.Y, 16, 16);
            if (tempsExplosion > 0)
            {
                Gfx.remplirRectangle(_position.X, _position.Y, 16, 16, 1, Color.Orange, Color.DarkRed);
            }
            else
            {
                // Explosion enclenchée - On dessine quatre rectangles représentant l'explosion
                /*Gfx.remplirRectangle((int)_position.X, (int)((float)portee * Bloc.TAILLE_BLOC - _position.Y), 8, portee * Bloc.TAILLE_BLOC, 1, Color.LightGoldenrodYellow, Color.LightGoldenrodYellow);
                Gfx.remplirRectangle((int)_position.X, (int)((float)portee * Bloc.TAILLE_BLOC + _position.Y), 8, portee * Bloc.TAILLE_BLOC, 1, Color.LightGoldenrodYellow, Color.LightGoldenrodYellow);
                Gfx.remplirRectangle((int)((float)portee * Bloc.TAILLE_BLOC - _position.X), (int)position.Y, portee * Bloc.TAILLE_BLOC, 8, 1, Color.LightGoldenrodYellow, Color.LightGoldenrodYellow);
                Gfx.remplirRectangle((int)((float)portee * Bloc.TAILLE_BLOC + _position.X), (int)position.Y, portee * Bloc.TAILLE_BLOC, 8, 1, Color.LightGoldenrodYellow, Color.LightGoldenrodYellow);*/
            }
        }

        void explose()
        {
            // Disparition de la bombe
            tuer();
            proprietaire.incrementerBombe();
        }

        public override void mettreAJour(Map map)
        {
            if (tempsExplosion > 0)
                tempsExplosion -= 16; // décrémente de 16 ms par image
            else
            {
                explose();
                // création de l'entité flamme qui brulera les entités sauf bombe
            }
        }
    }
}
