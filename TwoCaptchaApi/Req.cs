using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TwoCaptchaApi
{
    public static class Req
    {
        public static async Task<string> GetAsync(string url)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
