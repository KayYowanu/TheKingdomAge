using Oqtane.Models;
using Oqtane.Modules;

namespace TKA.Home
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Home",
            Description = "Home",
            Version = "1.0.0",
            ServerManagerType = "TKA.Home.Manager.HomeManager, Oqtane.Server",
            ReleaseVersions = "1.0.0"
        };
    }
}
