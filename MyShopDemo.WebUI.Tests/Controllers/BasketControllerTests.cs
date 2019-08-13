using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyShopDemo.Core.Contracts;
using MyShopDemo.Core.Models;
using MyShopDemo.Services;
using MyShopDemo.WebUI.Tests.Mocks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShopDemo.WebUI.Controllers;
using System.Web.Mvc;
using MyShopDemo.Core.ViewModels;

namespace MyShopDemo.WebUI.Tests.Controllers
{
    [TestClass]
    public class BasketControllerTests
    {
        //[TestMethod]
        //public void CaddAddBasketItem()
        //{
        //    // setup
        //    IRepository<Basket> baskets = new MockContext<Basket>();
        //    IRepository<Product> products = new MockContext<Product>();

        //    var httpContext = new MockHttpContext();

        //    IBasketService basketService = new BasketService(products, baskets);
        //    var controller = new BasketController(basketService);
        //    controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

        //    // act
        //    //basketService.AddToBasket(httpContext, "4");
        //    controller.AddToBasket("1");

        //    Basket basket = baskets.Collection().FirstOrDefault();

        //    // Assert
        //    Assert.IsNull(basket);
        //    Assert.AreEqual(1, baskets.Collection().Count());
        //    Assert.AreEqual("4", basket.BasketItems.ToList().FirstOrDefault().ProductId);
        //}

        [TestMethod]
        public void CanGetSummaryViewModel()
        {
            IRepository<Basket> baskets = new MockContext<Basket>();
            IRepository<Product> products = new MockContext<Product>();

            products.Insert(new Product() { Id = "1", Price = 20 });
            products.Insert(new Product() { Id = "2", Price = 200 });

            Basket basket = new Basket();
            basket.BasketItems.Add(new BasketItem() { ProductId = "1", Quantity = 2 });
            basket.BasketItems.Add(new BasketItem() { ProductId = "2", Quantity = 1 });
            baskets.Insert(basket);

            IBasketService basketService = new BasketService(products, baskets);

            var controller = new BasketController(basketService);
            var httpContext = new MockHttpContext();

            httpContext.Request.Cookies.Add(new System.Web.HttpCookie("eCommerceBasket") { Value = basket.Id });
            controller.ControllerContext = new System.Web.Mvc.ControllerContext(httpContext, new System.Web.Routing.RouteData(), controller);

            var result = controller.BasketSummary() as PartialViewResult;
            var basketSummary = (BasketSummaryViewModel)result.ViewData.Model;

            Assert.AreEqual(3, basketSummary.BasketCount);
            Assert.AreEqual(240, basketSummary.BasketTotal);
        }
    }
}
