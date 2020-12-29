using System.Collections.Generic;
using TKA.Contacts.Models;

namespace TKA.Contacts.Repository
{
    public interface IContactsRepository
    {
        IEnumerable<Models.Contacts> GetContactss(int ModuleId);
        Models.Contacts GetContacts(int ContactsId);
        Models.Contacts AddContacts(Models.Contacts Contacts);
        Models.Contacts UpdateContacts(Models.Contacts Contacts);
        void DeleteContacts(int ContactsId);
    }
}
