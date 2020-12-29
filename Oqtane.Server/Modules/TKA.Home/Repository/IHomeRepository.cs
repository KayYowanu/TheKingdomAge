using System.Collections.Generic;
using TKA.Home.Models;

namespace TKA.Home.Repository
{
    public interface IHomeRepository
    {
        IEnumerable<Models.Home> GetHomes(int ModuleId);
        Models.Home GetHome(int HomeId);
        Models.Home AddHome(Models.Home Home);
        Models.Home UpdateHome(Models.Home Home);
        void DeleteHome(int HomeId);
    }
}
