using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TNTMan.entitees;
using TNTMan.entitees.bonus;
using TNTMan.map.blocs;

namespace TNTMan.map
{
    class Map
    {
        public static readonly int LARGEUR_GRILLE = 15;
        public static readonly int LONGUEUR_GRILLE = 11;

        int id;
        Bloc[,] listeBlocs;
        List<Entite> listeEntites;
        List<Entite> listeEntitesAAjouter;
        public List<PointApparition> pointApparitions;

        public Map()
        {
            this.listeBlocs = new Bloc[Map.LARGEUR_GRILLE, Map.LONGUEUR_GRILLE];
            this.listeEntites = new List<Entite>();
            this.listeEntitesAAjouter = new List<Entite>();
            this.pointApparitions = new List<PointApparition>();
        }

        public int getId()
        {
            return id;
        }

        public void dessiner(IntPtr rendu)
        {
            foreach(var e in listeEntites.FindAll((e) => !e.estMort()))
            {
                e.dessiner(rendu);
            }
        }

        public void executerPourToutEntite(Action<Entite> action)
        {
            try
            {
                foreach (Entite e in listeEntites)
                    action(e);
            }
            catch { }
        }

        public Bloc getBlocA(int x, int y)
        {
            try
            {
                return listeBlocs[x, y];
            }
            catch
            {
                return null;
            }
        }

        // x => ?.5f
        // y => ?.5f
        public void tuerEntiteA(float x, float y)
        {
            executerPourToutEntite((e) => {
                var position = e.getPosition();
                if (!e.estMort())
                {
                    if (position.X >= x - 0.5f
                     && position.X <= x + 0.5f
                     && position.Y >= y - 0.5f
                     && position.Y <= y + 0.5f)
                    {
                        e.tuer();
                    }
                }
            });
        }

        internal void detruireBlocA(int x, int y)
        {
            Bonus bonus = Utils.creerBonusAleatoire(this, x + 0.5f, y + 0.5f);
            listeBlocs[x, y] = null;
            if(bonus != null)
                listeEntitesAAjouter.Add(bonus);
        }

        public static Point getPositionEcranDepuis(float x, float y, int w=32, int h=32)
        {
            Size resolution = Gfx.getResolution();
            Size tailleGrille = new Size(Map.LARGEUR_GRILLE * Bloc.TAILLE_BLOC, Map.LONGUEUR_GRILLE * Bloc.TAILLE_BLOC);
            float dw = ((float)Bloc.TAILLE_BLOC - w) / 2.0f;
            float dh = ((float)Bloc.TAILLE_BLOC - h) / 2.0f;
            return new Point((int)((resolution.Width - tailleGrille.Width) / 2 + x * Bloc.TAILLE_BLOC - dw), (int)((resolution.Height - tailleGrille.Height) / 2 + y * Bloc.TAILLE_BLOC - dh));
        }

        internal void ajoutEntite(Entite entite)
        {
            if (entite == null)
                return;
            listeEntitesAAjouter.Add(entite);
        }

        internal Type entiteExiste(int x, int y)
        {
            return listeEntites.Find((e) => (int)e.getPosition().X == x && (int)e.getPosition().Y == y).GetType();
        }

        internal List<Entite> trouverEntite(int x, int y, Type type)
        {
            List<Entite> entites = new List<Entite>(listeEntites);
            entites.AddRange(listeEntitesAAjouter);
            return entites.FindAll((e) => (e.GetType() == type || e.GetType().BaseType == type) && (int)e.getPosition().X == x && (int)e.getPosition().Y == y).ToList();
        }

        internal void dechargerMap()
        {
            int x, y;
            for (x = 0; x < Map.LARGEUR_GRILLE; x++)
            {
                for (y = 0; y < Map.LONGUEUR_GRILLE; y++)
                {
                    listeBlocs[x, y] = null;
                }
            }
            listeEntites.Clear();
            listeEntitesAAjouter.Clear();
        }

        internal void chargerMap(int _id)
        {
            id = _id;
            String chemin = String.Format("maps/{0}.bma", id);
            if (id == 0)
                chargerMapParDefaut();
            else
                throw new NotImplementedException();
        }

        internal void chargerMapParDefaut()
        {
            int x, y;
            for (x = 0; x < Map.LARGEUR_GRILLE; x++)
            {
                listeBlocs[x, 0] = new BlocIncassable();
                listeBlocs[x, Map.LONGUEUR_GRILLE - 1] = new BlocIncassable();
            }
            for (y = 0; y < Map.LONGUEUR_GRILLE; y++)
            {
                listeBlocs[0, y] = new BlocIncassable();
                listeBlocs[Map.LARGEUR_GRILLE - 1, y] = new BlocIncassable();
            }
            for (x = 2; x < Map.LARGEUR_GRILLE - 1; x += 2)
                for (y = 2; y < Map.LONGUEUR_GRILLE - 1; y += 2)
                {
                    listeBlocs[x, y] = new BlocIncassable();
                }

            for (int i = 0; i < 70;)
            {
                x = Program.random.Next(1, Map.LARGEUR_GRILLE - 1);
                y = Program.random.Next(1, Map.LONGUEUR_GRILLE - 1);

                // on ne génére pas dans les coins de la grille
                if ((x < 3 && y < 3)
                || (x < 3 && y > Map.LONGUEUR_GRILLE - 4)
                || (x > Map.LARGEUR_GRILLE - 4 && y < 3)
                || (x > Map.LARGEUR_GRILLE - 4 && y > Map.LONGUEUR_GRILLE - 4))
                    continue;

                // on ne change pas les blocs déjà générés
                if (listeBlocs[x, y] != null)
                    continue;

                listeBlocs[x, y] = new BlocTerre();
                i++;
            }

            pointApparitions.Add(new PointApparition(1, 1));
            pointApparitions.Add(new PointApparition(13, 1));
            pointApparitions.Add(new PointApparition(1, 9));
            pointApparitions.Add(new PointApparition(13, 9));
        }

        internal void gererTouches(byte[] etats)
        {
            IEnumerable<Entite> joueurs = listeEntites.FindAll((e) => e.GetType() == typeof(Joueur));
            foreach (var e in joueurs)
            {
                ((Joueur)e).mettreAJour(etats);
            }
        }

        internal void mettreAJour()
        {
            IEnumerable<Entite> entites = listeEntites.FindAll((e) => e.GetType() != typeof(Joueur));
            foreach(var e in entites)
            {
                e.mettreAJour();
            }

            listeEntites.RemoveAll((e) => e.estMort());
            listeEntites.AddRange(listeEntitesAAjouter);
            listeEntites.Sort(new Comparison<Entite>((e1, e2) =>
            {
                if (e1.GetType() == typeof(Joueur) && e2.GetType() != typeof(Joueur))
                    return 1;
                if (e1.GetType() != typeof(Joueur) && e2.GetType() == typeof(Joueur))
                    return -1;
                return 0;
            }));
            listeEntitesAAjouter.Clear();
        }
    }
}
