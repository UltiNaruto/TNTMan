using System;
using System.Drawing;

namespace TNTMan.map.blocs
{
    class BlocTerre : Bloc
    {
        public BlocTerre() : base(2, 1, true)
        {

        }

        public override void dessiner(IntPtr rendu, int bloc_x, int bloc_y)
        {
            Point position = Map.getPositionEcranDepuis(bloc_x, bloc_y);
            Gfx.dessinerImage(position.X, position.Y, TAILLE_BLOC, TAILLE_BLOC, Gfx.images["bloc_terre"]);
        }
    }
}
