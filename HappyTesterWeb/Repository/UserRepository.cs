using HappyTesterWeb.Data;
using HappyTesterWeb.Interfaces;
using HappyTesterWeb.Models;
using HappyTesterWeb.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp;

namespace HappyTesterWeb.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        public bool Delete(AppUser user)
        {
            _context.Remove(user);
            return Save();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetAllUsersSelectList()
        {
            return await _context.Users.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.UserName
            }).ToListAsync();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.Where(u => u.Id == id).Include(p => p.Projects).Include(t => t.Tickets).FirstOrDefaultAsync();
        }

        public async Task<AppUser> GetUserByIdAsNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save();
        }

        public async Task<AppUser> GetCurUser()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var user = await _context.Users.Where(x => x.Id == curUser).FirstOrDefaultAsync();
            return user;
        }
    }
}
