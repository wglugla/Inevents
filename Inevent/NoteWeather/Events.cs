using APIConnect;
using Inevent.Elements;
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
    public static class Events
    {
        public static async Task<Event[]> LoadEvents()
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("http://localhost:5000/api/events");
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

        public static async Task<Event[]> LoadEvent(int id)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("http://localhost:5000/api/events/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Event jsonObject = JsonConvert.DeserializeObject<Event>(result);
                    Event[] res = new Event[] { jsonObject };
                    return res;
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

        public static async Task<Event[]> LoadSigned(int userId)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("http://localhost:5000/api/users/" + userId + "/signed");
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
            catch(Exception e)
            {
                throw e;
            }
        }

        public static async Task<List<int>> LoadSignedId(int userId)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("http://localhost:5000/api/users/" + userId + "/signedid");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    List<int> jsonObject = JsonConvert.DeserializeObject<List<int>>(result);
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

        public static async Task<bool> AddMember(int eventId, int userId)
        {
            object data = new
            {
                userId
            };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync("http://localhost:5000/api/events/"+eventId+"/addmember", content);
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
            catch(Exception e)
            {
                throw e;
            }
        }

        public static async Task<bool> RemoveMember(int eventId, int userId)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync("http://localhost:5000/api/events/" + eventId + "/removemember/" + userId);
                if (response.IsSuccessStatusCode)
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

    }
}
