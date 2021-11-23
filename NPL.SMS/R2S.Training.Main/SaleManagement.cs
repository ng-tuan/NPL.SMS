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
        public static void createMenu()
        {
            Console.WriteLine(
                "1. Get list customers in the Orders table" +
                "\n2. Get all orders for a given customerId" +
                "\n3. Get all orders for a given customerId" +
                "\n4. Get all orders for a given customerId" +
                "\n5. Get all orders for a given customerId" +
                "\n6. Get all orders for a given customerId" +
                "\n7. Get all orders for a given customerId" +
                "\n8. Get all orders for a given customerId" +
                "\n9. Get all orders for a given customerId" +
                "\n10. Get all orders for a given customerId" +
                "\n11. Exit"
                );
            Console.Write("Enter your choice: ");
        }
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string option;
            const string LIST_CUSTOMERS = "1";
            const string LIST_ORDERS = "2";
            const string EXIT = "11";

            CustomerDAO CD = new CustomerDAO();

            do
            {
                createMenu();
                option = Console.ReadLine();

                switch (option)
                {
                    case LIST_CUSTOMERS:
                        {
                            List<Customer> list = CD.GetAllCustomers();

                            if (list.Count == 0)
                            {
                                Console.WriteLine("List customer empty!");
                            }
                            else
                            {
                                foreach (Customer customer in list)
                                {
                                    Console.WriteLine(customer);
                                }

                            }
                        }
                        break;
                    case LIST_ORDERS:
                        {
                            Console.Write("Enter customer id: ");
                            int customerId = int.Parse(Console.ReadLine());

                            List<Order> list = CD.GetAllOrdersByCustomerID(customerId);

                            if (list.Count == 0)
                            {
                                Console.WriteLine("List order empty!");
                            }
                            else
                            {
                                foreach (Order order in list)
                                {
                                    Console.WriteLine(order);
                                }
                            }
                        }
                        break;

                }
            }
            while (option != EXIT);  
        }
    }
}
