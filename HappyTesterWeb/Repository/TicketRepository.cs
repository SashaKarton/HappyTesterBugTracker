using HappyTesterWeb.Data;
using HappyTesterWeb.Interfaces;
using HappyTesterWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace HappyTesterWeb.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Ticket ticket)
        {
            _context.Add(ticket);
            return Save();
        }

        public bool Delete(Ticket ticket)
        {
            _context.Remove(ticket);
            return Save();
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _context.Tickets.Include(p => p.Project).ToListAsync();
        }

        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            return await _context.Tickets.Include(p => p.Project).Include(u => u.AppUsers).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Ticket> GetTicketByIdAsNoTracking(int id)
        {
            return await _context.Tickets.Include(p => p.Project).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }


        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Ticket ticket)
        {
            _context.Update(ticket);
            return Save();
        }
    }
}
