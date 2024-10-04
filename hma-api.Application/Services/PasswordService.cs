using hma_api.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hma_api.Application.Services
{
  public class PasswordService
  {
    private readonly PasswordHasher<User> _passwordHasher;
    public PasswordService()
    {
      _passwordHasher = new PasswordHasher<User>();
    }

    public string HashPassword(User user, string password)
    {
      return _passwordHasher.HashPassword(user, password);
    }

    public bool VerifyPassword(User user, string password, string providedPassword)
    {
      var result = _passwordHasher.VerifyHashedPassword(user, password, providedPassword);
      return result == PasswordVerificationResult.Success;
    }

  }
}
