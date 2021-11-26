using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.Entities.R2S.Training.Dao
{
    interface IOrderDAO
    {
        double ComputeOrderTotal(int orderId); // functional 4                                      
        bool AddOrder(LineItem item);    //function 8
        bool UpdateOrderTotal(int orderId); //function 10
    }
}
