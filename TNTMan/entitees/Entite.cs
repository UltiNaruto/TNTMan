using System.Drawing;

namespace TNTMan.entitees
{
    class Entite
    {
        protected PointF position;
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

        public void deplacer(float x, float y)
        {
            position = new PointF(position.X + x, position.Y + y);
        }
    }
}
