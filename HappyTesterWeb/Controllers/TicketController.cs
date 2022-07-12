using HappyTesterWeb.Data;
using HappyTesterWeb.Interfaces;
using HappyTesterWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HappyTesterWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;


namespace HappyTesterWeb.Controllers
{
    public class TicketController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITicketRepository _ticketRepository;

        public TicketController(IProjectRepository projectRepository, ITicketRepository ticketRepository)
        {
            _projectRepository = projectRepository;
            _ticketRepository = ticketRepository;
        }

        [HttpGet]
        [Authorize]
        [Route("ticket")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Ticket> tickets = await _ticketRepository.GetAll();
            return View(tickets);
        }


        [HttpGet]
        [Authorize]
        [Route("ticket/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            Ticket ticket = await _ticketRepository.GetTicketByIdAsync(id);

            if (ticket == null)
            {
                return View("Error");
            }

            return View(ticket);
        }

        [HttpGet]
        [Authorize]
        [Route("ticket/create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.ProjectsList = await _projectRepository.GetAllProjectsSelectList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [Route("ticket/create")]
        public async Task<IActionResult> Create(CreateTicketViewModel ticketVM)
        {
            if (ModelState.IsValid)
            {

                var ticket = new Ticket()
                {
                    Title = ticketVM.Title,
                    Description = ticketVM.Description,
                    IssuePriority = ticketVM.IssuePriority,
                    IssueType = ticketVM.IssueType,
                    ProjectId = ticketVM.ProjectId
                };

                _ticketRepository.Add(ticket);
                return RedirectToAction("Detail", new { id = ticket.Id });
            }

            return View(ticketVM);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("ticket/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(id);
            
            if (ticket == null) 
                {
                return View("Error");
                }

            var ticketVM = new EditTicketViewModel
            {
                Id = id,
                Title = ticket.Title,
                Description = ticket.Description,
                ProjectId = ticket.ProjectId,
                IssuePriority = ticket.IssuePriority,
                TicketStatus = ticket.TicketStatus,
                IssueType = ticket.IssueType
            };
            return View(ticketVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        [Route("ticket/edit/{id}")]
        public async Task<IActionResult> Edit(int id, EditTicketViewModel ticketVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit a ticket");
                return View("Edit", ticketVM);
            }

            var userTicket = await _ticketRepository.GetTicketByIdAsNoTracking(id);
            if (userTicket == null)
            {
                return View("Error");
            }

            var ticket = new Ticket
            {
                Id = id,
                Title = ticketVM.Title,
                Description = ticketVM.Description,
                ProjectId = ticketVM.ProjectId,
                IssuePriority = ticketVM.IssuePriority,
                TicketStatus = ticketVM.TicketStatus,
                IssueType = ticketVM.IssueType

            };
            _ticketRepository.Update(ticket);
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //[Authorize(Roles = "admin")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var ticket = await _ticketRepository.GetTicketByIdAsync(id);
        //    if (ticket == null) return View("error");
        //    return View(ticket);
        //}

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _ticketRepository.GetTicketByIdAsync(id);
            if (ticket == null) return View("Error");

            _ticketRepository.Delete(ticket);
            return RedirectToAction("Index");
        }


    }
}
