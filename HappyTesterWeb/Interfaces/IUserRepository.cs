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
        Task<AppUser> GetUserByIdAsNoTracking(string id);
        //void PopulateUserChoises(EditUserProjectViewModel editVM);
        //Task<AppUser> GetUserFromList();
        //bool Add(AppUser user);
        bool Update(AppUser user);
        bool Delete(AppUser user);
        bool Save();
    }
}
