using System.Diagnostics.CodeAnalysis;
using VerticalTemplate.Api.Common.Interfaces;

namespace VerticalTemplate.Api.Integration.Tests.Mocks;

[ExcludeFromCodeCoverage]
public class MockCurrentUserService : ICurrentUserService
{
    public string UserProfileId => "1";

    public string CorrelationId => "1";

    public string Token => "1";

    public string? UserId => "1";
}