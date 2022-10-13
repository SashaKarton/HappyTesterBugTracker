using HappyTesterWeb.Models;

namespace HappyTesterWeb.Interfaces
{
    public interface IDashboardRepository
    {
        Task<IEnumerable<AppUserProject>> GetAppUserProjects();
        Task<IEnumerable<Project>> GetAllUserProjects(List<int> ids);        
        Task<IEnumerable<AppUserTicket>> GetAppUserTickets();
        Task<IEnumerable<Ticket>> GetAllUserTickets(List<int> ids);
        Task<AppUser> GetUserAsync();
        Task<AppUser> GetUserNoTracking();
        bool UpdateUser(AppUser user);
        bool Save();
        
    }
}
