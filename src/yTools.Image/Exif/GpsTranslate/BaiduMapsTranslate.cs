using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using yTools.Common;

namespace yTools.Image.Exif.GpsTranslate
{
    public class BaiduMapsTranslate : Api, IGpsTranslate
    {
        public BaiduMapsTranslate(string key, string sKey)
        {
            _key = key;
            _sKey = sKey;
        }

        /// <summary>
        ///     translate to bd09ll coordinate system
        /// </summary>
        /// <param name="gps"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<Gps> Translate(Gps gps, int type = 0)
        {
            var query = new SortedDictionary<string, string>
            {
                {"coords", $"{gps.Latitude},{gps.Longitude}"}, {"ak", _key}, {"from", "1"}, {"to", "5"}
            };
            string url = BuildApiUrl(query);
            BaiduApi baiduApi = await Get<BaiduApi>(url);

            if (baiduApi.Status == 0 && baiduApi.Result != null && baiduApi.Result.Any())
            {
                BaiduApiCord r = baiduApi.Result.First();
                gps.Latitude = r.X;
                gps.Longitude = r.Y;
                return gps;
            }

            throw new YToolsException($"Baidu maps api request error: {baiduApi.Status}");
        }

        public string ToMapUrl(Gps gps, string remark = "") =>
            BuildMarkerUrl(gps, remark);

        public string ToAddress(Gps gps) => throw new NotImplementedException();

        private string BuildApiUrl(SortedDictionary<string, string> query) =>
            Signature(BaiduTransApi, query, _sKey, UrlEncode, $"{BaiduApiBase}{{0}}&sn={{1}}");

        private static string BuildMarkerUrl(Gps gps, string remark)
        {
            string url = string.Format(BaiduMarkerApi, gps.Latitude, gps.Longitude, remark, "Location");
            return $"{BaiduApiBase}{url}";
        }

        // via http://lbsyun.baidu.com/index.php?title=lbscloud/api/appendix
        private string UrlEncode(string str)
        {
            str = HttpUtility.UrlEncode(str);
            byte[] buf = Encoding.ASCII.GetBytes(str ?? throw new ArgumentNullException(nameof(str)));
            for (int i = 0; i < buf.Length; i++)
                if (buf[i] == '%')
                {
                    if (buf[i + 1] >= 'a') buf[i + 1] -= 32;
                    if (buf[i + 2] >= 'a') buf[i + 2] -= 32;
                    i += 2;
                }

            return Encoding.ASCII.GetString(buf);
        }

        #region api

        private const string BaiduApiBase = "http://api.map.baidu.com";
        private const string BaiduTransApi = "/geoconv/v1/";

        private const string BaiduMarkerApi =
            "/marker?location={0},{1}&title={2}&content={3}&output=html&src=webapp.baidu.openAPIdemo";

        private static string _key;
        private static string _sKey;

        #endregion

        #region baidu map api result

        private class BaiduApi
        {
            public int Status { get; set; }

            public List<BaiduApiCord> Result { get; set; }
        }

        private class BaiduApiCord
        {
            public double X { get; set; }

            public double Y { get; set; }
        }

        #endregion
    }
}
