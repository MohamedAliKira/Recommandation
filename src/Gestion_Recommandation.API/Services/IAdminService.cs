using Gestion_Recommandation.API.Models;
using Gestion_Recommandation.Shared.Response;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using Gestion_Recommandation.Shared.Models;

namespace Gestion_Recommandation.API.Services
{
    public interface IAdminService
    {
        // ------------------------- USERS
        Task<ApiResponse<IEnumerable<UserIdentity>>> GetAllUsersAsync();
        Task<ApiResponse<UserIdentity>> EditUserAsync(UserIdentity user);
        Task<ApiResponse<UserIdentity>> DeleteUserAsync(string userId);

        // ------------------------- ROLES
        Task<ApiResponse<IEnumerable<IdentityRole>>> GetAllRolesAsync();
        Task<ApiResponse<IdentityRole>> CreateRoleAsync(string roleName);
        Task<ApiResponse<IdentityRole>> EditRoleAsync(IdentityRole role);
        Task<ApiResponse<IdentityRole>> DeleteRoleAsync(string roleId);

        //-------------------------- MANAGE_USER_ROLE_CLAIMS
        Task<ApiResponse> ManageUserRolesAsync(List<IdentityRole> listOfRoles, string userId);
        Task<ApiResponse> ManageUserClaimsAsync(List<UsersClaims> Claims, string userId);

        //-------------------------- SERVICES
        Task<ApiResponse<IEnumerable<Service>>> GetAllServicesAsync();
        Task<ApiResponse<Service>> CreateServiceAsync(string serviceName, int tutelle);
        Task<ApiResponse<Service>> EditServiceAsync(Service service);
    }

    public class AdminService : IAdminService
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppIdentityDbContext _context;

        public AdminService(UserManager<UserIdentity> userManager, RoleManager<IdentityRole> roleManager, AppIdentityDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }


        // ------------------------- USERS
        public async Task<ApiResponse<IEnumerable<UserIdentity>>> GetAllUsersAsync()
        {
            return new ApiResponse<IEnumerable<UserIdentity>>
            {
                IsSuccess = true,
                Message = "List of Users",
                Value = await _userManager.Users.ToArrayAsync()
            };
        }
        public async Task<ApiResponse<UserIdentity>> EditUserAsync(UserIdentity userEdited)
        {
            var user = await _userManager.FindByIdAsync(userEdited.Id);
            if (user == null)
            {
                return new ApiResponse<UserIdentity>
                {
                    IsSuccess = false,
                    Message = "Pas d'utilisateur avec cette adresse",
                    Value = userEdited
                };
            }
            else
            {
                user.Email = userEdited.Email;
                user.Bureau = userEdited.Bureau;
                user.Identite = userEdited.Identite;
                user.Matricule = userEdited.Matricule;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return new ApiResponse<UserIdentity>
                    {
                        IsSuccess = true,
                        Message = "User updated!",
                        Value = userEdited
                    };
                }
                return new ApiResponse<UserIdentity>
                {
                    IsSuccess = false,
                    Message = result.Errors.Select(e => e.Description).ToString(),
                    Value = userEdited
                };
            }
        }
        public async Task<ApiResponse<UserIdentity>> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ApiResponse<UserIdentity>
                {
                    IsSuccess = false,
                    Message = "Pas d'utilisateur avec cette adresse",
                    Value = user
                };
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return new ApiResponse<UserIdentity>
                    {
                        IsSuccess = true,
                        Message = "User Deleted !",
                        Value = user
                    };
                }
                return new ApiResponse<UserIdentity>
                {
                    IsSuccess = false,
                    Message = result.Errors.Select(e => e.Description).ToString(),
                    Value = user
                };
            }
        }


        // ------------------------- ROLES

        public async Task<ApiResponse<IEnumerable<IdentityRole>>> GetAllRolesAsync()
        {
            return new ApiResponse<IEnumerable<IdentityRole>>
            {
                IsSuccess = true,
                Message = "List of Roles",
                Value = await _roleManager.Roles.ToArrayAsync()
            };
        }
        public async Task<ApiResponse<IdentityRole>> CreateRoleAsync(string role)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = role
            };
            IdentityResult result = await _roleManager.CreateAsync(identityRole);

            if (result.Succeeded)
            {
                return new ApiResponse<IdentityRole>
                {
                    IsSuccess = true,
                    Message = "Role created !",
                    Value = identityRole
                };
            }
            else
            {
                return new ApiResponse<IdentityRole>
                {
                    IsSuccess = false,
                    Message = result.Errors.Select(e => e.Description).ToString(),
                    Value = identityRole
                };
            }
        }
        public async Task<ApiResponse<IdentityRole>> EditRoleAsync(IdentityRole roleEdited)
        {
            var role = await _roleManager.FindByIdAsync(roleEdited.Id);
            if (role == null)
            {
                return new ApiResponse<IdentityRole>
                {
                    IsSuccess = false,
                    Message = $"Pas de role avec cette Id = {roleEdited.Id}",
                    Value = role
                };
            }
            else
            {
                role.Name = roleEdited.Name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return new ApiResponse<IdentityRole>
                    {
                        IsSuccess = true,
                        Message = "Role Updated !",
                        Value = role
                    };
                }
                return new ApiResponse<IdentityRole>
                {
                    IsSuccess = false,
                    Message = result.Errors.Select(e => e.Description).ToString(),
                    Value = role
                };
            }
        }
        public async Task<ApiResponse<IdentityRole>> DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return new ApiResponse<IdentityRole>
                {
                    IsSuccess = false,
                    Message = $"Pas de role avec cette Id = {roleId}",
                    Value = role
                };
            }
            else
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return new ApiResponse<IdentityRole>
                    {
                        IsSuccess = true,
                        Message = "Role Deleted !",
                        Value = role
                    };
                }
                return new ApiResponse<IdentityRole>
                {
                    IsSuccess = false,
                    Message = result.Errors.Select(e => e.Description).ToString(),
                    Value = role
                };
            }
        }


        //--------------------------- MANAGE_USER_ROLE_CLAIMS
        public async Task<ApiResponse> ManageUserRolesAsync(List<IdentityRole> listOfRoles, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Pas d'utilisateur avec cette adresse"
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {

            }

            result = await _userManager.AddToRolesAsync(user, listOfRoles.Select(y => y.Name));

            if (result.Succeeded)
            {
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "ManageUserRoles successfully !"
                };
            }
            return new ApiResponse
            {
                IsSuccess = false,
                Message = result.Errors.Select(e => e.Description).ToString()
            };
        }

        public async Task<ApiResponse> ManageUserClaimsAsync(List<UsersClaims> Claims, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = $"Pas d'utilisateur avec ce Id : {userId}"
                };
            }

            var claims = await _userManager.GetClaimsAsync(user);
            var result = await _userManager.RemoveClaimsAsync(user, claims);

            if (!result.Succeeded)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = result.Errors.Select(e => e.Description).ToString()
                };
            }

            result = await _userManager.AddClaimsAsync(user, Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType, c.ClaimType)));

            if (!result.Succeeded)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = result.Errors.Select(e => e.Description).ToString()
                };
            }
            return new ApiResponse
            {
                IsSuccess = true,
                Message = "ManageUserClaims successfully !"
            };

        }


        //-------------------------- SERVICES
        public async Task<ApiResponse<IEnumerable<Service>>> GetAllServicesAsync()
        {
            var _services = await(from p in _context.Service                                 
                                  select p).ToArrayAsync();


            return new ApiResponse<IEnumerable<Service>>
            {
                
            IsSuccess = true,
                Message = "Liste des services",
                Value = await (from p in _context.Service
                               select p).ToArrayAsync()
            };
        }
        public async Task<ApiResponse<Service>> CreateServiceAsync(string serviceName, int tutelle) 
        {
            if (string.IsNullOrWhiteSpace(serviceName) || tutelle == 0)
                return new ApiResponse<Service>
                {
                    IsSuccess = false,
                    Message = $"Nom du Service est vide / Tutelle {tutelle}!",
                    Value = null
                };

            var serviceModel = new Service
            {
                Libelle = serviceName,
                Tuttelle = tutelle
            };

            await _context.Service.AddAsync(serviceModel);
            await _context.SaveChangesAsync();

            return new ApiResponse<Service>
            {
                IsSuccess = true,
                Message = "création faite avec success !",
                Value = serviceModel
            };
        }
        public async Task<ApiResponse<Service>> EditServiceAsync(Service service) 
        {
            var serv = await _context.Service.FindAsync(service.Id);
            if (serv == null)
                return new ApiResponse<Service>
                {
                    IsSuccess = false,
                    Message = $"Service with the Id: {serv.Id} not found",
                    Value = null
                };

            serv.Libelle = service.Libelle;
            serv.Tuttelle = service.Tuttelle;            
            await _context.SaveChangesAsync();

            return new ApiResponse<Service>
            {
                IsSuccess = true,
                Message = "Modification faite avec success !",
                Value = serv
            };
        }
    }
}
