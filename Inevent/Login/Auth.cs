using APIConnect;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NoteWeather;
using Newtonsoft.Json;

namespace Auth
{
    class Auth
    {
        public async Task<bool> Login(string username, string password)
        {
            try
            {
                string json = JsonConvert.SerializeObject(new
                {
                    username,
                    password
                });
                HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync("http://localhost:3000/api/login", new StringContent(json));
                if (response.IsSuccessStatusCode)
                {
                    string token = await response.Content.ReadAsStringAsync();
                    AuthenticationToken.SaveToken(token);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
