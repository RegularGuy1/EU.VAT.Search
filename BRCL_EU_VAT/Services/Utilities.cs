using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace BRCL_EU_VAT.Services
{
    /// <summary>
    /// Common methods
    /// </summary>
    public static class Utilities
    {
        public static async Task<string> GetWebDataAsync(string url)
        {
            string webData = string.Empty;
            WebClient webClient = new WebClient();
            webData = await webClient.DownloadStringTaskAsync(url);
            webClient.Dispose();
            return webData;
        }
    }
}
