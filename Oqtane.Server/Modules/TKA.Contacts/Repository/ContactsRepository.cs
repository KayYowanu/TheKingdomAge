using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using TKA.Contacts.Models;

namespace TKA.Contacts.Repository
{
    public class ContactsRepository : IContactsRepository, IService
    {
        private readonly ContactsContext _db;

        public ContactsRepository(ContactsContext context)
        {
            _db = context;
        }

        public IEnumerable<Models.Contacts> GetContactss(int ModuleId)
        {
            return _db.Contacts.Where(item => item.ModuleId == ModuleId);
        }

        public Models.Contacts GetContacts(int ContactsId)
        {
            return _db.Contacts.Find(ContactsId);
        }

        public Models.Contacts AddContacts(Models.Contacts Contacts)
        {
            _db.Contacts.Add(Contacts);
            _db.SaveChanges();
            return Contacts;
        }

        public Models.Contacts UpdateContacts(Models.Contacts Contacts)
        {
            _db.Entry(Contacts).State = EntityState.Modified;
            _db.SaveChanges();
            return Contacts;
        }

        public void DeleteContacts(int ContactsId)
        {
            Models.Contacts Contacts = _db.Contacts.Find(ContactsId);
            _db.Contacts.Remove(Contacts);
            _db.SaveChanges();
        }
    }
}
