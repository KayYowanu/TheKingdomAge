using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using TKA.Contacts.Models;

namespace TKA.Contacts.Services
{
    public class ContactsService : ServiceBase, IContactsService, IService
    {
        private readonly SiteState _siteState;

        public ContactsService(HttpClient http, SiteState siteState) : base(http)
        {
            _siteState = siteState;
        }

         private string Apiurl => CreateApiUrl(_siteState.Alias, "Contacts");

        public async Task<List<Models.Contacts>> GetContactssAsync(int ModuleId)
        {
            List<Models.Contacts> Contactss = await GetJsonAsync<List<Models.Contacts>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", ModuleId));
            return Contactss.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.Contacts> GetContactsAsync(int ContactsId, int ModuleId)
        {
            return await GetJsonAsync<Models.Contacts>(CreateAuthorizationPolicyUrl($"{Apiurl}/{ContactsId}", ModuleId));
        }

        public async Task<Models.Contacts> AddContactsAsync(Models.Contacts Contacts)
        {
            return await PostJsonAsync<Models.Contacts>(CreateAuthorizationPolicyUrl($"{Apiurl}", Contacts.ModuleId), Contacts);
        }

        public async Task<Models.Contacts> UpdateContactsAsync(Models.Contacts Contacts)
        {
            return await PutJsonAsync<Models.Contacts>(CreateAuthorizationPolicyUrl($"{Apiurl}/{Contacts.ContactsId}", Contacts.ModuleId), Contacts);
        }

        public async Task DeleteContactsAsync(int ContactsId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{ContactsId}", ModuleId));
        }
    }
}
