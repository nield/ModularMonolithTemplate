using System.Security.Claims;
using static ModularMonolithTemplate.Api.Common.Constants;
using ModularMonolithTemplate.Api.Common.Extensions;

namespace ModularMonolithTemplate.Api.Common.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => 
        _httpContextAccessor.HttpContext?.User?.FindFirstValue(Headers.UserProfileId);

    public string? UserProfileId =>
        _httpContextAccessor.HttpContext?.User?.FindFirstValue(Headers.UserProfileId);

    public string? CorrelationId =>
        _httpContextAccessor.HttpContext?.GetCorrelationId(allowEmpty: true);

    public string? Token =>
        _httpContextAccessor.HttpContext
            ?.Request?.Headers?.FirstOrDefault(x => x.Key == Headers.Authorization)
            .Value;
}