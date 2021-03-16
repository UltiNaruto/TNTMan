using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TNTMan.entitees.bonus
{
    class Bonus_Joueur : Bonus
    {
        public Bonus_Joueur() : base("Vitesse", Color.AliceBlue, Color.DarkCyan, 10, enums.TypeBonus.Personnage, true)
        {

        }

        public override void activer()
        {
            if (!this.estActif())
            {
                setActif(true);
            }
            // L'effet par défaut du bonus est de doubler la vitesse du joueur
            getProprietaire().setVitesse(getProprietaire().getVitesse() * 2);
        }
    }
}
