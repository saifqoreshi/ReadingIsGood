using System;
using System.Linq;

namespace BookStoreApi.Models
{
    public class UsersRepository : IDisposable
    {

        private BookStoreContext context = new BookStoreContext();

        public UsersRepository( )
        {
             
        }

        //This method is used to check and validate the user credentials
        public  User ValidateUser(string username, string password)
        {
            return context.Users.FirstOrDefault(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password);
        }

        public void Dispose()
        {
            context.Dispose();
        }

    }


}
