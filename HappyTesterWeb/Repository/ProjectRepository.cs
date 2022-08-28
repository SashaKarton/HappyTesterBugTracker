using HappyTesterWeb.Data;
using HappyTesterWeb.Interfaces;
using HappyTesterWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HappyTesterWeb.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public bool Add(Project project)
        //{
        //    _context.Add(project);
        //    return Save();
        //}

        public bool Delete(Project project)
        {
            _context.Remove(project);
            return Save();
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _context.Projects.ToListAsync();
        }


        public async Task<Project> GetTicketsByProjectIdAsync(int projectId)
        {
            return await _context.Projects.Include(t => t.Tickets).FirstOrDefaultAsync(i => i.Id == projectId);
        }

        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            return await _context.Projects.FirstOrDefaultAsync(i => i.Id == projectId);
        }
        //public async Task<Project> GetProjectWithUserByIdAsync(int projectId)
        //{
        //    return await _context.Projects.Include(u => u.AppUsers).FirstOrDefaultAsync(i => i.Id == projectId);
        //}
        //public async Task<Project> GetProjectWithUserByIdNoTracking(int projectId)
        //{
        //    return await _context.Projects.Include(u => u.AppUsers).AsNoTracking().FirstOrDefaultAsync(i => i.Id == projectId);
        //}


        public async Task<Project> GetProjectByIdAsNoTracking(int projectId)
        {
            return await _context.Projects.AsNoTracking().FirstOrDefaultAsync(i => i.Id == projectId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Project project)
        {
            _context.Update(project);
            return Save();
        }

        public (int id, bool success) AddAndGetId(Project project)
        {
            _context.Add(project);

            bool success = Save();
            int id = project.Id;
            return (id, success);
        }
        public async Task<IEnumerable<SelectListItem>> GetAllProjectsSelectList()
        {
            return await _context.Projects.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Title
            }).ToListAsync();
        }

        //public bool UpdateUserProject(Project project, IEnumerable<string> model)
        //{
        //    _context.UpdateRange(project, model);
        //    return Save();
        //}

        public async Task<Project> GetUsersByProjectIdAsync(int projectId)
        {
            return await _context.Projects.Include(u => u.AppUsers).FirstOrDefaultAsync(i => i.Id == projectId);
        }
    }

}
