using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using TKA.Contacts.Models;

namespace TKA.Contacts.Repository
{
    public class ContactsContext : DBContextBase, IService
    {
        public virtual DbSet<Models.Contacts> Contacts { get; set; }

        public ContactsContext(ITenantResolver tenantResolver, IHttpContextAccessor accessor) : base(tenantResolver, accessor)
        {
            // ContextBase handles multi-tenant database connections
        }
    }
}
