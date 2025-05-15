using System.Security.Claims;
using VerticalTemplate.Api.Common.Extensions;

namespace VerticalTemplate.Api.Common.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string? UserId =>
        _httpContextAccessor.HttpContext?.User?.FindFirstValue(HeaderConstants.UserProfileId);
    public string? UserProfileId =>
        _httpContextAccessor.HttpContext?.User?.FindFirstValue(HeaderConstants.UserProfileId);

    public string? CorrelationId =>
        _httpContextAccessor.HttpContext?.GetCorrelationId(allowEmpty: true);
    public string? Token =>
    _httpContextAccessor.HttpContext
            ?.Request?.Headers?.FirstOrDefault(x => x.Key == HeaderConstants.Authorization)
            .Value;
}