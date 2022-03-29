using Gestion_Recommandation.API.Models;
using Gestion_Recommandation.API.Services;
using Gestion_Recommandation.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestion_Recommandation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }


        //-------------------- USERS

        // api/Admin/AllUsers
        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _adminService.GetAllUsersAsync();
            if (result.IsSuccess)
                return Ok(result); //200

            return BadRequest(result); //400 

        }
        // api/Admin/User
        [HttpPut("User")]
        public async Task<IActionResult> EditUserAsync([FromBody] UserIdentity user)
        {
            var result = await _adminService.EditUserAsync(user);
            if (result.IsSuccess)
                return Ok(result); //200

            return BadRequest(result); //400 

        }
        // api/Admin/User?userId={userId}
        [HttpDelete("User")]
        public async Task<IActionResult> DeleteUserAsync(string userId)
        {
            var result = await _adminService.DeleteUserAsync(userId);
            if (result.IsSuccess)
                return Ok(result); //200

            return BadRequest(result); //400 

        }


        //-------------------- ROLES

        // api/Admin/AllRoles
        [HttpGet("AllRoles")]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var result = await _adminService.GetAllRolesAsync();
            if (result.IsSuccess)
                return Ok(result); //200

            return BadRequest(result); //400 

        }
        // api/Admin/Role?roleName={role}
        [HttpPost("Role")]
        public async Task<IActionResult> CreateRoleAsync(string roleName)
        {
            var result = await _adminService.CreateRoleAsync(roleName);
            if (result.IsSuccess)
                return Ok(result); //200

            return BadRequest(result); //400 
        }
        // api/Admin/Role
        [HttpPut("Role")]
        public async Task<IActionResult> EditRoleAsync([FromBody] IdentityRole role)
        {
            var result = await _adminService.EditRoleAsync(role);
            if (result.IsSuccess)
                return Ok(result); //200

            return BadRequest(result); //400 

        }
        // api/Admin/Role?userId={userId}
        [HttpDelete("Role")]
        public async Task<IActionResult> DeleteRoleAsync(string roleId)
        {
            var result = await _adminService.DeleteRoleAsync(roleId);
            if (result.IsSuccess)
                return Ok(result); //200

            return BadRequest(result); //400 

        }



        //--------------------------- MANAGE_USER_ROLE_CLAIMS

        // api/Admin/UserRoles?userId={userId}
        [HttpPost("UserRoles")]
        public async Task<IActionResult> ManageUserRolesAsync([FromBody] List<IdentityRole> listOfRoles, string userId)
        {
            var result = await _adminService.ManageUserRolesAsync(listOfRoles, userId);
            if (result.IsSuccess)
                return Ok(result); //200

            return BadRequest(result); //400 
        }

        // api/Admin/UserClaims?userId={userId}
        [HttpPost("UserClaims")]
        public async Task<IActionResult> ManageUserClaimsAsync([FromBody] List<UsersClaims> Claims, string userId)
        {
            var result = await _adminService.ManageUserClaimsAsync(Claims, userId);
            if (result.IsSuccess)
                return Ok(result); //200

            return BadRequest(result); //400 
        }


        //-------------------------- SERVICES
        // api/Admin/AllServices
        [HttpGet("AllServices")]
        public async Task<IActionResult> GetAllServicesAsync()
        {
            var result = await _adminService.GetAllServicesAsync();
            if (result.IsSuccess)
                return Ok(result); //200

            return BadRequest(result); //400 

        }
        // api/Admin/Service?serviceName={role}&tutelle={tutelle}
        [HttpPost("Service")]
        public async Task<IActionResult> CreateServiceAsync(string serviceName, int tutelle)
        {
            var result = await _adminService.CreateServiceAsync(serviceName, tutelle);
            if (result.IsSuccess)
                return Ok(result); //200

            return BadRequest(result); //400 
        }
        // api/Admin/Service
        [HttpPut("Service")]
        public async Task<IActionResult> EditServiceAsync([FromBody] Service service)
        {
            var result = await _adminService.EditServiceAsync(service);
            if (result.IsSuccess)
                return Ok(result); //200

            return BadRequest(result); //400 
        }

    }
}
