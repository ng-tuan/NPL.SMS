using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.Entities.R2S.Training.Dao
{
    interface ICustomerDAO
    {
        List<Customer> GetAllCustomers(); // functional 1  
        bool AddCustomer(Customer customer); //functional 5
        bool UpdateCustomer(Customer customer); //functional 6
        bool DeleteCustomer(int customerId); //functional 7
    }
}
