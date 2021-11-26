﻿using System;
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
        
        public void Input()
        {
            Console.Write("Enter Customer name: ");
            CustomerName = Console.ReadLine();
        }

        public override string ToString()
        {
            return String.Format("CustomerId: {0}  CustomerName: {1}", customerId, customerName);
        }
    }
}
