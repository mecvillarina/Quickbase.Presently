using Microsoft.AspNetCore.Http;
using Presently.Common.Models;

namespace Presently.FunctionApp.Providers.Abstractions
{
    public interface IAccessTokenProvider
    {
        AuthToken GenerateToken(string id);
        AccessTokenResult ValidateToken(HttpRequest request);
    }
}
