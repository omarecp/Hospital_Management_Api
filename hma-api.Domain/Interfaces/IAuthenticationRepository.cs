using hma_api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hma_api.Domain.Interfaces
{
  public interface IAuthenticationRepository
  {
    Task<User> Login(User user);
    Task<User> Register(User user);
  }
}
