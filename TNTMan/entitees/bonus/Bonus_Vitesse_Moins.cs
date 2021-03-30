using System;
using System.Drawing;
using TNTMan.entitees.bonus.enums;
using TNTMan.map;

namespace TNTMan.entitees.bonus
{
    class Bonus_Vitesse_Moins : Bonus
    {
        public Bonus_Vitesse_Moins(Map _map, float x, float y) : base(_map, x, y)
        {

        }

        public override string getNom()
        {
            return "Vitesse-";
        }

        public override TypeBonus getType()
        {
            return TypeBonus.Personnage;
        }

        public override void dessiner(IntPtr rendu)
        {
            Point _position = Map.getPositionEcranDepuis(position.X, position.Y, 16, 16);
            Gfx.remplirRectangle(_position.X, _position.Y, 16, 16, 1, Color.DarkGreen, Color.Black);
        }

        public override void activer(Joueur joueur)
        {
            // L'effet par défaut du bonus est de diminuer la vitesse de 0.01f
            joueur.decrementerVitesse();
        }
    }
}
