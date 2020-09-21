using MetadataExtractor;
using yTools.Image.Exif.GpsTranslate;

namespace yTools.Image.Exif
{
    public static class ExifExtensions
    {
        public static Gps ToGps(this GeoLocation geo) =>
            new Gps {Latitude = geo.Latitude, Longitude = geo.Longitude};
    }
}
