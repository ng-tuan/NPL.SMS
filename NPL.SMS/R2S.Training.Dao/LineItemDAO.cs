using NPL.SMS.R2S.Training.Entities;
using NPL.SMS.R2S.Training.Entities.R2S.Training.Dao;
using NPL.SMS.R2S.Training.Main;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;



namespace NPL.SMS.R2S.Training.Dao
{
    class LineItemDAO : ILineItemDAO
    {
        const string ADD_LINEITEM = @"INSERT INTO dbo.LineItem (order_id, product_id, quantity, price)
                                      VALUES(@order_id, @product_id, @quantity, @price)";
        const string UPDATE_LINEITEM = @"UPDATE dbo.LineItem SET quantity += @quantity, price += @price 
                                         WHERE order_id = @order_id AND product_id = @product_id";
        const string SELECT_ITEM_BY_ORDERID = "SELECT *FROM LineItem WHERE order_id=@orderId";
        const string SELECT_ALL_LINEITEM = "SELECT *FROM LineItem";

        //Kiểm tra orderId có tồn tại trong bảng LineItem chưa
        public static bool CheckOrderId(int orderId)
        {
            bool check = false;

            using SqlConnection conn = Connect.GetSqlConnection();

            // Open the SqlConnection
            conn.Open();

            // The following code uses a SqlCommand based on the SqlConnection
            using SqlCommand cmd = Connect.GetSqlCommand(SELECT_ALL_LINEITEM, conn);
            using SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                if (orderId == (int)dr["order_id"])
                {
                    check = true;
                    break;
                }
            }
            return check;
        }
        //xuất ra 1 danh sách lineItem theo orderId 
        public List<LineItem> GetAllItemByOrderId(int orderId)
        {
            using SqlConnection conn = Connect.GetSqlConnection();

            // Open the SqlConnection
            conn.Open();

            // The following code uses a SqlCommand based on the SqlConnection
            using SqlCommand cmd = Connect.GetSqlCommand(SELECT_ITEM_BY_ORDERID, conn);

            // Add multiple parameters to SQL command in one statement
            cmd.Parameters.AddRange(new[]
            {
                new SqlParameter("@orderId", orderId),
            });

            using SqlDataReader dr = cmd.ExecuteReader();

            List<LineItem> list = new List<LineItem>();

            while (dr.Read())
            {
                LineItem line = new LineItem
                {
                    OrderId = (int)dr["order_id"],
                    ProductId = (int)dr["product_id"],
                    Quantity = (int)dr["quantity"],
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
            using SqlCommand cmd = Connect.GetSqlCommand(SELECT_ALL_LINEITEM, conn);
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
        //Update quantity và price
        public bool UpdateLineItem(LineItem item)
        {
            using SqlConnection conn = Connect.GetSqlConnection();

            // Open the SqlConnection
            conn.Open();

            // The following code uses a SqlCommand based on the SqlConnection
            using SqlCommand cmd = Connect.GetSqlCommand(UPDATE_LINEITEM, conn);

            cmd.Parameters.AddRange(new[]
            {
                new SqlParameter("@order_id", item.OrderId),
                new SqlParameter("@product_id", item.ProductId),
                new SqlParameter("@quantity", item.Quantity),
                new SqlParameter("@price", item.Price)
            });
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else
                return false;
        }
        //Thêm 1 lineItem
        public bool AddLineItem(LineItem item)
        {
            if (CheckLineItem(item))
            {
                return UpdateLineItem(item);    //Kiểm tra nếu đã có orderId và productId thì cập nhật
            }
            else    //Ngược lại sẽ tạo 1 lineItem mới
            {
                using SqlConnection conn = Connect.GetSqlConnection();

                // Open the SqlConnection
                conn.Open();

                // The following code uses a SqlCommand based on the SqlConnection
                using SqlCommand cmd = Connect.GetSqlCommand(ADD_LINEITEM, conn);

                cmd.Parameters.AddRange(new[]
                {
                new SqlParameter("@order_id", item.OrderId),
                new SqlParameter("@product_id", item.ProductId),
                new SqlParameter("@quantity", item.Quantity),
                new SqlParameter("@price", item.Price)
            });
            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else
                return false;
            }
        }
    }
}
