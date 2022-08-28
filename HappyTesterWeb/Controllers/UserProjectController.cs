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

        private void MapEditUserProject(EditUserProjectViewModel result, AppUserProject userProject, int projectId)
        {
            foreach (var userId in result.AppUsersIds)
            {
                userProject.AppUsersId = userId;
            }
            userProject.ProjectsId = projectId;
            
        }


        [HttpGet("projects/{projectId}/users")]
        [Authorize]
        public async Task<IActionResult> Index(int projectId)
        {
            var project = await _projectRepository.GetUsersByProjectIdAsync(projectId);
            ViewBag.ProjectId = projectId;

            List<UserProjectViewModel> result = new List<UserProjectViewModel>();
            foreach (var user in project.AppUsers)
            {
                var userProjectVM = new UserProjectViewModel()
                {
                    Id = user.Id,
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

        [HttpGet("projects/{projectId}/adduser")]
        [Authorize(Roles = "admin")]        
        public async Task<IActionResult> EditUserProject(int projectId)
        {
            var usersProjects = await _userProjectRepository.GetUserProjectByProjectIdAsync(projectId);
            if (usersProjects == null) return View("Error");

            ViewBag.UserList = await _userRepository.GetAllUsersSelectList();
            ViewBag.ProjectId = projectId;

            //var result = new List<EditUserProjectViewModel>();
            //foreach (var userProject in usersProjects)
            //{
            //    var editVM = new EditUserProjectViewModel()
            //    {
            //        ProjectsId = userProject.ProjectsId,
            //        AppUsersId = userProject.AppUsersId,
            //    };
            //    result.Add(editVM);
            //}

            return View(/*result*/);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [Route("projects/{projectId}/adduser")]
        public async Task<IActionResult> EditUserProject(/*List<EditUserProjectViewModel>*/EditUserProjectViewModel result, int projectId)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to add a user");
                return View("Edit", result);
            }      
            var getUsersProjects = await _userProjectRepository.GetUserProjectByProjectNoTracking(result.ProjectsId);
            if (getUsersProjects == null) return View("Error");

            /////          1ST APPROACH
            //var usersProjects = new List<AppUserProject>();
            //foreach (var editVM in result)
            //{
            //    var userProject = new AppUserProject
            //    {
            //        ProjectsId = editVM.ProjectsId,
            //        AppUsersId = editVM.AppUsersId,

            //    };

            //    usersProjects.Add(userProject);
            //}

            ////////           2ND APPROACH
            //var usersProjects = result.ConvertAll(res => new AppUserProject
            //{
            //    ProjectsId = res.ProjectsId,
            //    AppUsersId = res.AppUsersId
            //});
            ///////            3RD APPROACH
            var usersProjects = new List<AppUserProject>();
            var userProject = new AppUserProject();
            //foreach (var userProject in usersProjects)
            //{
            //    //MapEditUserProject(result, userProject, projectId);
            //    foreach(var userId in result.AppUsersIds)
            //    {
            //        userProject.AppUsersId = userId;
            //    }
            //    userProject.ProjectsId = projectId;

            //    usersProjects.Add(userProject);
            //}
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
    }
}
