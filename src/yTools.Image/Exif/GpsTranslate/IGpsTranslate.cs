using System.Threading.Tasks;

namespace yTools.Image.Exif.GpsTranslate
{
    public interface IGpsTranslate
    {
        /// <summary>
        ///     convert WGS84 coordinates to other coordinate system
        /// </summary>
        /// <param name="gps">WGS84 coordinate</param>
        /// <param name="type">coordinate type</param>
        /// <returns></returns>
        Task<Gps> Translate(Gps gps, int type = 0);

        string ToMapUrl(Gps gps, string remark = "");

        string ToAddress(Gps gps);
    }
}
