using System;
using System.IO;
using System.Threading.Tasks;
using CommandLine;
using yTools.Common;
using yTools.Image;

namespace yTools.Tools
{
    public class Image : BaseTools<ImageOptions>
    {
        private const string ConfigFile = "config.json";

        public override async Task<int> RunOptions(ImageOptions opts)
        {
            var config = Config.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFile));
            if (config is null)
            {
                Error("config file not valid.");
                return 1;
            }

            var t = new ImageTools(config);
            var result = await t.GetExif(opts.File, opts.Map);
            if (result.Success)
            {
                if (opts.Open)
                {
                    OpenBrowser(result.Message);
                }
                else
                {
                    Console.WriteLine($"Map Url: {result.Message}");
                }

                return 0;
            }

            Error(result.Message);
            return 1;
        }
    }

    [Verb("image", HelpText = "Image tools.")]
    public class ImageOptions : IOptions
    {
        [Option('o', "open", Default = false, HelpText = "Open browser.")]
        public bool Open { get; set; }

        [Option('f', "file", Required = true, HelpText = "File path.")]
        public string File { get; set; }

        [Option('m', "map", Required = true, HelpText = "Map provider.")]
        public string Map { get; set; }
    }
}
