using HappyTesterWeb.Interfaces;
using HappyTesterWeb.Models;
using HappyTesterWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyTesterWeb.Controllers
{
    public class UserTicketController : Controller
    {
        private readonly IUserTicketRepository _userTicketRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserProjectRepository _userProjectRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public UserTicketController(IUserTicketRepository userTicketRepository, ITicketRepository ticketRepository, IUserProjectRepository userProjectRepository, IProjectRepository projectRepository, IUserRepository userRepository)
        {
            _userTicketRepository = userTicketRepository;
            _ticketRepository = ticketRepository;
            _userProjectRepository = userProjectRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        [HttpGet("tickets/{id}/users")]
        [Authorize]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.TicketId = id;
            var ticket = await _ticketRepository.GetTicketWithUsersByIdAsync(id);

            List<UserTicketViewModel> result = new List<UserTicketViewModel>();
            foreach (var user in ticket.AppUsers)
            {
                var userTicketVM = new UserTicketViewModel()
                {
                    Id = user.Id,
                    TicketId = id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ProfileImageUrl = user.ProfileImageUrl,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                };
                result.Add(userTicketVM);
            }
            return View(result);
        }

        [HttpGet("tickets/{id}/adduser")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditUserTicket(int id)
        {
            var usersTickets = await _userTicketRepository.GetUserTicketByTicketIdAsync(id);
            if (usersTickets == null) return View("Error");

            ViewBag.UserList = await _userRepository.GetAllUsersSelectList();
            ViewBag.TicketId = id;

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [Route("tickets/{id}/adduser")]
        public async Task<IActionResult> EditUserTicket(EditUserTicketViewModel result, int id)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to add a user");
                return View("Edit", result);
            }
            var getUsersTickets = await _userTicketRepository.GetUserTicketByTicketNoTracking(result.TicketsId);
            if (getUsersTickets == null) return View("Error");


            var usersTickets = new List<AppUserTicket>();
            var userTicket = new AppUserTicket();

            foreach (var userId in result.AppUsersIds)
            {
                userTicket.AppUsersId = userId;
                userTicket.TicketsId = id;

                usersTickets.Add(userTicket);
            }
            _userTicketRepository.AddRange(usersTickets);

            return RedirectToAction("Index", new { id });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int ticketId, string id)
        {
            var userTicket = await _userTicketRepository.GetUserTicketByBothIds(ticketId, id);

            if (userTicket == null) return View("Error");

            _userTicketRepository.Delete(userTicket);
            return RedirectToAction("Index", new { id = ticketId });
        }
    }
}
