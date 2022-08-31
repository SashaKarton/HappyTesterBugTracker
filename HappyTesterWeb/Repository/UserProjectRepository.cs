using HappyTesterWeb.Data;
using HappyTesterWeb.Interfaces;
using HappyTesterWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HappyTesterWeb.Repository
{
    public class UserProjectRepository : IUserProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public UserProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Delete(AppUserProject userProject)
        {
            _context.Remove(userProject);
            return Save();
        }

        public async Task<IEnumerable<AppUserProject>> GetUserProjectByProjectIdAsync(int projectId)
        {
            return await _context.AppUserProjects.Where(p => p.ProjectsId == projectId).ToListAsync();
        }
        public async Task<IEnumerable<AppUserProject>> GetUserProjectByProjectNoTracking(int projectId)
        {
            return await _context.AppUserProjects.Where(p => p.ProjectsId == projectId).AsNoTracking().ToListAsync();
        }
        public async Task<AppUserProject> GetUserProjectByUserIdAsync(string id)
        {
            return await _context.AppUserProjects.Where(u => u.AppUsersId == id).FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        //public bool UpdateRange(IEnumerable<AppUserProject> usersProjects)
        //{
        //    _context.UpdateRange(usersProjects);
        //    return Save();
        //}
        public bool AddRange(IEnumerable<AppUserProject> usersProjects)
        {
            _context.AddRange(usersProjects);
            return Save();
        }
        //public bool Update(AppUserProject userProject)
        //{
        //    _context.Update(userProject);
        //    return Save();
        //}

        public async Task<AppUserProject> GetUserProjectByBothIds(int projectId, string userId)
        {
            return await _context.AppUserProjects.Where(x => (x.AppUsersId == userId) && (x.ProjectsId == projectId)).FirstOrDefaultAsync();
        }
    }
}
