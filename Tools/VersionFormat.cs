using System;
using System.Reflection;

namespace UV7_Program_Manager.Tools
{
    public static class VersionFormat
    {
        public static string ReadableVersion
        {
            get
            {
                Version ver = Assembly.GetExecutingAssembly().GetName().Version;
                if (ver.Build > 0 && ver.Revision > 0)
                    return $"Version {ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision}";
                else if (ver.Build > 0 && ver.Revision == 0)
                    return $"Version {ver.Major}.{ver.Minor}.{ver.Build}";
                else if (ver.Build == 0 && ver.Revision > 0)
                    return $"Version {ver.Major}.{ver.Minor}.{ver.Revision}";
                else
                    return $"Version {ver.Major}.{ver.Minor}";
            }
        }
    }
}
