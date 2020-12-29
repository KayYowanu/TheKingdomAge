using Oqtane.Models;
using Oqtane.Modules;

namespace TKA.Contacts
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Contacts",
            Description = "Contacts",
            Version = "1.0.0",
            ServerManagerType = "TKA.Contacts.Manager.ContactsManager, Oqtane.Server",
            ReleaseVersions = "1.0.0"
        };
    }
}
