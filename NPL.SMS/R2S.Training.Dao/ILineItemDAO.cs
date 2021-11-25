using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.Entities.R2S.Training.Dao
{
    interface ILineItemDAO
    {
        List<LineItem> GetAllItemsByOrderId(int orderId); //functional 3
        bool AddLineItem(LineItem item);    //functional 9
    }
}
