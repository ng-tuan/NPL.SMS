using System;
using System.Collections.Generic;
using System.Text;

namespace NPL.SMS.R2S.Training.Entities
{
    class Employee
    {
        private int employeeId;
        private string employeeName;
        private double salary;
        private int spvrId;

        public Employee()
        {

        }
        public Employee(int employeeId, string employeeName, double salary, int spvrId)
        {
            this.employeeId = employeeId;
            this.employeeName = employeeName;
            this.salary = salary;
            this.spvrId = spvrId;
        }

        public int EmployeeId { get => employeeId; set => employeeId = value; }
        public string EmployeeName { get => employeeName; set => employeeName = value; }
        public double Salary { get => salary; set => salary = value; }
        public int SpvrId { get => spvrId; set => salary = value; }

    }
}
