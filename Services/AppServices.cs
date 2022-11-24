using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Uporg.Model;

namespace Uporg.Services
{
    public class AppServices : IAppServices
    {
        FirebaseClient client = new FirebaseClient("https://uporg-8e01d-default-rtdb.firebaseio.com/");
        public async Task DeleteApps(int id)
        {
            var todelete = (await client.Child("App").OnceAsync<AppModel>()).Where(a => a.Object.Id == id).FirstOrDefault();
            await client.Child("App").Child(todelete!.Key).DeleteAsync();
        }

        public async Task<List<AppModel>> GetApps(string user)
        {
            return (await client.Child("App").OnceAsync<AppModel>()).Where(x=>x.Object.User == user).Select(item => new AppModel
            {
                Name = item.Object.Name,
                Image = item.Object.Image,
                Id = item.Object.Id,
                Url = item.Object.Url,
                Date = item.Object.Date,
                User = item.Object.User
            }).ToList();
        }

        public async Task InsertApps(AppModel app)
        {
            
            await client.Child("App").PostAsync(new AppModel()
            {
                Name = app.Name,
                Image = app.Image,
                Url = app.Url,
                User = app.User,
                Date = app.Date,
                Id = app.Id
            });
        }

        public async Task<List<AppModel>> Search(string name)
        {
            return (await client.Child("App").OnceAsync<AppModel>()).Where(x => x.Object.Name.ToLower().Contains(name.ToLower())||x.Object.Url.ToLower().Contains(name.ToLower())).Select(item => new AppModel
            {
                Name = item.Object.Name,
                Image = item.Object.Image,
                Id = item.Object.Id,
                Url = item.Object.Url,
                Date = item.Object.Date,
                User = item.Object.User
            }).ToList();
        }
    }
}
