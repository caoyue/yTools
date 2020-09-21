using System;
using System.Collections.Generic;
using System.Linq;
using yTools.Windows.Powerplan;
using yTools.Windows.TaskBar;

namespace yTools.Windows
{
    public static class WindowsTools
    {
        public static List<string[]> PowerplanList()
        {
            string YOrN(bool b)
            {
                return b ? "Y" : "N";
            }

            return PowerManager.GetAll().ToList()?.Select(
                    p => new[] {p.Name, YOrN(p.IsActive), p.Id.ToString()})
                .ToList();
        }

        public static void PowerplanActive(Guid id)
        {
            PowerManager.Active(id);
        }

        public static bool TaskBar(string action)
        {
            bool success = true;
            var bar = new TaskBarManger();
            switch (action)
            {
                case "hide":
                    bar.SetTaskbarState(AppBarStates.AutoHide);
                    break;
                case "show":
                    bar.SetTaskbarState(AppBarStates.AlwaysOnTop);
                    break;
                case "toggle":
                    var status = bar.GetTaskbarState();
                    bar.SetTaskbarState(
                        status == AppBarStates.AlwaysOnTop
                            ? AppBarStates.AutoHide
                            : AppBarStates.AlwaysOnTop);
                    break;
                default:
                    success = false;
                    break;
            }

            return success;
        }
    }
}
