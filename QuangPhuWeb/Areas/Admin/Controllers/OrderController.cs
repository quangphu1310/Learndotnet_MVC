using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuangPhu.DataAccess.Repository.IRepository;
using QuangPhu.Models;
using QuangPhu.Models.ViewModels;
using QuangPhu.Utility;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using System.Security.Claims;
namespace QuangPhuWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
       
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderVM orderVM { get; set; }
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(string status)
        {
            List<OrderHeader> objOrderHeaders;
            if(User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
            {
                objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                objOrderHeaders = _unitOfWork.OrderHeader.GetAll(u => u.ApplicationUserId == userId, includeProperties: "ApplicationUser").ToList();
            }
            switch (status)
            {
                case "pending":
                    objOrderHeaders = objOrderHeaders.Where(x => x.PaymentStatus == SD.PaymentStatusPending).ToList();
                    break;
                case "inprocess":
                    objOrderHeaders = objOrderHeaders.Where(x => x.PaymentStatus == SD.StatusInProcess).ToList();
                    break;
                case "completed":
                    objOrderHeaders = objOrderHeaders.Where(x => x.PaymentStatus == SD.StatusShipped).ToList();
                    break;
                case "approved":
                    objOrderHeaders = objOrderHeaders.Where(x => x.PaymentStatus == SD.StatusApproved).ToList();
                    break;
                default:
                    break;
            }
            return View(objOrderHeaders);
        }
        public IActionResult Details(int? orderId)
        {
            orderVM = new OrderVM()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(x => x.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetail = _unitOfWork.OrderDetail.GetAll(x => x.OrderHeaderId ==  orderId, includeProperties: "Product").ToList()
            };
            return View(orderVM);
        }
        [HttpPost]
        [Authorize(Roles =SD.Role_Admin + "," + SD.Role_Employee)]
        public IActionResult UpdateOrderDetail()
        {
            var OrderHeaderFromDB = _unitOfWork.OrderHeader.Get(x => x.Id == orderVM.OrderHeader.Id);
            OrderHeaderFromDB.Name = orderVM.OrderHeader.Name;
            OrderHeaderFromDB.PhoneNumber = orderVM.OrderHeader.PhoneNumber;
            OrderHeaderFromDB.StreetAddress = orderVM.OrderHeader.StreetAddress;
            OrderHeaderFromDB.City = orderVM.OrderHeader.City;
            OrderHeaderFromDB.State = orderVM.OrderHeader.State;
            OrderHeaderFromDB.PostalCode = orderVM.OrderHeader.PostalCode;
            if (!string.IsNullOrEmpty(orderVM.OrderHeader.Carrier))
            {
                OrderHeaderFromDB.Carrier = orderVM.OrderHeader.Carrier;
            }
            if (!string.IsNullOrEmpty(orderVM.OrderHeader.TrackingNumber))
            {
                OrderHeaderFromDB.TrackingNumber = orderVM.OrderHeader.TrackingNumber;
            }
            _unitOfWork.OrderHeader.Update(OrderHeaderFromDB);
            _unitOfWork.Save();
            TempData["Success"] = "Order Details Update Successfully";
            return RedirectToAction(nameof(Details), new { orderId = OrderHeaderFromDB.Id });
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<OrderHeader> objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            return Json(new {data = objOrderHeaders});
        }
        #endregion
    }
}
