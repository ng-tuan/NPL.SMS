using NPL.SMS.R2S.Training.Entities;
using NPL.SMS.R2S.Training.Entities.R2S.Training.Dao;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using NPL.SMS.R2S.Training.Main;

namespace NPL.SMS.R2S.Training.Dao
{
    class CustomerDAO : ICustomerDAO
    {
        const string SELECT_ALLCUSTOMERS = "SELECT * FROM Customer WHERE EXISTS(SELECT Orders.customer_id FROM Orders WHERE Orders.customer_id = Customer.customer_id)";
        const string SELECT_ORDERS_BY_CUSID = "SELECT * FROM Orders WHERE customer_id= @customerId";
        public List<Customer> GetAllCustomers()
        {
            using SqlConnection conn = Connect.GetSqlConnection();

            conn.Open();

            using SqlCommand cmd = Connect.GetSqlCommand(SELECT_ALLCUSTOMERS, conn);
            using SqlDataReader dataReader = cmd.ExecuteReader();

            List<Customer> list = new List<Customer>();
            while (dataReader.Read())
            {
                Customer customer = new Customer
                {
                    CustomerId = dataReader.GetInt32(0),
                    CustomerName = dataReader.GetString(1)
                };

                list.Add(customer);
            }

            return list;
        }

        public List<Order> GetAllOrdersByCustomerID(int customerId)
        {
            using SqlConnection conn = Connect.GetSqlConnection();

            conn.Open();

            using SqlCommand cmd = Connect.GetSqlCommand(SELECT_ORDERS_BY_CUSID, conn);

            cmd.Parameters.AddRange(new[]
            {
                new SqlParameter("@customerId", customerId)
            });

            using SqlDataReader dataReader = cmd.ExecuteReader();

            List<Order> list = new List<Order>();
            while (dataReader.Read())
            {
                Order customer = new Order
                {
                    OrderId = dataReader.GetInt32(0),
                    OrderDate = dataReader.GetDateTime(1),
                    CustomerId = dataReader.GetInt32(2),
                    EmployeeId = dataReader.GetInt32(3),
                    Total = dataReader.GetDouble(4)
                };

                list.Add(customer);
            }

            return list;
        }
    }
}
