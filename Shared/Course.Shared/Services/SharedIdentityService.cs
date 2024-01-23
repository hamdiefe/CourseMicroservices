using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Course.Shared.Services
{
    public class SharedIdentityService : ISharedIdentityService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public SharedIdentityService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetUserId => _contextAccessor.HttpContext.User.Claims
                                                               .Where(x => x.Type == "sub")
                                                               .FirstOrDefault().Value;

    }
}
