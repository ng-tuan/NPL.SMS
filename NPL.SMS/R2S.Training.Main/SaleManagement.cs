using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using NPL.SMS.R2S.Training.Entities;
using NPL.SMS.R2S.Training.Dao;

namespace NPL.SMS.R2S.Training.Main
{
    class SaleManagement
    {
       
        static void Main(string[] args)
        {
            CustomerDAO CD = new CustomerDAO();
            List<Customer> list = CD.GetAllCustomers();

            if(list.Count == 0)
            {
                Console.WriteLine("List customer empty!");
            } 
            else
            {
                foreach (Customer customer in CD.GetAllCustomers())
                {
                    customer.Export();
                } 
                    
            }
                
        }
    }
}
