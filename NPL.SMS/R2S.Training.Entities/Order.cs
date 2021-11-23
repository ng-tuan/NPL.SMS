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
        public int CustomerId { get => customerId; set => customerId = value; }
        public int EmployeeId { get => employeeId; set => employeeId = value; }
        public double Total { get => total; set => total = value; }
        public override string ToString()
        {
            return String.Format("Order ID: {0}, Order time: {1}, Customer ID: {2}, Employee ID: {3}, Total: {4}", OrderId, OrderDate, CustomerId, EmployeeId, Total);
        }
    }
}
