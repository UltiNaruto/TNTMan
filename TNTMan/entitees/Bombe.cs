using System;
using System.Drawing;
using TNTMan.map;

namespace TNTMan.entitees
{
    class Bombe : Entite
    {
        // Attributs

        int id;
        string nom;
        int portee;
        Joueur proprietaire;
        int tempsRestantExplosion;

        // Méthodes
        string getNom()
        {
            return this.nom;
        }

        int getPortee()
        {
            return this.portee;
        }

        Joueur getProprietaire()
        {
            return this.proprietaire;
        }

        void explose()
        {
            // Explosion enclenchée
        }

        public override void mettreAJour()
        {
            if(tempsExplosion > 0)
                tempsExplosion -= 16; // décrémente de 16 ms par image
            else
                explose();
        }

        void poserBombe()
        {
            position = proprietaire.getPosition();
        }
    }
}
