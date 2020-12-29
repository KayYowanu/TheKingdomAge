using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using TKA.Contacts.Models;
using TKA.Contacts.Repository;

namespace TKA.Contacts.Controllers
{
    [Route(ControllerRoutes.Default)]
    public class ContactsController : Controller
    {
        private readonly IContactsRepository _ContactsRepository;
        private readonly ILogManager _logger;
        protected int _entityId = -1;

        public ContactsController(IContactsRepository ContactsRepository, ILogManager logger, IHttpContextAccessor accessor)
        {
            _ContactsRepository = ContactsRepository;
            _logger = logger;

            if (accessor.HttpContext.Request.Query.ContainsKey("entityid"))
            {
                _entityId = int.Parse(accessor.HttpContext.Request.Query["entityid"]);
            }
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.Contacts> Get(string moduleid)
        {
            return _ContactsRepository.GetContactss(int.Parse(moduleid));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.Contacts Get(int id)
        {
            Models.Contacts Contacts = _ContactsRepository.GetContacts(id);
            if (Contacts != null && Contacts.ModuleId != _entityId)
            {
                Contacts = null;
            }
            return Contacts;
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Contacts Post([FromBody] Models.Contacts Contacts)
        {
            if (ModelState.IsValid && Contacts.ModuleId == _entityId)
            {
                Contacts = _ContactsRepository.AddContacts(Contacts);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Contacts Added {Contacts}", Contacts);
            }
            return Contacts;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Contacts Put(int id, [FromBody] Models.Contacts Contacts)
        {
            if (ModelState.IsValid && Contacts.ModuleId == _entityId)
            {
                Contacts = _ContactsRepository.UpdateContacts(Contacts);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Contacts Updated {Contacts}", Contacts);
            }
            return Contacts;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.Contacts Contacts = _ContactsRepository.GetContacts(id);
            if (Contacts != null && Contacts.ModuleId == _entityId)
            {
                _ContactsRepository.DeleteContacts(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Contacts Deleted {ContactsId}", id);
            }
        }
    }
}
