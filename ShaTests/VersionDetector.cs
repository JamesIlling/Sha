using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace DotNetVersion
{
    public class VersionDetector
    {
        private const string BaseKey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP";
        private const string Version4Identifier = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full";
        public List<string> dotNetVersionsInstalled()
        {
            var key = Registry.LocalMachine.OpenSubKey(BaseKey);
            var versions = key.GetSubKeyNames().Where(x => x.StartsWith("v")).Select(x => x.Substring(1)).ToList();

            if (versions.Contains("4"))
            {
                versions.RemoveAll(x => x.StartsWith("4"));
                var version4key = Registry.LocalMachine.OpenSubKey(Version4Identifier);
                var releaseId = version4key.GetValue("Release");
                var release = (int)releaseId;
                switch (release)
                {
                    case 378389:
                        versions.Add("4.5.0");
                        break;
                    case 378675:
                    case 378758:
                        versions.Add("4.5.1");
                        break;
                    case 379893:
                        versions.Add("4.5.2");
                        break;
                    case 393295:
                    case 393297:
                        versions.Add("4.6.0");
                        break;
                    case 394254:
                    case 394271:
                        versions.Add("4.6.1");
                        break;
                    case 394802:
                    case 394806:
                        versions.Add("4.6.2");
                        break;
                    case 460798:
                    case 460805:
                        versions.Add("4.7.0");
                        break;
                }
            }

            return versions;
        }        
    }
}
