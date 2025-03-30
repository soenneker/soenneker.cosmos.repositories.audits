using Soenneker.Cosmos.Repositories.Audits.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Cosmos.Repositories.Audits.Tests;

[Collection("Collection")]
public class AuditsRepositoryTests : FixturedUnitTest
{
    private readonly IAuditsRepository _util;

    public AuditsRepositoryTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IAuditsRepository>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
