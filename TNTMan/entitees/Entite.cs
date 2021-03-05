using System;
using System.Drawing;
using TNTMan.map;

namespace TNTMan.entitees
{
    class Entite
    {
        protected PointF position;
        protected PointF vitesse;
        protected bool statut;

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

        public bool getStatut()
        {
            return statut;
        }

        public virtual void deplacer(float _ax, float _ay)
        {
            vitesse = new PointF(Math.Clamp(vitesse.X + _ax, -1.0f, 1.0f), Math.Clamp(vitesse.Y + _ay, -1.0f, 1.0f));
        }

        public virtual void mettreAJour(Map map)
        {
        }
    }
}
