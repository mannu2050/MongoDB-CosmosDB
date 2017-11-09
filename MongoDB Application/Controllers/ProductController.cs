using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MongoDB_Application.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public RedirectToRouteResult Index()
        {
            return RedirectToAction("List");
        }

        [ActionName("Edit")]
        public ViewResult Edit(string id)
        {
            ProductInfo pInfo = new ProductInfo();
            return View(pInfo.GetProductByID(Convert.ToInt32(id)));
            
        }
        [HttpPost]
        public RedirectToRouteResult Edit(FormCollection objForm)
        {

            ProductInfo pInfo = new ProductInfo();
            ProductModel product = new ProductModel();
            product.ProductID = int.Parse(objForm["ProductID"]);
            product.ProductName = objForm["ProductName"];
            product.ProductDescription = objForm["ProductDescription"];
            product.ProductPrice = decimal.Parse(objForm["ProductPrice"]);
            pInfo.UpdateProduct(product);
            return RedirectToAction("List");
        }

        public ViewResult List()
        {
            ProductInfo pInfo = new ProductInfo();
            return View(pInfo.GetAllProducts());
        }

        public ViewResult View(string id)
        {
            ProductInfo pInfo = new ProductInfo();
            return View(pInfo.GetProductByID(Convert.ToInt32(id)));

        }
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public RedirectToRouteResult Create(ProductModel objModel)
        {
            ProductInfo pInfo = new ProductInfo();
            pInfo.Add(objModel);
            return RedirectToAction("List");
        }
    }
}