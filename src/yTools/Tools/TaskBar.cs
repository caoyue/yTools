using System.Threading.Tasks;
using CommandLine;
using yTools.Windows;

namespace yTools.Tools
{
    public class TaskBar : BaseTools<TaskBarOptions>
    {
        public override Task<int> RunOptions(TaskBarOptions opts)
        {
            int r = 0;

            if (!WindowsTools.TaskBar(opts.Action))
            {
                Error("action not valid.");
                r = 1;
            }

            return Task.FromResult(r);
        }
    }

    [Verb("taskbar", HelpText = "TaskBar visibility")]
    public class TaskBarOptions : IOptions
    {
        [Option('a', "action", Required = true, HelpText = "TaskBar hide|show|toggle.")]
        public string Action { get; set; }
    }
}
