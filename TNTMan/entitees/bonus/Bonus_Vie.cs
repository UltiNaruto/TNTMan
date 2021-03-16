using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TNTMan.entitees.bonus
{
    class Bonus_Vie : Bonus
    {
        public Bonus_Vie() : base("Vitesse", Color.GreenYellow, Color.DarkGreen, 10, enums.TypeBonus.Vie, true)
        {

        }

        public override void activer()
        {
            if (!this.estActif())
            {
                setActif(true);
            }
            // L'effet par défaut du bonus est la récupération de vie
        }
    }
}
