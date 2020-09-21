using System;
using System.IO;
using System.Threading.Tasks;
using MetadataExtractor;
using yTools.Common;
using yTools.Image.Exif;
using yTools.Image.Exif.GpsTranslate;

namespace yTools.Image
{
    public class ImageTools
    {
        private static Config _config;

        public ImageTools(Config config)
        {
            _config = config;
        }

        public async Task<Result> GetExif(string path, string mapProvider)
        {
            var result = new Result {Success = false};
            Gps gps;
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                result.Message = "File does not exists.";
                return result;
            }

            try
            {
                GeoLocation geo = ExifExtractor.GetGpsInfo(path);
                if (geo == null || geo.IsZero)
                {
                    result.Message = "This image does not have a Geo tag.";
                    return result;
                }

                gps = geo.ToGps();
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                return result;
            }

            IGpsTranslate t = Translate(mapProvider);
            string name = Path.GetFileName(path);
            return await TranslateTask(t, gps, name);
        }

        private static async Task<Result> TranslateTask(IGpsTranslate t, Gps gps, string remark)
        {
            var result = new Result {Success = false};
            try
            {
                gps = await t.Translate(gps);
                var url = t.ToMapUrl(gps, remark);
                result.Success = true;
                result.Message = url;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }

            return result;
        }

        private static IGpsTranslate Translate(string mapProvider)
        {
            IGpsTranslate t = null;

            switch (mapProvider)
            {
                case "baidu":
                    t = new BaiduMapsTranslate(_config.BaiduKey, _config.BaiduSecureKey);
                    break;
                case "google":
                    t = new GoogleMapsTranslate();
                    break;
                case "tencent":
                    t = new TencentMapsTranslate(_config.TencentKey, _config.TencentSecureKey);
                    break;
            }

            return t;
        }
    }
}
