using hma_api.Application.Addapters;
using hma_api.Application.DTOs;
using hma_api.Application.Interfaces;
using hma_api.Domain.Entities;
using hma_api.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace hma_api.Application.Services
{
  public class AuthenticationService : IAuthenticationService
  {
    private readonly IConfiguration _environmentSettings;
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly PasswordService _passwordService;
    public AuthenticationService(IConfiguration environmentSettings, IAuthenticationRepository authenticationRepository, PasswordService passwordService)
    {
      _environmentSettings = environmentSettings;
      _authenticationRepository = authenticationRepository;
      _passwordService = passwordService;
    }

    public async Task<string> Login(AuthorizationDTO user)
    {
      var adaptedUser = UserAdapter.ToDomain(user);
      var validUser = await _authenticationRepository.Login(adaptedUser);
      if (!(validUser.Id > 0))
        return String.Empty;

      var isPasswordValid = _passwordService.VerifyPassword(validUser, validUser.Password, adaptedUser.Password);
      return isPasswordValid ? GenerateJWT(validUser) : String.Empty;
    }

    public async Task<string> Register(AuthorizationDTO user)
    {
      var adaptedUser = UserAdapter.ToDomain(user);
      adaptedUser.Password = _passwordService.HashPassword(adaptedUser, adaptedUser.Password);
      var validUser = await _authenticationRepository.Register(adaptedUser);
      if (validUser == null)
        return String.Empty;

      return GenerateJWT(validUser);
    }

    #region Helpers
    private string GenerateJWT(User user)
    {
      //Generate JWT
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_environmentSettings["Jwt:Key"]);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[]
        {
          new Claim(ClaimTypes.Name, user.Email)
        }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
        SecurityAlgorithms.HmacSha256Signature
        ),
        Issuer = _environmentSettings["Jwt:Issuer"],
        Audience = _environmentSettings["Jwt:Issuer"]
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
    #endregion
  }
}