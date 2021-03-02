using System.Drawing;

namespace TNTMan.entitees
{
    class Entite
    {
        protected PointF position;
        private bool statut;
        public void tuer()
        {
            this.statut = false;
        }
        public bool estMort()
        {
            return !this.statut;
        }
    }
}
