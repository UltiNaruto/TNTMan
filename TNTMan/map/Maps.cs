using System;
using System.Collections.Generic;
using TNTMan.entitees;
using TNTMan.map.blocs;

namespace TNTMan.map
{
    class Maps
    {
        internal static void parID(ref Map map)
        {
            if (map.getId() == 0)
                map.chargerMapParDefaut();
            else
                map.chargerMap(map.getId());
        }
    }
}
