using NPL.SMS.R2S.Training.Entities;
using NPL.SMS.R2S.Training.Entities.R2S.Training.Dao;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.Dao
{
    class CustomerDAO : ICustomerDAO
    {
        public List<Customer> GetAllCustoners()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrdersByCustomerID(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
