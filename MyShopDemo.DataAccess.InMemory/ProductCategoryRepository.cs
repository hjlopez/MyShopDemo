using MyShopDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShopDemo.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productsCategories;

        public ProductCategoryRepository()
        {
            productsCategories = cache["productsCategories"] as List<ProductCategory>;
            if (productsCategories == null)
            {
                productsCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productsCategories"] = productsCategories;
        }

        public void Insert(ProductCategory productIn)
        {
            productsCategories.Add(productIn);
        }

        public void Update(ProductCategory productUpd)
        {
            ProductCategory productToUpdate = productsCategories.Find(a => a.id == productUpd.id);

            if (productToUpdate != null)
            {
                productToUpdate = productUpd;
            }
            else
            {
                throw new Exception("Product Category not found!");
            }
        }

        public ProductCategory Find(string id)
        {
            ProductCategory product = productsCategories.Find(a => a.id == id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product Category not found!");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productsCategories.AsQueryable();
        }

        public void Delete(string id)
        {
            ProductCategory productToDelete = productsCategories.Find(a => a.id == id);

            if (productToDelete != null)
            {
                productsCategories.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product Category not found!");
            }
        }
    }
}
