using System;

namespace DataLayer
{
    class User
    {
        private string userID;
        private string userName;
        private string passwordHash;
        private string passwordSalt;
        private string email;
        private string firstName;
        private string lastName;
        private string phoneNumber;
        private string address;
        private string profilePicture;
        private int roleID;
        private DateTime? lastLogin;
        private bool isActive;
        private DateTime createdDate;
        private DateTime updatedDate;

        public User()
        {
        }

        public User(string userID, string userName, string passwordHash, string passwordSalt, string email, string firstName, string lastName, string phoneNumber, string address, string profilePicture, int roleID, DateTime? lastLogin, bool isActive, DateTime createdDate, DateTime updatedDate)
        {
            this.userID = userID;
            this.userName = userName;
            this.passwordHash = passwordHash;
            this.passwordSalt = passwordSalt;
            this.email = email;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.profilePicture = profilePicture;
            this.roleID = roleID;
            this.lastLogin = lastLogin;
            this.isActive = isActive;
            this.createdDate = createdDate;
            this.updatedDate = updatedDate;
        }

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string PasswordHash
        {
            get { return passwordHash; }
            set { passwordHash = value; }
        }

        public string PasswordSalt
        {
            get { return passwordSalt; }
            set { passwordSalt = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string ProfilePicture
        {
            get { return profilePicture; }
            set { profilePicture = value; }
        }

        public int RoleID
        {
            get { return roleID; }
            set { roleID = value; }
        }

        public DateTime? LastLogin
        {
            get { return lastLogin; }
            set { lastLogin = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
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

