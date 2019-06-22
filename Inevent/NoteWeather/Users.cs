using APIConnect;
using Inevent.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Inevent
{
    /// <summary>
    /// Statyczna klasa obsługująca zapytania do API dotyczące użytkowników
    /// </summary>
    public static class Users
    {
        /// <summary>
        /// Pobiera wszystkich użytkowników z bazy
        /// </summary>
        /// <returns> Tablica obiektów zawierających informacje o użytkownikach </returns>
        public static async Task<object> LoadUsers()
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("http://localhost:5000/api/users");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Pobiera informacje na temat zalogowanego użytkownika
        /// </summary>
        /// <remarks>
        /// (na podstawie ID pobranego podczas logowania)
        /// </remarks>
        /// <returns> Obiekty typu User </returns>
        public static async Task<User> LoadUser()
        {
            int userId = Properties.Settings.Default.id;
            try
            {
                using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5000/api/users/" + userId))
                {
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.accessToken);
                    HttpResponseMessage response = await ApiHelper.ApiClient.SendAsync(requestMessage);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        User jsonObject = JsonConvert.DeserializeObject<User>(result);
                        return jsonObject;
                    }
                    else
                    {
                        throw new Exception("Status code: " + response.StatusCode);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Modyfikuje tagi, który są przypisane do użytkownika
        /// </summary>
        /// <param name="userId"> ID użytkownika </param>
        /// <param name="tagsIds"> Tablica id tagów, które mają być przypisane do użytkownika </param>
        /// <returns></returns>
        public static async Task<bool> ChangeUserTags(int userId, int[] tagsIds)
        {
            var json = JsonConvert.SerializeObject(tagsIds);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync("http://localhost:5000/api/users/" + userId + "/tags", content);
                int code = (int)response.StatusCode;
                if (code >= 200 && code < 300)
                {
                    return true;
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
        /// Pobiera wydarzenia stworzone przez użytkownika
        /// </summary>
        /// <param name="userId"> ID użytkownika </param>
        /// <returns> Tablica obiektów typu Event zawierająca wydarzenia utworzone przez podanego użytkownika </returns>
        public static async Task<Event[]> GetCreatedEvents(int userId)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("http://localhost:5000/api/users/" + userId + "/created");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Event[] jsonObject = JsonConvert.DeserializeObject<Event[]>(result);
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
