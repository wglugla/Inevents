using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIConnect
{
    /// <summary>
    /// Klasa ułatwiająca wysyłanie requestów w formie json
    /// </summary>
    /// <remarks>
    /// Tworzy nowy obiekt HttpClient, ustawiając odpowiednie nagłówki dla application/json
    /// </remarks>
    public static class ApiHelper
    {
        /// <summary>
        /// Statyczna instancja HttpClient
        /// </summary>
        public static HttpClient ApiClient { get; set; }

        /// <summary>
        /// Wywołuje konstruktor i ustawia domyślny nagłówek na obsługę application/json
        /// </summary>
        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
