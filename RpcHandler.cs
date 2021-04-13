using System;
using System.Collections.Generic;
using Hazel;
using HarmonyLib;
using BepInEx;

namespace tk.pandapip1.hyperdrive
{
    public static enum HyperdriveRPC
    {
        SetUserRole = 1
    }
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    private static class RPCHandler
    {
        public static bool Prefix([HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader)
        {
            if (callId == 69)
            {
                switch ((HyperdriveRPC) reader.ReadInt32())
                {
                    case HyperdriveRPC.SetUserRole:
                        RoleApi.Pid2Role[reader.ReadString()] = reader.ReadByte();
                        break;
                }
            }
        }
    }
}
