﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.Dao
{
    interface IOrderDao
    {
        double ComputeOrderTotal(int orderId); // functional 4     
    }
}
