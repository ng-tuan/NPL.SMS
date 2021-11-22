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
        public List<Customer> GetAllCustoners()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrdersByCustomerID(int customerId)
        {
            throw new NotImplementedException();
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
    }
}
