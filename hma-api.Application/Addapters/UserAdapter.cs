using hma_api.Application.DTOs;
using hma_api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hma_api.Application.Addapters
{
  public static class UserAdapter
  {
    public static User ToDomain(AuthorizationDTO authorizationDTO)
    {
      return new User
      {
        Email = authorizationDTO.Email,
        Password = authorizationDTO.Password
      };
    }
    public static AuthorizationDTO ToAuthorizationDTO(User user)
    {
      return new AuthorizationDTO
      {
        Email = user.Email,
        Password = user.Password
      };
    }
  }
}
