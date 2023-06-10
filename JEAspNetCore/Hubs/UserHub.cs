using Microsoft.AspNetCore.SignalR;

namespace JEAspNetCore.User.Hubs
{
    public class UserHub:Hub
    {
        public async Task SendMessage(string user, string message)
        {
            Console.WriteLine(user);
            Console.WriteLine(message);
            await Clients.All.SendAsync( user, message);
        }

    }
}
