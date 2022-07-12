using HappyTesterWeb.Data;
using HappyTesterWeb.Interfaces;
using HappyTesterWeb.Models;
using Microsoft.AspNetCore.Mvc;
using HappyTesterWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace HappyTesterWeb.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITicketRepository _ticketRepository;

        public ProjectController(IProjectRepository projectRepository, ITicketRepository ticketRepository)
        {
            _projectRepository = projectRepository;
            _ticketRepository = ticketRepository;
        }

        [HttpGet]
        [Authorize]
        [Route("project")]        
        public async Task<IActionResult> Index()
        {
            IEnumerable<Project> projects = await _projectRepository.GetAll();
            return View(projects);
        }

        [HttpGet]
        [Authorize]        
        [Route("project/{projectId}")]        
        public async Task<IActionResult> Detail(int projectId)
        {
            Project project = await _projectRepository.GetTicketsByProjectIdAsync(projectId);
            return View(project); 

        }

        [HttpGet]
        [Authorize]
        [Route("project/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Route("project/create")]
        public async Task<IActionResult> Create(CreateProjectViewModel projectVM)
        {
            if(String.IsNullOrEmpty(projectVM.Title) == false)
            {
                var project = new Project()
                {
                    Title = projectVM.Title,
                    Description = projectVM.Description,
               
                };

                (int id, bool success) = _projectRepository.AddAndGetId(project);

                if (success == false) return BadRequest(error: "Adding project failed");

                if(projectVM.TicketTitle is not null)
                {
                    Ticket newTicket = new Ticket()
                    {
                        Title = projectVM.TicketTitle,
                        Description = projectVM.TicketDescription,
                        ProjectId = id,
                        IssuePriority = projectVM.IssuePriority,
                        IssueType = projectVM.IssueType
                    };
                    _ticketRepository.Add(newTicket);
                }

                return RedirectToAction("Detail", new { projectId = id });
            }
            return View(projectVM);             
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("project/edit/{projectId}")]
        public async Task<IActionResult> Edit(int projectId)
        {
            var project = await _projectRepository.GetProjectByIdAsync(projectId);
            if (project == null) return View("Error");
            var projectVM = new EditProjectViewModel
            {
                Title = project.Title,
                Description = project.Description                
            };
            return View(projectVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [Route("project/edit/{projectId}")]
        public async Task<IActionResult> Edit(int projectId, EditProjectViewModel projectVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit a project");
                return View("Edit", projectVM);
            }

            var userProject = await _projectRepository.GetProjectByIdAsNoTracking(projectId);
            if (userProject == null)
            {
                return View("Error");
            }
            
            var project = new Project
            {
                Id = projectId,
                Title = projectVM.Title,
                Description = projectVM.Description                
            };
            _projectRepository.Update(project);
            return RedirectToAction("Index");
        }


        //[HttpGet]
        //[Authorize]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var project = await _projectRepository.GetProjectByIdAsync(id);
        //    if (project == null) return View("error");
        //    return View(project);
        //}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project == null) return View("Error");

            _projectRepository.Delete(project);
            return RedirectToAction("Index");
        }



    }
}
