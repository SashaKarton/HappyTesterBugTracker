using HappyTesterWeb.Models;

namespace HappyTesterWeb.Interfaces
{
    public interface IUserTicketRepository
    {
        bool Delete(AppUserTicket userTicket);
        bool Save();
        Task<IEnumerable<AppUserTicket>> GetUserTicketByTicketIdAsync(int id);
        Task<IEnumerable<AppUserTicket>> GetUserTicketByTicketNoTracking(int id);
        Task<AppUserTicket> GetUserTicketByBothIds(int ticketId, string userId);
        bool AddRange(IEnumerable<AppUserTicket> usersTickets);
        Task<AppUserTicket> GetUserTicketByUserIdAsync(string id);
    }
}
