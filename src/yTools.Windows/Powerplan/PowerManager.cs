using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace yTools.Windows.Powerplan
{
    public static class PowerManager
    {
        public static IEnumerable<PowerProperty> GetAll()
        {
            var schemeGuid = Guid.Empty;

            uint sizeSchemeGuid = (uint)Marshal.SizeOf(typeof(Guid));
            uint schemeIndex = 0;

            while (PowerApi.PowerEnumerate(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero,
                (uint)PowerApi.AccessFlags.AccessScheme, schemeIndex, ref schemeGuid, ref sizeSchemeGuid) == 0)
            {
                string friendlyName = ReadPowerName(schemeGuid);
                bool isActive = IsActive(schemeGuid);
                yield return new PowerProperty {Id = schemeGuid, Name = friendlyName, IsActive = isActive};
                schemeIndex++;
            }
        }

        public static void Active(Guid id)
        {
            PowerApi.PowerSetActiveScheme(IntPtr.Zero, ref id);
        }

        private static bool IsActive(Guid id)
        {
            IntPtr pCurrentSchemeGuid = IntPtr.Zero;
            PowerApi.PowerGetActiveScheme(IntPtr.Zero, ref pCurrentSchemeGuid);
            var currentSchemeGuid = (Guid)Marshal.PtrToStructure(pCurrentSchemeGuid, typeof(Guid));
            return currentSchemeGuid == id;
        }

        private static string ReadPowerName(Guid id)
        {
            uint sizeName = 1024;
            IntPtr pSizeName = Marshal.AllocHGlobal((int)sizeName);

            string powerName;

            try
            {
                PowerApi.PowerReadFriendlyName(IntPtr.Zero, ref id, IntPtr.Zero, IntPtr.Zero, pSizeName, ref sizeName);
                powerName = Marshal.PtrToStringUni(pSizeName);
            }
            finally
            {
                Marshal.FreeHGlobal(pSizeName);
            }

            return powerName;
        }
    }
}
