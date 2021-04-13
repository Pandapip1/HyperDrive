using System;
using System.Collections.Generic;
using Hazel;
using HarmonyLib;
using BepInEx;

namespace tk.pandapip1.hyperdrive
{
    public static class TeamApi
    {
        private static List<string> Teams = new List<string>();
        
        public static void CreateTeam(string guid)
        {
            Teams.Add(guid);
        }
    }
}
