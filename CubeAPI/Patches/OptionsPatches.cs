
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AllOfUs.CubeAPI.Options;
using AllOfUs.Extensions;
using AllOfUs.Rpc;
using HarmonyLib;
using UnityEngine;
using Object = UnityEngine.Object;
// ReSharper disable All

namespace AllOfUs.CubeAPI.Patches
{
    public static class OptionsPatches
    {
        public static List<CustomNumberOption> CustomNumberOptions = new();
        public static List<CustomToggleOption> CustomToggleOptions = new();
        public static List<CustomStringOption> CustomStringOptions = new();

        [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.Start))]
        public static class GameOptionsMenuStartPatch
        {
            public static void Prefix(GameOptionsMenu __instance)
            {
                __instance.GetComponentInParent<Scroller>().YBounds.max = 12;

                const float start = -8.7f;
                const float offset = -0.5f;

                var i = 0;
				
                foreach (var customOption in CustomNumberOptions)
                {
                    var option = Object.Instantiate(__instance.GetComponentsInChildren<NumberOption>()[0], __instance.transform);
                    option.transform.localPosition = new Vector3(option.transform.localPosition.x, start + offset * i, option.transform.localPosition.z);
                    option.Title = customOption.StringName;
                    option.TitleText.text = customOption.Title;
                    option.Value = customOption.ConfigEntry.Value;
                    option.ValidRange = customOption.ValidRange;
                    option.ZeroIsInfinity = customOption.ZeroIsInfinity;
                    option.Increment = customOption.Increment;
                    option.OnValueChanged = new Action<OptionBehaviour>(customOption.OnValueChanged);
                    i++;
                }
                
                foreach (var customOption in CustomToggleOptions)
                {
                    var option = Object.Instantiate(__instance.GetComponentsInChildren<ToggleOption>()[1], __instance.transform);
                    option.transform.localPosition = new Vector3(option.transform.localPosition.x, start + offset * i, option.transform.localPosition.z);
                    option.Title = customOption.StringName;
                    option.TitleText.text = customOption.Title;
                    option.CheckMark.enabled = customOption.ConfigEntry.Value;
                    option.OnValueChanged = new Action<OptionBehaviour>(customOption.OnValueChanged);
                    customOption.Option = option;
                    i++;
                }
                
                foreach (var customOption in CustomStringOptions)
                {
                    var option = Object.Instantiate(__instance.GetComponentsInChildren<StringOption>()[1], __instance.transform);
                    option.transform.localPosition = new Vector3(option.transform.localPosition.x, start + offset * i, option.transform.localPosition.z);
                    option.Title = customOption.StringName;
                    option.TitleText.text = customOption.Title;
                    option.Value = customOption.ConfigEntry.Value;
                    option.Values = customOption.Values;
                    option.OnValueChanged = new Action<OptionBehaviour>(customOption.OnValueChanged);
                    customOption.Option = option;
                    i++;
                }
            }
        }

        [HarmonyPatch]
        public static class ExecutionLogger
        {
            public static IEnumerable<MethodBase> TargetMethods()
            {
                var bases = new List<MethodBase>();
                for (var index = 0; index < typeof(GameOptionsData).GetMethods().Length; index++)
                {
                    var method = typeof(GameOptionsData).GetMethods()[index];
                    if (method.ReturnType == typeof(string) && method.GetParameters().Length == 1)
                    {
                        bases.Add(method);
                    }
                }

                return bases.AsEnumerable();
            }
            
            // ReSharper disable once InconsistentNaming
            public static void Postfix(ref string __result)
            {
                var builder = new StringBuilder(__result);
                foreach (var option in CustomNumberOptions)
                {
                    builder.AppendLine($"{option.Title}: {option.Value}");
                }
                foreach (var option in CustomToggleOptions)
                {
                    builder.AppendLine($"{option.Title}: {(option.Value ? "On" : "Off")}");
                }
                foreach (var option in CustomStringOptions)
                {
                    builder.AppendLine($"{option.Title}: {option.StringValue}");
                }

                __result = builder.ToString();
            }
        }
        
        [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSyncSettings))]
        public static class RpcSyncSettingsPatch
        {
            public static void Postfix()
            {
                RpcSyncSettings.Send();
            }
        }
        
        [HarmonyPatch(typeof(NumberOption), nameof(NumberOption.FixedUpdate))]
        public static class OptionPatch1
        {
            public static void Postfix(NumberOption __instance)
            {
                if (!__instance.IsCustomOption()) return;
                __instance.OnValueChanged = new Action<OptionBehaviour>(
                    CustomNumberOptions.Find(x => 
                        x.StringName == __instance.Title).OnValueChanged);
            }
        }
        
        [HarmonyPatch(typeof(ToggleOption), nameof(ToggleOption.FixedUpdate))]
        public static class OptionPatch2
        {
            public static void Postfix(ToggleOption __instance)
            {
                if (!__instance.IsCustomOption()) return;
                __instance.OnValueChanged = new Action<OptionBehaviour>(
                    CustomToggleOptions.Find(x => 
                        x.StringName == __instance.Title).OnValueChanged);
            }
        }
        
        [HarmonyPatch(typeof(StringOption), nameof(StringOption.FixedUpdate))]
        public static class OptionPatch3
        {
            public static void Postfix(StringOption __instance)
            {
                if (!__instance.IsCustomOption()) return;
                __instance.OnValueChanged = new Action<OptionBehaviour>(
                    CustomStringOptions.Find(x => 
                        x.StringName == __instance.Title).OnValueChanged);
            }
        }
        
    }
}
