using NPL.SMS.R2S.Training.Entities;
using NPL.SMS.R2S.Training.Entities.R2S.Training.Dao;
using NPL.SMS.R2S.Training.Main;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace NPL.SMS.R2S.Training.Dao
{
    class Order1DAO : IOrder1DAO
    {
        const string ADD_ORDER = @"INSERT INTO dbo.Order1 (order_id, product_id, price)
                                      VALUES(@order_id, @product_id)";
        const string UPDATE_LINEITEM = @"UPDATE dbo.Order1 SET price += @price 
                                         WHERE order_id = @order_id AND product_id = @product_id";
        const string SELECT_ITEM_BY_ORDERID = "SELECT *FROM LineItem WHERE order_id=@orderId";
        const string SELECT_ALL_LINEITEM = "SELECT *FROM LineItem";
        private SqlDbType customerId;
        private static string SELECT_ALL_ORDER;

        public string SELECT_ITEM_BY_CUSTOMERID { get; private set; }

        //Xuất ra danh sách order theo custumerId
        public List<LineItem> GetAllItemByOrderId(int orderId)
        {
            using SqlConnection conn = Connect.GetSqlConnection();

            // Open the SqlConnection
            conn.Open();

            // The following code uses a SqlCommand based on the SqlConnection
            using SqlCommand cmd = Connect.GetSqlCommand(SELECT_ITEM_BY_CUSTOMERID, conn);

            // Add multiple parameters to SQL command in one statement
            cmd.Parameters.AddRange(new[]
            {
                new SqlParameter("@customerId", customerId),
            });

            using SqlDataReader dr = cmd.ExecuteReader();

            List<Order> list = new List<Order>();

            while (dr.Read())
            {
                LineItem line = new LineItem
                {
                    OrderId = (int)dr["order_id"],
                    ProductId = (int)dr["product_id"],
                    Price = (double)dr["price"],
                };
                list.Add(line);
            }
            return list;
        }
        //Kiểm tra orderId và productId đã tồn tại chưa
        public static bool CheckLineItem(LineItem lineItem)
        {
            bool check = false;

            using SqlConnection conn = Connect.GetSqlConnection();

            // Open the SqlConnection
            conn.Open();

            // The following code uses a SqlCommand based on the SqlConnection
            using SqlCommand cmd = Connect.GetSqlCommand(SELECT_ALL_ORDER, conn);
            using SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (lineItem.OrderId == (int)dr["order_id"] && lineItem.ProductId == (int)dr["product_id"])
                {
                    check = true;
                    break;
                }
            }
            return check;
        }
        //Thêm 1 Order
        public bool AddOrder1(LineItem item)
        {
            if (CheckLineItem(item))
            {
                return UpdateOrder(item);   
            }
            else    
            {
                using SqlConnection conn = Connect.GetSqlConnection();

                // Open the SqlConnection
                conn.Open();

                // The following code uses a SqlCommand based on the SqlConnection
                using SqlCommand cmd = Connect.GetSqlCommand(ADD_ORDER, conn);

                cmd.Parameters.AddRange(new[]
                {
                new SqlParameter("@order_id", item.OrderId),
                new SqlParameter("@product_id", item.ProductId),
                new SqlParameter("@price", item.Price)
            });
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
                else
                    return false;
            }
        }

        private bool UpdateOrder(LineItem item)
        {
            throw new NotImplementedException();
        }

        public List<LineItem> UpdateAnOrderTotalIntoDatabase(int orderId)
        {
            throw new NotImplementedException();
        }

        public bool AddOrder(LineItem item)
        {
            throw new NotImplementedException();
        }
    }
}
