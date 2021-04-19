using AllOfUs.CubeAPI.Patches;
using AllOfUs.Rpc;
using BepInEx.Configuration;
using UnityEngine;

namespace AllOfUs.CubeAPI.Options
{
    public class CustomNumberOption : CustomOption
    {
        public CustomNumberOption(string Title, float InitialValue, FloatRange ValidRange, float Increment, bool ZeroIsInfinity)
        {
            this.Title = Title;
            this.ValidRange = ValidRange;
            this.Increment = Increment;
            this.ZeroIsInfinity = ZeroIsInfinity;
            ConfigEntry = AllOfUsPlugin.Instance.Config.Bind("Custom Number Options", Title, InitialValue);
            StringName = CustomStringName.Register(Title);
            OptionsPatches.CustomNumberOptions.Add(this);
        }

        public float Value => Mathf.Clamp(ConfigEntry.Value, ValidRange.min, ValidRange.max);
        public ConfigEntry<float> ConfigEntry { get; }
        public FloatRange ValidRange { get; }
        public float Increment { get; }
        public bool ZeroIsInfinity { get; }
        protected override float PrivateFloatValue => ConfigEntry.Value;
        protected override int PrivateIntValue => (int)ConfigEntry.Value;
        public void OnValueChanged(OptionBehaviour optionBehaviour)
        { 
            ConfigEntry.Value = optionBehaviour.GetFloat();
            RpcSyncSettings.Send();
        }
    }
}
