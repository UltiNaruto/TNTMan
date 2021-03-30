using System;
using System.Drawing;
using TNTMan.entitees.bonus.enums;
using TNTMan.map;

namespace TNTMan.entitees.bonus
{
    class Bonus_Portee_Plus : Bonus
    {
        public Bonus_Portee_Plus(Map _map, float x, float y) : base(_map, x, y)
        {

        }

        public override string getNom()
        {
            return "Portee+";
        }

        public override TypeBonus getType()
        {
            return TypeBonus.Bombe;
        }

        public override void dessiner(IntPtr rendu)
        {
            Point _position = Map.getPositionEcranDepuis(position.X, position.Y, 16, 16);
            Gfx.remplirRectangle(_position.X, _position.Y, 16, 16, 1, Color.AliceBlue, Color.DarkCyan);
        }

        public override void activer(Joueur joueur)
        {
            // L'effet par défaut du bonus est d'augmenter la portée de 1 case
            joueur.incrementerPorteeBombe();
        }
    }
}
