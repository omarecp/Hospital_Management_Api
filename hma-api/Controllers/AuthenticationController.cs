using hma_api.Application.DTOs;
using hma_api.Application.Interfaces;
using hma_api.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hma_api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthenticationController : ControllerBase
  {
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
      _authenticationService = authenticationService;
    }

    [ActionName("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] AuthorizationDTO user)
    {
      try
      {
        var token = await _authenticationService.Login(user);
        if (String.IsNullOrEmpty(token))
          return NotFound(ApiResponse<string>.CreateResponse(String.Empty, "Invalid credentials"));

        return Ok(ApiResponse<TokenDTO>.CreateResponse(new TokenDTO(token), "Login successful"));
      }
      catch (Exception)
      {
        return StatusCode(500, ApiResponse<string>.CreateError("An unexpected error ocurred", 500));
      }
    }

    [ActionName("Register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] AuthorizationDTO user)
    {
      try
      {
        var token = await _authenticationService.Register(user);
        if (String.IsNullOrEmpty(token))
          return NotFound(ApiResponse<string>.CreateResponse(String.Empty, "Error creating user"));

        return Ok(ApiResponse<TokenDTO>.CreateResponse(new TokenDTO(token), "Register successful"));
      }
      catch (Exception)
      {
        return StatusCode(500, ApiResponse<string>.CreateError("An unexpected error ocurred", 500));
      }
    }
  }
}
