using System.Collections.Generic;
using System.Threading.Tasks;
using CommandLine;
using yTools.Tools;

namespace yTools
{
    internal class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var image = new Tools.Image();
            var power = new PowerPlan();
            var taskbar = new TaskBar();
            return await Parser.Default
                .ParseArguments<ImageOptions, PowerOptions, TaskBarOptions>(args)
                .MapResult(
                    (ImageOptions i) => image.RunOptions(i),
                    (PowerOptions p) => power.RunOptions(p),
                    (TaskBarOptions t) => taskbar.RunOptions(t),
                    HandleErrors
                );
        }

        private static async Task<int> HandleErrors(IEnumerable<Error> errors)
        {
            return await Task.FromResult(1);
        }
    }
}
