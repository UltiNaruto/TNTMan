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
            Gfx.remplirRectangle(position.X, position.Y, Bloc.TAILLE_BLOC, Bloc.TAILLE_BLOC, 1, Color.Brown, Color.Black);
        }
    }
}
