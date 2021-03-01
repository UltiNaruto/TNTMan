using System.Collections.Generic;
using TNTMan.entitees;

namespace TNTMan.map
{
    class Map
    {
        int id;
        Bloc[][] listeBlocs;
        List<Entite> listEntites;

        public int getId()
        {
            return id;
        }

        public Bonus[] getToutLesBonus()
        {
            return listEntites.FindAll((e) => e.GetType() == typeof(Bonus)).ToArray() as Bonus[];
        }

        public Bombe[] getToutLesBombes()
        {
            return listEntites.FindAll((e) => e.GetType() == typeof(Bonus)).ToArray() as Bombe[];
        }

        public Bloc getBlocA(int x, int y)
        {
            return listeBlocs[x][y];
        }
    }
}
