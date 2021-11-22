using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.Entities.R2S.Training.Dao
{
    interface ILineItemDAO
    {
        List<LineItem> GetAllItemByOrderId(int orderId);//Yêu cầu 3
        bool AddLineItem(LineItem item);    //yêu cầu 9
    }
}
