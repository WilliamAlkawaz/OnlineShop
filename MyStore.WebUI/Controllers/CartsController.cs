using MyStore.Domain.Concrete;
using MyStore.Domain.Entities;
using MyStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.WebUI.Controllers
{
    public class CartsController : Controller
    {
        /*
        private IProductRepository repository;
        public CartController(IProductRepository repo) { repository = repo; } */
        EFDbContext db = new EFDbContext();

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = GetCart(), ReturnUrl = returnUrl });
        }

        public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        {
            Product product = db.Products.FirstOrDefault(p => p.id_product == productId);
            if (product != null) { GetCart().AddItem(product, 1); }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = db.Products.FirstOrDefault(p => p.id_product == productId);
            if (product != null) { GetCart().RemoveLine(product); }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"]; if (cart == null) { cart = new Cart(); Session["Cart"] = cart; }
            return cart;
        }
    }
}