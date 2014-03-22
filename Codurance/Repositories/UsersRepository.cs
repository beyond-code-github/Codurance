namespace Codurance.Repositories
{
    using System;

    using Codurance.Entities;

    public interface IUsersRepository
    {
        User GetUser(string username);
    }

    public class UsersRepository : IUsersRepository
    {
        public User GetUser(string username)
        {
            throw new NotImplementedException();
        }
    }
}
