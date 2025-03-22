using System;

namespace DataLayer
{
    class Project
    {
        private int projectID;
        private string projectName;
        private string description;
        private DateTime? startDate;
        private DateTime? endDate;
        private decimal budget;
        private int statusID;
        private int managerID;
        private int priority;
        private decimal percentComplete;
        private int createdBy;
        private DateTime createdDate;
        private DateTime updatedDate;

        public Project()
        {
        }

        public Project(int projectID, string projectName, string description, DateTime? startDate, DateTime? endDate, decimal budget, int statusID, int managerID, int priority, decimal percentComplete, int createdBy, DateTime createdDate, DateTime updatedDate)
        {
            this.projectID = projectID;
            this.projectName = projectName;
            this.description = description;
            this.startDate = startDate;
            this.endDate = endDate;
            this.budget = budget;
            this.statusID = statusID;
            this.managerID = managerID;
            this.priority = priority;
            this.percentComplete = percentComplete;
            this.createdBy = createdBy;
            this.createdDate = createdDate;
            this.updatedDate = updatedDate;
        }

        public int ProjectID
        {
            get { return projectID; }
            set { projectID = value; }
        }

        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public DateTime? StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public DateTime? EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public decimal Budget
        {
            get { return budget; }
            set { budget = value; }
        }

        public int StatusID
        {
            get { return statusID; }
            set { statusID = value; }
        }

        public int ManagerID
        {
            get { return managerID; }
            set { managerID = value; }
        }

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public decimal PercentComplete
        {
            get { return percentComplete; }
            set { percentComplete = value; }
        }

        public int CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
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


