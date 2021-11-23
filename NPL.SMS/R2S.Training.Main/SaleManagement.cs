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
        
        public static void CreateMenu()
        {
            Console.WriteLine(
                "1. Get list customers in the Orders table" +
                "\n2. Get all orders for a given customerId" +
                "\n3. Get all lineitem for a given orderId" +
                "\n4. Compute order total" +
                "\n5. Add new customer" +
                "\n6. Get all orders for a given customerId" +
                "\n7. Get all orders for a given customerId" +
                "\n8. Get all orders for a given customerId" +
                "\n9. Add line item" +
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
            const string LIST_LINEITEM = "3";
            const string COMPUTE_TOTAL = "4";
            const string ADD_CUSTOMER = "5";
            const string ADD_ITEM = "9";
            const string EXIT = "11";


            CustomerDAO CD = new CustomerDAO();
            LineItemDAO LD = new LineItemDAO();
            OrderDao OD = new OrderDao();

            do
            {
                CreateMenu();
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
                                foreach (Customer customers in list)
                                {
                                    Console.WriteLine(customers);
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
                    case LIST_LINEITEM:
                        Console.Write("Enter order Id: ");
                        int orderId = int.Parse(Console.ReadLine());

                        try
                        {
                            if (LineItemDAO.CheckOrderId(orderId))
                            {
                                Console.WriteLine("----------LIST LINE ITEM OF ORDER ID-----------");
                                foreach (LineItem item in LD.GetAllItemByOrderId(orderId))
                                {
                                    Console.WriteLine(item);
                                }
                            }
                            else
                            {
                                Console.WriteLine("This order id not exits");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case  ADD_ITEM: 
                        Console.WriteLine("--------ADD LINE ITEM--------");
                        LineItem lineItem = new LineItem();
                        lineItem.AddInfor();
                        if (LD.AddLineItem(lineItem))
                            Console.WriteLine("Successfully!!");
                        else
                            Console.WriteLine("Failed");
                        break;
                    case COMPUTE_TOTAL:
                        Console.Write("Enter order Id: ");
                        int orderId_total = int.Parse(Console.ReadLine());
                        Console.WriteLine(OD.ComputeOrderTotal(orderId_total));
                        break;
                    case ADD_CUSTOMER:
                        Customer customer = new Customer();

                        Console.Write("Enter name: ");
                        customer.CustomerName = Console.ReadLine();

                        try
                        {
                            if (CD.AddCustomer(customer))
                            {
                                Console.WriteLine("Successfully");
                            }
                            else
                            {
                                Console.WriteLine("Failed");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                }
            }
            while (option != EXIT);  
        }
    }
}
