using Soenneker.Cosmos.Repositories.Audits.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Cosmos.Repositories.Audits.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class AuditsRepositoryTests : HostedUnitTest
{
    private readonly IAuditsRepository _util;

    public AuditsRepositoryTests(Host host) : base(host)
    {
        _util = Resolve<IAuditsRepository>(true);
    }

    [Test]
    public void Default()
    {

    }
}
