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

        //Check orderId of lineitem table
        public static bool CheckOrderIdofLineItemTable(int orderId)
        {
            bool check = false;

            using SqlConnection conn = Connect.GetSqlConnection();

            // Open the SqlConnection
            conn.Open();

            // The following code uses a SqlCommand based on the SqlConnection
            using SqlCommand cmd = Connect.GetSqlCommand(SELECT_ALL_LINEITEM, conn);
            using SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                if (orderId == (int)dataReader["order_id"])
                {
                    check = true;
                    break;
                }
            }
            return check;
        }
        //List with all lineItem for a given orderId
        public List<LineItem> GetAllItemsByOrderId(int orderId)
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

            using SqlDataReader dataReader = cmd.ExecuteReader();

            List<LineItem> list = new List<LineItem>();

            while (dataReader.Read())
            {
                LineItem lineitem = new LineItem
                {
                    OrderId = (int)dataReader["order_id"],
                    ProductId = (int)dataReader["product_id"],
                    Quantity = (int)dataReader["quantity"],
                    Price = (double)dataReader["price"]
                };
                list.Add(lineitem);
            }
            return list;
        }
        //Check orderId and productId in lineitem table
        public static bool CheckLineItem(LineItem lineItem)
        {
            bool check = false;

            using SqlConnection conn = Connect.GetSqlConnection();

            // Open the SqlConnection
            conn.Open();

            // The following code uses a SqlCommand based on the SqlConnection
            using SqlCommand cmd = Connect.GetSqlCommand(SELECT_ALL_LINEITEM, conn);
            using SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                if (lineItem.OrderId == (int)dataReader["order_id"] && lineItem.ProductId == (int)dataReader["product_id"])
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
        //Create a lineitem into the database
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
