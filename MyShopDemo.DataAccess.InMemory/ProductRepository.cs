using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShopDemo.Core.Models;
using System.Security.AccessControl;

namespace MyShopDemo.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product productIn)
        {
            products.Add(productIn);
        }

        public void Update(Product productUpd)
        {
            Product productToUpdate = products.Find(a => a.id == productUpd.id);

            if (productToUpdate != null)
            {
                productToUpdate = productUpd;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }

        public Product Find(string id)
        {
            Product product = products.Find(a => a.id == id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string id)
        {
            Product productToDelete = products.Find(a => a.id == id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found!");
            }
        }
    }
}
