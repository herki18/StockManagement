using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StockManagement.Api.Contract.Helpers;
using StockManagement.Api.Contract.Interfaces.Repositories;
using StockManagement.Api.Contract.Interfaces.Services;
using StockManagement.Api.Contract.Models;

namespace StockManagement.Api.BLL
{
    public class UsersService : IUsersService
    {
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly IUsersRepository _repository;

        public UsersService(IUsersRepository repository, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _repository = repository;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        public UserDTO Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var userEntity = _repository.GetUser(username);

            // check if username exists
            if (userEntity == null)
                return null;

            // check if password is correct
            if (!PasswordHelper.VerifyPasswordHash(password, userEntity.PasswordHash, userEntity.PasswordSalt))
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userEntity.Id.ToString()),
                    new Claim(ClaimTypes.Role, userEntity.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var userDTO = _mapper.Map<UserDTO>(userEntity);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            userDTO.Token = tokenHandler.WriteToken(token);

            // authentication successful
            return userDTO;
        }

        public IEnumerable<UserDTO> GetAll()
        {
            return _mapper.Map<List<UserDTO>>(_repository.GetAll());
        }

        public UserDTO GetById(int id)
        {
            return _mapper.Map<UserDTO>(_repository.FindUser(id));
        }
    }
}