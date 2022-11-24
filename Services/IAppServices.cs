using Uporg.Model;

namespace Uporg.Services
{
    public interface IAppServices
    {
        public Task<List<AppModel>> GetApps(string user);
        public Task<List<AppModel>> Search(string name);
        public Task InsertApps(AppModel app);
        public Task DeleteApps(int id);
    }
}
