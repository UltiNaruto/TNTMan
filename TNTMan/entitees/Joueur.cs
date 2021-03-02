using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TNTMan.entitees
{
    class Joueur : Entite
    {
        // Attributs

        int id;
        Color couleur;
        float vitesse; // Valeur par défaut ?
        Score score;
        int nbBombes;
        int porteeBombes;

        // Constructeurs 

        /* Constructeur par défaut */
        Joueur() : base()
        {
            this.id = 1;
            this.couleur = Color.Red;
        }

        /* Constructeur avec choix */
        Joueur(int newId, Color c)
        {
            this.id = newId;
            this.couleur = c;
        }

        // Méthodes

        int GetId()
        {
            return this.id;
        }

        Color GetCouleur()
        {
            return this.couleur;
        }

        float GetVitesse()
        {
            return this.vitesse;
        }

        void SetVitesse(float nouvelleVitesse)
        {
            if (nouvelleVitesse > 0)
                this.vitesse = nouvelleVitesse;
            /* else
                Message d'erreur */
        }

        int GetNBTues()
        {
            return score.NbTues;
        }

        int GetNbVictoires()
        {
            return score.NbVictoires;
        }

        int GetNbBombes()
        {
            return this.nbBombes;
        }

        void PoserBombe()
        {

        }

        void AugmenterPorteeBombe()
        {

        }

        void DiminuerPorteeBombe()
        {

        }

        void IncrementerScore()
        {

        }

        void DecrementerScore()
        {

        }

        void MettreAJour()
        {

        }

        }
    }
}
