using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.Entities
{
    class Product
    {
        private int productId;
        private string productName;
        private double productPrice;

        public Product()
        {}
        public Product(int productId, string productName, double productPrice)
        {
            this.ProductId  = productId;
            this.ProductName = productName;
            this.ProductPrice = productPrice;
        }
        public double ProductPrice { get => productPrice; set => productPrice = value; }
        public int ProductId { get => productId;  set => productId = value;  }
        public string ProductName {  get => productName; set => productName = value; }
    }
}

