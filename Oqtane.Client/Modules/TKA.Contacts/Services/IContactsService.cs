using System.Collections.Generic;
using System.Threading.Tasks;
using TKA.Contacts.Models;

namespace TKA.Contacts.Services
{
    public interface IContactsService 
    {
        Task<List<Models.Contacts>> GetContactssAsync(int ModuleId);

        Task<Models.Contacts> GetContactsAsync(int ContactsId, int ModuleId);

        Task<Models.Contacts> AddContactsAsync(Models.Contacts Contacts);

        Task<Models.Contacts> UpdateContactsAsync(Models.Contacts Contacts);

        Task DeleteContactsAsync(int ContactsId, int ModuleId);
    }
}
