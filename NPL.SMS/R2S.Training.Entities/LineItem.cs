using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.Entities
{
    class LineItem
    {
        private int orderId;
        private int productId;
        private int quantity;
        private double price;

        public LineItem()
        {
        }
        public LineItem(int orderId, int productId, int quantity, double price)
        {
            this.OrderId = orderId;
            this.ProductId = productId;
            this.Quantity = quantity;
            this.Price = price;
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (value > 0)
                {
                    quantity = value;
                }
                else
                {
                    do
                    {
                        Console.WriteLine("Quantity must > 0, enter again: ");
                        quantity = int.Parse(Console.ReadLine());
                    } while (quantity <= 0);
                }
            }
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
        public int OrderId
        {
            get { return orderId; }
            set
            {
                if (value > 0)
                {
                    orderId = value;
                }
                else
                {
                    do
                    {
                        Console.WriteLine("OrderId must > 0, enter again: ");
                        orderId = int.Parse(Console.ReadLine());
                    } while (orderId <= 0);
                }
            }
        }
        public void AddInfor()
        {
            Console.Write("Enter order id: ");
            OrderId = int.Parse(Console.ReadLine());

            Console.Write("Enter product id: ");
            ProductId = int.Parse(Console.ReadLine());

            Console.Write("Enter quantity: ");
            Quantity = int.Parse(Console.ReadLine());

            Console.Write("Enter price: ");
            Price = double.Parse(Console.ReadLine());
        }
        public override string ToString()
        {
            return string.Format($"Order Id: {OrderId}, Product Id: {ProductId}, " +
                 $"Quantity: {Quantity}, Price: {Price}");
        }
    }
}
