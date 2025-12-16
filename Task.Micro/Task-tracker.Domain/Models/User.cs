using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_tracker.Domain.Models;
public class User
{
    public virtual Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public User(string userName, string email, string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }
}
