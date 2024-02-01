using Course.Shared.Dtos;
using Course.Web.Models;
using Course.Web.Services.Interfaces;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;

namespace Course.Web.Services
{
	public class IdentityService : IIdentityService
	{
		private readonly HttpClient _httpClient;
		private readonly IHttpContextAccessor _contextAccessor;
		private readonly ClientSettings _clientSettings;
		private readonly ServiceApiSettings _serviceApiSettings;

		public IdentityService(HttpClient httpClient, 
							  IHttpContextAccessor contextAccessor, 
							  IOptions<ClientSettings> clientSettings,
							  IOptions<ServiceApiSettings> serviceApiSettings)
		{
			_httpClient = httpClient;
			_contextAccessor = contextAccessor;
			_clientSettings = clientSettings.Value;
			_serviceApiSettings = serviceApiSettings.Value;
		}

		public Task<TokenResponse> GetAccessTokenByRefreshToken()
		{
			throw new System.NotImplementedException();
		}

		public Task RevokeRefreshToken()
		{
			throw new System.NotImplementedException();
		}

		public Task<Response<bool>> SignIn(SignInInput signInInput)
		{
			throw new System.NotImplementedException();
		}
	}
}
