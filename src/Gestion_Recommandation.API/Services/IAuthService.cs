using Gestion_Recommandation.API.Models;
using Gestion_Recommandation.Shared.Models;
using Gestion_Recommandation.Shared.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Recommandation.API.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResult>> LoginAsync(LoginRequest loginRequest);
        Task<ApiResponse> RegisterAsync(RegisterRequest registerRequest);       
    }

    public class AuthService : IAuthService
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly IConfiguration _config;
        public AuthService(UserManager<UserIdentity> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }
               
        public async Task<ApiResponse<LoginResult>> LoginAsync(LoginRequest loginRequest)
        {
            if (loginRequest == null)
                throw new NullReferenceException("loginRequest model is not valid");

            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                new ApiResponse<LoginResult>
                {
                    IsSuccess = false,
                    Message = "Pas d'utilisateur avec cette adresse",
                    Value = null
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!result)
                return new ApiResponse<LoginResult>
                {
                    IsSuccess = false,
                    Message = "Mot de passe invalide !!",
                    Value = null
                };

            var _claims = new[]
            {
                new Claim("Email", user.Email),
                new Claim("Bureau", user.Bureau),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                //new Claim("Role", loginRequest.Email),
                new Claim("Identite", user.Identite),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthSettings:Key"]));
            var token = new JwtSecurityToken(
                issuer: _config["AuthSettings:Issuer"],
                audience: _config["AuthSettings:Audience"],
                claims: _claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new ApiResponse<LoginResult>
            {
                IsSuccess = true,
                Message = "Login successfully !",
                Value = new LoginResult
                {
                    Token = tokenAsString,
                    ExpiryDate = token.ValidTo 
                }
            };
        }

        public async Task<ApiResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            if (registerRequest == null)
                throw new NullReferenceException("registerRequest model is not valid");

            if (registerRequest.Password != registerRequest.ConfirmPassword)
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Mot de passe et confirm Mot passe ne sont pas identique !"
                };

            var userIdentity = new UserIdentity
            {
                Bureau = registerRequest.Bureau,
                Email = registerRequest.Email,
                Identite = registerRequest.FirstName,
                Matricule = registerRequest.Matricule,
                UserName = registerRequest.Email,
                Application = "GestionRecommandation"
            };

            var result = await _userManager.CreateAsync(userIdentity, registerRequest.Password);

            if (result.Succeeded)
            {
                return new ApiResponse
                {
                    Message = "User a été créer avec succes !",
                    IsSuccess = true
                };
            }
            return new ApiResponse
            {
                IsSuccess = false,
                Message = $"User n'a pas été créer <erreur : {result.Errors.Select(e => e.Code)} | {result.Errors.Select(e => e.Description)} >"
            };
        }
    }
}
