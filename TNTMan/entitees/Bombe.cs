using System;
using System.Drawing;
using TNTMan.map;
using TNTMan.map.blocs;

namespace TNTMan.entitees
{
    class Bombe : Entite
    {
        // Attributs
        int portee;
        Joueur proprietaire;
        DateTime tempsPoser;
        protected int tempsExplosion;

        //Constructeur
        public Bombe(int x, int y, Joueur joueur) : base(joueur.getMap())
        {
            proprietaire = joueur;
            position = new PointF(0.5f + x, 0.5f + y);
            tempsExplosion = 2000; // 2 secondes par défaut
            statut = true;
            portee = joueur.getPortee();
            tempsPoser = DateTime.Now;
        }

        // Méthodes
        int getPortee()
        {
            return this.portee;
        }

        public Joueur getProprietaire()
        {
            return this.proprietaire;
        }

        public int getTempsExplosion()
        {
            return tempsExplosion;
        }

        public override void dessiner(IntPtr rendu)
        {
            Point _position = Map.getPositionEcranDepuis((int)position.X, (int)position.Y, 32, 32);
            if (tempsExplosion > 0)
            {
                Gfx.dessinerImage(_position.X, _position.Y, 32, 32, Gfx.images["bombe"]);
            }
        }

        public void explose()
        {
            int _x = (int)position.X;
            int _y = (int)position.Y;
            Bloc bloc_en_collision = null;

            // Disparition de la bombe
            tuer();
            // Jouer le son de l'explosion
            Sfx.JouerSon("explosion");
            // Apparition des flammes de l'explosion de la bombe
            map.ajoutEntite(new Flamme(_x, _y, proprietaire, map));
            for (int x = _x - 1; x >= _x - portee; x--)
            {
                bloc_en_collision = map.getBlocA(x, _y);
                if (bloc_en_collision == null)
                {
                    if (x != _x && x > 0 && x < Map.LARGEUR_GRILLE)
                    {
                        if (map.trouverEntite(x, _y, typeof(Flamme)).Count == 0)
                        {
                            map.ajoutEntite(new Flamme(x, _y, proprietaire, map));
                        }
                    }
                }
                else
                {
                    if (bloc_en_collision.GetType() != typeof(BlocIncassable)
                    && bloc_en_collision.estSolide())
                    {
                        if (bloc_en_collision.getDurabilite() > 1)
                        {
                            bloc_en_collision.subiDegats();
                        }
                        else
                        {
                            map.detruireBlocA(x, _y);
                        }
                    }
                    break;
                }
            }

            for (int x = _x + 1; x <= _x + portee; x++)
            {
                bloc_en_collision = map.getBlocA(x, _y);
                if (bloc_en_collision == null)
                {
                    if (x != _x && x > 0 && x < Map.LARGEUR_GRILLE)
                    {
                        if (map.trouverEntite(x, _y, typeof(Flamme)).Count == 0)
                        {
                            map.ajoutEntite(new Flamme(x, _y, proprietaire, map));
                        }
                    }
                }
                else
                {
                    if (bloc_en_collision.GetType() != typeof(BlocIncassable)
                    && bloc_en_collision.estSolide())
                    {
                        if (bloc_en_collision.getDurabilite() > 1)
                        {
                            bloc_en_collision.subiDegats();
                        }
                        else
                        {
                            map.detruireBlocA(x, _y);
                        }
                    }
                    break;
                }
            }

            for (int y = _y - 1; y >= _y - portee; y--)
            {
                bloc_en_collision = map.getBlocA(_x, y);
                if (bloc_en_collision == null)
                {
                    if (y != _y && y > 0 && y < Map.LONGUEUR_GRILLE)
                    {
                        if (map.trouverEntite(_x, y, typeof(Flamme)).Count == 0)
                        {
                            map.ajoutEntite(new Flamme(_x, y, proprietaire, map));
                        }
                    }
                }
                else
                {
                    if (bloc_en_collision.GetType() != typeof(BlocIncassable)
                    && bloc_en_collision.estSolide())
                    {
                        if (bloc_en_collision.getDurabilite() > 1)
                        {
                            bloc_en_collision.subiDegats();
                        }
                        else
                        {
                            map.detruireBlocA(_x, y);
                        }
                    }
                    break;
                }
            }

            for (int y = _y + 1; y <= _y + portee; y++)
            {
                bloc_en_collision = map.getBlocA(_x, y);
                if (bloc_en_collision == null)
                {
                    if (y != _y && y > 0 && y < Map.LONGUEUR_GRILLE)
                    {
                        if (map.trouverEntite(_x, y, typeof(Flamme)).Count == 0)
                        {
                            map.ajoutEntite(new Flamme(_x, y, proprietaire, map));
                        }
                    }
                }
                else
                {
                    if (bloc_en_collision.GetType() != typeof(BlocIncassable)
                    && bloc_en_collision.estSolide())
                    {
                        if (bloc_en_collision.getDurabilite() > 1)
                        {
                            bloc_en_collision.subiDegats();
                        }
                        else
                        {
                            map.detruireBlocA(_x, y);
                        }
                    }
                    break;
                }
            }
            proprietaire.incrementerBombe();
        }

        public override void mettreAJour()
        {
            DateTime temps_actuel = DateTime.Now;
            if (estMort())
            {
                return;
            }

            if ((temps_actuel - tempsPoser).TotalMilliseconds > tempsExplosion)
            {
                explose();
            }
        }
    }
}
