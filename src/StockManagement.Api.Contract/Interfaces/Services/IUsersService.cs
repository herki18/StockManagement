using System.Collections.Generic;
using StockManagement.Api.Contract.Models;

namespace StockManagement.Api.Contract.Interfaces.Services
{
    public interface IUsersService
    {
        UserDTO Authenticate(string username, string password);
        IEnumerable<UserDTO> GetAll();
        UserDTO GetById(int id);
    }
}