using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuangPhu.DataAccess.Repository.IRepository;
using QuangPhu.Models;
using QuangPhu.Models.ViewModels;
using System.Security.Claims;

namespace QuangPhuWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private IUnitOfWork _unitofWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitofWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            ShoppingCartVM = new (){
                ShoppingCartList = _unitofWork.ShoppingCart.GetAll(x => x.ApplicationUserId == userId, includeProperties: "Product"),
            };

            foreach( var cart in ShoppingCartVM.ShoppingCartList )
            {
                cart.Price = GetCartPrice(cart);
                ShoppingCartVM.OrderTotal += cart.Price * cart.Count;
            }
            return View(ShoppingCartVM);
        }
        public double GetCartPrice(ShoppingCart shoppingCart) {
            if (shoppingCart.Count <= 50)
                return shoppingCart.Product.Price;
            else if (shoppingCart.Count <= 100)
                return shoppingCart.Product.Price50;
            else
                return shoppingCart.Product.Price100;
        }
        public IActionResult plus(int cartID)
        {
            var cart = _unitofWork.ShoppingCart.Get(x=>x.Id == cartID);
            cart.Count += 1;
            _unitofWork.ShoppingCart.Update(cart);
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult minus(int cartID)
        {
            var cart = _unitofWork.ShoppingCart.Get(x => x.Id == cartID);
            cart.Count -= 1;
            _unitofWork.ShoppingCart.Update(cart);
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult remove(int cartID)
        {
            var cart = _unitofWork.ShoppingCart.Get(x => x.Id == cartID);
            _unitofWork.ShoppingCart.Remove(cart);
            _unitofWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Summary() 
        {
            return View();
        }
    }
}

