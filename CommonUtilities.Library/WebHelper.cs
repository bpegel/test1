using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using RestSharp;
using System.IO;
using RestSharp.Authenticators;

namespace CommonUtilities.Library
{
    public class WebResponseInfo
    {
        public HttpStatusCode HttpStatus { get; set; }
        public string WebResponse { get; set; }
    }

    public enum WebTool
    {
        REST_SHARP,
        HTTP_CLIENT
    }
    public class WebHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseURL"></param>
        /// <param name="requestPathUri"></param>
        /// <param name="networkCredentials"></param>
        /// <returns></returns>
        public static WebResponseInfo GetWebData(string baseURL, string requestPathUri, string userName, string password, WebTool webTool)
        {
            WebResponseInfo webResponseInfo = new Library.WebResponseInfo();

            if (webTool == WebTool.HTTP_CLIENT)
            {
                using (var webClient = new HttpClient(new HttpClientHandler() { Credentials = new NetworkCredential(userName, password) }))
                {
                    webClient.BaseAddress = new Uri(baseURL);
                    webClient.DefaultRequestHeaders.Accept.Clear();
                    webClient.DefaultRequestHeaders.Add("User-Agent", @"Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.10136");

                    // connect to the REST endpoint            
                    HttpResponseMessage response = webClient.GetAsync(requestPathUri).Result;

                    webResponseInfo.HttpStatus = response.StatusCode;
                    webResponseInfo.WebResponse = response.Content.ReadAsStringAsync().Result;
                }
            }
            else
            {
                try
                {
                    var client = new RestClient(baseURL);
                    if (!string.IsNullOrEmpty(userName))
                        client.Authenticator = new NtlmAuthenticator(userName, password);
                    else
                        client.Authenticator = new HttpBasicAuthenticator("username", "password");

                    var request = new RestRequest(requestPathUri, Method.GET);
                    var response = client.Execute(request);
                    webResponseInfo.HttpStatus = response.StatusCode;
                    webResponseInfo.WebResponse = Encoding.Default.GetString(response.RawBytes);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return webResponseInfo;
            
        }

    }
}
