using Microsoft.AspNetCore.Mvc.Testing;

namespace PWPA.CallLogging.BackEnd.API.IntegrationTests;

[CollectionDefinition(nameof(IntegrationTestsCollection))]
public sealed class IntegrationTestsCollection : ICollectionFixture<WebApplicationFactory<Program>>
{
}