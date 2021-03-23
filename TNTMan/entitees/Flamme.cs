using System;
using System.Drawing;
using TNTMan.map;

namespace TNTMan.entitees
{
    class Flamme : Entite
    {
        public static readonly int TAILLE_FEU = 32;
        Joueur proprietaire;
        DateTime tempsExplosion;
        protected int tempsAvantExtinction;

        public Flamme(int x, int y, Joueur _proprietaire, DateTime temps_actuel, Map _map) : base(_map)
        {
            statut = true;
            position = new PointF(x + 0.5f, y + 0.5f);
            proprietaire = _proprietaire;
            tempsExplosion = temps_actuel;
            tempsAvantExtinction = 300;
        }

        public Joueur getProprietaire()
        {
            return this.proprietaire;
        }

        public int getTempsAvantExtinction()
        {
            return tempsAvantExtinction;
        }

        public override void dessiner(IntPtr rendu)
        {
            Point _position = Map.getPositionEcranDepuis((int)position.X, (int)position.Y, 32, 32);
            if (tempsAvantExtinction > 0)
            {
                Gfx.dessinerImage(_position.X, _position.Y, TAILLE_FEU, TAILLE_FEU, Gfx.images["feu"]);
            }
        }

        public override void mettreAJour()
        {
            DateTime temps_actuel = DateTime.Now;
            if ((temps_actuel - tempsExplosion).TotalMilliseconds > tempsAvantExtinction)
            {
                tuer();
            }
        }
    }
}
