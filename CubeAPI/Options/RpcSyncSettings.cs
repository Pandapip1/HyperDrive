using System;
using System.Linq;
using Hazel;
using HarmonyLib;
using HyperDrive;

namespace AllOfUs.CubeAPI.Options
{
    public class RpcSyncSettings
    {
        public static void Send()
        {
            try
            {
                if (!AmongUsClient.Instance.AmHost) return;
                var messageWriter = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId,
                    255, SendOption.Reliable);
                messageWriter.Write((int)HyperdriveRPC.SyncSettings);
                messageWriter.WritePacked(OptionsPatches.CustomNumberOptions.Count);
                if (OptionsPatches.CustomNumberOptions.Count > 0)
                {
                    foreach (var value in OptionsPatches.CustomNumberOptions.Select(x => x.Value))
                    {
                        messageWriter.Write(value);
                    }
                }

                messageWriter.WritePacked(OptionsPatches.CustomToggleOptions.Count);
                if (OptionsPatches.CustomToggleOptions.Count > 0)
                {
                    foreach (var value in OptionsPatches.CustomToggleOptions.Select(x => x.Value))
                    {
                        messageWriter.Write(value);
                    }
                }

                messageWriter.WritePacked(OptionsPatches.CustomStringOptions.Count);
                if (OptionsPatches.CustomStringOptions.Count > 0)
                {
                    foreach (var value in OptionsPatches.CustomStringOptions.Select(x => x.Value))
                    {
                        messageWriter.WritePacked(value);
                    }
                }

                messageWriter.EndMessage();
            }
            catch (Exception e)
            {
            }
        }

        public static void Handle(MessageReader reader)
        {
            var numberLength = reader.ReadPackedInt32();
            if (numberLength > 0)
            {
                for (var i = 0; i < numberLength; i++)
                {
                    OptionsPatches.CustomNumberOptions[i].ConfigEntry.Value = reader.ReadSingle();
                }
            }

            var toggleLength = reader.ReadPackedInt32();
            if (toggleLength > 0)
            {
                for (var i = 0; i < toggleLength; i++)
                {
                    OptionsPatches.CustomToggleOptions[i].ConfigEntry.Value = reader.ReadBoolean();
                }
            }

            var stringLength = reader.ReadPackedInt32();
            if (stringLength > 0)
            {
                for (var i = 0; i < stringLength; i++)
                {
                    OptionsPatches.CustomStringOptions[i].ConfigEntry.Value = reader.ReadPackedInt32();
                }
            }
        }
    }
}
