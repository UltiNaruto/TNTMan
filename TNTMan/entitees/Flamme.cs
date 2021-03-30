using System;
using System.Drawing;
using System.Linq;
using TNTMan.entitees.bonus;
using TNTMan.map;

namespace TNTMan.entitees
{
    class Flamme : Entite
    {
        Joueur proprietaire;
        DateTime tempsExplosion;
        protected int tempsAvantExtinction;

        public Flamme(int x, int y, Joueur _proprietaire, Map _map) : base(_map)
        {
            statut = true;
            position = new PointF(x + 0.5f, y + 0.5f);
            proprietaire = _proprietaire;
            tempsExplosion = DateTime.Now;
            tempsAvantExtinction = 200;
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
                Gfx.dessinerImage(_position.X, _position.Y, 32, 32, Gfx.images["explosion"]);
            }
        }

        public override void mettreAJour()
        {
            Bombe bombe = null;
            Bonus bonus = null;
            DateTime temps_actuel = DateTime.Now;
            
            map.trouverEntite((int)position.X, (int)position.Y, typeof(Joueur)).ForEach((e) =>
            {
                var joueur = e as Joueur;
                if (proprietaire != joueur)
                    proprietaire.incrementerTue();
                joueur.tuer();
            });

            bombe = map.trouverEntite((int)position.X, (int)position.Y, typeof(Bombe)).FirstOrDefault() as Bombe;
            if (bombe != null && !bombe.estMort())
            {
                bombe.explose();
            }

            bonus = map.trouverEntite((int)position.X, (int)position.Y, typeof(Bonus)).FirstOrDefault() as Bonus;
            if (bonus != null && !bonus.estMort())
            {
                bonus.tuer();
            }

            if ((temps_actuel - tempsExplosion).TotalMilliseconds > tempsAvantExtinction)
            {
                tuer();
            }
        }
    }
}
