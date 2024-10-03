using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hma_api.Domain.Entities
{
  public class Patient
  {
    public int Id { get; set; }
    public int User_id { get; set; }
    public string Date_of_birth { get; set; }
    public int Gender { get; set; }
    public int Marital_status { get; set; }
    public string Adress { get; set; }
    public string Phone_number { get; set; }
  }
}
