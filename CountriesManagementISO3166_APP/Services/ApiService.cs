using CountriesManagementISO3166_APP.Interfaces;
using Fusillade;
using ModernHttpClient;
using Plugin.Connectivity;
using Refit;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace CountriesManagementISO3166_APP.Services
{
    public class ApiService<T> : IApiService<T>
    {
        private readonly Func<HttpMessageHandler, T> createClient;
        public ApiService(string apiBaseAddress)
        {
            createClient = messageHandler =>
            {              
                HttpClient client = new HttpClient()
                {
                    BaseAddress = new Uri(apiBaseAddress)
                };

                //if (DatosGlobales.Obtiene().Token != null)
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", DatosGlobales.Obtiene().Token); 
                
                client.DefaultRequestHeaders.Add("User-Agent", "App Movil");
                return RestService.For<T>(client);
            };
        }

        public T Background => new Lazy<T>(() => createClient(
                                                new RateLimitedHttpMessageHandler(new NativeMessageHandler()
                                                { DisableCaching = true }, Priority.Background))).Value;
        public T UserInitiated => new Lazy<T>(() => createClient(
                                              new RateLimitedHttpMessageHandler(new NativeMessageHandler(), 
                                                  Priority.UserInitiated))).Value;
        public T Speculative => new Lazy<T>(() => createClient(
                                            new RateLimitedHttpMessageHandler(new NativeMessageHandler(), 
                                                Priority.Speculative))).Value;
    }

    public static class AsyncErrorHandler
    {
        public static void HandleException(Exception error)
        {
            Debug.WriteLine(error);
        }
    }

    public static class CheckInternetAccess
    {
        public static bool CheckConnection() 
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return false;
            }
                return true;
        }
    }
}
