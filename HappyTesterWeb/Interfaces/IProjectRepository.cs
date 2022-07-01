using HappyTesterWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HappyTesterWeb.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAll();
        
        Task<Project> GetTicketsByProjectIdAsync(int projectId);

        Task<IEnumerable<SelectListItem>> GetAllProjectsSelectList();
        Task<Project> GetProjectByIdAsync(int projectId);

        Task<Project> GetProjectByIdAsNoTracking(int projectId);

        (int id, bool success) AddAndGetId(Project project);

        bool Add(Project project);
        bool Update(Project project);
        bool Delete(Project project);
        bool Save();
    }
}
