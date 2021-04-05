using System;
using System.Drawing;
using TNTMan.entitees.bonus.enums;
using TNTMan.map;

namespace TNTMan.entitees.bonus
{
    class Bonus_Bombe_Plus : Bonus
    {
        public Bonus_Bombe_Plus(Map _map, float x, float y) : base(_map, x, y)
        {
            texture = Gfx.images["bonus_bombe_plus"];
        }

        public override string getNom()
        {
            return "Bombe+";
        }

        public override TypeBonus getType()
        {
            return TypeBonus.Bombe;
        }

        public override void dessiner(IntPtr rendu)
        {
            Point _position = Map.getPositionEcranDepuis(position.X, position.Y, 16, 16);
            Gfx.dessinerImage(_position.X, _position.Y, 16, 16, texture);
        }

        public override void activer(Joueur joueur)
        {
            // L'effet par défaut du bonus est d'augmenter le nombre de bombes de 1
            joueur.incrementerMaxBombe();
            joueur.incrementerBombe();
        }
    }
}
