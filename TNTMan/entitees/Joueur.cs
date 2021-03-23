using SDL2;
using System;
using System.Drawing;
using TNTMan.map;
using TNTMan.entitees.enums;

namespace TNTMan.entitees
{
    class Joueur : Entite
    {
        public static readonly int TAILLE_JOUEUR = 28;

        int id;
        Color couleur;
        float vitesse_deplacement;
        DirectionJoueur direction;
        int nb_bombes;
        int portee_bombe;
        DateTime temps_avant_derniere_bombe_poser = DateTime.Now;

        public Joueur(int _id, float _x, float _y, DirectionJoueur _direction, Map _map) : base(_map)
        {
            id = _id;
            statut = true;
            position = new PointF(_x, _y);
            vitesse_deplacement = 0.05f;
            direction = _direction;
            portee_bombe = 1;
            nb_bombes = 1;
            if (id == 1)
                couleur = Color.Blue;
            if (id == 2)
                couleur = Color.Red;
            if (id == 3)
                couleur = Color.Yellow;
            if (id == 4)
                couleur = Color.Pink;
        }

        public int getId()
        {
            return id;
        }

        public Color getCouleur()
        {
            return couleur;
        }

        // Usage des tailles sur la grille et non en pixels
        public override bool enCollisionAvec()
        {
            bool res = false;
            Point direction = new Point(vitesse.X == 0 ? 0 : (int)(vitesse.X / Math.Abs(vitesse.X)), vitesse.Y == 0 ? 0 : (int)(vitesse.Y / Math.Abs(vitesse.Y)));
            RectangleF rect_joueur = new RectangleF((int)position.X + direction.X + 0.1f, (int)position.Y + direction.Y + 0.1f, 0.8f, 0.8f);
            RectangleF rect_bloc = new RectangleF((int)position.X, (int)position.Y, 1.0f, 1.0f);
            if (map.getBlocA((int)position.X + direction.X, (int)position.Y + direction.Y) != null)
                return true;
            map.executerPourToutEntite((e) =>
            {
                if (e.GetType() == typeof(Bombe))
                {
                    if (rect_joueur.IntersectsWith(rect_bloc))
                    {
                        res = true;
                        return;
                    }
                }
            });
            return res;
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
        }

        public override void dessiner(IntPtr rendu)
        {
            Point _position = Map.getPositionEcranDepuis(position.X, position.Y, 24, 24);
            if(id == 1)
            {
                if(direction == enums.DirectionJoueur.Haut)
                {

                    Gfx.dessinerImage(_position.X, _position.Y, TAILLE_JOUEUR, TAILLE_JOUEUR, Gfx.images["j1_haut"]);
                }
                else if(direction == enums.DirectionJoueur.Bas)
                {

                    Gfx.dessinerImage(_position.X, _position.Y, TAILLE_JOUEUR, TAILLE_JOUEUR, Gfx.images["j1_bas"]);
                }
                else if (direction == enums.DirectionJoueur.Gauche)
                {

                    Gfx.dessinerImage(_position.X, _position.Y, TAILLE_JOUEUR, TAILLE_JOUEUR, Gfx.images["j1_gauche"]);
                }
                else if (direction == enums.DirectionJoueur.Droite)
                {

                    Gfx.dessinerImage(_position.X, _position.Y, TAILLE_JOUEUR, TAILLE_JOUEUR, Gfx.images["j1_droite"]);
                }
            }
            if (id == 2)
            {
                if (direction == enums.DirectionJoueur.Haut)
                {

                    Gfx.dessinerImage(_position.X, _position.Y, TAILLE_JOUEUR, TAILLE_JOUEUR, Gfx.images["j2_haut"]);
                }
                else if (direction == enums.DirectionJoueur.Bas)
                {

                    Gfx.dessinerImage(_position.X, _position.Y, TAILLE_JOUEUR, TAILLE_JOUEUR, Gfx.images["j2_bas"]);
                }
                else if (direction == enums.DirectionJoueur.Gauche)
                {

                    Gfx.dessinerImage(_position.X, _position.Y, TAILLE_JOUEUR, TAILLE_JOUEUR, Gfx.images["j2_gauche"]);
                }
                else if (direction == enums.DirectionJoueur.Droite)
                {

                    Gfx.dessinerImage(_position.X, _position.Y, TAILLE_JOUEUR, TAILLE_JOUEUR, Gfx.images["j2_droite"]);
                }
            }
            if (id == 3)
            {
                if (direction == enums.DirectionJoueur.Haut)
                {

                    Gfx.dessinerImage(_position.X, _position.Y, TAILLE_JOUEUR, TAILLE_JOUEUR, Gfx.images["j3_haut"]);
                }
                else if (direction == enums.DirectionJoueur.Bas)
                {

                    Gfx.dessinerImage(_position.X, _position.Y, TAILLE_JOUEUR, TAILLE_JOUEUR, Gfx.images["j3_bas"]);
                }
                else if (direction == enums.DirectionJoueur.Gauche)
                {

                    Gfx.dessinerImage(_position.X, _position.Y, TAILLE_JOUEUR, TAILLE_JOUEUR, Gfx.images["j3_gauche"]);
                }
                else if (direction == enums.DirectionJoueur.Droite)
                {

                    Gfx.dessinerImage(_position.X, _position.Y, TAILLE_JOUEUR, TAILLE_JOUEUR, Gfx.images["j3_droite"]);
                }
            }
            if (id == 4)
            {
                if (direction == enums.DirectionJoueur.Haut)
                {

                    Gfx.dessinerImage(_position.X, _position.Y, TAILLE_JOUEUR, TAILLE_JOUEUR, Gfx.images["j4_haut"]);
                }
                else if (direction == enums.DirectionJoueur.Bas)
                {

                    Gfx.dessinerImage(_position.X, _position.Y, TAILLE_JOUEUR, TAILLE_JOUEUR, Gfx.images["j4_bas"]);
                }
                else if (direction == enums.DirectionJoueur.Gauche)
                {

                    Gfx.dessinerImage(_position.X, _position.Y, TAILLE_JOUEUR, TAILLE_JOUEUR, Gfx.images["j4_gauche"]);
                }
                else if (direction == enums.DirectionJoueur.Droite)
                {

                    Gfx.dessinerImage(_position.X, _position.Y, TAILLE_JOUEUR, TAILLE_JOUEUR, Gfx.images["j4_droite"]);
                }
            }
        }

        public void mettreAJour(byte[] etats)
        {
            float vx = 0.0f;
            float vy = 0.0f;

            if (estMort())
                return;

            if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_W] > 0)
            {
                direction = enums.DirectionJoueur.Haut;
                deplacer(0.0f, -1.0f);
            }
            else if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_S] > 0)
            {
                direction = enums.DirectionJoueur.Bas;
                deplacer(0.0f, 1.0f);
            }
            else if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_A] > 0)
            {
                direction = enums.DirectionJoueur.Gauche;
                deplacer(-1.0f, 0.0f);
            }
            else if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_D] > 0)
            {
                direction = enums.DirectionJoueur.Droite;
                deplacer(1.0f, 0.0f);
            }
            if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_SPACE] > 0)
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

        public void incrementerBombe()
        {
            nb_bombes++;
        }

        public void decrementerBombe()
        {
            nb_bombes--;
        }

        public Bombe poserBombe()
        {
            DateTime temps_actuel = DateTime.Now;
            if (nb_bombes == 0)
                return null;

            if ((temps_actuel - temps_avant_derniere_bombe_poser).TotalMilliseconds >= 120)
            {
                // vérifier si la bombe est déjà posé sur la case
                if (map.entiteExiste((int)position.X, (int)position.Y) == typeof(Bombe))
                    return null;

                decrementerBombe();
                temps_avant_derniere_bombe_poser = temps_actuel;
                return new Bombe(this);
            }
            return null;
        }

        public int getPortee()
        {
            return this.portee_bombe;
        }

        public void setPortee(int p)
        {
            this.portee_bombe = p;
        }

        public float getVitesse()
        {
            return this.vitesse_deplacement;
        }

        public void setVitesse(float vitesse)
        {
            this.vitesse_deplacement = vitesse;
        }
    }
}
