using HappyTesterWeb.Models;
using HappyTesterWeb.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HappyTesterWeb.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsers();
        Task<IEnumerable<SelectListItem>> GetAllUsersSelectList();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetCurUser();
        Task<AppUser> GetUserByIdAsNoTracking(string id);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();
    }
}
