using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using NPL.SMS.R2S.Training.Main;
using NPL.SMS.R2S.Training.Entities.R2S.Training.Dao;
using NPL.SMS.R2S.Training.Entities;
using System.Data;

namespace NPL.SMS.R2S.Training.Dao
{
    class OrderDAO : IOrderDAO
    {        
        const string COMPUTE_ORDER_TOTAL = "select dbo.fn_compute_order_total(@order_id)";
        const string SELECT_ORDERS_BY_CUSID = "SELECT * FROM Orders WHERE customer_id= @customerId";
        const string ADD_ORDER = @"INSERT INTO Orders (order_date, customer_id, employee_id, total)
                                    VALUES (@order_date, @customer_id, @employee_id, @total)";
        const string SELECT_ALL_ORDER = "SELECT * FROM Orders";
        const string UPDATE_ORDER_TOTAL = "UPDATE Orders set total= @total where order_id= @order_id"; 

        /// <summary>
        /// Check orderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static bool CheckOrderId(int orderId)
        {
            bool check = false;
            // Create a connection
            using SqlConnection conn = Connect.GetSqlConnection();

            // Open a connection
            conn.Open();

            // Create a sql command
            using SqlCommand cmd = Connect.GetSqlCommand(SELECT_ALL_ORDER, conn);
            using SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                if (orderId == (int)dataReader["order_id"])
                {
                    check = true;
                    break;
                }
            }
            return check;
        }

        /// <summary>
        /// Compute order total using Function
        /// </summary>        
        /// <param name="orderId">Order information</param>
        /// <returns></returns>
        public double ComputeOrderTotal(int orderId)
        {
            // Create a connection
            using SqlConnection conn = Connect.GetSqlConnection();

            // Create a sql command
            using SqlCommand cmd = Connect.GetSqlCommand(COMPUTE_ORDER_TOTAL, conn);

            // Create parameter
            SqlParameter param = new SqlParameter
            {
                ParameterName = "@order_id",
                Value = orderId
            };

            // Add parameter to sql command
            cmd.Parameters.Add(param);

            try
            {
                // Open a connection
                conn.Open();

                // Execute sql compute order total statement 
                return (double)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// Get all orders by customerId
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<Order> GetAllOrdersByCustomerID(int customerId)
        {
            // Create a connection
            using SqlConnection conn = Connect.GetSqlConnection();

            // Open a connection
            conn.Open();

            // Create a sql command
            using SqlCommand cmd = Connect.GetSqlCommand(SELECT_ORDERS_BY_CUSID, conn);

            // Create and add parameter to sql command
            cmd.Parameters.Add(new SqlParameter("@customerId", customerId));

            using SqlDataReader dataReader = cmd.ExecuteReader(); // Excute command

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

        /// <summary>
        /// Create an order into the database
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool AddOrder(Order order)
        {
            if(CustomerDAO.CheckCustomerID(order.CustomerId) ==true &&
                EmployeeDAO.CheckEmployeeID(order.EmployeeId) == true)
            {
                // Create a connection
                using SqlConnection conn = Connect.GetSqlConnection();

                // Open a connection
                conn.Open();

                // Create a sql command
                using SqlCommand cmd = Connect.GetSqlCommand(ADD_ORDER, conn);

                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@order_date", order.OrderDate),
                    new SqlParameter("@customer_id", order.CustomerId),
                    new SqlParameter("@employee_id", order.EmployeeId),
                    new SqlParameter("@total", order.Total)
                });

                if (cmd.ExecuteNonQuery() > 0) return true;
                else return false;
            }
            else
            {
                Console.WriteLine("CustomerId or EmployeeId invalid!");
                return false;
            }
        }

        public bool UpdateOrderTotal(int orderId)
        {
            if (CheckOrderId(orderId) == true)
            {
                // Create a connection
                using SqlConnection conn = Connect.GetSqlConnection();

                // Open a connection
                conn.Open();

                // Create a sql command
                using SqlCommand cmd = Connect.GetSqlCommand(UPDATE_ORDER_TOTAL, conn);

                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@order_id", orderId),
                    new SqlParameter("@total", ComputeOrderTotal(orderId))
                });

                if (cmd.ExecuteNonQuery() > 0) return true;
                else return false;
            } 
            else
            {
                Console.WriteLine("Order ID is not exist in database!");
                return false;
            }            
        }

    }
}
