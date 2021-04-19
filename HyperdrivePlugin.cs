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

namespace tk.pandapip1.hyperdrive
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class HyperDrivePlugin : BasePlugin
    {
        private const string Id = "tk.pandapip1.easyroles";

        private Harmony Harmony { get; } = new Harmony(Id);
        
        internal string DefaultRole = "tk.pandapip1.hyperdrive.crewmate";

        internal static ManualLogSource Logger = new ManualLogSource("HyperDrive");

        public override void Load()
        {
            BepInEx.Logging.Logger.Sources.Add(Logger);

            HyperDrive.CreateRole("tk.pandapip1.hyperdrive.crewmate");
            HyperDrive.CreateRole("tk.pandapip1.hyperdrive.impostor");
            HyperDrive.CreateRole("tk.pandapip1.hyperdrive.individual");

            HyperDrive.SetName("tk.pandapip1.hyperdrive.crewmate", "Crewmate");
            HyperDrive.SetName("tk.pandapip1.hyperdrive.impostor", "Impostor");
            
            HyperDrive.SetDesc("tk.pandapip1.hyperdrive.crewmate", "Do your tasks");
            HyperDrive.SetDesc("tk.pandapip1.hyperdrive.impostor", "Kill the crewmates");
            
            HyperDrive.SetColor("tk.pandapip1.hyperdrive.crewmate", Color.White);
            HyperDrive.SetColor("tk.pandapip1.hyperdrive.impostor", Palette.ImpostorRed);
            
            HyperDrive.SetICColor("tk.pandapip1.hyperdrive.crewmate", Color.CrewmateBlue);
            
            HyperDrive.SetCanSabotage("tk.pandapip1.hyperdrive.impostor", true);
            HyperDrive.SetCanKill("tk.pandapip1.hyperdrive.impostor", true);
            
            HyperDrive.SetChoosable("tk.pandapip1.hyperdrive.crewmate", false);
            HyperDrive.SetInternal("tk.pandapip1.hyperdrive.individual", true);
            
            Harmony.PatchAll();
        }
    }
}
