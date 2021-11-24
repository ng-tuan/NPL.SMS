﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.Entities.R2S.Training.Dao
{
    interface ICustomerDAO
    {
        List<Customer> GetAllCustomers(); // functional 1  
        List<Order> GetAllOrdersByCustomerID(int customerId); // functional 2
        bool AddCustomer(Customer customer); //functional 5
        bool UpdateCustomer(Customer customer); //func 6
        bool DeleteCustomer(int customerId); //func 7
    }
}
