using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BepInEx;
using BepInEx.IL2CPP;
using System.IO;
using BepInEx.Logging;
using HarmonyLib;
using Reactor;
using UnhollowerBaseLib;
using UnityEngine;

namespace tk.pandapip1.easyroles
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class EasyRolesPlugin : BasePlugin
    {
        private const string Id = "tk.pandapip1.easyroles";

        private Harmony Harmony { get; } = new Harmony(Id);

        internal static ManualLogSource Logger = new ManualLogSource("EasyRoles");

        public override void Load()
        {
            // Initialize Logger
            BepInEx.Logging.Logger.Sources.Add(Logger);
            // Initialize Default Teams
            RoleApi.CreateTeam("com.innersloth.spacemafia.crewmate");
            RoleApi.CreateTeam("com.innersloth.spacemafia.impostor");
            RoleApi.CreateTeam("com.innersloth.spacemafia.individual");
            // Initialize Default Roles
            RoleApi.CreateRole("com.innersloth.spacemafia.crewmate");
            RoleApi.CreateRole("com.innersloth.spacemafia.impostor");
            // Default Role Teams
            RoleApi.AssignRoleToTeam("com.innersloth.spacemafia.crewmate", "com.innersloth.spacemafia.crewmate");
            RoleApi.AssignRoleToTeam("com.innersloth.spacemafia.impostor", "com.innersloth.spacemafia.impostor");
            // Default Role Properties
            RoleApi.SetIntroCutscene("com.innersloth.spacemafia.crewmate", "Crewmate", "Do your tasks or eject the impostors", Palette.CrewmateBlue, Palette.CrewmateBlue);
            RoleApi.SetIntroCutscene("com.innersloth.spacemafia.impostor", "Impostor", "Destroy the crewmates", Palette.ImpostorRed, Palette.ImpostorRed);
            RoleApi.SetRoleCanKill("com.innersloth.spacemafia.impostor", true);
            RoleApi.SetRoleColorAndBroadcast("com.innersloth.spacemafia.impostor", Palette.ImpostorRed, false);
            // Patch
            Harmony.PatchAll();
        }
    }
}
