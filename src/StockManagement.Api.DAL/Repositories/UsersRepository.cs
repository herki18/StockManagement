using System.Collections.Generic;
using System.Linq;
using StockManagement.Api.Contract.Entities;
using StockManagement.Api.Contract.Interfaces.Repositories;

namespace StockManagement.Api.DAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApiContext _context;

        public UsersRepository(ApiContext context)
        {
            _context = context;
        }

        public UserEntity GetUser(string userName)
        {
            return _context.Users.SingleOrDefault(x => x.Username == userName);
        }

        public List<UserEntity> GetAll()
        {
            return _context.Users.ToList();
        }

        public UserEntity FindUser(int id)
        {
            return _context.Users.Find(id);
        }

        public bool CheckIfUserNameIsInUse(string username)
        {
            return _context.Users.Any(x => x.Username == username);
        }

        public void AddUser(UserEntity userEntity)
        {
            _context.Users.Add(userEntity);
            _context.SaveChanges();
        }

        public void UpdateUser(UserEntity user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Remove(UserEntity user)
        {
            var userTemp = _context.Users.Find(user.Id);
            if (userTemp != null)
            {
                _context.Users.Remove(userTemp);
                _context.SaveChanges();
            }
        }
    }
}