using System;
using System.Collections.Generic;
using System.Text;

namespace TNTMan.entitees
{
    class Score
    {
        public int NbVictoires;
        public int NbTues;

        void IncrementerVictoire()
        {
            this.NbVictoires++;
        }

        void IncrementerTues()
        {
            this.NbTues++;
        }
    }
}
