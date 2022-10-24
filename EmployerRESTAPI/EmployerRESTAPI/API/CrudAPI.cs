using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EmployerRESTAPI.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;

namespace EmployerRESTAPI.API
{
    // NOTE:
    // Little Note 
    // One is able to access the API without the Token do not know if this is intended but going to https://gorest.co.in/public/v2/users returns list of users
    public class CrudAPI
    {
        // for diffrent pages use e.g. https://gorest.co.in/public/v2/users?page=5

        /// <summary>
        /// Private read only fields
        /// </summary>
        /// 
        private static readonly string baseUrl = "https://gorest.co.in/public/v2/";
        private static readonly string access_token = "0bf7fb56e6a27cbcadc402fc2fce8e3aa9ac2b40d4190698eb4e8df9284e2023";
        HttpClient httpClient; // HTTP requests session instance.
        HttpResponseMessage response;

        /// <summary>
        /// Constructor definition
        /// </summary>
        public CrudAPI()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(access_token);
            response = new HttpResponseMessage();
        }

        /// <summary>
        /// Returns employee list
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAllEmployees()
        {
            response = await httpClient.GetAsync(baseUrl + "users");
            return response;
        }

        /// <summary>
        /// Get employee by name e.g. https://gorest.co.in/public-api/users?name=Agneya
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> FindEmployeeByName(string name)
        {
            response = await httpClient.GetAsync(baseUrl + "users?name=" + name);
            return response;
        }

        /// <summary>
        /// Create/Register new employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="email"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> AddEmployee(int id, string name, string gender, string email, string status)
        {
            var inputData = new Employee
            {
                ID = id,
                Name = name,
                Gender = gender,
                Email = email,
                Status = status
            };
            var json = JsonConvert.SerializeObject(inputData); // passing data in request body as a raw string
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            response = await httpClient.PostAsync(baseUrl + "users/", stringContent);
            return response;
        }

        /// <summary>
        /// Delete employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteById(int id)
        {
            response = await httpClient.DeleteAsync(baseUrl + "users/" + id);
            return response;
        }
    }
}
