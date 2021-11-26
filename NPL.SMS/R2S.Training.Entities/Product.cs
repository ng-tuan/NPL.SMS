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
        { }
        public Product(int productId, string productName, double productPrice)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.ProductPrice = productPrice;
        }
        public double ProductPrice
        {
            get { return productPrice; }
            set { productPrice = value; }
        }
        public int ProductId
        {
            get { return productId; }
            set
            {
                if (value > 0)
                {
                    productId = value;
                }
                else
                {
                    do
                    {
                        Console.WriteLine("ProductID must > 0, enter again: ");
                        productId = int.Parse(Console.ReadLine());
                    } while (productId <= 0);
                }
            }
        }
        public string ProductName
        { get => productName; set => productName = value; }
        public void AddInfor()
        {
            Console.Write("Input Order ID: ");
            ProductId = int.Parse(Console.ReadLine());

            Console.Write("Input Product Name: ");
            ProductName = Console.ReadLine();

            Console.Write("Input Product Price: ");
            ProductPrice = double.Parse(Console.ReadLine());
        }
        public override string ToString()
        {
            return String.Format("Product ID: {0}, Product Name: {1}, Product Price: {2}", productId, productName, ProductPrice);
        }
    }
}

