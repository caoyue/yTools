using System;
using System.Threading.Tasks;

namespace yTools.Image.Exif.GpsTranslate
{
    public class GoogleMapsTranslate : IGpsTranslate
    {
        public async Task<Gps> Translate(Gps gps, int type = 0) =>
            await Task.FromResult(gps);

        public string ToMapUrl(Gps gps, string remark = "") =>
            $"https://www.google.com/maps/search/{gps.Latitude},{gps.Longitude}";

        public string ToAddress(Gps gps) => throw new NotImplementedException();
    }
}
