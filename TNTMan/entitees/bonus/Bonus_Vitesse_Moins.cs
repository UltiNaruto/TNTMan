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
            texture = Gfx.images["bonus_vitesse_moins"];
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
            Point _position = Map.getPositionEcranDepuis((int)position.X, (int)position.Y, 32, 32);
            Gfx.dessinerImage(_position.X, _position.Y, 32, 32, texture);
        }

        public override void activer(Joueur joueur)
        {
            // L'effet par défaut du bonus est de diminuer la vitesse de 0.01f
            joueur.decrementerVitesse();
            Sfx.JouerSon("bonus_vitesse_moins");
        }
    }
}
