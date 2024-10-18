using Application.Auxiliary;
using Application.Dto;
using Application.Interfaces;
using Common.Interfaces;
using Common.ServiceResults;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private IUserRepository _userRepository;
        private IUserRoleRepository _userRoleRepository;
        private IEncryption _encryption;
        private IConfiguration _configuration;

        public AccountService(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IEncryption encryption, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _encryption = encryption;
            _configuration = configuration;
        }

        public async Task<ServiceCallResult> Register(CreateUserDto dto)
        {
            var user = await _userRepository.GetByUsernameAsync(dto.Username);
            if (user is not null)
            {
                return ServiceCallResult.Fail("The user already exists!");
            }  
                       
            var userRole = await _userRoleRepository.GetByIdAsync(dto.UserRole);
            if (userRole is null)
            {
                return ServiceCallResult.Fail("User Role not found");
            }

            string encrPassword = _encryption.Encrypt(dto.Password);

            User newUser = new( dto.Username, dto.Name, dto.Surname, encrPassword, userRole! );
            try
            {
                await _userRepository.CreateAsync(newUser);
            }catch (Exception ex)
            {
                return ServiceCallResult.Fail(ex.Message);
            }
           
            return ServiceCallResult.Success();
        }

        public async Task<LoginResult> Login(LoginDto dto)
        {
            var encrPassword = _encryption.Encrypt(dto.Password);
            var user = await _userRepository.GetByUsernameAsync(dto.Username);
            if (user is null || user.Password != encrPassword)
            {
                return LoginResult.Fail("Username or Password is wrong!");
            }

            var isCorrectRole = user.Role.Id == dto.UserRole;
            if (!isCorrectRole)
            {
                return LoginResult.Fail("The selected Role is not available to this user");
            }

            var secretKey = _configuration["JWTSettings:SuperSecretKey"];
            var issuer = _configuration["JWTSettings:Issuer"];
            var audience = _configuration["JWTSettings:Audience"];
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new InvalidOperationException("JWT Secret Key is not configured!");
            }
            if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                throw new InvalidOperationException("Issuer or Audience is not configured!");
            }


            var token = JwtTokenHelper.GenerateToken(dto.Username, secretKey, issuer, audience);

            return LoginResult.Success(token);
        }      
    }
}
