using Dominio.Entity;
using Service.Path;
using Newtonsoft.Json;
namespace Services
{
    public class UserServices
    {
        
        public static async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(PathService.PathUsers);
                if (response.IsSuccessStatusCode)
                {
                    var usersJson = await response.Content.ReadAsStringAsync();
                    UserCollection coll = JsonConvert.DeserializeObject<UserCollection>(usersJson);
                    List<User> users = coll.Users.ToList();
                    return users;
                }
                else { throw new Exception("erro no else!"); }
            }

            }
            catch (JsonException e)
            {
                throw new Exception("Erro: " + e.Message);
            }
        }
    }
}
