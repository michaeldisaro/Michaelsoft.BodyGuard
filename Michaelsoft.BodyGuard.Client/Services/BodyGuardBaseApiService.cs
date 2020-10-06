using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Michaelsoft.BodyGuard.Client.Models;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace Michaelsoft.BodyGuard.Client.Services
{
    public class BodyGuardBaseApiService
    {

        protected string BasePath;

        protected IHttpClientFactory HttpClientFactory;

        protected async Task<BaseApiResult> GetRequest<T>(string url,
                                                          Dictionary<string, string> queryParams = null) where T : class
        {
            try
            {
                using var client = HttpClientFactory.CreateClient();
                client.BaseAddress = new Uri($"{BasePath}");

                if (queryParams != null)
                    url = QueryHelpers.AddQueryString(url, queryParams);

                var uri = new Uri(url, UriKind.Relative);

                var response = await client.GetAsync(uri);

                return await BuildBaseApiResultFromResponse<T>(response);
            }
            catch (Exception ex)
            {
                // TODO: Log exception
                return new BaseApiResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        protected async Task<BaseApiResult> PostRequest<T>(string url,
                                                           dynamic requestObject,
                                                           Dictionary<string, string> queryParams = null)
            where T : class
        {
            try
            {
                using var client = HttpClientFactory.CreateClient();
                client.BaseAddress = new Uri($"{BasePath}");

                if (queryParams != null)
                    url = QueryHelpers.AddQueryString(url, queryParams);

                var requestContent = new StringContent(JsonConvert.SerializeObject(requestObject),
                                                       Encoding.UTF8,
                                                       "application/json");

                var uri = new Uri(url, UriKind.Relative);

                var response = await client.PostAsync(uri, requestContent);

                return await BuildBaseApiResultFromResponse<T>(response);
            }
            catch (Exception ex)
            {
                // TODO: Log exception
                return new BaseApiResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
        
        protected async Task<BaseApiResult> PutRequest<T>(string url,
                                                           dynamic requestObject,
                                                           Dictionary<string, string> queryParams = null)
            where T : class
        {
            try
            {
                using var client = HttpClientFactory.CreateClient();
                client.BaseAddress = new Uri($"{BasePath}");

                if (queryParams != null)
                    url = QueryHelpers.AddQueryString(url, queryParams);

                var requestContent = new StringContent(JsonConvert.SerializeObject(requestObject),
                                                       Encoding.UTF8,
                                                       "application/json");

                var uri = new Uri(url, UriKind.Relative);

                var response = await client.PutAsync(uri, requestContent);

                return await BuildBaseApiResultFromResponse<T>(response);
            }
            catch (Exception ex)
            {
                // TODO: Log exception
                return new BaseApiResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        protected async Task<BaseApiResult> BuildBaseApiResultFromResponse<T>(HttpResponseMessage response)
            where T : class
        {
            try
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception(response.ReasonPhrase);

                var json = await response.Content.ReadAsStringAsync();

                return new BaseApiResult
                {
                    Success = true,
                    Response = JsonConvert.DeserializeObject<T>(json)
                };
            }
            catch (Exception ex)
            {
                // TODO: Log exception
                return new BaseApiResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

    }
}