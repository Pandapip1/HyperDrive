using System;
using System.Collections.Generic;
using Hazel;
using HarmonyLib;
using BepInEx;

namespace HyperDrive
{
    public static class RoleManager
    {
        internal static Dictionary<string, string> Roles = new Dictionary<string, string>();
        internal static Dictionary<byte, string> Pid2Role = new Dictionary<string, string>();
        
        public static void CreateRole(string guid, string parent)
        {
            Roles[guid] = parent;
        }
        
        public static void CreateRole(string guid)
        {
            Roles[guid] = "";
        }
        
        public static void internal_AssignRoleToPlayer(byte pid, string role)
        {
            Pid2Role[pid] = role;
        }
        
        public static void AssignRoleToPlayer(byte pid, string role)
        {
            internal_AssignRoleToPlayer(pid, role);
            var messageWriter = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, 69, SendOption.Reliable);
            messageWriter.Write((int) HyperdriveRPC.SetUserRole);
            messageWriter.EndMessage();
        }
        
        public static bool UserHasRole(byte playerId, string role)
        {
            if (!Roles.ContainsKey(role)) return false;
            if (role == "") return true;
            while (role != ""){
                if (Pid2Role.ContainsKey(playerId) && Pid2Role[playerId] == role) return true;
                role = Roles[role];
            }
            return false;
        }
        
        public static string GetUserRole(byte playerId)
        {
            if (Pid2Role.ContainsKey(playerId))
                return Pid2Role[playerId];
            return "tk.pandapip1.hyperdrive.crewmate";
        }
        
        public static string GetBaseRole(string role)
        {
            while (Roles.ContainsKey(role) && Roles[role] != "")
                role = Roles[role];
            return role;
        }
    }
}
