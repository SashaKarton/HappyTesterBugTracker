using HappyTesterWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HappyTesterWeb.Interfaces
{
    public interface IUserProjectRepository
    {
        //bool UpdateRange(IEnumerable<AppUserProject> usersProjects);
        //bool Update(AppUserProject userProject);
        bool Delete(AppUserProject userProject);
        bool Save();
        Task<IEnumerable<AppUserProject>> GetUserProjectByProjectIdAsync(int id);
        Task<IEnumerable<AppUserProject>> GetUserProjectByProjectNoTracking(int id);
        Task<AppUserProject> GetUserProjectByBothIds(int projectId, string userId);
        bool AddRange(IEnumerable<AppUserProject> usersProjects);
        Task<AppUserProject> GetUserProjectByUserIdAsync(string id);
    }
}
