using CloudinaryDotNet.Actions;
using HappyTesterWeb.Interfaces;
using HappyTesterWeb.Models;
using HappyTesterWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HappyTesterWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IPhotoService _photoService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserProjectRepository _userProjectRepository;
        
        public UserController(IUserRepository userRepository, IPhotoService photoService, UserManager<AppUser> userManager, IUserProjectRepository userProjectRepository)
        {
            _userRepository = userRepository;
            _photoService = photoService;
            _userManager = userManager;
            _userProjectRepository = userProjectRepository;
        }

        private void MapUserEdit(AppUser user, EditUserViewModel editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.UserName = editVM.UserName;
            user.FirstName = editVM.FirstName;
            user.LastName = editVM.LastName;
            user.Email = editVM.Email;
            user.PhoneNumber = editVM.PhoneNumber;
            user.ProfileImageUrl = photoResult.Url.ToString();
        }

        [HttpGet("users")]
        [Authorize]
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
                    ProfileImageUrl = user.ProfileImageUrl,
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
            
            var userProject = await _userProjectRepository.GetUserProjectByUserIdAsync(id);
            ViewBag.ProjectId = userProject.ProjectsId;

            var userDetailViewModel = new DetailUserViewModel()
            {
                Id = user.Id,
                ProfileImageUrl = user.ProfileImageUrl,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Projects = user.Projects,

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
                ProfileImageUrl = user.ProfileImageUrl
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
                ModelState.AddModelError("", "Failed to edit photo");
                return View("Edit", editVM);
            }

            var user = await _userRepository.GetUserByIdAsNoTracking(editVM.Id);

            if(user.ProfileImageUrl == null || user.ProfileImageUrl == "")
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
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _userRepository.Update(user);

                return RedirectToAction("Detail", "User", new { id = user.Id });
            }          

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
