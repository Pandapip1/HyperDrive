using UnityEngine;
using System.Collections.Generic;

namespace tk.pandapip1.hyperdrive
{
    public static class IntroCutsceneManager
    {
        private static Dictionary<string, string> RoleTitles = new Dictionary<string, string>();
        private static Dictionary<string, string> ImpostorTexts = new Dictionary<string, string>();
        private static Dictionary<string, Color> BackgroundBarColors = new Dictionary<string, string>();
        private static Dictionary<string, Color> TitleColors = new Dictionary<string, string>();
        
        public static void SetRoleTitle(string role, string title)
        {
            RoleTitles[role] = title;
        }
        
        public static string GetRoleTitle(string role)
        {
            return RoleTitles[role];
        }
        
        public static void SetImpostorText(string role, string text)
        {
            ImpostorTexts[role] = text;
        }
        
        public static string GetImpostorText(string role)
        {
            return ImpostorTexts[role];
        }
        
        public static void SetBackgroundBarColor(string role, Color color)
        {
            BackgroundBarColors[role] = color;
        }
        
        public static Color GetBackgroundBarColor(string role)
        {
            return BackgroundBarColors[role];
        }
        
        public static void SetTitleColor(string role, Color color)
        {
            TitleColors[role] = color;
        }
        
        public static Color GetTitleColor(string role)
        {
            return TitleColors[role];
        }
    }
}
