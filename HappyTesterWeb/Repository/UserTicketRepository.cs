using HappyTesterWeb.Data;
using HappyTesterWeb.Interfaces;
using HappyTesterWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace HappyTesterWeb.Repository
{
    public class UserTicketRepository : IUserTicketRepository
    {
        private readonly ApplicationDbContext _context;
        public UserTicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddRange(IEnumerable<AppUserTicket> usersTickets)
        {
            _context.AddRange(usersTickets);
            return Save();
        }

        public bool Delete(AppUserTicket userTicket)
        {
            _context.Remove(userTicket);
            return Save();
        }

        public async Task<AppUserTicket> GetUserTicketByBothIds(int ticketId, string userId)
        {
            return await _context.AppUserTickets.Where(x => (x.AppUsersId == userId) && (x.TicketsId == ticketId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AppUserTicket>> GetUserTicketByTicketIdAsync(int id)
        {
            return await _context.AppUserTickets.Where(t => t.TicketsId == id).ToListAsync();
        }

        public async Task<IEnumerable<AppUserTicket>> GetUserTicketByTicketNoTracking(int id)
        {
            return await _context.AppUserTickets.Where(t => t.TicketsId == id).AsNoTracking().ToListAsync();
        }

        public async Task<AppUserTicket> GetUserTicketByUserIdAsync(string id)
        {
            return await _context.AppUserTickets.Where(u => u.AppUsersId == id).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
