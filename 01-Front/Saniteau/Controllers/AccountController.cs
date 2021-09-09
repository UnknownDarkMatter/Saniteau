using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Saniteau.Auth;
using Saniteau.Infrastructure.DataContext;
using Saniteau.Infrastructure.DataModel.Identity;
using Saniteau.Models;
using Saniteau.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Saniteau.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SaniteauDbContext _dbContext;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        public AccountController(UserManager<ApplicationUser> userManager, SaniteauDbContext dbContext, IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAccount([FromBody] User userModel)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userModel.UserName))
                {
                    return Json(new LoginResponse(true, $"Veuillez renseigner le login"));
                }
                if (string.IsNullOrWhiteSpace(userModel.Prenom))
                {
                    return Json(new LoginResponse(true, $"Veuillez renseigner un prénom"));
                }
                if (string.IsNullOrWhiteSpace(userModel.Nom))
                {
                    return Json(new LoginResponse(true, $"Veuillez renseigner un nom"));
                }
                if (string.IsNullOrWhiteSpace(userModel.Password))
                {
                    return Json(new LoginResponse(true, $"Veuillez renseigner un mot de passe"));
                }
                var user = new ApplicationUser(userModel.UserName, userModel.Prenom, userModel.Nom);
                var result = await _userManager.CreateAsync(user, userModel.Password);
                if (!result.Succeeded)
                {
                    return Json(new LoginResponse(true, $"L'utilisateur {userModel.UserName} n'a pas pu être créé : {string.Join(". ", result.Errors.Select(m=>m.Description))}"));
                }

                var identity = await GetClaimsIdentity(userModel.UserName, userModel.Password);
                string id = identity.Claims.Single(c => c.Type == "id").Value;
                string auth_token = await _jwtFactory.GenerateEncodedToken(userModel.UserName, identity);
                int expires_in_seconds = (int)_jwtOptions.ValidFor.TotalSeconds;

                var response = new Token(id, auth_token, expires_in_seconds);
                return Json(new LoginResponse(false, "", response));
            }
            catch (Exception ex)
            {
                return Json(new LoginResponse(true, ex.Message));
            }
        }


        [HttpPost]
        public async Task<ActionResult> Login([FromBody] User userModel)
        {
            try
            {
                var identity = await GetClaimsIdentity(userModel.UserName, userModel.Password);
                if (identity == null)
                {
                    return Json(new LoginResponse(true, $"Mot de passe non valide"));
                }
                string id = identity.Claims.Single(c => c.Type == "id").Value;
                string auth_token = await _jwtFactory.GenerateEncodedToken(userModel.UserName, identity);
                int expires_in_seconds = (int)_jwtOptions.ValidFor.TotalSeconds;

                var response = new Token(id, auth_token, expires_in_seconds);
                return Json(new LoginResponse(false, "", response));
            }
            catch (Exception ex)
            {
                return Json(new LoginResponse(true, ex.Message));
            }
        }

        [Authorize(Policy = Constants.JWT.ApiAccessPolicyName)]
        [HttpPost]
        public async Task<ActionResult> IsAuthorized([FromBody] User userModel)
        {
            try
            {
                //seules les personnes authorisées peuvent créer un nouveau token
                var user = await _userManager.FindByNameAsync(userModel.UserName);
                if(user == null)
                {
                    return Json(new LoginResponse(true, $"L'utilisateur {userModel.UserName} n'existe pas"));
                }
                var identity = _jwtFactory.GenerateClaimsIdentity(userModel.UserName, user.Id.ToString());

                string id = identity.Claims.Single(c => c.Type == "id").Value;
                string auth_token = await _jwtFactory.GenerateEncodedToken(userModel.UserName, identity);
                int expires_in_seconds = (int)_jwtOptions.ValidFor.TotalSeconds;

                var response = new Token(id, auth_token, expires_in_seconds);
                return Json(new LoginResponse(false, "", response));
            }
            catch (Exception ex)
            {
                return Json(new LoginResponse(true, ex.Message, null));
            }
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                // get the user to verifty
                var userToVerify = await _userManager.FindByNameAsync(userName);

                if (userToVerify != null)
                {
                    // check the credentials  
                    if (await _userManager.CheckPasswordAsync(userToVerify, password))
                    {
                        return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id.ToString()));
                    }
                }
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }


    }
}
