using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuangPhuWebRazor.Data;
using QuangPhuWebRazor.Models;

namespace QuangPhuWebRazor.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category? category { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if (id != null && id!= 0)
            {
                category = _db.Categories.FirstOrDefault(c => c.Id == id);
            }
        }
        public IActionResult OnPost()
        
        {
            //category = _db.Categories.Find(id);
            if(ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = $"Update \"{category.Name}\" Successfully";

                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
