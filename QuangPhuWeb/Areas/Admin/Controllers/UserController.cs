using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var userRole = _db.UserRoles.ToList();
            var role = _db.Roles.ToList();
            var listUser = _db.ApplicationUsers.Include(x => x.Company).ToList();
            foreach (var user in listUser)
            {
                var userRoleId = userRole.FirstOrDefault(x => x.UserId ==  user.Id).RoleId;
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
            if(user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
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
    }
}
