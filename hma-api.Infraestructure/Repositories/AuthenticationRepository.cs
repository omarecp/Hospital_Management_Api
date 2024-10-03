using hma_api.Domain.Entities;
using hma_api.Domain.Interfaces;
using hma_api.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hma_api.Infraestructure.Repositories
{
  public class AuthenticationRepository : IAuthenticationRepository
  {
    private readonly MyDbContext _context;
    public AuthenticationRepository(MyDbContext context)
    {
      _context = context;
    }
    public async Task<User> Login(User user)
    {
      var res = _context.User.Where(a => a.Email == user.Email).FirstOrDefault();
      return res ?? new User();
    }

    public async Task<User> Register(User user)
    {
      await _context.User.AddAsync(user);
      await _context.SaveChangesAsync();
      return user;
    }
  }
}
