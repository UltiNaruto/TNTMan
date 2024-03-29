﻿using System.Drawing;
using System.Linq;
using TNTMan.entitees.bonus.enums;
using TNTMan.map;

namespace TNTMan.entitees.bonus
{
    class Bonus : Entite
    {
        public Bonus (Map _map, float x, float y) : base(_map)
        {
            position = new PointF(x, y);
            statut = true;
        }

        public virtual string getNom()
        {
            return "";
        }

        public virtual long getDuree()
        {
            return -1;
        }

        public virtual TypeBonus getType()
        {
            return default(TypeBonus);
        }

        public virtual bool estActif()
        {
            return false;
        }

        public override void mettreAJour()
        {
            Joueur joueur = (Joueur)map.trouverEntite((int)position.X, (int)position.Y, typeof(Joueur)).FirstOrDefault();
            if (joueur != null)
            {
                ramasser(joueur);
                if (!estActif())
                {
                    activer(joueur);
                }
                tuer();
            }
        }

        public void ramasser(Joueur joueur)
        {
            // si bonus actif alors stocker son nom pour le joueur
            // ainsi que sa durée restante
        }

        public virtual void activer(Joueur joueur)
        {
        }
    }
}
