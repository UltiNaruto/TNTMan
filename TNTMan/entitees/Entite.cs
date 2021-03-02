using System;
using System.Drawing;
using TNTMan.map;

namespace TNTMan.entitees
{
    class Entite
    {
        protected PointF position;
<<<<<<< HEAD
        protected PointF vitesse;
        protected bool statut;

=======
        private bool statut;
>>>>>>> 38b087b (AJOUTS & MODIFICATIONS DE CLASSES)
        public void tuer()
        {
            this.statut = false;
        }
        public bool estMort()
        {
            return !this.statut;
        }
<<<<<<< HEAD

        public PointF getPosition()
        {
            return position;
        }

        public void setPosition(PointF _position)
        {
            position = _position;
        }

        public virtual void deplacer(float _ax, float _ay)
        {
            vitesse = new PointF(Math.Clamp(vitesse.X + _ax, -1.0f, 1.0f), Math.Clamp(vitesse.Y + _ay, -1.0f, 1.0f));
        }

        public virtual void mettreAJour(Map map)
        {
        }
=======
>>>>>>> 38b087b (AJOUTS & MODIFICATIONS DE CLASSES)
    }
}
