using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.Entities
{
    class Customer
    {
        private int customerId;
        private string customerName;

        public Customer()
        {
        }

        public Customer(int customerId, string customerName)
        {
            this.customerId = customerId;
            this.customerName = customerName;
        }

        public int CustomerId { get => customerId; set => customerId = value; }
        public string CustomerName { get => customerName; set => customerName = value; }

        public void Output()
        {
            Console.WriteLine("CustomerId: {0}  CustomerName: {1}", customerId, customerName);
        }
    }
}
