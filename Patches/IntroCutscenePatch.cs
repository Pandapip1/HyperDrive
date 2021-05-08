using Il2CppSystem.Collections.Generic;
using HarmonyLib;
using tk.pandapip1.hyperdrive;

namespace HyperDrive.Patches
{
    [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.BeginImpostor))]
    private static class IntroCutscenePatchImpostor
    {
        public static bool Prefix(IntroCutscene __instance, [HarmonyArgument(0)] ref List<PlayerControl> yourTeam)
        {
            __instance.BeginCrewmate(yourTeam);
            return false;
        }
    }
    [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.BeginCrewmate))]
    private static class IntroCutscenePatchCrewmate
    {
        public static void Prefix([HarmonyArgument(0)] ref List<PlayerControl> yourTeam)
        {
            var myRole = RoleManager.GetUserRole(PlayerControl.LocalPlayer.PlayerId);
            var myBaseRole = RoleManager.GetBaseRole(myRole);
            var team = new List<PlayerControl>();
            team.Add(PlayerControl.LocalPlayer);
            if (myBaseRole != "tk.pandapip1.hyperdrive.individual")
            {
                var amCrewmate = myBaseRole == "tk.pandapip1.hyperdrive.crewmate";
                foreach (PlayerControl p in PlayerControl.AllPlayerControls)
                {
                    if (p.PlayerId != PlayerControl.LocalPlayer.PlayerId && (amCrewmate || RoleManager.GetBaseRole(RoleManager.GetUserRole(p.PlayerId)) == myBaseRole))
                    {
                        team.Add(p);
                    }
                }
            }
            yourTeam = team;
        }
        public static void Postfix(IntroCutscene __instance)
        {
            var myRole = RoleManager.GetUserRole(PlayerControl.LocalPlayer.PlayerId);
            
            __instance.ImpostorText.gameObject.SetActive(true);
            
            __instance.Title.text = IntroCutsceneManager.GetRoleTitle(myRole);
            __instance.ImpostorText.text = IntroCutsceneManager.GetImpostorText(myRole);
            __instance.BackgroundBar.material.SetColor("_Color", IntroCutsceneManager.GetBackgroundBarColor(myRole));
            __instance.Title.Color = IntroCutsceneManager.GetTitleColor(myRole);
        }
    }
}
