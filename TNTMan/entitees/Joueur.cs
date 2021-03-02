using System;
using System.Drawing;
using TNTMan.map;
using TNTMan.map.blocs;

namespace TNTMan.entitees
{
    class Joueur : Entite
    {
        int id;
        Color couleur;
        float vitesse_deplacement;
        public Joueur(int _id, float _x, float _y)
        {
            id = _id;
            position = new PointF(_x, _y);
            vitesse_deplacement = 0.05f;
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

        public bool enCollisionAvecBloc(Map map, PointF pos)
        {
            return map.getBlocA((int)pos.X, (int)pos.Y) != null;
        }

        public override void deplacer(float _ax, float _ay)
        {
            base.deplacer(_ax * vitesse_deplacement, _ay * vitesse_deplacement);
        }

        public override void mettreAJour(Map map)
        {
            float vitesse_deplacement_restante_abs_x = Math.Min(Math.Abs(vitesse.X - vitesse_deplacement), vitesse_deplacement);
            float vitesse_deplacement_restante_abs_y = Math.Min(Math.Abs(vitesse.Y - vitesse_deplacement), vitesse_deplacement);
            float vx = Math.Clamp(vitesse.X, -1 * vitesse_deplacement_restante_abs_x, vitesse_deplacement_restante_abs_x);
            float vy = Math.Clamp(vitesse.Y, -1 * vitesse_deplacement_restante_abs_y, vitesse_deplacement_restante_abs_y);
            if (vx != 0 || vy != 0)
            {
                // vérifier la collision avec les blocs
                if (enCollisionAvecBloc(map, Utils.ArrondiPositionBloc(position.X + vx, position.Y + vy)))
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
    }
}
