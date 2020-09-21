using System;
using System.Threading.Tasks;
using CommandLine;
using yTools.Windows;

namespace yTools.Tools
{
    public class PowerPlan : BaseTools<PowerOptions>
    {
        public override Task<int> RunOptions(PowerOptions opts)
        {
            int r = 0;
            if (opts.List)
            {
                var split = new string('-', 72);
                Console.WriteLine(split);
                Console.WriteLine("{0,-20} | {1,-10} | {2,-40}", "Powerplan Name", "Active", "Guid");
                Console.WriteLine(split);
                foreach (var s in WindowsTools.PowerplanList())
                {
                    Console.WriteLine("{0,-20} | {1,-10} | {2,-40}", s[0], s[1], s[2]);
                }
            }
            else
            {
                if (opts.Id != null && Guid.TryParse(opts.Id.Trim(), out Guid id))
                {
                    WindowsTools.PowerplanActive(id);
                }
                else
                {
                    Error("powerplan id not valid.");
                    r = 1;
                }
            }

            return Task.FromResult(r);
        }
    }

    [Verb("power", HelpText = "Powerplan manage.")]
    public class PowerOptions : IOptions
    {
        [Option('l', "list", Default = false, HelpText = "List all powerplans.")]
        public bool List { get; set; }

        [Option('a', "active", HelpText = "Active powerplan.")]
        public string Id { get; set; }
    }
}
