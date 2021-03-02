using System;
using System.Collections.Generic;
using System.Text;
using TNTMan.entitees;

namespace TNTMan.map
{
    class Session
    {
        List<Joueur> joueurs;
        Map map;
        int MancheActuelle;
        int nbManches;
        long tempsImparti;
        long tempsRestant;
        long tempsMortSubite;
    }
}
