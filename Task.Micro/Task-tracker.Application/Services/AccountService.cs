namespace Task_tracker.Application.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_tracker.Domain.Models;
using Task_tracker.Infrastructure.Repositories;

public class AccountService(AccountRepository accountRepository, JwtService jwtService)
{
    public void Register(string userName, string email, string password)
    {
        var user = new User()
        { 
            UserName = userName,
            Email = email,
            Password = password
        };
        accountRepository.Add(user);
    }

    public string Login(string email, string password)
    {
        var user = accountRepository.GetByEmail(email);
        if (user.Password == password)
        {
            return jwtService.GenerateToken(user);
        }
        else
        {
            throw new Exception("Ошибка аутентификации");
        }
    }
}
