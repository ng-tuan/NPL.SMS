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
        private const string LIST_LINEITEM = "3";
        private const string ADD_ITEM = "9";
        private static void Menu()
        {
            Console.WriteLine("------------MENU-----------");
            Console.WriteLine("3.List lint item of Order ID");
            Console.WriteLine("9.ADD LINE ITEM");
            Console.WriteLine("11.EXIT");
            Console.Write("Your choice: ");
        }
        static void Main(string[] args)
        {
            LineItemDAO LD = new LineItemDAO();
            string option;

            do
            {
                Menu();
                option = Console.ReadLine();
                switch (option)
                {
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
                    case ADD_ITEM:
                        Console.WriteLine("--------ADD LINE ITEM--------");
                        LineItem lineItem = new LineItem();
                        lineItem.AddInfor();
                        if (LD.AddLineItem(lineItem))
                            Console.WriteLine("Successfully!!");
                        else
                            Console.WriteLine("Failed");
                        break;
                }
            } while (option != "11");
        }
    }
}
