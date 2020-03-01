using System;
using System.Configuration;

namespace AdvocateAPI.Common
{
    public static class ConfigHelper
    {
        static readonly AppSettingsReader Settings = new AppSettingsReader();

        public static int GetIntValue(string settingName)
        {
            return int.Parse(Settings.GetValue(settingName, type: Type.GetType("System.Int32")).ToString());
        }

        public static string GetStringValue(string settingName)
        {
            return Settings.GetValue(settingName, type: Type.GetType("System.String")).ToString();
        }

    }
}