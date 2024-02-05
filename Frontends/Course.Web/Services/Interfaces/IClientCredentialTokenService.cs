using System.Threading.Tasks;
using System;

namespace Course.Web.Services.Interfaces
{
    public interface IClientCredentialTokenService
    {
        Task<String> GetToken();
    }
}
