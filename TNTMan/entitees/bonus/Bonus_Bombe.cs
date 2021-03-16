using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TNTMan.entitees.bonus
{
    class Bonus_Bombe : Bonus
    {
        public Bonus_Bombe() : base("Vitesse", Color.MediumPurple, Color.Purple, 10, enums.TypeBonus.Bombe, true)
        {

        }

        public override void activer()
        {
            if (!this.estActif())
            {
                setActif(true);
            }
            // L'effet par défaut du bonus est d'augmenter la portée de 5
            getProprietaire().setPortee(getProprietaire().getPortee() + 5);
        }
    }
}
