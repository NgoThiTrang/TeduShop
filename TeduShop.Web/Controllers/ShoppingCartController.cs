using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TeduShop.Common;
using TeduShop.Model.Model;
using TeduShop.Service;
using TeduShop.Web.Models;
using System.Linq;
using TeduShop.Web.App_Start;
using TeduShop.Web.InFrastructure.Extension;

namespace TeduShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        IProductService _productService;
        IOrderService _orderService;
        private ApplicationUserManager _userManager;
        public ShoppingCartController(IProductService productService, IOrderService orderService, ApplicationUserManager userManager)
        {
            this._productService = productService;
            this._orderService = orderService;
            this._userManager = userManager;
        }
        public JsonResult GetAll()
        {
            if (Session[CommonConstants.SessionCart] == null)

                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];


            return Json(new
            {
                data = cart,
                status = true //  đã load thành công 
            }, JsonRequestBehavior.AllowGet); // trả về danh sách dạng item 
        }


        // GET: ShoppingCart
        public ActionResult Index()
        {
            if (Session[CommonConstants.SessionCart] == null)

                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];

            return View();
        }
        [HttpPost]
        public JsonResult Add(int productId)
        {
            var product = _productService.GetById(productId);
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if (cart == null)
            {
                cart = new List<ShoppingCartViewModel>();
            }
            if (product.Quantity == 0)
            {
                return Json(new
                {
                    status = false,
                    message = "sarn phẩm hết hàng"
                });
            }
            if (cart.Any(x => x.ID == productId))
            {
                foreach (var item in cart)
                {
                    if (item.ID == productId)
                    {
                        item.Quantity += 1;
                    }
                }
            }
            else
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel();
                newItem.ID = productId;

                newItem.product = Mapper.Map<Product, ProductViewModel>(product);
                newItem.Quantity = 1;
                cart.Add(newItem);
            }
            Session[CommonConstants.SessionCart] = cart;
            return Json(new
            {
                status = true
            });
        }
        [HttpPost]
        public JsonResult Update(string cartData)
        {
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(cartData);
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            foreach (var item in cartSession)
            {
                foreach (var jitem in cartViewModel)
                {
                    item.Quantity = jitem.Quantity;

                }
            }
            Session[CommonConstants.SessionCart] = cartSession;

            return Json(new
            {
                status = true
            });

        }
        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            return Json(new
            {
                status = true
            });

        }
        [HttpPost]
        public JsonResult DeleteItem(int productId)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if (cartSession != null)
            {
                cartSession.RemoveAll(x => x.ID == productId);
                Session[CommonConstants.SessionCart] = cartSession;
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }
        public ActionResult Checkout()
        {
            if (Session[CommonConstants.SessionCart] == null)
            {
                Redirect("/gio-hang.html");
            }
            return View();

        }
        public JsonResult GetUser()
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = _userManager.FindById(userId);
                return Json(new
                {
                    data = user,
                    status = true
                });

            }
            else return Json
                    (new
                    {
                        status = false
                    });
        }
        public JsonResult CreateOrder(string orderViewModel)
        {
            bool isEnough = true;
            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);
            var orderNew = new Order();
            orderNew.UpdateOrder(order);
            if (Request.IsAuthenticated)
            {
                orderNew.CustomerId = User.Identity.GetUserId();
                orderNew.CreatedBy = User.Identity.GetUserName();
            }
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var item in cart)
            {
                var detail = new OrderDetail();
                detail.ProductID = item.ID;
                detail.Quantity = item.Quantity;
                detail.Price = item.product.Price;
                orderDetails.Add(detail);
                isEnough = _productService.ShellProduct(item.ID, item.Quantity);
                break;

            }
            if(isEnough)
            {
                _orderService.Create(orderNew, orderDetails);
                _productService.Save();
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false,
                    message = "không đủ hàng"
                });
            }
           


        }


    }

}