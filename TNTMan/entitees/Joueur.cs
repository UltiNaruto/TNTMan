using SDL2;
using System;
using System.Drawing;
using TNTMan.map;

namespace TNTMan.entitees
{
    class Joueur : Entite
    {
        int id;
        Color couleur;
        float vitesse_deplacement;
        int nb_bombes;
        int portee_bombe;
        public Joueur(int _id, float _x, float _y)
        {
            id = _id;
            statut = true;
            position = new PointF(_x, _y);
            vitesse_deplacement = 0.05f;
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
        public bool enCollisionAvec(Map map, PointF vitesse_appliquee)
        {
            bool res = false;
            RectangleF rect_joueur = new RectangleF((int)position.X - 0.125f, position.Y - 0.125f, 0.75f, 0.75f);
            RectangleF rect_joueur_f = new RectangleF(position.X - 0.125f + vitesse_appliquee.X, position.Y - 0.125f + vitesse_appliquee.Y, 0.75f, 0.75f);
            RectangleF rect_bloc = RectangleF.Empty;
            for (int x = 0; x < Map.LARGEUR_GRILLE && !res; x++)
                for (int y = 0; y < Map.LONGUEUR_GRILLE && !res; y++)
                {
                    if (map.getBlocA(x, y) == null)
                        continue;
                    rect_bloc = new RectangleF(x, y, 1.0f, 1.0f);
                    if (rect_joueur_f.IntersectsWith(rect_bloc))
                        res = true;
                }
            map.executerPourToutEntite((e) =>
            {
                rect_bloc = new RectangleF((int)e.getPosition().X, (int)e.getPosition().Y, 1.0f, 1.0f);
                if (e.GetType() == typeof(Bombe))
                {
                    if (!rect_joueur.IntersectsWith(rect_bloc) && rect_joueur_f.IntersectsWith(rect_bloc))
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
            base.deplacer(_ax * vitesse_deplacement, _ay * vitesse_deplacement);
        }

        public override void dessiner(IntPtr rendu)
        {
            Point _position = Map.getPositionEcranDepuis(position.X, position.Y, 24, 24);
            Gfx.remplirRectangle(_position.X, _position.Y, 24, 24, 1, this.getCouleur(), this.getCouleur());
        }

        public void mettreAJour(Map map, byte[] etats)
        {
            float vitesse_deplacement_restante_abs_x = 0.0f;
            float vitesse_deplacement_restante_abs_y = 0.0f;
            float vx = 0.0f;
            float vy = 0.0f;

            if (estMort())
                return;

            if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_W] > 0)
            {
                deplacer(0.0f, -1.0f);
            }
            else if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_S] > 0)
            {
                deplacer(0.0f, 1.0f);
            }
            else if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_A] > 0)
            {
                deplacer(-1.0f, 0.0f);
            }
            else if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_D] > 0)
            {
                deplacer(1.0f, 0.0f);
            }
            if (etats[(int)SDL.SDL_Scancode.SDL_SCANCODE_SPACE] > 0)
            {
                map.ajoutEntite(poserBombe());
            }

            vitesse_deplacement_restante_abs_x = Math.Min(Math.Abs(vitesse.X - vitesse_deplacement), vitesse_deplacement);
            vitesse_deplacement_restante_abs_y = Math.Min(Math.Abs(vitesse.Y - vitesse_deplacement), vitesse_deplacement);
            vx = Math.Clamp(vitesse.X, -1 * vitesse_deplacement_restante_abs_x, vitesse_deplacement_restante_abs_x);
            vy = Math.Clamp(vitesse.Y, -1 * vitesse_deplacement_restante_abs_y, vitesse_deplacement_restante_abs_y);

            if (vx != 0 || vy != 0)
            {
                // vérifier la collision avec les blocs
                if (enCollisionAvec(map, new PointF(vx, vy)))
                {
                    vitesse.X = 0;
                    vitesse.Y = 0;
                }
                else
                {
                    position.X = (float)Math.Round(position.X, 2) + vx;
                    position.Y = (float)Math.Round(position.Y, 2) + vy;
                    vitesse.X = (float)Math.Round(vitesse.X, 2) - vx;
                    vitesse.Y = (float)Math.Round(vitesse.Y, 2) - vy;
                }
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
            if (nb_bombes == 0)
                return null;

            // vérifier si la bombe est déjà posé sur la case

            decrementerBombe();
            return new Bombe(this);
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
