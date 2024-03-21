using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuangPhuWebRazor.Data;
using QuangPhuWebRazor.Models;

namespace QuangPhuWebRazor.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category? category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                category = _db.Categories.FirstOrDefault(c => c.Id == id);
            }
        }
        public IActionResult OnPost()
        {
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["success"] = $"Delete \"{category.Name}\" Successfully";
            return RedirectToPage("Index");
        }
       
    }
}
