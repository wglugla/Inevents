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
    public static class Users
    {
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
