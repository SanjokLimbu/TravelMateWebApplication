using System.Net.Http;
using System.Net.Http.Headers;

namespace TravelMate.ModelFolder.ContextFolder
{
    public static class ApiInitialization
    {
        public static HttpClient GetClient { get; set; }
        public static void InitializeClient()
        {
            GetClient = new HttpClient();
            GetClient.DefaultRequestHeaders.Accept.Clear();
            GetClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
