using Xunit;

namespace HledacFirmy.EntityFrameworkCore;

[CollectionDefinition(HledacFirmyTestConsts.CollectionDefinitionName)]
public class HledacFirmyEntityFrameworkCoreCollection : ICollectionFixture<HledacFirmyEntityFrameworkCoreFixture>
{

}
