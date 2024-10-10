using hma_api.Application.Interfaces;
using hma_api.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace hma_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UserController : CrudController<User>
  {
    public UserController(ICrudService<User> service) : base(service)
    {
    }
  }
}
