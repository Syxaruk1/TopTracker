namespace Task_tracker.Infrastructure.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_tracker.Domain.Models;
public class AccountRepository
{
    private static IDictionary<string, User> Users = new Dictionary<string, User>();

    public void Add(User user)
    {
        Users[user.Email] = user;
    }

    public User? GetByEmail(string email)
    {
        try
        {
            return Users[email];
        }
        catch
        {
            return null;
        }
    }

}