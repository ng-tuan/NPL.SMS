using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System;
using NPL.SMS.R2S.Training.Entities.R2S.Training.Dao;
using NPL.SMS.R2S.Training.Entities;
using NPL.SMS.R2S.Training.Main;

namespace NPL.SMS.R2S.Training.Dao
{    
    class CustomerDAO : ICustomerDAO
    {

        private const string ADD_CUSTOMER = "sp_add_customer";
        const string SELECT_ALLCUSTOMERS = "SELECT * FROM Customer WHERE EXISTS(SELECT Orders.customer_id FROM Orders WHERE Orders.customer_id = Customer.customer_id)";
        const string SELECT_ORDERS_BY_CUSID = "SELECT * FROM Orders WHERE customer_id= @customerId";
        const string UPDATE_CUSTOMER = "sp_updateCustomer @customer_id, @customer_name";
        const string DELETE_CUSTOMER = "sp_deleteCustomer @customer_id";
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

        /// <summary>
        /// Add new a customer using Store Procedure
        /// </summary>
        /// <param name="customer">Customer information</param>
        /// <returns>Logic status</returns>
        public bool AddCustomer(Customer customer)
        {
            // Create a connection
            using SqlConnection conn = Connect.GetSqlConnection();

            // Open a connection
            conn.Open();

            // Create parameter
            SqlParameter param = new SqlParameter
            {
                ParameterName = "@customer_name",
                Value = customer.CustomerName
            };

            // Create a sql command
            using SqlCommand cmd = Connect.GetSqlCommand(ADD_CUSTOMER, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Add parameter to sql command
            cmd.Parameters.Add(param);

            // Execute sql delete command and return logic status
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;           
        }

        /// <summary>
        /// Delete a customer using Store Procedure
        /// </summary>
        /// <param customerID="">Customer information</param>
        /// <returns>Logic status</returns>
        public bool DeleteCustomer(int customerId)
        {
            if (CheckCUSID(customerId) == true)
            {
                // tạo kết nối 
                using SqlConnection conn = Connect.GetSqlConnection();
                
                // mở kết nối
                conn.Open();
                
                //tạo command
                using SqlCommand cmd = Connect.GetSqlCommand(DELETE_CUSTOMER, conn);
                
                //thêm parameter 
                cmd.Parameters.Add(new SqlParameter("@customer_id", customerId));

                if ((int)cmd.ExecuteScalar() > 0)
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// Update a customer using Store Procedure
        /// </summary>
        /// <param customer="">Customer information</param>
        /// <returns>Logic status</returns>

        public bool UpdateCustomer(Customer customer)
        {
            if (CheckCUSID(customer.CustomerId) == true)
            {
                using SqlConnection conn = Connect.GetSqlConnection();

                // mở kết nối
                conn.Open();
                
                //tạo command
                using SqlCommand cmd = Connect.GetSqlCommand(UPDATE_CUSTOMER, conn);

                //tạo parameters
                cmd.Parameters.AddRange(new[]
                {
                    new SqlParameter("@customer_id",customer.CustomerId),
                    new SqlParameter("@customer_name",customer.CustomerName)
                }
                );

                if (cmd.ExecuteNonQuery() > 0)
                    return true;
                return false;
            }
            return false;
        }

        public static bool CheckCUSID(int customer_id)
        {
            //bool check = false;
            using SqlConnection conn = Connect.GetSqlConnection();

            // mở kết nối
            conn.Open();

            using SqlCommand cmd = Connect.GetSqlCommand("sp_checkcusID @customer_id", conn);

            cmd.Parameters.Add(new SqlParameter("@customer_id", customer_id));

            if ((int)cmd.ExecuteScalar() > 0)
                return true;
            else
            {
                Console.WriteLine("ID does not exist!!");
                return false;
            }
        }
    }
}

