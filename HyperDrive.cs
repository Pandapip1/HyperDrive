using System;
using System.Collections.Generic;
using Hazel;
using HarmonyLib;
using BepInEx;

namespace tk.pandapip1.hyperdrive
{
    public static class HyperDrive
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
        
        internal static void internal_AssignRoleToPlayer(byte pid, string role)
        {
            Pid2Role[pid] = role;
        }
        
        public static void AssignRoleToPlayer(byte pid, string role)
        {
            internal_AssignRoleToPlayer(pid, role);
            var messageWriter = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, 69, SendOption.Reliable);
            messageWriter.Write((byte) RPC.AssignRoleToPlayer);
            messageWriter.EndMessage();
        }
        
        public static bool UserHasRole(byte playerId, string role)
        {
            if (!Roles.ContainsKey(role)) return false;
            if (role == "") return true;
            while (role != ""){
                if (Pid2Role.ContainsKey(playerId) && Pid2Role[playerId].StartsWith(role)) return true;
                role = Roles[role];
            }
            return false;
        }
    }
}
