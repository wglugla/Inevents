using APIConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NoteWeather
{
    class Users
    {
        public async Task<object> LoadUsers()
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("http://localhost:3000/api/users");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return response.StatusCode;
                }
            }
            catch(Exception e)
            {
                return e;
            }
        }
    }
}
