using HappyTesterWeb.Data;
using HappyTesterWeb.Interfaces;
using HappyTesterWeb.Models;
using HappyTesterWeb.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HappyTesterWeb.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //public bool Add(AppUser user)
        //{
        //    _context.Add(user);
        //    return Save();
        //}

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
            return await _context.Users.Where(u => u.Id == id).Include(p => p.Projects).FirstOrDefaultAsync();
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

        //public void PopulateUserChoises(EditUserProjectViewModel editVM)
        //{
        //    editVM.UserChoises = _context.Users.Select(u => new SelectListItem
        //    {
        //        Value = u.Id,
        //        Text = u.UserName
        //    });
        //}
        //public Task<AppUser> GetUserFromList()
        //{
        //    return _context.Users.Where(u => u.Id.Contains(u.Id));
        //}
    }
}
