using HappyTesterWeb.Data;
using HappyTesterWeb.Interfaces;
using HappyTesterWeb.Models;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp;

namespace HappyTesterWeb.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<IEnumerable<AppUserProject>> GetAppUserProjects()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userProjects = _context.AppUserProjects.Where(u => u.AppUsersId == curUser);            
            return userProjects.ToList();
        }

        public async Task<IEnumerable<Project>> GetAllUserProjects(List<int> ids)
        {
            return await _context.Projects.Include(p => p.AppUsers).Include(t => t.Tickets).Where(x => ids.Contains(x.Id)).ToListAsync();
        }
        public async Task<IEnumerable<AppUserTicket>> GetAppUserTickets()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId(); ;
            var userTickets = await _context.AppUserTickets.Where(u => u.AppUsersId == curUser.ToString()).ToListAsync();
            return userTickets;
        }

        public async Task<IEnumerable<Ticket>> GetAllUserTickets(List<int> ids)
        {
            return await _context.Tickets.Where(x => ids.Contains(x.Id)).ToListAsync();
        }


        public async Task<AppUser> GetUserAsync()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var appUser = await _context.Users.Where(x => x.Id == curUser).FirstOrDefaultAsync();
            return appUser;
            
        }

        public async Task<AppUser> GetUserNoTracking()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var appUser = await _context.Users.Where(x => x.Id == curUser).AsNoTracking().FirstOrDefaultAsync();
            return appUser;
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(AppUser user)
        {
            _context.Update(user);
            return Save();
        }


    }
}
