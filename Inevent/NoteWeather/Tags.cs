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
    public static class Tags
    {
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
