using hma_api.Application.Interfaces;
using hma_api.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hma_api.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class PatientController : CrudController<Patient>
  {
    public PatientController(ICrudService<Patient> service) : base(service)
    {

    }
  }
}
