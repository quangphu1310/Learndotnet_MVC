using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuangPhu.DataAccess.Data;
using QuangPhu.DataAccess.Repository.IRepository;
using QuangPhu.Models;
using QuangPhu.Models.ViewModels;
using QuangPhu.Utility;
using System.ComponentModel.Design;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace QuangPhuWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var userRole = _db.UserRoles.ToList();
            var role = _db.Roles.ToList();
            var listUser = _db.ApplicationUsers.Include(x => x.Company).ToList();
            foreach (var user in listUser)
            {
                var userRoleId = userRole.FirstOrDefault(x => x.UserId == user.Id).RoleId;
                user.Role = role.FirstOrDefault(x => x.Id == userRoleId).Name;
            }
            return View(listUser);
        }
        public IActionResult LockUnLock(string? userid)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(x => x.Id == userid);
            if (user == null)
            {
                return NotFound();
            }
            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now;
                TempData["success"] = "Unlock User Successfully";
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(1000);
                TempData["success"] = "Lock User Successfully";
            }
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult RoleManagement(string? userid)
        {
            string roleID = _db.UserRoles.FirstOrDefault(x => x.UserId == userid).RoleId;
            UserVM userVM = new UserVM
            {
                ApplicationUser = _db.ApplicationUsers.Include(x => x.Company).FirstOrDefault(x => x.Id == userid),
                RoleList = _db.Roles.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Name
                }),
                CompanyList = _db.Companys.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
            };
            userVM.ApplicationUser.Role = _db.Roles.FirstOrDefault(x => x.Id == roleID).Name;
            return View(userVM);
        }

        [HttpPost]
        public IActionResult RoleManagement(UserVM roleManagmentVM)
        {

            string RoleID = _db.UserRoles.FirstOrDefault(u => u.UserId == roleManagmentVM.ApplicationUser.Id).RoleId;
            string oldRole = _db.Roles.FirstOrDefault(u => u.Id == RoleID).Name;

            if (!(roleManagmentVM.ApplicationUser.Role == oldRole))
            {
                //a role was updated
                ApplicationUser applicationUser = _db.ApplicationUsers.FirstOrDefault(u => u.Id == roleManagmentVM.ApplicationUser.Id);
                if (roleManagmentVM.ApplicationUser.Role == SD.Role_Company)
                {
                    applicationUser.CompanyId = roleManagmentVM.ApplicationUser.CompanyId;
                }
                if (oldRole == SD.Role_Company)
                {
                    applicationUser.CompanyId = null;
                }
                _db.SaveChanges();

                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, roleManagmentVM.ApplicationUser.Role).GetAwaiter().GetResult();

            }
            TempData["success"] = "Permission successfully";

            return RedirectToAction("Index");
        }
    }
}
