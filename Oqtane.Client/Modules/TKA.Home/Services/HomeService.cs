using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using TKA.Home.Models;

namespace TKA.Home.Services
{
    public class HomeService : ServiceBase, IHomeService, IService
    {
        private readonly SiteState _siteState;

        public HomeService(HttpClient http, SiteState siteState) : base(http)
        {
            _siteState = siteState;
        }

         private string Apiurl => CreateApiUrl(_siteState.Alias, "Home");

        public async Task<List<Models.Home>> GetHomesAsync(int ModuleId)
        {
            List<Models.Home> Homes = await GetJsonAsync<List<Models.Home>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", ModuleId));
            return Homes.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.Home> GetHomeAsync(int HomeId, int ModuleId)
        {
            return await GetJsonAsync<Models.Home>(CreateAuthorizationPolicyUrl($"{Apiurl}/{HomeId}", ModuleId));
        }

        public async Task<Models.Home> AddHomeAsync(Models.Home Home)
        {
            return await PostJsonAsync<Models.Home>(CreateAuthorizationPolicyUrl($"{Apiurl}", Home.ModuleId), Home);
        }

        public async Task<Models.Home> UpdateHomeAsync(Models.Home Home)
        {
            return await PutJsonAsync<Models.Home>(CreateAuthorizationPolicyUrl($"{Apiurl}/{Home.HomeId}", Home.ModuleId), Home);
        }

        public async Task DeleteHomeAsync(int HomeId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{HomeId}", ModuleId));
        }
    }
}
