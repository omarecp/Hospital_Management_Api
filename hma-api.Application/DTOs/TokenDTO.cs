using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hma_api.Application.DTOs
{
  public class TokenDTO
  {
    public string Token { get; set; }
    public TokenDTO(string token)
    {
      Token = token;
    }
  }
}
