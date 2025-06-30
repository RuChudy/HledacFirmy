using HledacFirmy.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace HledacFirmy.Permissions;

public class HledacFirmyPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(HledacFirmyPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(HledacFirmyPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HledacFirmyResource>(name);
    }
}
