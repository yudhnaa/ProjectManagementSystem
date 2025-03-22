using System;

namespace DataLayer
{
    class ProjectStatus
    {
        private int statusID;
        private string statusName;
        private string description;
        private DateTime createdDate;
        private DateTime updatedDate;

        public ProjectStatus()
        {
        }

        public ProjectStatus(int statusID, string statusName, string description, DateTime createdDate, DateTime updatedDate)
        {
            this.statusID = statusID;
            this.statusName = statusName;
            this.description = description;
            this.createdDate = createdDate;
            this.updatedDate = updatedDate;
        }

        public int StatusID
        {
            get { return statusID; }
            set { statusID = value; }
        }

        public string StatusName
        {
            get { return statusName; }
            set { statusName = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
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


