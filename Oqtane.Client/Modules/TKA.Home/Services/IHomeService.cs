using System.Collections.Generic;
using System.Threading.Tasks;
using TKA.Home.Models;

namespace TKA.Home.Services
{
    public interface IHomeService 
    {
        Task<List<Models.Home>> GetHomesAsync(int ModuleId);

        Task<Models.Home> GetHomeAsync(int HomeId, int ModuleId);

        Task<Models.Home> AddHomeAsync(Models.Home Home);

        Task<Models.Home> UpdateHomeAsync(Models.Home Home);

        Task DeleteHomeAsync(int HomeId, int ModuleId);
    }
}
