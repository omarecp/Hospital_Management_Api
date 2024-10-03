using hma_api.Application.DTOs;
using hma_api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hma_api.Application.Interfaces
{
  public interface IAuthenticationService
  {
    Task<string> Login(AuthorizationDTO user);
    Task<string> Register(AuthorizationDTO user);
  }
}
