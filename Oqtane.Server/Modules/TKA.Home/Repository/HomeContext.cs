using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using TKA.Home.Models;

namespace TKA.Home.Repository
{
    public class HomeContext : DBContextBase, IService
    {
        public virtual DbSet<Models.Home> Home { get; set; }

        public HomeContext(ITenantResolver tenantResolver, IHttpContextAccessor accessor) : base(tenantResolver, accessor)
        {
            // ContextBase handles multi-tenant database connections
        }
    }
}
