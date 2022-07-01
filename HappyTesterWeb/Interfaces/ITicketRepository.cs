using HappyTesterWeb.Models;

namespace HappyTesterWeb.Interfaces
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAll();     
        Task<Ticket> GetTicketByIdAsync(int id);
        Task<Ticket> GetTicketByIdAsNoTracking(int id);
        bool Add(Ticket ticket);
        bool Update(Ticket ticket);
        bool Delete(Ticket ticket);
        bool Save();
    }
}
