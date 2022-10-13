using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HappyTesterWeb.Interfaces;
using HappyTesterWeb.ViewModels;
using HappyTesterWeb.Models;

namespace HappyTesterWeb.Controllers
{
    public class UserProjectController : Controller
    {
        private readonly IUserProjectRepository _userProjectRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        
        public UserProjectController(IUserProjectRepository userProjectRepository, IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _userProjectRepository = userProjectRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }


        [HttpGet("projects/{projectId}/users")]
        [Authorize]
        public async Task<IActionResult> Index(int projectId)
        {
            ViewBag.ProjectId = projectId;
            var project = await _projectRepository.GetProjectWithUsersByIdAsync(projectId);                   
            
            List<UserProjectViewModel> result = new List<UserProjectViewModel>();
            foreach (var user in project.AppUsers)
            {
                var userProjectVM = new UserProjectViewModel()
                {
                    Id = user.Id,
                    ProjectId = projectId,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ProfileImageUrl = user.ProfileImageUrl,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,                    
                };
                result.Add(userProjectVM);
            }
            return View(result);             
        }

        [HttpGet("{projectId}/adduser")]
        [Authorize]        
        public async Task<IActionResult> EditUserProject(int projectId)
        {
            var usersProjects = await _userProjectRepository.GetUserProjectByProjectIdAsync(projectId);
            if (usersProjects == null) return View("Error");

            ViewBag.UserList = await _userRepository.GetAllUsersSelectList();
            ViewBag.ProjectId = projectId;            

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Route("{projectId}/adduser")]
        public async Task<IActionResult> EditUserProject(EditUserProjectViewModel result, int projectId)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to add a user");
                return View("Edit", result);
            }      
            var getUsersProjects = await _userProjectRepository.GetUserProjectByProjectNoTracking(result.ProjectsId);
            if (getUsersProjects == null) return View("Error");

           
            var usersProjects = new List<AppUserProject>();
            var userProject = new AppUserProject();
            
            foreach(var userId in result.AppUsersIds)
            {
                userProject.AppUsersId = userId;
                userProject.ProjectsId = projectId;

                usersProjects.Add(userProject);
            }
            _userProjectRepository.AddRange(usersProjects);

            //editVM.UserChoises = await _userRepository.GetAllUsersSelectList();
            //if (editVM.UserIds != null)
            //{
            //    List<SelectListItem> selectedItems = editVM.UserChoises.Where(u => editVM.UserIds.Contains(string.Parse(u.Value))).ToList();
            //}

            //_userProjectRepository.UpdateRange(usersProjects);
            return RedirectToAction("Index", new { projectId = projectId });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int projectId, string id)
        {
            var userProject = await _userProjectRepository.GetUserProjectByBothIds(projectId, id);

            if (userProject == null) return View("Error");

            _userProjectRepository.Delete(userProject);
            return RedirectToAction("Index", new { projectId = projectId });
        }
    }
}
