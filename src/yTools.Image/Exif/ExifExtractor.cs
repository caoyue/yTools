using System;
using System.Collections.Generic;
using System.Linq;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using yTools.Common;

namespace yTools.Image.Exif
{
    public static class ExifExtractor
    {
        public static GeoLocation GetGpsInfo(string path)
        {
            IReadOnlyList<Directory> info;

            try
            {
                info = ImageMetadataReader.ReadMetadata(path);
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine(e);
#endif
                throw new YToolsException("Cannot read metadata from this image.");
            }

            GpsDirectory gps = info.OfType<GpsDirectory>().FirstOrDefault();
            return gps?.GetGeoLocation();
        }
    }
}
