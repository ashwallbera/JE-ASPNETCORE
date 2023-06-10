using Firebase.Database;
using JEAspNetCore.Model;
using System.Reflection;

namespace JEAspNetCore.Repository
{
   
    public class AttendanceRepository
    {
        string AuthSecret = "A4mpOGwFLos77X7uJMttMaeBPqURI1vKdQ2iwWBK";
        string BasePath = "https://jadocenterprises-default-rtdb.asia-southeast1.firebasedatabase.app";
        private readonly FirebaseClient firebaseClient;
        public AttendanceRepository() {
            firebaseClient = new FirebaseClient(
                 BasePath,
                 new FirebaseOptions
                 {
                     AuthTokenAsyncFactory = () => Task.FromResult(AuthSecret)
                 });
        }

        public async Task<List<UserModel>> getUsers()
        {
            var result = await firebaseClient
                  .Child("users")
                 .OnceAsync<UserModel>();

            List<UserModel> userModels = new List<UserModel>();
            foreach (var userModel in result)
            {
                userModels.Add((UserModel)userModel.Object);
            }
            return userModels;
        }

        public async Task<bool> isUserExist(UserModel userModel)
        {
            try
            {

                var result = await firebaseClient
                  .Child("users")
                  .OnceAsync<UserModel>();

                foreach (var data in result)
                {
                    if (data.Object.id.Equals(userModel.id))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new SystemException(""+ex.Message);
            }
           
        } 
    }
}
