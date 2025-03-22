using System;

namespace DataLayer
{
    class Department
    {
        private int departmentID;
        private string departmentName;
        private string description;
        private string managerID;
        private DateTime createdDate;
        private DateTime updatedDate;

        public Department()
        {
        }

        public Department(int departmentID, string departmentName, string description, string managerID, DateTime createdDate, DateTime updatedDate)
        {
            this.departmentID = departmentID;
            this.departmentName = departmentName;
            this.description = description;
            this.managerID = managerID;
            this.createdDate = createdDate;
            this.updatedDate = updatedDate;
        }

        public int DepartmentID
        {
            get { return departmentID; }
            set { departmentID = value; }
        }

        public string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string ManagerID
        {
            get { return managerID; }
            set { managerID = value; }
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

