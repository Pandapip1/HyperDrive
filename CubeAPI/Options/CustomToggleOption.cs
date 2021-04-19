using AllOfUs.CubeAPI.Patches;
using AllOfUs.Rpc;
using BepInEx.Configuration;

namespace AllOfUs.CubeAPI.Options
{
    public class CustomToggleOption : CustomOption
    {
        public CustomToggleOption(string Title, bool InitialValue)
        {
            this.Title = Title;
            ConfigEntry = AllOfUsPlugin.Instance.Config.Bind("Custom Boolean Options", Title, InitialValue);
            StringName = CustomStringName.Register(Title);
            OptionsPatches.CustomToggleOptions.Add(this);
        }
        public bool Value => ConfigEntry.Value;
        public ConfigEntry<bool> ConfigEntry { get; }
        public ToggleOption Option { get; set; }
        protected override bool PrivateBoolValue => Option != null && Option.GetBool();
        public void OnValueChanged(OptionBehaviour optionBehaviour)
        {
            ConfigEntry.Value = optionBehaviour.GetBool();
            RpcSyncSettings.Send();
        }
    }
}
