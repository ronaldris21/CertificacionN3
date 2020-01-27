using appTudo.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace appTudo.Services
{
    public class RestClient
    {
        private string url = "https://apitudo20200127084445.azurewebsites.net/api/buses";
        /// <summary>
        /// Metood para obtener todos los datos
        /// </summary>
        /// <returns></returns>
        public async Task<List<buses>> GetAll()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<buses>>(json);
                }
                catch (System.Exception e)
                {
                    return new List<buses>();
                }
            }
        }
        /// <summary>
        /// Metodo pare eliminar un registro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int id)
        {
            using (var client = new HttpClient())
            {

                try
                {
                    var response = await client.DeleteAsync(url+"/"+id.ToString());
                    return response.IsSuccessStatusCode;
                }
                catch (System.Exception e)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Metodo para actualizar un registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bus"></param>
        /// <returns></returns>
        public async Task<bool> Put(int id, buses bus)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var content = JsonConvert.SerializeObject(bus);
                    var contentData = new StringContent(content, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(url + "/" + id.ToString(), contentData);
                    return response.IsSuccessStatusCode;
                }
                catch (System.Exception e)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Metodo para agregar un registro
        /// </summary>
        /// <param name="bus"></param>
        /// <returns></returns>
        public async Task<bool> Post(buses bus)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var content = JsonConvert.SerializeObject(bus);
                    var contentData = new StringContent(content, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, contentData);
                    return response.IsSuccessStatusCode;
                }
                catch (System.Exception e)
                {
                    return false;
                }
            }
        }

    }
}
