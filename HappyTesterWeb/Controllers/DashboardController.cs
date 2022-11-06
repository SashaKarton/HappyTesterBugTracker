using CloudinaryDotNet.Actions;
using HappyTesterWeb.Interfaces;
using HappyTesterWeb.Models;
using HappyTesterWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HappyTesterWeb.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IPhotoService _photoService;
        private readonly IProjectRepository _projectRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly ITicketRepository _ticketRepository;


        public DashboardController(IDashboardRepository dashboardRepository, IPhotoService photoService, IProjectRepository projectRepository, UserManager<AppUser> userManager, IUserRepository userRepository, ITicketRepository ticketRepository)
        {
            _dashboardRepository = dashboardRepository;
            _photoService = photoService;
            _projectRepository = projectRepository;
            _userManager = userManager;
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
        }

        private void MapUserEdit(AppUser user, EditProfileDashboardViewModel editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.UserName = editVM.UserName;
            user.FirstName = editVM.FirstName;
            user.LastName = editVM.LastName;
            user.Email = editVM.Email;
            user.PhoneNumber = editVM.PhoneNumber;
            user.ProfileImageUrl = photoResult.Url.ToString();
        }

        public async Task<IActionResult> Index()
        {
            var appUserProjects = await _dashboardRepository.GetAppUserProjects();
            var projectsId = appUserProjects.Select(p => p.ProjectsId).ToList();
            var appUserTickets = await _dashboardRepository.GetAppUserTickets();
            var tickets = appUserTickets.Select(t => t.TicketsId).ToList();            
            var userProjects = await _dashboardRepository.GetAllUserProjects(projectsId);
            var result = new List<DashboardViewModel>();
            foreach(var project in userProjects)
            {
                var dashboardVM = new DashboardViewModel()
                {
                    Id = project.Id,
                    Title = project.Title,
                    Description = project.Description,
                    AppUsers = project.AppUsers,
                    TicketCount = project.Tickets == null ? 0 : project.Tickets.Count()
                };
                result.Add(dashboardVM);
            }

            return View(result);
        }



        public async Task<IActionResult> Detail(int projectId)
        {
            var tickets = await _projectRepository.GetTicketsByProjectIdAsync(projectId);
            if (tickets == null) return View("Error");

            var detailDashboardViewModel = new DetailDashboardViewModel()
            {
                Id = tickets.Id,
                Title = tickets.Title,
                Description = tickets.Description,
                Tickets = tickets.Tickets,
                AppUsers = tickets.AppUsers

            };
            return View(detailDashboardViewModel);

        }



        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _dashboardRepository.GetUserAsync();

            if (user == null) return View("Error");

            ViewBag.UserPhoto = user.ProfileImageUrl;
            ViewBag.Username = user.UserName;
            var profileDashboardViewModel = new ProfileDashboardViewModel()
            {
                Id = user.Id,
                ProfileImageUrl = user.ProfileImageUrl,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

            };

            return View(profileDashboardViewModel);

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _dashboardRepository.GetUserAsync();

            if (user == null) return View("Error");


            var editVM = new EditProfileDashboardViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ProfileImageUrl = user.ProfileImageUrl
            };
            return View(editVM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileDashboardViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit photo");
                return View("Edit", editVM);
            }

            var user = await _dashboardRepository.GetUserNoTracking();

            if (user.ProfileImageUrl == null || user.ProfileImageUrl == "")
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _userRepository.Update(user);

                return RedirectToAction("Detail", "User", new { id = user.Id });
            }

            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _dashboardRepository.UpdateUser(user);

                return RedirectToAction("Profile", "Dashboard", new { id = user.Id });
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project == null) return View("Error");

            _projectRepository.Delete(project);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(id);

            if (ticket == null) return View("Error");

            _ticketRepository.Delete(ticket);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }
    }
}
