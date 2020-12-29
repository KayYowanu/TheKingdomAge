using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Repository;
using TKA.Home.Models;
using TKA.Home.Repository;

namespace TKA.Home.Manager
{
    public class HomeManager : IInstallable, IPortable
    {
        private IHomeRepository _HomeRepository;
        private ISqlRepository _sql;

        public HomeManager(IHomeRepository HomeRepository, ISqlRepository sql)
        {
            _HomeRepository = HomeRepository;
            _sql = sql;
        }

        public bool Install(Tenant tenant, string version)
        {
            return _sql.ExecuteScript(tenant, GetType().Assembly, "TKA.Home." + version + ".sql");
        }

        public bool Uninstall(Tenant tenant)
        {
            return _sql.ExecuteScript(tenant, GetType().Assembly, "TKA.Home.Uninstall.sql");
        }

        public string ExportModule(Module module)
        {
            string content = "";
            List<Models.Home> Homes = _HomeRepository.GetHomes(module.ModuleId).ToList();
            if (Homes != null)
            {
                content = JsonSerializer.Serialize(Homes);
            }
            return content;
        }

        public void ImportModule(Module module, string content, string version)
        {
            List<Models.Home> Homes = null;
            if (!string.IsNullOrEmpty(content))
            {
                Homes = JsonSerializer.Deserialize<List<Models.Home>>(content);
            }
            if (Homes != null)
            {
                foreach(var Home in Homes)
                {
                    _HomeRepository.AddHome(new Models.Home { ModuleId = module.ModuleId, Name = Home.Name });
                }
            }
        }
    }
}