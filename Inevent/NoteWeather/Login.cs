using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using APIConnect;
using Newtonsoft.Json;

namespace Inevent
{
    public class Login
    {
        public async Task<bool> LoginUser (string username, string password)
        {
            object data = new
            {
                username = username,
                password = password
            };

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync("http://localhost:5000/api/login", content);
                if (response.IsSuccessStatusCode)
                {
                    string token = await response.Content.ReadAsStringAsync();
                    AuthenticationToken.SaveToken(token);
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
