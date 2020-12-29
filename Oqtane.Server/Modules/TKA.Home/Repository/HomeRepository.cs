using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using TKA.Home.Models;

namespace TKA.Home.Repository
{
    public class HomeRepository : IHomeRepository, IService
    {
        private readonly HomeContext _db;

        public HomeRepository(HomeContext context)
        {
            _db = context;
        }

        public IEnumerable<Models.Home> GetHomes(int ModuleId)
        {
            return _db.Home.Where(item => item.ModuleId == ModuleId);
        }

        public Models.Home GetHome(int HomeId)
        {
            return _db.Home.Find(HomeId);
        }

        public Models.Home AddHome(Models.Home Home)
        {
            _db.Home.Add(Home);
            _db.SaveChanges();
            return Home;
        }

        public Models.Home UpdateHome(Models.Home Home)
        {
            _db.Entry(Home).State = EntityState.Modified;
            _db.SaveChanges();
            return Home;
        }

        public void DeleteHome(int HomeId)
        {
            Models.Home Home = _db.Home.Find(HomeId);
            _db.Home.Remove(Home);
            _db.SaveChanges();
        }
    }
}
