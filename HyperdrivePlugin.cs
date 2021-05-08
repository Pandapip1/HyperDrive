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

namespace HyperDrive
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class HyperDrivePlugin : BasePlugin
    {
        private const string Id = "HyperDrive";

        private Harmony Harmony { get; } = new Harmony(Id);
        
        public static readonly string CrewmateRole = "hyperdrive.crewmate";
        public static readonly string IndividualRole = "hyperdrive.individual";

        internal static ManualLogSource Logger = new ManualLogSource("HyperDrive");

        public override void Load()
        {
            BepInEx.Logging.Logger.Sources.Add(Logger);

            RoleManager.CreateRole("hyperdrive.crewmate");
            RoleManager.CreateRole("hyperdrive.impostor");
            RoleManager.CreateRole("hyperdrive.individual");

            IntroCutsceneManager.SetRoleTitle("hyperdrive.crewmate", "Crewmate");
            IntroCutsceneManager.SetRoleTitle("hyperdrive.impostor", "Impostor");
            IntroCutsceneManager.SetImpostorText("hyperdrive.crewmate", "Do your tasks");
            IntroCutsceneManager.SetImpostorText("hyperdrive.impostor", "Kill the crewmates");
            IntroCutsceneManager.SetBackgroundBarColor("hyperdrive.crewmate", Palette.CrewmateBlue);
            IntroCutsceneManager.SetBackgroundBarColor("hyperdrive.impostor", Palette.ImpostorRed);
            IntroCutsceneManager.SetTitleColor("hyperdrive.crewmate", Palette.CrewmateBlue);
            IntroCutsceneManager.SetTitleColor("hyperdrive.impostor", Palette.ImpostorRed);
            
            AbilitiesManager.SetCanSabotage("hyperdrive.impostor", true);
            AbilitiesManager.SetCanKill("hyperdrive.impostor", true);
            
            OptionsManager.SetChoosable("hyperdrive.crewmate", false);
            OptionsManager.SetChoosable("hyperdrive.individual", false);
            
            Harmony.PatchAll();
        }
    }
}
