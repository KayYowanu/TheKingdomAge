using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Repository;
using TKA.Contacts.Models;
using TKA.Contacts.Repository;

namespace TKA.Contacts.Manager
{
    public class ContactsManager : IInstallable, IPortable
    {
        private IContactsRepository _ContactsRepository;
        private ISqlRepository _sql;

        public ContactsManager(IContactsRepository ContactsRepository, ISqlRepository sql)
        {
            _ContactsRepository = ContactsRepository;
            _sql = sql;
        }

        public bool Install(Tenant tenant, string version)
        {
            return _sql.ExecuteScript(tenant, GetType().Assembly, "TKA.Contacts." + version + ".sql");
        }

        public bool Uninstall(Tenant tenant)
        {
            return _sql.ExecuteScript(tenant, GetType().Assembly, "TKA.Contacts.Uninstall.sql");
        }

        public string ExportModule(Module module)
        {
            string content = "";
            List<Models.Contacts> Contactss = _ContactsRepository.GetContactss(module.ModuleId).ToList();
            if (Contactss != null)
            {
                content = JsonSerializer.Serialize(Contactss);
            }
            return content;
        }

        public void ImportModule(Module module, string content, string version)
        {
            List<Models.Contacts> Contactss = null;
            if (!string.IsNullOrEmpty(content))
            {
                Contactss = JsonSerializer.Deserialize<List<Models.Contacts>>(content);
            }
            if (Contactss != null)
            {
                foreach(var Contacts in Contactss)
                {
                    _ContactsRepository.AddContacts(new Models.Contacts { ModuleId = module.ModuleId, Name = Contacts.Name });
                }
            }
        }
    }
}