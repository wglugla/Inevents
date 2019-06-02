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
            string json = JsonConvert.SerializeObject(new
            {
                username,
                password
            });
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("http://localhost:5000/api/users");
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
