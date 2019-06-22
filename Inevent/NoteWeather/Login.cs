using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using APIConnect;
using Inevent.Models;
using Newtonsoft.Json;

namespace Inevent
{
    /// <summary>
    /// Klasa obsługująca komunikację z API z zakresu logowania i rejestracji użytkownika
    /// </summary>
    public class Login
    {
        /// <summary>
        /// Metoda realizująca zapytanie z danymi do logowania
        /// </summary>
        /// <param name="username"> Login użytkownika </param>
        /// <param name="password"> Hasło użytkownika </param>
        /// <returns> Wartość true/false określające powodzenie requesta </returns>
        public async Task<bool> LoginUser (string username, string password)
        {
            object data = new
            {
                username,
                password
            };

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync("http://localhost:5000/api/login", content);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    LoginResponse jsonObject = JsonConvert.DeserializeObject<LoginResponse>(result);
                    AuthenticationToken.SaveToken(jsonObject.token);
                    Properties.Settings.Default.id = Convert.ToInt32(jsonObject.id);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
