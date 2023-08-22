using System;

namespace PMS.Entity
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
    }
}
