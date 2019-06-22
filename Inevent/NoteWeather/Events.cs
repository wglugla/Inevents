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
    /// <summary>
    /// Statyczna klasa obsługująca wszystkie zapytania związane z wydarzeniami
    /// </summary>
    public static class Events
    {
        /// <summary>
        /// Asynchroniczne zadanie z zapytaniem do API o wszystkie wydarzenia, jakie znajdują się w bazie
        /// </summary>
        /// <returns> Tablica obiektów Event zawierająca informację o wszystkich eventach </returns>
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

        /// <summary>
        /// Asynchroniczne zadanie z zapytaniem do API o wszystkie wydarzenia, wydzialące jedynie ich identyfikatory
        /// </summary>
        /// <returns> Tablica identyfikatorów wszystkich eventów (tablica typu int) </returns>
        public static async Task<int[]> LoadEventsId()
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("http://localhost:5000/api/events");
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Event[] jsonObject = JsonConvert.DeserializeObject<Event[]>(result);
                    List<int> res = new List<int>();
                    foreach(Event ev in jsonObject)
                    {
                        res.Add(ev.Id);
                    }
                    return res.ToArray();
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
        /// Pobiera szczegółowe informacje o evencie
        /// </summary>
        /// <param name="id"> ID eventu </param>
        /// <returns> Tablica typu Event z obiektem zawierającym informacje na temat wydarzenia </returns>
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

        /// <summary>
        /// Pobiera wydarzenia, w których dany użytkownik weźmie udział
        /// </summary>
        /// <param name="userId"> ID użytkownika </param>
        /// <returns> Tablica eventów typu Event </returns>
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

        /// <summary>
        /// Pobiera identyfikatory eventów, w których użytkownik weźmie udział
        /// </summary>
        /// <param name="userId"> ID użytkownika </param>
        /// <returns> Tablica identyfikatorów (typu int) </returns>
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

        /// <summary>
        /// Pobiera wszystkie eventy, które posiadają tag o podanym identyfikatorze
        /// </summary>
        /// <param name="tagId"> ID tagu </param>
        /// <returns> Tablica eventów typu Event </returns>
        public static async Task<Event[]> FindEventByTag(int tagId)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync("http://localhost:5000/api/events/tag/" + tagId);
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


        /// <summary>
        /// Realizuje zapyytanie typu POST dodające nowego członka wydarzenia
        /// </summary>
        /// <param name="eventId"> ID wydarzenia </param>
        /// <param name="userId"> ID użytkownika </param>
        /// <returns> Informację true/false na teamt powodzenia dodania członka </returns>
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

        /// <summary>
        /// Realizuje zapytanie typu DELETE usuwające użytkownika z listy członków wydarzenia
        /// </summary>
        /// <param name="eventId"> ID wydarzenia </param>
        /// <param name="userId"> ID użytkownika </param>
        /// <returns> Informację true/false na temat powodzenia usunięcia </returns>
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

        /// <summary>
        /// Tworzy nowe wydarzenie
        /// </summary>
        /// <param name="ownerId"> ID właściciela </param>
        /// <param name="title"> Tytuł wydarzenia </param>
        /// <param name="place"> Miejsce wydarzenia </param>
        /// <param name="date"> Data wydarzenia </param>
        /// <param name="description"> Opis wydarzenia </param>
        /// <returns> ID nowoutworzonego wydarzenia </returns>
        public static async Task<int> CreateEvent(int ownerId, string title, string place, string date, string description)
        {
            object obj = new
            {
                ownerId,
                title,
                place,
                date,
                description
            };
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.PostAsync("http://localhost:5000/api/events", content);
                int code = (int)response.StatusCode;
                if (code >= 200 && code < 300)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Event jsonObject = JsonConvert.DeserializeObject<Event>(result);
                    return jsonObject.Id;
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
        /// Usuwa wydarzenie
        /// </summary>
        /// <param name="eventId"> ID wydarzenia </param>
        /// <returns> Wartość true/false w zależności od powodzenia operacji usunięcia </returns>
        public static async Task<bool> DeleteEvent(int eventId)
        {
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync("http://localhost:5000/api/events/" + eventId);
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

        /// <summary>
        /// Modyfikuje tagi przypisane do wydarzenia
        /// </summary>
        /// <param name="eventId"> ID wydarzenia </param>
        /// <param name="tagsIds"> Tablica zawierająca identyfikatory tagów, które mają być przypisane do wydarzenia </param>
        /// <returns>  Wartość true/false określająca powodzenie operacji </returns>
        public static async Task<bool> ChangeEventTags(int eventId, int[] tagsIds)
        {
            var json = JsonConvert.SerializeObject(tagsIds);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync("http://localhost:5000/api/events/" + eventId + "/tags", content);
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
        /// Modyfikuje informacje o wydarzeniu 
        /// </summary>
        /// <param name="id"> ID wydarzenia </param>
        /// <param name="ownerId"> ID właściciela  </param>
        /// <param name="title"> Tytuł </param>
        /// <param name="place"> Miejsce </param>
        /// <param name="date"> Data </param>
        /// <param name="description"> Opis </param>
        /// <returns> Wartość true/false określająca powodzenie operacji </returns>
        public static async Task<bool> UpdateEvent(int id, int ownerId, string title, string place, string date, string description)
        {
            object obj = new
            {
                id,
                ownerId,
                title,
                date,
                place,
                description
            };
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await ApiHelper.ApiClient.PutAsync("http://localhost:5000/api/events/" + id, content);
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

    }
}
