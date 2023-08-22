using PMS.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using PMS.DAL;

namespace PMS.BL
{
   public class ProductBL
    {
        ProductDAO prodDao = null;
        public ProductBL()
        {
            prodDao = new ProductDAO();
        }
        public bool AddProduct(Product prodObj)
        {
            //Calling DAO class Function
            return prodDao.AddProduct(prodObj);
        }

        public bool UpdateProduct(int prodId,Product prodObj)
        {
            return prodDao.UpdateProduct(prodId,prodObj);
        }

        public bool DropProduct(int prodID)
        {
            return prodDao.DropProduct(prodID);
        }

        public Product SearchProductByID(int prodID)
        {
            return prodDao.SearchProductByID(prodID);
        }

        public List<Product> SearchProductByName(string prodName)
        {
            return prodDao.SearchProductByName(prodName);
        }

        public List<Product> SearchProductByPrice(int price)
        {
            return prodDao.SearchProductByPrice(price);
        }

        public List<Product> ShowAllProducts()
        {
            return prodDao.ShowAllProducts();
        }

    }
}
