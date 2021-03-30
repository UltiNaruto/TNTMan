using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TNTMan.entitees.bonus;
using TNTMan.map;

namespace TNTMan
{
    class Utils
    {
        internal static byte[] PtrToArray(IntPtr ptr, int size)
        {
            List<byte> liste = null;

            if (ptr == IntPtr.Zero) return null;

            liste = new List<byte>();
            for (int i = 0; i < size; i++)
                liste.Add(Marshal.ReadByte(ptr + i));

            return liste.ToArray();
        }

        static String selectionnerBonusAleatoire()
        {
            Dictionary<String, int> dict_bonus = new Dictionary<String, int>()
            {
                { "Vitesse+", 10 },
                { "Vitesse-", 5 },
                { "Portee+", 10 },
                { "Portee-", 5 },
                { "Bombe+", 10 },
                { "Bombe-", 5 },
                { "Vide", 30 }
            };

            int nombre = Program.random.Next(0, dict_bonus.Values.Sum());
            int n = 0;
            int n_suivant = 0;
            foreach (KeyValuePair<String, int> kvp in dict_bonus)
            {
                n_suivant += kvp.Value;
                if (nombre >= n && nombre < n_suivant)
                    return kvp.Key;
                n = n_suivant;
            }

            // ne devrait pas arriver mais on retourne quand même
            return "Vide";
        }

        internal static Bonus creerBonusAleatoire(Map _map, float x, float y)
        {
            String nom_bonus = selectionnerBonusAleatoire();
            switch (nom_bonus)
            {
                case "Vitesse+":
                    return new Bonus_Vitesse_Plus(_map, x, y);
                case "Vitesse-":
                    return new Bonus_Vitesse_Moins(_map, x, y);
                case "Portee+":
                    return new Bonus_Portee_Plus(_map, x, y);
                case "Portee-":
                    return new Bonus_Portee_Moins(_map, x, y);
                case "Bombe+":
                    return new Bonus_Bombe_Plus(_map, x, y);
                case "Bombe-":
                    return new Bonus_Bombe_Moins(_map, x, y);
                case "Vide":
                default:
                    return null;
            }
        }
    }
}
