using Ecommerce.API;
using Ecommerce.BuildingBlocks.Application;
using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.Catalog.Application;
using Ecommerce.Catalog.Domain;
using Ecommerce.Catalog.Infrastructure;
using Ecommerce.Catalog.IntegrationEvents;

namespace Ecommerce.Catalog.Tests.ArchTests;

internal record References
{
    internal static readonly string ApplicationNamespace = typeof(ApplicationReference).Namespace!;
    internal static readonly string InfrastructureNamespace = typeof(InfrastructureReference).Namespace!;
    internal static readonly string ApiNamespace = typeof(ApiReference).Namespace!;
    internal static readonly string IntegrationEventsNamespace = typeof(IntegrationEventsReference).Namespace!;
    internal static readonly string DomainNamespace = typeof(DomainReference).Namespace!;

    internal static readonly string ApplicationBuildingBlocksNamespace = typeof(ApplicationBuildingBlocksReference).Namespace!;
    internal static readonly string InfrastuctureBuildingBlocksNamespace = typeof(InfrastructureBuildingBlocksReference).Namespace!;
    internal static readonly string DomainBuildingBlocksNamespace = typeof(DomainBuildingBlocksReference).Namespace!;
}
