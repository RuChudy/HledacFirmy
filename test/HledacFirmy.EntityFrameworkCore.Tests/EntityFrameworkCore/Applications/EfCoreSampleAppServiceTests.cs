using HledacFirmy.Samples;
using Xunit;

namespace HledacFirmy.EntityFrameworkCore.Applications;

[Collection(HledacFirmyTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<HledacFirmyEntityFrameworkCoreTestModule>
{

}
