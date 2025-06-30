using Volo.Abp.Settings;

namespace HledacFirmy.Settings;

public class HledacFirmySettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(HledacFirmySettings.MySetting1));
    }
}
