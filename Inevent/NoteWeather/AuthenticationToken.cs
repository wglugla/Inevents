using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inevent
{
    /// <summary>
    /// Klasa statyczna zapewniająca interakcje z pamięcią, gdzie przechowywany jest token sesji
    /// </summary>
    public static class AuthenticationToken
    {
        /// <summary>
        /// Metoda zapisująca nowy token w pamięci
        /// </summary>
        /// <param name="token"> Token otrzymany w odpowiedzi serwera po logowaniu </param>
        public static void SaveToken(string token)
        {
            Properties.Settings.Default.accessToken = token;
        }

        /// <summary>
        /// Funkcja pobierajca token z pamieci
        /// </summary>
        /// <returns> Token w formacie string </returns>
        public static string GetToken()
        {
            return Properties.Settings.Default.accessToken;
        }
    }
}
