using APIConnect;
using Inevent.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Inevent
{
    /// <summary>
    /// Klasa statyczna obsługująca komunikację z API dotyczącą tagów
    /// </summary>
    public static class Tags
    {
        /// <summary>
        /// Pobiera wszystkie tagi z bazy
        /// </summary>
        /// <returns> Tablica obiektów Tag zawierająca wszystkie istniejące tagi </returns>
        public static async Task<Tag[]> GetAllTags()
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("http://localhost:5000/api/tags");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Tag[] jsonObject = JsonConvert.DeserializeObject<Tag[]>(result);
                    return jsonObject;
                }
                else
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Pobiera tagi przypisane do użytkownika
        /// </summary>
        /// <param name="userId"> ID użytkownika </param>
        /// <returns> Tablica obiektów typu Tag zawierająca tagi użytkownika </returns>
        public static async Task<Tag[]> GetUserTags(int userId)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("http://localhost:5000/api/users/" + userId + "/tags");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Tag[] jsonObject = JsonConvert.DeserializeObject<Tag[]>(result);
                    return jsonObject;
                }
                else
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
