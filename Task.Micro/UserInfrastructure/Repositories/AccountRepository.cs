namespace Task_tracker.Infrastructure.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_tracker.Domain.Models;

public class AccountRepository
{
    private AccountContext Context { get; set; }
    public AccountRepository(AccountContext accountContext)
    {
        Context = accountContext;
    }

    public void Add(User user)
    {
        Context.Users.Add(user);
        Context.SaveChanges();
    }

    public void Delete(User user)
    {
        Context.Users.Remove(user);
        Context.SaveChanges();
    }

    public User? GetById(int id)
    {
        try
        {
            return Context.Users.Find(id);
        }
        catch
        {
            return null;
        }
    }
    public User? GetByEmail(string email)
    {
        try
        {
            return Context.Users.FirstOrDefault(x => x.Email == email);
        }
        catch
        {
            return null;
        }
    }
}