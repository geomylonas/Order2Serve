using Application.Dto;
using Common.ServiceResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        public Task<ServiceCallResult> Register(CreateUserDto dto);
        public  Task<LoginResult> Login(LoginDto dto);

    }
}
