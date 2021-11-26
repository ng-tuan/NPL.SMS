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
        private const string COMPUTE_ORDER_TOTAL = "select dbo.fn_compute_order_total(@order_id)";
        
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
        #8 Create Order
        public bool AddOrder(Order order)
        {
            if (CheckOrder(order))
            {
                return UpdateOrder(order);    // Check if there is orderId then update
            }
            else    // Otherwise, create a new order
            {
                // Create a connection
                using SqlConnection conn = Connect.GetSqlConnection();

                // Open the SqlConnection
                conn.Open();

                // The following code uses a SqlCommand based on the SqlConnection
                using SqlCommand cmd = Connect.GetSqlCommand(ADD_ORDER, conn);

                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@order_id", orderId),
                    new SqlParameter("@order_date", orderDate),
                    new SqlParameter("@order_time", orderTime);
                });
            if (cmd.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #10 Update Order
        public bool UpdateOrderTotal(int orderId)
            if(CheckOrderId(orderId) ==true)
            {
                using SqlConnection conn = Common.GetSqlConection();
                conn.Open();
                
                using SqlCommand cmd = Common.GetSqlCommand(UPDATE, conn);
                
                cmd.Parameters.AddRange(new[]
                {
                new SqlParameter("@total",ComputerOrderTotal(orderId)),
                new SqlParameter("@order_id", orderId)});
                
                if(cmd.ExcuteNowQuery() > 0) return true;
                else return false;
            }
            else
            {
                Console.WriteLine("The order is not in the database");
                return false;
            }  
    }
}
'
