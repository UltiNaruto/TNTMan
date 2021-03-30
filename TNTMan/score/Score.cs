namespace TNTMan.entitees
{
    class Score
    {
        int nbVictoires;
        int nbTues;

        public int getNbVictoires()
        {
            return this.nbVictoires;
        }

        public int getNbTues()
        {
            return this.nbTues;
        }

        public void incrementerVictoire()
        {
            this.nbVictoires++;
        }

        public void incrementerTue()
        {
            this.nbTues++;
        }
    }
}
