using HledacFirmy.Samples;
using Xunit;

namespace HledacFirmy.EntityFrameworkCore.Domains;

[Collection(HledacFirmyTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<HledacFirmyEntityFrameworkCoreTestModule>
{

}
