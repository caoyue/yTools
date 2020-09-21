using System;
using System.IO;
using Newtonsoft.Json;

namespace yTools.Common
{
    public class Config
    {
        public string TencentKey { get; set; }
        public string TencentSecureKey { get; set; }

        public string BaiduKey { get; set; }
        public string BaiduSecureKey { get; set; }

        public static Config Load(string path)
        {
            try
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<Config>(json);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
