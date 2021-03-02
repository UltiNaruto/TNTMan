using System.Drawing;

namespace TNTMan.entitees
{
    class Joueur : Entite
    {
        int id;
        public Joueur(int _id, float _x, float _y)
        {
            id = _id;
            position = new PointF(_x, _y);
        }

        public int getId()
        {
            return id;
        }
    }
}
