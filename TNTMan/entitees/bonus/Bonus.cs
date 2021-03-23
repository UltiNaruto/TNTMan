using System;
using System.Drawing;
using TNTMan.entitees.bonus.enums;
using TNTMan.map;

namespace TNTMan.entitees.bonus
{
    class Bonus : Entite
    {
        Joueur proprietaire;
        string nom;
        Color couleur, couleurExt;
        long duree;
        TypeBonus type;
        bool actif;

        public Bonus (string n, Color c, Color ce, long d, TypeBonus tb, bool a)
        {
            proprietaire = null;
            nom = n;
            couleur = c;
            couleurExt = ce;
            duree = d;
            type = tb;
            actif = a;
        }

        public Joueur getProprietaire()
        {
            return proprietaire;
        }

        public void setProprietaire(Joueur joueur)
        {
            proprietaire = joueur;
        }

        public string getNom()
        {
            return nom;
        }

        public void setNom(string n)
        {
            nom = n;
        }

        public long getDuree()
        {
            return duree;
        }

        public void setDuree(long d)
        {
            duree = d;
        }

        public TypeBonus getType()
        {
            return type;
        }

        public void setType(TypeBonus t)
        {
            type= t;
        }

        public bool getActif()
        {
            return actif;
        }

        public void setActif(bool a)
        {
            actif = a;
        }

        public bool estActif()
        {
            if (getActif())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public virtual void activer()
        {
        }

        public override void dessiner(IntPtr rendu)
        {
            Point _position = Map.getPositionEcranDepuis(position.X, position.Y, 16, 16);
            Gfx.remplirRectangle(_position.X, _position.Y, 16, 16, 1, couleur, couleurExt);
        }

        public override void mettreAJour()
        {
            int _x = (int)position.X;
            int _y = (int)position.Y;
            Joueur joueur_en_collision = null;
            // Disparition de l'écran si le joueur rentre en collision
            // Disparition de la liste des entités après que l'effet soit dissipé
        }
    }
}
