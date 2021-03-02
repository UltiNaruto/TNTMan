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

        void Explose()
        {

        }

        void MettreAJour()
        {

        }
    }
}