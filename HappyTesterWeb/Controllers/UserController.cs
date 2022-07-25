using HappyTesterWeb.Interfaces;
using HappyTesterWeb.Models;
using HappyTesterWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyTesterWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;           
        }
        [HttpGet("users")]
        [Authorize]

        private void MapUserEdit(AppUser user, EditUserViewModel editVM)
        {
            user.Id = editVM.Id;
            user.UserName = editVM.UserName;
            user.FirstName = editVM.FirstName;
            user.LastName = editVM.LastName;
            user.Email = editVM.Email;
            user.PhoneNumber = editVM.PhoneNumber;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();

            if (users == null) return View("Error");

            List<UserViewModel> result = new List<UserViewModel>();
            foreach(var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    //ProfileImageUrl = user.ProfileImageUrl,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null) return View("Error");


            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

            };
            return View(userDetailViewModel);

        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null) return View("Error");
            

            var editVM = new EditUserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

            };
            return View(editVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(EditUserViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editVM);
            }

            var user = await _userRepository.GetUserByIdAsNoTracking(editVM.Id);

            if (user == null) return View("Error");

            MapUserEdit(user, editVM);
            _userRepository.Update(user);
            return RedirectToAction("Detail", new {id = user.Id});
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null) return View("Error");

            _userRepository.Delete(user);
            return RedirectToAction("Index");
        }

    }
}
