using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace allergens_extractor.Utils
{
    public class ApiCaller
    {
        public async Task<dynamic> RestApiCalling(string uri, string method, dynamic header, dynamic body)
        {
            bool isSuccessfulStatus = false;

            try
            {
                HttpClientHandler httpClientHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; },
                    SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                    AllowAutoRedirect = true,
                    AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate)
                };

                var httpClient = new HttpClient(httpClientHandler);
                httpClient.DefaultRequestHeaders.Clear();

                if (header != null)
                {
                    foreach (dynamic item in header)
                    {
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);
                    }
                }

                HttpContent content = null;
                if (body != null)
                {
                    dynamic val = JsonConvert.SerializeObject(body);
                    content = new StringContent(val, Encoding.UTF8, "application/json");
                }
                HttpResponseMessage httpResponseMessage = null;

                switch (method.ToLower().Trim())
                {
                    case "post":
                        httpResponseMessage = httpClient.PostAsync(uri, content).Result;
                        break;
                    case "get":
                        httpResponseMessage = httpClient.GetAsync(uri).Result;
                        break;
                }

                var statusCode = httpResponseMessage.StatusCode;
                isSuccessfulStatus = statusCode.ToString().ToUpper().Equals("OK") ? true : false;

                return await Task.FromResult(httpResponseMessage.Content.ReadAsStringAsync().Result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
