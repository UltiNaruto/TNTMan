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

        public bool enCollisionAvecBloc(Map map, float bloc_x, float bloc_y, float x, float y)
        {
            Size resolution = Gfx.getResolution();
            Size tailleGrille = new Size(Map.LARGEUR_GRILLE * Bloc.TAILLE_BLOC, Map.LONGUEUR_GRILLE * Bloc.TAILLE_BLOC);
            Rectangle joueurRect = new Rectangle((resolution.Width - tailleGrille.Width) / 2 + (int)(x * Bloc.TAILLE_BLOC) - 8, (resolution.Height - tailleGrille.Height) / 2 + (int)(y * Bloc.TAILLE_BLOC) - 8, 16, 16);
            Rectangle blocRect = Rectangle.Empty;

            if (map.getBlocA((int)bloc_x, (int)bloc_y) == null)
                return false;

            blocRect = new Rectangle((resolution.Width - tailleGrille.Width) / 2 + (int)(bloc_x * Bloc.TAILLE_BLOC), (resolution.Height - tailleGrille.Height) / 2 + (int)(bloc_y * Bloc.TAILLE_BLOC), Bloc.TAILLE_BLOC, Bloc.TAILLE_BLOC);
            return Rectangle.Intersect(blocRect, joueurRect) != Rectangle.Empty;
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
            // vérifier la collision avec les blocs
            if (enCollisionAvecBloc(map, (int)(position.X + vx), (int)(position.Y + vy), position.X + vx * 2, position.Y + vy * 2))
            {
                vitesse.X = 0;
                vitesse.Y = 0;
            }
            else
            {
                position.X += vx;
                position.Y += vy;
                vitesse.X -= vx;
                vitesse.Y -= vy;
            }
        }
    }
}
