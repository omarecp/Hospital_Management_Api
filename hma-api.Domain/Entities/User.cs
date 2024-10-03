using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hma_api.Domain.Entities
{
  public class User
  {
    public int? Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    [NotMapped]
    public DateTime? Created_at { get; set; }
    [NotMapped]
    public DateTime? Updated_at { get; set; }
  }
}
