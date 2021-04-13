using System;
using System.Collections.Generic;
using Hazel;
using HarmonyLib;
using BepInEx;

namespace tk.pandapip1.hyperdrive
{
    public static class RoleApi
    {
        private static List<string> Roles = new List<string>();
        private static Dictionary<byte, string> Pid2Role = new List<string>();
        private static Dictionary<byte, string> Role2Team = new List<string>();
        
        public static void CreateRole(string guid)
        {
            Roles.Add(guid);
        }
        
        public static void AssignRoleToTeam(string role, string team)
        {
            Role2Team[role] = team;
        }
        
        public static void AssignRoleToPlayer(string role, string team)
        {
        }
        
        public static string GetUserRole(byte playerId)
        {
            if (Pid2Role.ContainsKey(playerId)) return Pid2Role[playerId];
            return null;
        }
    }
}
