using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TNTMan
{
    class Sfx
    {
        internal static Dictionary<String, IntPtr> sons;
        internal static Dictionary<String, IntPtr> musiques;

        static int prechargerSons()
        {
            try
            {
                sons = new Dictionary<String, IntPtr>()
                {
                    // fin de manche
                    { "temps_ecoule", SDL_mixer.Mix_LoadWAV(@"res\sons\temps_ecoule.wav") },
                    // bonus
                    { "bonus_vitesse_plus", SDL_mixer.Mix_LoadWAV(@"res\sons\bonus\vitesse+.wav") },
                    { "bonus_vitesse_moins", SDL_mixer.Mix_LoadWAV(@"res\sons\bonus\vitesse-.wav") },
                    { "bonus_portee_plus", SDL_mixer.Mix_LoadWAV(@"res\sons\bonus\portee+.wav") },
                    { "bonus_portee_moins", SDL_mixer.Mix_LoadWAV(@"res\sons\bonus\portee-.wav") },
                    { "bonus_bombe_plus", SDL_mixer.Mix_LoadWAV(@"res\sons\bonus\bombe+.wav") },
                    { "bonus_bombe_moins", SDL_mixer.Mix_LoadWAV(@"res\sons\bonus\bombe-.wav") },
                    // explosion
                    { "explosion", SDL_mixer.Mix_LoadWAV(@"res\sons\bombes\explosion.wav") },
                    // boutons
                    { "clic_bouton", SDL_mixer.Mix_LoadWAV(@"res\sons\clic_bouton.wav") }
                };
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        static int prechargerMusiques()
        {
            try
            {
                musiques = new Dictionary<String, IntPtr>()
                {
                    // écran jeu
                    { "jeu_1", SDL_mixer.Mix_LoadMUS(@"res\musiques\jeu_1.mp3") },
                    { "jeu_2", SDL_mixer.Mix_LoadMUS(@"res\musiques\jeu_2.mp3") },
                    // écran titre
                    { "titre", SDL_mixer.Mix_LoadMUS(@"res\musiques\titre.mp3") }
                };
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        internal static int initialiser_son()
        {
            if (SDL_mixer.Mix_OpenAudio(44100, SDL_mixer.MIX_DEFAULT_FORMAT, SDL_mixer.MIX_DEFAULT_CHANNELS, 1024) < 0)
            {
                Program.MessageErr("Mix_OpenAudio: ERREUR ({0})\n", SDL.SDL_GetError());
                SDL.SDL_Quit();
                return 1;
            }

            if(prechargerSons() > 0)
            {
                Program.MessageErr("prechargerSons: ERREUR ({0})\n", SDL.SDL_GetError());
                SDL_mixer.Mix_CloseAudio();
                SDL.SDL_Quit();
                return 2;
            }

            if (prechargerMusiques() > 0)
            {
                Program.MessageErr("prechargerMusiques: ERREUR ({0})\n", SDL.SDL_GetError());
                SDL_mixer.Mix_CloseAudio();
                SDL.SDL_Quit();
                return 3;
            }

            SDL_mixer.Mix_AllocateChannels(sons.Count);
            ArreterJouerSons();
            ReglerVolumeSons(3);
            ReglerVolumeMusique(3);

            return 0;
        }

        internal static void deinitialiser_son()
        {
            foreach (var musique in musiques)
                SDL_mixer.Mix_FreeMusic(musique.Value);
            musiques.Clear();
            foreach (var son in sons)
                SDL_mixer.Mix_FreeChunk(son.Value);
            sons.Clear();
            SDL_mixer.Mix_CloseAudio();
        }

        internal static void ReglerVolumeSon(String son, int volume)
        {
            SDL_mixer.Mix_Volume(sons.Keys.ToList().IndexOf(son), volume);
        }

        internal static void ReglerVolumeSons(int volume)
        {
            foreach (var son in sons)
                ReglerVolumeSon(son.Key, volume);
        }

        internal static void JouerSon(String son)
        {
            int id_canal = sons.Keys.ToList().IndexOf(son);

            // trop de canaux utilisés
            if (id_canal == -1)
                return;

            // Son déjà joué
            if (SDL_mixer.Mix_Playing(id_canal) > 0)
                return;

            SDL_mixer.Mix_PlayChannel(id_canal, sons[son], 0);
        }

        internal static void ArreterJouerSon(String son)
        {
            int id_canal = sons.Keys.ToList().IndexOf(son);

            // Son non trouvé
            if (id_canal == -1)
                return;

            // Son non joué
            if (SDL_mixer.Mix_Playing(id_canal) == 0)
                return;

            SDL_mixer.Mix_HaltChannel(id_canal);
        }

        internal static void ArreterJouerSons()
        {
            foreach(var son in sons)
                ArreterJouerSon(son.Key);
        }

        internal static void ReglerVolumeMusique(int volume)
        {
            SDL_mixer.Mix_VolumeMusic(volume);
        }

        internal static void JouerMusique(String musique)
        {
            SDL_mixer.Mix_PlayMusic(musiques[musique], -1);
        }

        internal static void ArreterJouerMusique()
        {
            SDL_mixer.Mix_HaltMusic();
        }
    }
}
