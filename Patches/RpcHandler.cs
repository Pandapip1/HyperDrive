using System;
using System.Collections.Generic;
using Hazel;
using HarmonyLib;
using BepInEx;
using tk.pandapip1.hyperdrive;

namespace tk.pandapip1.hyperdrive.patches
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    public static class RPCHandler
    {
        public static bool Prefix([HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader)
        {
            if (callId == 255)
            {
                switch ((HyperdriveRPC) reader.ReadInt32())
                {
                    case HyperdriveRPC.SetUserRole:
                        RoleManager.Pid2Role[reader.ReadByte()] = reader.ReadString();
                        break;
                }
            }
        }
    }
}
