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
        string GetNom()
        {
            return this.nom;
        }

        int GetPortee()
        {
            return this.portee;
        }

        Joueur GetProprietaire()
        {
            return this.proprietaire;
        }

        void Attendre()
        {
            TimeSpan duree;
            duree = TimeSpan.FromSeconds(this.tempsRestantExplosion);
        }

        void Explose()
        {
            // En attente 
            Attendre();

            // Explosion enclenchée
        }

        void MettreAJour()
        {

        }

        void PoserBombe()
        {
            position = proprietaire.getPosition();
        }
    }
}