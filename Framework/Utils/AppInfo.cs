using Framework.Common;
using System;
using System.Globalization;

namespace Framework.Utils
{
    public interface IAppInfo
    {
        public string GetCultura();
        public string GetVersion();
        public string GetBuildDate();
    }
    public class AppInfo : IAppInfo
    {
        private string Version;

        private string BuildDate;

        private string Culture = Constants.CULTURE_DEFAULT;

        public AppInfo()
        {
            var objV = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            Version = objV.ToString();

            var buildDateTime = new DateTime(2000, 1, 1)
                .Add(new TimeSpan(TimeSpan.TicksPerDay * objV.Build + // days since 1 January 2000
                                    TimeSpan.TicksPerSecond * 2 * objV.Revision)); // seconds since midnight, (multiply by 2 to get original)
            BuildDate = buildDateTime.ToString("yyyy-MM-ddThh:mm:ss");
        }

        public string GetCultura()
        {
            if (CultureInfo.CurrentCulture != null)
                Culture = CultureInfo.CurrentCulture.Name;

            return Culture;
        }

        public string GetVersion()
        {
            return this.Version;
        }

        public string GetBuildDate()
        {
            return this.BuildDate;
        }
    }
}
