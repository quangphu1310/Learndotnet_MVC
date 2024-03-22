using Microsoft.AspNetCore.Mvc;
using QuangPhu.DataAccess.Repository.IRepository;
using QuangPhu.Models;
using QuangPhu.Models.ViewModels;
using System.ComponentModel.Design;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace QuangPhuWeb.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var listCompany = _unitOfWork.Company.GetAll();
            return View(listCompany);
        }
        public IActionResult Upsert(int? id)
        {
            Company company = new Company();
            if (id == null || id == 0)
            {
                //create
                return View(company);
            }
            else
            {
                //update
                company = _unitOfWork.Company.Get(x => x.Id == id);
                return View(company);
            }
        }
        [HttpPost, ActionName("Upsert")]
        public IActionResult UpsertPost(Company company)
        {

            if (company.Id != 0)
            {
                _unitOfWork.Company.Update(company);
            }
            else
            {
                _unitOfWork.Company.Add(company);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Company company = _unitOfWork.Company.Get(x => x.Id == id);
            return View(company);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            Company company = _unitOfWork.Company.Get(x => x.Id==id);
            _unitOfWork.Company.Remove(company);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
