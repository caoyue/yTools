using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace yTools.Common
{
    public static class Request
    {
        public static async Task<string> Get(string url)
        {
            HttpWebRequest req = WebRequest.CreateHttp(url);
            req.Method = WebRequestMethods.Http.Get;
            req.ContentType = "application/json; charset=utf-8";
            HttpWebResponse res = req.GetResponse() as HttpWebResponse;

            using (Stream rs = res?.GetResponseStream())
            {
                StreamReader reader = new StreamReader(
                    rs ?? throw new InvalidOperationException(), Encoding.UTF8);
                return await reader.ReadToEndAsync();
            }
        }

        public static async Task<T> Get<T>(string url)
        {
            var json = await Get(url);

            T t;
            try
            {
                t = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception)
            {
                t = default;
            }

            return t;
        }
    }
}
