using HappyTesterWeb.ViewModels;
using HappyTesterWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using HappyTesterWeb.Data;
using HappyTesterWeb.Interfaces;
using HappyTesterWeb.Data.Enum;

namespace HappyTesterWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDashboardRepository _dashboardRepository;
        

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, IDashboardRepository dashboardRepository)
        {
            _logger = logger;
            _userManager = userManager;            
            _dashboardRepository = dashboardRepository;
            
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Register", "Home");
            }
            else
            {
                var appUserProjects = await _dashboardRepository.GetAppUserProjects();
                var projectsId = appUserProjects.Select(p => p.ProjectsId).ToList();
                var projects = await _dashboardRepository.GetAllUserProjects(projectsId);
                var appUserTickets = await _dashboardRepository.GetAppUserTickets();
                var ticketsId = appUserTickets.Select(t => t.TicketsId).ToList();
                var tickets = await _dashboardRepository.GetAllUserTickets(ticketsId);
                var homeVM = new HomeViewModel()
                {
                    NumberOfProjects = projects.Count(),
                    NumberOfNewTickets = tickets.Select(x => x.TicketStatus is IssueStatusEnum.New).Count(),
                    NumberOfReopenedTickets = tickets.Select(x => x.TicketStatus is IssueStatusEnum.Reopened).Count(),
                    NumberOfResolvedTickets = tickets.Select(x => x.TicketStatus is IssueStatusEnum.Resolved).Count(),                    

                };
                return View(homeVM);

            }           

        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = new HomeRegisterViewModel();
            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(HomeRegisterViewModel registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var userEmail = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (userEmail != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var userName = await _userManager.FindByEmailAsync(registerVM.UserName);
            if (userName != null)
            {
                TempData["Error"] = "This Username is already in use";
                return View(registerVM);
            }

            var newUser = new AppUser()
            {
                Email = registerVM.EmailAddress,
                UserName = registerVM.UserName,
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return RedirectToAction("Index", "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}