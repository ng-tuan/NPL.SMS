using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using NPL.SMS.R2S.Training.Entities;

namespace NPL.SMS.R2S.Training.Main
{
    class SaleManagement
    {
        private const string SELECT = "Select * From Customer";

        private static List<Customer> GetAllCustomers()
        {
            using SqlConnection conn = Connect.GetSqlConnection();

            conn.Open();

            using SqlCommand cmd = Connect.GetSqlCommand(SELECT, conn);
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
        static void Main(string[] args)
        {
            foreach (Customer customer in GetAllCustomers())
            {
                Console.WriteLine(customer);
            }
        }
    }
}
