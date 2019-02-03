using System.Collections.Generic;
using StockManagement.Api.Contract.Entities;

namespace StockManagement.Api.Contract.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        UserEntity GetUser(string userName);
        List<UserEntity> GetAll();
        UserEntity FindUser(int id);
        bool CheckIfUserNameIsInUse(string username);
        void AddUser(UserEntity userEntity);
        void UpdateUser(UserEntity user);
        void Remove(UserEntity user);
    }
}