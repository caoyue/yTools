using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using yTools.Common;

namespace yTools.Image.Exif.GpsTranslate
{
    public class TencentMapsTranslate : Api, IGpsTranslate
    {
        public TencentMapsTranslate(string key, string sKey)
        {
            _key = key;
            _sKey = sKey;
        }

        public async Task<Gps> Translate(Gps gps, int type = 0)
        {
            SortedDictionary<string, string> dict = new SortedDictionary<string, string>
            {
                {"locations", $"{gps.Latitude},{gps.Longitude}"}, {"type", "1"}, {"key", _key}
            };
            string url = BuildApiUrl(TencentTransApi, dict);
            TencentApi res = await Get<TencentApi>(url);
            if (res.Status == 0 && res.Locations != null && res.Locations.Any())
            {
                TencentApiCord l = res.Locations.First();
                gps.Latitude = l.Lat;
                gps.Longitude = l.Lng;
                return gps;
            }

            throw new YToolsException($"Tencent maps api request error: {res.Message}");
        }

        public string ToMapUrl(Gps gps, string remark = "") =>
            BuildMarkerUrl(gps, remark);

        public string ToAddress(Gps gps) => throw new NotImplementedException();

        private string BuildApiUrl(string transApi, SortedDictionary<string, string> query) =>
            Signature(transApi, query, _sKey, null, $"{TencentApiBase}{{0}}&sig={{1}}");

        private static string BuildMarkerUrl(Gps gps, string remark)
        {
            string url = string.Format(TencentMarkerApi, gps.Latitude, gps.Longitude, remark, "Location");
            return $"{TencentApiBase}{url}";
        }

        #region api

        private const string TencentApiBase = "https://apis.map.qq.com";
        private const string TencentTransApi = "/ws/coord/v1/translate";

        private const string TencentMarkerApi =
            "/uri/v1/marker?marker=coord:{0},{1};title:{2};addr:{3}&referer=myapp";

        private static string _key;
        private static string _sKey;

        #endregion

        #region Api Class

        private class TencentApi
        {
            public int Status { get; set; }

            public string Message { get; set; }

            public List<TencentApiCord> Locations { get; set; }
        }

        private class TencentApiCord
        {
            public double Lng { get; set; }

            public double Lat { get; set; }
        }

        #endregion
    }
}
