using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.Entities
{
    class Order
    {
        private int orderId, customerId, employeeId;
        private double total;
        private DateTime orderDate;
        public Order()
        {
        }
        public Order(int orderId, DateTime orderDate, int customerId, int employeeId, double total)
        {
            this.orderId = orderId;
            this.orderDate = orderDate;
            this.customerId = customerId;
            this.employeeId = employeeId;
            this.total = total;
        }
        public int OrderId { get => orderId; set => orderId = value; }
        public DateTime OrderDate { get => orderDate; set => orderDate = value; }
        public int CustomerId
        {
            get { return customerId; }
            set
            {
                if (value > 0)
                {
                    customerId = value;
                }
                else
                {
                    do
                    {
                        Console.WriteLine("ID must > 0, enter again: ");
                        customerId = int.Parse(Console.ReadLine());
                    } while (customerId <= 0);
                }
            }
        }
        public int EmployeeId
        {
            get { return employeeId; }
            set
            {
                if (value > 0)
                {
                    employeeId = value;
                }
                else
                {
                    do
                    {
                        Console.WriteLine("ID must > 0, enter again: ");
                        employeeId = int.Parse(Console.ReadLine());
                    } while (employeeId <= 0);
                }
            }
        }
        public double Total { get => total; set => total = value; }
        
        public void AddInfor()
        {
            OrderDate = DateTime.Now;
            Total = 0;
            Console.Write("Enter customer id: ");
            CustomerId = int.Parse(Console.ReadLine());
            Console.Write("Enter employee id: ");
            EmployeeId = int.Parse(Console.ReadLine());
        }
        public override string ToString()
        {
            return String.Format("Order ID: {0}, Order time: {1}, Customer ID: {2}, Employee ID: {3}, Total: {4}", OrderId, OrderDate, CustomerId, EmployeeId, Total);
        }
    }
}
