using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using TKA.Home.Models;
using TKA.Home.Repository;

namespace TKA.Home.Controllers
{
    [Route(ControllerRoutes.Default)]
    public class HomeController : Controller
    {
        private readonly IHomeRepository _HomeRepository;
        private readonly ILogManager _logger;
        protected int _entityId = -1;

        public HomeController(IHomeRepository HomeRepository, ILogManager logger, IHttpContextAccessor accessor)
        {
            _HomeRepository = HomeRepository;
            _logger = logger;

            if (accessor.HttpContext.Request.Query.ContainsKey("entityid"))
            {
                _entityId = int.Parse(accessor.HttpContext.Request.Query["entityid"]);
            }
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.Home> Get(string moduleid)
        {
            return _HomeRepository.GetHomes(int.Parse(moduleid));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.Home Get(int id)
        {
            Models.Home Home = _HomeRepository.GetHome(id);
            if (Home != null && Home.ModuleId != _entityId)
            {
                Home = null;
            }
            return Home;
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Home Post([FromBody] Models.Home Home)
        {
            if (ModelState.IsValid && Home.ModuleId == _entityId)
            {
                Home = _HomeRepository.AddHome(Home);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Home Added {Home}", Home);
            }
            return Home;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Home Put(int id, [FromBody] Models.Home Home)
        {
            if (ModelState.IsValid && Home.ModuleId == _entityId)
            {
                Home = _HomeRepository.UpdateHome(Home);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Home Updated {Home}", Home);
            }
            return Home;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.Home Home = _HomeRepository.GetHome(id);
            if (Home != null && Home.ModuleId == _entityId)
            {
                _HomeRepository.DeleteHome(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Home Deleted {HomeId}", id);
            }
        }
    }
}
