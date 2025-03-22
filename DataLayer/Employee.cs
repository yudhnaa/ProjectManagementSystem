using System;

namespace DataLayer
{
    class Employee
    {
        private string employeeID;
        private string userID;
        private string position;
        private DateTime hireDate;
        private int departmentID;
        private string reportsTo;
        private decimal salary;
        private DateTime createdDate;
        private DateTime updatedDate;

        public Employee()
        {
        }

        public Employee(string employeeID, string userID, string position, DateTime hireDate, int? departmentID, string reportsTo, decimal salary, DateTime createdDate, DateTime updatedDate)
        {
            this.employeeID = employeeID;
            this.userID = userID;
            this.position = position;
            this.hireDate = hireDate;
            this.departmentID = departmentID;
            this.reportsTo = reportsTo;
            this.salary = salary;
            this.createdDate = createdDate;
            this.updatedDate = updatedDate;
        }

        public string EmployeeID
        {
            get { return employeeID; }
            set { employeeID = value; }
        }

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string Position
        {
            get { return position; }
            set { position = value; }
        }

        public DateTime HireDate
        {
            get { return hireDate; }
            set { hireDate = value; }
        }

        public int? DepartmentID
        {
            get { return departmentID; }
            set { departmentID = value; }
        }

        public string? ReportsTo
        {
            get { return reportsTo; }
            set { reportsTo = value; }
        }

        public decimal Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        public DateTime UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }
    }
}


