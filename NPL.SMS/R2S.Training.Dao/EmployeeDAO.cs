using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using NPL.SMS.R2S.Training.Main;

namespace NPL.SMS.R2S.Training.Dao
{
    class EmployeeDAO
    {
        const string SELECT = "select * from Orders";

        /// <summary>
        /// Check employeeId
        /// </summary>
        /// <param name="employee_id"></param>
        /// <returns></returns>
        public static bool CheckEmployeeID(int employee_id)
        {
            bool check = false;
            // Create a connection
            using SqlConnection conn = Connect.GetSqlConnection();

            // Open a connection
            conn.Open();

            // Create a sql command
            using SqlCommand cmd = Connect.GetSqlCommand(SELECT, conn);
            using SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                if (employee_id == (int)dataReader["employee_id"])
                {
                    check = true;
                    break;
                }
            }
            return check;
        }
    }
}
