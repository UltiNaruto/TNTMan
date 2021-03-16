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
        DateTime tempsPoser;
        protected int tempsExplosion;

        //Constructeur
        public Bombe(Joueur joueur) : base(joueur.getMap())
        {
            proprietaire = joueur;
            position = new PointF((int)proprietaire.getPosition().X + 0.5f, (int)proprietaire.getPosition().Y + 0.5f);
            tempsExplosion = 2000; // 2 secondes par défaut
            statut = true;
            portee = joueur.getPortee();
            tempsPoser = DateTime.Now;
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
            Point _position = Map.getPositionEcranDepuis(position.X, position.Y, 16, 16);
            if (tempsExplosion > 0)
            {
                Gfx.remplirRectangle(_position.X, _position.Y, 16, 16, 1, Color.Orange, Color.DarkRed);
            }
        }

        void explose(DateTime temps_actuel)
        {
            int _x = (int)position.X;
            int _y = (int)position.Y;
            Bloc bloc_en_collision = null;
            Entite entite_en_collision = null;

            // Disparition de la bombe
            tuer();
            // Apparition des flammes de l'explosion de la bombe
            map.ajoutEntite(new Flamme(_x, _y, proprietaire, temps_actuel, map));
            for (int x = (int)position.X - portee; x < (int)position.X; x++)
            {
                bloc_en_collision = map.getBlocA(x, _y);
                if (bloc_en_collision == null)
                {
                    if (x != _x && x > 0 && x < Map.LARGEUR_GRILLE)
                    {
                        map.ajoutEntite(new Flamme(x, _y, proprietaire, temps_actuel, map));
                        entite_en_collision = map.trouverEntite(x, _y);
                        if (entite_en_collision != null)
                        {
                            if (entite_en_collision.GetType() == typeof(Bombe))
                            {
                                if(!entite_en_collision.estMort())
                                    ((Bombe)entite_en_collision).explose(temps_actuel);
                            }
                            if (entite_en_collision.GetType() != typeof(Bombe)
                            && entite_en_collision.GetType() != typeof(Flamme))
                            {
                                entite_en_collision.tuer();
                            }
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

            for (int x = (int)position.X + 1; x <= (int)position.X + portee; x++)
            {
                bloc_en_collision = map.getBlocA(x, _y);
                if (bloc_en_collision == null)
                {
                    if (x != _x && x > 0 && x < Map.LARGEUR_GRILLE)
                    {
                        map.ajoutEntite(new Flamme(x, _y, proprietaire, temps_actuel, map));
                        entite_en_collision = map.trouverEntite(x, _y);
                        if (entite_en_collision != null)
                        {
                            if (entite_en_collision.GetType() == typeof(Bombe))
                            {
                                if (!entite_en_collision.estMort())
                                    ((Bombe)entite_en_collision).explose(temps_actuel);
                            }
                            if (entite_en_collision.GetType() != typeof(Bombe)
                            && entite_en_collision.GetType() != typeof(Flamme))
                            {
                                entite_en_collision.tuer();
                            }
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

            for (int y = (int)position.Y - portee; y < (int)position.Y; y++)
            {
                bloc_en_collision = map.getBlocA(_x, y);
                if (bloc_en_collision == null)
                {
                    if (y != _y && y > 0 && y < Map.LONGUEUR_GRILLE)
                    {
                        map.ajoutEntite(new Flamme(_x, y, proprietaire, temps_actuel, map));
                        entite_en_collision = map.trouverEntite(_x, y);
                        if (entite_en_collision != null)
                        {
                            if (entite_en_collision.GetType() == typeof(Bombe))
                            {
                                if (!entite_en_collision.estMort())
                                    ((Bombe)entite_en_collision).explose(temps_actuel);
                            }
                            if (entite_en_collision.GetType() != typeof(Bombe)
                            && entite_en_collision.GetType() != typeof(Flamme))
                            {
                                entite_en_collision.tuer();
                            }
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

            for (int y = (int)position.Y + 1; y <= (int)position.Y + portee; y++)
            {
                bloc_en_collision = map.getBlocA(_x, y);
                if (bloc_en_collision == null)
                {
                    if (y != _y && y > 0 && y < Map.LONGUEUR_GRILLE)
                    {
                        map.ajoutEntite(new Flamme(_x, y, proprietaire, temps_actuel, map));
                        entite_en_collision = map.trouverEntite(_x, y);
                        if (entite_en_collision != null)
                        {
                            if (entite_en_collision.GetType() == typeof(Bombe))
                            {
                                if (!entite_en_collision.estMort())
                                    ((Bombe)entite_en_collision).explose(temps_actuel);
                            }
                            if (entite_en_collision.GetType() != typeof(Bombe)
                            && entite_en_collision.GetType() != typeof(Flamme))
                            {
                                entite_en_collision.tuer();
                            }
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
            if ((temps_actuel - tempsPoser).TotalMilliseconds > tempsExplosion)
            {
                explose(temps_actuel);
            }
        }
    }
}
