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
            BepInEx.Logging.Logger.Sources.Add(Logger);
            Harmony.PatchAll();
        }
    }
}
