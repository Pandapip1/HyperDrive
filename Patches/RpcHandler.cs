using System;
using System.Collections.Generic;
using Hazel;
using HarmonyLib;
using BepInEx;
using HyperDrive;
using AllOfUs.CubeAPI.Options;

namespace HyperDrive.Patches
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
                        RoleManager.internal_AssignRoleToPlayer(reader.ReadByte(), reader.ReadString());
                        break;
                    case HyperdriveRPC.SyncSettings:
                        RpcSyncSettings.Handle(reader);
                        break;
                }
            }
        }
    }
}
