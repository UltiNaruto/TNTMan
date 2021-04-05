using SDL2;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TNTMan.map;

namespace TNTMan.entitees
{
    class Joueur : Entite
    {
        int id;
        float vitesse_deplacement;
        int nb_bombes;
        int max_nb_bombes;
        int portee_bombe;
        DateTime temps_avant_derniere_bombe_poser = DateTime.Now;
        SDL.SDL_Scancode[] touches;
        Score score;

        public Joueur(int _id, float _x, float _y, Map _map, params SDL.SDL_Scancode[] _touches) : base(_map)
        {
            id = _id;
            statut = true;
            position = new PointF(_x, _y);
            vitesse_deplacement = 0.05f;
            portee_bombe = 1;
            nb_bombes = max_nb_bombes = 2;
            if (id == 1)
                texture = Gfx.images["j1_bas"];
            if (id == 2)
                texture = Gfx.images["j2_bas"];
            if (id == 3)
                texture = Gfx.images["j3_bas"];
            if (id == 4)
                texture = Gfx.images["j4_bas"];
            touches = _touches;
            score = new Score();
        }

        public int getId()
        {
            return id;
        }

        public IntPtr getTexture()
        {
            return texture;
        }

        // Usage des tailles sur la grille et non en pixels
        public override bool enCollisionAvec()
        {
            Point direction = new Point(vitesse.X == 0 ? 0 : (int)(vitesse.X / Math.Abs(vitesse.X)), vitesse.Y == 0 ? 0 : (int)(vitesse.Y / Math.Abs(vitesse.Y)));
            if (map.getBlocA((int)position.X + direction.X, (int)position.Y + direction.Y) != null)
                return true;

            return map.trouverEntite((int)position.X + direction.X, (int)position.Y + direction.Y, typeof(Bombe)).Count == 1;
        }

        public override void deplacer(float _ax, float _ay)
        {
            if (vitesse.X != 0 || vitesse.Y != 0)
                return;
            if(vitesse.Y == 0 && _ax != 0)
                base.deplacer(_ax, _ay);
            if (vitesse.X == 0 && _ay != 0)
                base.deplacer(_ax, _ay);

            // vérifier la collision avec les blocs
            if (enCollisionAvec())
            {
                vitesse.X = 0;
                vitesse.Y = 0;
            }
            else
            {
                if(_ax > 0.0f)
                    texture = Gfx.images["j"+getId()+"_droite"];
                if (_ax < 0.0f)
                    texture = Gfx.images["j" + getId() + "_gauche"];
                if (_ay < 0.0f)
                    texture = Gfx.images["j" + getId() + "_haut"];
                if (_ay > 0.0f)
                    texture = Gfx.images["j" + getId() + "_bas"];
            }
        }

        public override void dessiner(IntPtr rendu)
        {
            Point _position = Map.getPositionEcranDepuis(position.X, position.Y, 24, 24);
            Gfx.dessinerImage(_position.X, _position.Y, 24, 24, texture);
        }

        public void mettreAJour(byte[] etats)
        {
            float vx = 0.0f;
            float vy = 0.0f;

            if (estMort())
                return;

            if (etats[(int)touches[0]] > 0)
            {
                deplacer(0.0f, -1.0f);
            }
            else if (etats[(int)touches[1]] > 0)
            {
                deplacer(0.0f, 1.0f);
            }
            else if (etats[(int)touches[2]] > 0)
            {
                deplacer(-1.0f, 0.0f);
            }
            else if (etats[(int)touches[3]] > 0)
            {
                deplacer(1.0f, 0.0f);
            }
            if (etats[(int)touches[4]] > 0)
            {
                map.ajoutEntite(poserBombe());
            }

            vx = Math.Clamp(vitesse.X, -1.0f * vitesse_deplacement, vitesse_deplacement);
            vy = Math.Clamp(vitesse.Y, -1.0f * vitesse_deplacement, vitesse_deplacement);

            if (vx == 0 && vy == 0)
            {
                position.X = (int)position.X + 0.25f;
                position.Y = (int)position.Y + 0.25f;
            }
            else
            {
                position.X += vx;
                vitesse.X -= vx;

                position.Y += vy;
                vitesse.Y -= vy;
            }
        }

        public int getNbBombes()
        {
            return this.nb_bombes;
        }

        public int getNbTues()
        {
            return score.getNbTues();
        }

        public void incrementerTue()
        {
            score.incrementerTue();
        }

        public int getNbVictoires()
        {
            return score.getNbVictoires();
        }

        public void incrementerVictoire()
        {
            score.incrementerVictoire();
        }

        public void incrementerBombe()
        {
            if (nb_bombes < max_nb_bombes)
                nb_bombes++;
        }

        public void decrementerBombe()
        {
            if (nb_bombes > 0)
                nb_bombes--;
        }

        public int getMaxNbBombes()
        {
            return this.max_nb_bombes;
        }

        public void incrementerMaxBombe()
        {
            if(max_nb_bombes < 10)
                max_nb_bombes++;
        }

        public void decrementerMaxBombe()
        {
            if(max_nb_bombes > 1)
                max_nb_bombes--;
        }

        public void incrementerPorteeBombe()
        {
            if(portee_bombe < 10)
                portee_bombe++;
        }

        public void decrementerPorteeBombe()
        {
            if (portee_bombe > 1)
                portee_bombe--;
        }

        public void incrementerVitesse()
        {
            if (vitesse_deplacement < 0.1f)
                vitesse_deplacement += 0.01f;
        }

        public void decrementerVitesse()
        {
            if (vitesse_deplacement > 0.05f)
                vitesse_deplacement -= 0.01f;
        }

        public Bombe poserBombe()
        {
            Point position_bombe = new Point((int)position.X, (int)position.Y);
            DateTime temps_actuel = DateTime.Now;
            Point direction = new Point(vitesse.X == 0 ? 0 : (int)(vitesse.X / Math.Abs(vitesse.X)), vitesse.Y == 0 ? 0 : (int)(vitesse.Y / Math.Abs(vitesse.Y)));
            if (nb_bombes == 0)
                return null;

            // vérifier si la bombe va être posé sur un bloc déjà existant
            if (map.getBlocA(position_bombe.X, position_bombe.Y) != null)
                return null;

            // vérifier si la bombe est déjà posé sur la case
            if (map.trouverEntite(position_bombe.X, position_bombe.Y, typeof(Bombe)).Count() > 0)
                return null;

            if ((temps_actuel - temps_avant_derniere_bombe_poser).TotalMilliseconds >= 80)
            {
                decrementerBombe();
                temps_avant_derniere_bombe_poser = temps_actuel;
                return new Bombe(position_bombe.X, position_bombe.Y, this);
            }
            return null;
        }

        public int getPortee()
        {
            return this.portee_bombe;
        }

        public float getVitesse()
        {
            return this.vitesse_deplacement;
        }

        public void reapparaitre(float x, float y)
        {
            this.position = new PointF(x, y);
            this.nb_bombes = this.max_nb_bombes = 1;
            this.vitesse_deplacement = 0.05f;
            this.portee_bombe = 1;
            this.vitesse = new PointF(0.0f, 0.0f);
            this.statut = true;
        }
    }
}
