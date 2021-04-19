using System.Linq;
using AllOfUs.CubeAPI.Patches;
using AllOfUs.Rpc;
using BepInEx.Configuration;
using UnhollowerBaseLib;
using UnityEngine;

namespace AllOfUs.CubeAPI.Options
{
    public class CustomStringOption : CustomOption
    {
        public CustomStringOption(string Title, params string[] Values)
        {
            this.Title = Title;
            this.Values = Values.Select(x => (StringNames)CustomStringName.Register(x)).ToArray();
            ConfigEntry = AllOfUsPlugin.Instance.Config.Bind("Custom String Options", Title+" (Int)", 0);
            StringName = CustomStringName.Register(Title);
            OptionsPatches.CustomStringOptions.Add(this);
        }
        public int Value => Mathf.Clamp(ConfigEntry.Value, Values.Length-1, 0);
        public string StringValue => ((CustomStringName)Values[Value]).Value;
        public ConfigEntry<int> ConfigEntry { get; }
        public StringOption Option { get; set; }
        public StringNames[] Values { get; }
        protected override int PrivateIntValue => Option != null ? Option.GetInt() : 0;
        protected override string PrivateStringValue => Option != null ? 
            DestroyableSingleton<TranslationController>.Instance
                .GetString(Option.Values[Option.Value], new Il2CppReferenceArray<Il2CppSystem.Object>(0)) : 
            DestroyableSingleton<TranslationController>.Instance
                .GetString(Values[ConfigEntry.Value], new Il2CppReferenceArray<Il2CppSystem.Object>(0));

        public void OnValueChanged(OptionBehaviour optionBehaviour)
        {
            ConfigEntry.Value = Option.Value;
            RpcSyncSettings.Send();
        }
    }
}
