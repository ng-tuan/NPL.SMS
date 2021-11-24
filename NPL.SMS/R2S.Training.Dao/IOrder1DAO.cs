using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.Entities.R2S.Training.Dao
{
    interface IOrder1DAO
    {
        List<LineItem> UpdateAnOrderTotalIntoDatabase(int orderId); //function 10
        bool AddOrder(LineItem item);    //function 8
    }
}