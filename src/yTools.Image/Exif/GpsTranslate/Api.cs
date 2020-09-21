using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using yTools.Common;

namespace yTools.Image.Exif.GpsTranslate
{
    public abstract class Api
    {
        public virtual async Task<T> Get<T>(string url)
        {
            return await Request.Get<T>(url);
        }

        public virtual string Signature(string api, SortedDictionary<string, string> query,
            string sKey, Func<string, string> encode, string formatter)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(api);
            sb.Append("?");
            foreach (KeyValuePair<string, string> pair in query)
            {
                sb.Append($"{pair.Key}={pair.Value}&");
            }

            sb.Remove(sb.Length - 1, 1); //remove last &
            string url = sb.ToString();

            string sUrl = $"{url}{sKey}";
            if (encode != null)
            {
                sUrl = encode(sUrl);
            }

            using (var md5 = MD5.Create())
            {
                byte[] md5Bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(sUrl));
                string md5Str = BitConverter.ToString(md5Bytes).Replace("-", "").ToLower();
                return string.Format(formatter, url, md5Str);
            }
        }
    }
}
