using System;
using System.Drawing;
using TNTMan.map;

namespace TNTMan.entitees
{
    class Entite
    {
        protected PointF position;
        public PointF vitesse;
        protected bool statut;
        protected Map map;

        protected Entite() { }

        protected Entite(Map _map)
        {
            map = _map;
        }

        public Map getMap()
        {
            return map;
        }

        public void tuer()
        {
            this.statut = false;
        }

        public bool estMort()
        {
            return !this.statut;
        }

        public PointF getPosition()
        {
            return position;
        }

        public void setPosition(PointF _position)
        {
            position = _position;
        }

        public virtual bool enCollisionAvec()
        {
            Entite entite_en_collision = null;
            if (map.getBlocA((int)position.X, (int)position.Y) == null)
                return false;
            entite_en_collision = map.trouverEntite((int)position.X, (int)position.Y);
            if (entite_en_collision == null)
                return false;
            return true;
        }

        public virtual void deplacer(float _ax, float _ay)
        {
            vitesse = new PointF(Math.Clamp(vitesse.X + _ax, -1.0f, 1.0f), Math.Clamp(vitesse.Y + _ay, -1.0f, 1.0f));
        }

        public virtual void dessiner(IntPtr rendu)
        {
            
        }

        public virtual void mettreAJour()
        {
        }
    }
}
