using System;
using System.Runtime.InteropServices;

namespace yTools.Windows.Powerplan
{
    public class PowerApi
    {
        public enum AccessFlags : uint
        {
            AccessScheme = 16,
            AccessSubgroup = 17,
            AccessIndividualSetting = 18
        }

        [DllImport("PowrProf.dll")]
        public static extern uint PowerEnumerate(IntPtr rootPowerKey, IntPtr schemeGuid,
            IntPtr subGroupOfPowerSettingGuid, uint acessFlags, uint index, ref Guid buffer, ref uint bufferSize);

        [DllImport("PowrProf.dll")]
        public static extern uint PowerReadFriendlyName(IntPtr rootPowerKey, ref Guid schemeGuid,
            IntPtr subGroupOfPowerSettingGuid, IntPtr powerSettingGuid, IntPtr buffer, ref uint bufferSize);

        [DllImport("PowrProf.dll")]
        public static extern uint PowerGetActiveScheme(IntPtr userRootPowerKey, ref IntPtr activePolicyGuid);

        [DllImport("PowrProf.dll")]
        public static extern uint PowerSetActiveScheme(IntPtr userRootPowerKey, ref Guid schemeGuid);
    }
}
