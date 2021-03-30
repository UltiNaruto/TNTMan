using SDL2;
using System;
using System.Collections.Generic;
using System.Drawing;
using TNTMan.ecrans;
using TNTMan.entitees;
using TNTMan.map.blocs;

namespace TNTMan.map
{
    class Session
    {
        List<Joueur> joueurs;
        Map map;
        int mancheActuelle;
        int nbManches;
        long tempsImparti;
        DateTime tempsDebutManche;
        long tempsMortSubite;

        public Session(int nb_joueurs, int id_map, int nb_manches, long temps_imparti, long temps_mort_subite)
        {
            joueurs = new List<Joueur>();
            map = new Map();
            map.chargerMap(id_map);
            for (int i = 0; i < nb_joueurs; i++)
            {
                switch (i)
                {
                    case 0:
                        joueurs.Add(new Joueur(1, 1.5f, 1.5f, map, SDL.SDL_Scancode.SDL_SCANCODE_W, SDL.SDL_Scancode.SDL_SCANCODE_S, SDL.SDL_Scancode.SDL_SCANCODE_A, SDL.SDL_Scancode.SDL_SCANCODE_D, SDL.SDL_Scancode.SDL_SCANCODE_SPACE));
                        break;
                    case 1:
                        joueurs.Add(new Joueur(2, 13.5f, 1.5f, map, SDL.SDL_Scancode.SDL_SCANCODE_UP, SDL.SDL_Scancode.SDL_SCANCODE_DOWN, SDL.SDL_Scancode.SDL_SCANCODE_LEFT, SDL.SDL_Scancode.SDL_SCANCODE_RIGHT, SDL.SDL_Scancode.SDL_SCANCODE_RETURN));
                        break;
                    case 2:
                        joueurs.Add(new Joueur(3, 1.5f, 9.5f, map));
                        break;
                    case 3:
                        joueurs.Add(new Joueur(4, 13.5f, 9.5f, map));
                        break;
                    default:
                        Program.MessageErr("Nombre de joueurs incorrect!");
                        break;
                }
            }
            foreach(var joueur in joueurs)
            {
                map.ajoutEntite(joueur);
            }
            mancheActuelle = 1;
            nbManches = nb_manches;
            tempsDebutManche = DateTime.Now;
            tempsImparti = temps_imparti;
            tempsMortSubite = temps_mort_subite;
        }

        public void dessiner(IntPtr rendu)
        {
            DateTime temps_actuel = DateTime.Now;
            Size resolution = Gfx.getResolution();
            long temps_restant = tempsImparti - (long)(temps_actuel - tempsDebutManche).TotalSeconds;
            Size cadre_temps_restant = Gfx.getTailleRectangleTexte(18, "Temps Restant : {0}:{1:D2}", temps_restant / 60, temps_restant % 60);
            for (int x = 0; x < Map.LARGEUR_GRILLE; x++)
                for (int y = 0; y < Map.LONGUEUR_GRILLE; y++)
                {
                    Bloc bloc = map.getBlocA(x, y);
                    if (bloc != null)
                    {
                        bloc.dessiner(rendu, x, y);
                    }
                }

            map.executerPourToutEntite((e) =>
            {
                if (!e.estMort())
                {
                    e.dessiner(rendu);
                }
            });

            for (int i = 0; i < joueurs.Count; i++)
            {
                if (i >= 2)
                {
                    Gfx.dessinerTexte(5, resolution.Height - i * 20, 18, Color.Black, "J{0} - B:{1}/{4}|P:{2}|V:{3}", i + 1, joueurs[i].getNbBombes(), joueurs[i].getPortee(), (int)((joueurs[i].getVitesse() - 0.05) / 0.01f) + 1, joueurs[i].getMaxNbBombes());
                }
                else
                {
                    Gfx.dessinerTexte(5, 5 + i * 20, 18, Color.Black, "J{0} - B:{1}/{4}|P:{2}|V:{3}", i + 1, joueurs[i].getNbBombes(), joueurs[i].getPortee(), (int)((joueurs[i].getVitesse() - 0.05) / 0.01f) + 1, joueurs[i].getMaxNbBombes());
                }
            }

            Gfx.dessinerTexte(resolution.Width / 2 - cadre_temps_restant.Width / 2, 5, 18, Color.Black, "Temps Restant : {0}:{1:D2}", temps_restant / 60, temps_restant % 60);
        }

        public void gererTouches(byte[] etats)
        {
            map.executerPourToutEntite((e) =>
            {
                if (e.estMort())
                {
                    map.supprimerEntite(e);
                }
                else
                {
                    if (e.GetType() != typeof(Joueur))
                        e.mettreAJour();
                    else
                        ((Joueur)e).mettreAJour(etats);
                }
            });
        }

        void finDeLaManche(int raison)
        {
            // si un joueur gagne
            if (raison > 0)
            {
                joueurs[raison - 1].incrementerVictoire();
            }

            if (mancheActuelle <= nbManches)
            {
                map.dechargerMap();
                map.chargerMap(map.getId());
                foreach (var joueur in joueurs)
                {
                    switch (joueur.getId())
                    {
                        case 1:
                            joueur.reapparaitre(1.5f, 1.5f);
                            break;
                        case 2:
                            joueur.reapparaitre(13.5f, 1.5f);
                            break;
                        case 3:
                            joueur.reapparaitre(1.5f, 9.5f);
                            break;
                        case 4:
                            joueur.reapparaitre(13.5f, 9.5f);
                            break;
                        default:
                            Program.MessageErr("Id joueur incorrect!");
                            break;
                    }
                    map.ajoutEntite(joueur);
                }

                // On réinitialise le temps
                tempsDebutManche = DateTime.Now;
                // On passe à la manche suivante
                mancheActuelle++;
            }
            else
            {
                // fin de la partie
                Gfx.changerEcran(new Ecran_Titre());
            }
        }

        public void mettreAJour()
        {
            DateTime temps_actuel = DateTime.Now;
            List<Joueur> joueurs_en_vie = joueurs.FindAll((j) => !j.estMort());
            // Si il ne reste aucun joueur en vie ou que le temps est écoulé c'est la fin de la manche
            if (joueurs_en_vie.Count < 1 || tempsImparti - (temps_actuel - tempsDebutManche).TotalSeconds <= 0)
                finDeLaManche(0);
            // Si il reste un joueur en vie il est déclaré vainqueur
            if (joueurs_en_vie.Count == 1)
                finDeLaManche(joueurs_en_vie[0].getId());
        }
    }
}
