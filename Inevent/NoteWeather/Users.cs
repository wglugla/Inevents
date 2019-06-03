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
    class Users
    {
        public async Task<object> LoadUsers()
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

        public async Task<User> LoadUser()
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
    }
}
