﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using eimprovement.Proxy.Model;

namespace eimprovement.Proxy.Pet
{
    public class PetProxy
    {
        private string baseUrl { get; set; }
        private string keyAzure { get; set; }
        private string contentType { get; set; }

        HttpClient httpClient = new HttpClient();

        public PetProxy()
        {
            baseUrl = ConfigurationManager.AppSettings.Get("BaseURL");
            keyAzure = ConfigurationManager.AppSettings.Get("KeyAzure");
            contentType = ConfigurationManager.AppSettings.Get("ContentType");
        }

        public async Task<Response> GetPets(string filter = "available")
        {
            Response responseModel = new Response();
            
            try
            {
                using (httpClient)
                {
                    httpClient.DefaultRequestHeaders.Clear();                   
                    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", keyAzure);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                    HttpResponseMessage response = new HttpResponseMessage();

                    response = await httpClient.GetAsync(baseUrl+ "pet/findByStatus?status=" + filter);

                    responseModel = new Response()
                    {
                        code = (int)response.StatusCode,
                        content = response.Content.ReadAsStringAsync().Result,
                        message = response.RequestMessage.ToString()
                    };
                }
            }
            catch
            {
                throw;
            }
            return responseModel;
        }

        public async Task<Response> AddPet(string body)
        {
            Response responseModel = new Response();

            try
            {
                using (httpClient)
                {
                    httpClient.DefaultRequestHeaders.Clear();                  
                    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", keyAzure);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                    HttpResponseMessage response = new HttpResponseMessage();

                    HttpContent bodyContent = new StringContent(body, Encoding.UTF8, contentType);

                    response = await httpClient.PostAsync(baseUrl + "pet/", bodyContent);

                    responseModel = new Response()
                    {
                        code = (int)response.StatusCode,
                        content = response.Content.ReadAsStringAsync().Result,
                        message = response.RequestMessage.ToString()
                    };
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            return responseModel;
        }

        public async Task<Response> DeletePet(string id)
        {
            Response responseModel = new Response();

            try
            {
                using (httpClient)
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", keyAzure);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                    HttpResponseMessage response = new HttpResponseMessage();

                    response = await httpClient.DeleteAsync(baseUrl + "pet/" + id);

                    responseModel = new Response()
                    {
                        code = (int)response.StatusCode,
                        content = response.Content.ReadAsStringAsync().Result,
                        message = response.RequestMessage.ToString()
                    };
                }
            }
            catch
            {
                throw;
            }
            return responseModel;
        }

        public async Task<Response> GetPetById(string id)
        {
            Response responseModel = new Response();

            try
            {
                using (httpClient)
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", keyAzure);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                    HttpResponseMessage response = new HttpResponseMessage();

                    response = await httpClient.GetAsync(baseUrl + "pet/" + id);

                    responseModel = new Response()
                    {
                        code = (int)response.StatusCode,
                        content = response.Content.ReadAsStringAsync().Result,
                        message = response.RequestMessage.ToString()
                    };
                }
            }
            catch
            {
                throw;
            }
            return responseModel;
        }

        public async Task<Response> UpdatePet(string body)
        {
            Response responseModel = new Response();

            try
            {
                using (httpClient)
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", keyAzure);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));

                    HttpResponseMessage response = new HttpResponseMessage();

                    HttpContent bodyContent = new StringContent(body, Encoding.UTF8, contentType);

                    response = await httpClient.PutAsync(baseUrl + "pet/", bodyContent);

                    responseModel = new Response()
                    {
                        code = (int)response.StatusCode,
                        content = response.Content.ReadAsStringAsync().Result,
                        message = response.RequestMessage.ToString()
                    };
                }
            }
            catch
            {
                throw;
            }
            return responseModel;
        }

    }
}