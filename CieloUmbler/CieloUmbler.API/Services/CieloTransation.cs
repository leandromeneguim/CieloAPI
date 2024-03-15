using CieloUmbler.API.Domain.Entities;
using CieloUmbler.MVC.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CieloUmbler.API.Services
{
    public class CieloTransation : ITransation
    {
        private readonly IConfiguration _config;

        public CieloTransation(IConfiguration config)
        {
            _config = config;
        }

        public async Task<Dictionary<string, object>> QueryCardBin(string cardBin)
        {
            string urlBase = _config.GetValue<string>("CieloSettings:ApiQueryUrl");
            string path = $"{urlBase}/1/cardBin/{cardBin}";

            HttpClient client = new();
            client.DefaultRequestHeaders.Add("MerchantId",_config.GetValue<string>("CieloSettings:MerchantId"));
            client.DefaultRequestHeaders.Add("MerchantKey",_config.GetValue<string>("CieloSettings:MerchantKey"));

            HttpResponseMessage response = await client.GetAsync(path);

            var jsonString = await response.Content.ReadAsStringAsync();
            return TrataRetornoAPI(jsonString);
            
            /*if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Dictionary<string, object>>();
            }
            else
            {
                
            }*/
        }

        public async Task<Dictionary<string, object>> AuthenticateCard(BaseEntity entity)
        {
            Capture capture = (Capture)entity;
            string urlBase = _config.GetValue<string>("CieloSettings:ApiUrl");
            string path = $"{urlBase}/1/sales/";
            var jsonCard = JsonSerializer.Serialize(capture);
            
            HttpContent corpoRequisicao = new StringContent(jsonCard, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await PostUriApi(new Uri(path), corpoRequisicao);

            var jsonString = await response.Content.ReadAsStringAsync();

            if (jsonString.Contains("\"Code\""))
            {
                return JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString.Replace("[", "").Replace("]", ""));
            }
            else
            {
                return TrataRetornoAPI(jsonString);
            }

            //return (Dictionary<string, object>)jsonString.Select(x => x.Split(':'))
              //              .ToDictionary(x => x[0].Replace("\"", "").Trim(), x => (object)x[1].Replace("\"", "").Trim()); ;

        }


        public Task<Dictionary<string, object>> CancelProcess()
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<string, object>> Capture(string tokenCapture)
        {
            string urlBase = _config.GetValue<string>("CieloSettings:ApiUrl");
            string path = $"{urlBase}/1/sales/{tokenCapture}/capture";

            
            HttpResponseMessage response = await SendUriApi(new Uri(path), null, HttpMethod.Put);

            var jsonString = await response.Content.ReadAsStringAsync();
            return TrataRetornoAPI(jsonString);
        }
              

        public async Task<Dictionary<string, object>> TokenCard(BaseEntity entity)
        {
            Card card = (Card)entity;
            string urlBase = _config.GetValue<string>("CieloSettings:ApiUrl");
            string path = $"{urlBase}/1/card/";

            var jsonCard = JsonSerializer.Serialize(card);
            HttpContent corpoRequisicao = new StringContent(jsonCard, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await PostUriApi(new Uri(path), corpoRequisicao);
            
            var jsonString = await response.Content.ReadAsStringAsync();
            return TrataRetornoAPI(jsonString);
        }

        private Dictionary<string, object> TrataRetornoAPI(string erroRetorno)
        {
            var result = erroRetorno.Replace("[", "").Replace("]", "")
                            .Replace("{", "").Replace("}", "").Trim()
                            .Split(",")
                            .Select(x => x.Split(':'))
                            .ToDictionary(x => x[0].Replace("\"", "").Trim(), x => (object)x[1].Replace("\"", "").Trim());
            return result;
        }

        private async Task<HttpResponseMessage> PostUriApi(Uri uri, HttpContent corpoRequisicao)
        {
            HttpClient client = new();
            client.DefaultRequestHeaders.Add("MerchantId", _config.GetValue<string>("CieloSettings:MerchantId"));
            client.DefaultRequestHeaders.Add("MerchantKey", _config.GetValue<string>("CieloSettings:MerchantKey"));

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = uri,
                Content = corpoRequisicao
            };

            return await client.SendAsync(request);
        }

        private async Task<HttpResponseMessage> SendUriApi(Uri uri, HttpContent corpoRequisicao, HttpMethod method)
        {
            HttpClient client = new();
            client.DefaultRequestHeaders.Add("MerchantId", _config.GetValue<string>("CieloSettings:MerchantId"));
            client.DefaultRequestHeaders.Add("MerchantKey", _config.GetValue<string>("CieloSettings:MerchantKey"));

            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = uri,
                Content = corpoRequisicao
            };

            return await client.SendAsync(request);
        }
    }
}
