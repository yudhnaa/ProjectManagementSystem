-- Create Database
IF NOT EXISTS(SELECT *
FROM sys.databases
WHERE name = 'ProjectManagementSystemDB')
    BEGIN
    CREATE DATABASE [ProjectManagementSystemDB]
END
GO

USE ProjectManagementSystemDB;
GO

-- Initial Tables with Modifications
CREATE TABLE [Roles]
(
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE [Users]
(
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(128) NOT NULL,
    --PasswordSalt NVARCHAR(128) NOT NULL, -- Salt for password hashing but is it necessary?
    Email NVARCHAR(100) NOT NULL UNIQUE,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Address NVARCHAR(200),
    Avatar VARCHAR(255),
    RoleID INT NOT NULL,
    LastLogin DATETIME,
    IsActive BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoleID) REFERENCES [Roles](RoleID)
);

CREATE TABLE [Departments]
(
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(200),
    ManagerID INT,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ManagerID) REFERENCES [Users](UserID)
);

CREATE TABLE [Employees]
(
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL UNIQUE,
    Position NVARCHAR(100),
    HireDate DATE,
    DepartmentID INT,
    ReportsTo INT,
    Salary DECIMAL(18, 2),
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES [Users](UserID),
    FOREIGN KEY (DepartmentID) REFERENCES [Departments](DepartmentID),
    FOREIGN KEY (ReportsTo) REFERENCES [Employees](EmployeeID)
);

CREATE TABLE [ProjectStatus]
(
    StatusID INT PRIMARY KEY IDENTITY(1,1),
    StatusName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200),
    --ColorCode NVARCHAR(7) DEFAULT '#808080', -- is it necessary?
    --DisplayOrder INT DEFAULT 0, -- is it necessary?
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE ProjectPirority
(
    -- 0: Low, 1: Medium, 2: High
    PriorityID INT PRIMARY KEY IDENTITY(1,1),
    PriorityName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200),
    -- ColorCode NVARCHAR(7) DEFAULT '#808080',
    -- DisplayOrder INT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [Projects]
(
    ProjectID INT PRIMARY KEY IDENTITY(1,1),
    ProjectName NVARCHAR(100) NOT NULL,
    ProjectCode NVARCHAR(20),
    Description NVARCHAR(500),
    StartDate DATE,
    EndDate DATE,
    Budget DECIMAL(18, 2),
    StatusID INT NOT NULL,
    ManagerID INT NOT NULL,
    PriorityID INT NOT NULL,
    -- 0: Low, 1: Medium, 2: High
    PercentComplete DECIMAL(5, 2) DEFAULT 0,
    IsDeleted BIT DEFAULT 0,
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (StatusID) REFERENCES [ProjectStatus](StatusID),
    FOREIGN KEY (ManagerID) REFERENCES [Users](UserID),
    FOREIGN KEY (CreatedBy) REFERENCES [Users](UserID),
    FOREIGN KEY (PriorityID) REFERENCES [ProjectPirority](PriorityID)
);

CREATE TABLE [ProjectMembers]
(
    ProjectMemberID INT PRIMARY KEY IDENTITY(1,1),
    ProjectID INT NOT NULL,
    UserID INT NOT NULL,
    JoinDate DATE DEFAULT GETDATE(),
    RoleInProject NVARCHAR(100),
    IsConfirmed BIT DEFAULT 0,
    ConfirmationDate DATETIME,
    -- date when the user confirms the invitation
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProjectID) REFERENCES [Projects](ProjectID),
    FOREIGN KEY (UserID) REFERENCES [Users](UserID),
    CONSTRAINT UQ_ProjectMember UNIQUE (ProjectID, UserID)
);

CREATE TABLE [TaskStatus]
(
    StatusID INT PRIMARY KEY IDENTITY(1,1),
    StatusName NVARCHAR(50) NOT NULL,
    -- New, In Progress, Blocked, Done, etc.
    Description NVARCHAR(200),
    -- ColorCode NVARCHAR(7) DEFAULT '#808080',
    -- DisplayOrder INT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE [TaskPriority]
(
    PriorityID INT PRIMARY KEY IDENTITY(1,1),
    PriorityName NVARCHAR(50) NOT NULL,
    -- Low, Medium, High, Urgent
    Description NVARCHAR(200),
    -- ColorCode NVARCHAR(7) DEFAULT '#808080',
    -- DisplayOrder INT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE [Tasks]
(
    TaskID INT PRIMARY KEY IDENTITY(1,1),
    TaskCode NVARCHAR(20),
    TaskName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    ProjectID INT NOT NULL,
    MainUserID INT NOT NULL,
    -- Main user responsible for the task
    StatusID INT NOT NULL,
    PriorityID INT NOT NULL,
    StartDate DATE,
    DueDate DATE,
    EstimatedHours DECIMAL(8, 2),
    ActualHours DECIMAL(8, 2),
    PercentComplete DECIMAL(5, 2) DEFAULT 0,
    ParentTaskID INT,
    -- for sub-tasks
    LastStatusChangeDate DATETIME,
    BlockReason NVARCHAR(500),
    -- reason for blocking the task
    IsDeleted BIT DEFAULT 0,
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProjectID) REFERENCES [Projects](ProjectID),
    FOREIGN KEY (MainUserID) REFERENCES [Users](UserID),
    FOREIGN KEY (StatusID) REFERENCES [TaskStatus](StatusID),
    FOREIGN KEY (PriorityID) REFERENCES [TaskPriority](PriorityID),
    FOREIGN KEY (ParentTaskID) REFERENCES [Tasks](TaskID),
    FOREIGN KEY (CreatedBy) REFERENCES [Users](UserID)
);

CREATE TABLE [TaskHelpRequests]
(
    RequestID INT PRIMARY KEY IDENTITY(1,1),
    TaskID INT NOT NULL,
    RequestedBy INT NOT NULL,
    RequestedTo INT NOT NULL,
    RequestMessage NVARCHAR(500),
    IsResolved BIT DEFAULT 0,
    ResolvedDate DATETIME,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TaskID) REFERENCES [Tasks](TaskID),
    FOREIGN KEY (RequestedBy) REFERENCES [Users](UserID),
    FOREIGN KEY (RequestedTo) REFERENCES [Users](UserID)
);

CREATE TABLE [TaskDependencies]
(
    DependencyID INT PRIMARY KEY IDENTITY(1,1),
    TaskID INT NOT NULL,
    DependsOnTaskID INT NOT NULL,
    DependencyType INT NOT NULL,
    -- 1: Finish to Start, 2: Start to Start, 3: Finish to Finish, 4: Start to Finish
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TaskID) REFERENCES [Tasks](TaskID),
    FOREIGN KEY (DependsOnTaskID) REFERENCES [Tasks](TaskID),
    CONSTRAINT UQ_TaskDependency UNIQUE (TaskID, DependsOnTaskID)
);

CREATE TABLE [TaskComments]
(
    CommentID INT PRIMARY KEY IDENTITY(1,1),
    TaskID INT NOT NULL,
    UserID INT NOT NULL,
    CommentText NVARCHAR(1000) NOT NULL,
    IsEdited BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TaskID) REFERENCES [Tasks](TaskID),
    FOREIGN KEY (UserID) REFERENCES [Users](UserID)
);

CREATE TABLE [TaskHistory]
(
    HistoryID INT PRIMARY KEY IDENTITY(1,1),
    TaskID INT NOT NULL,
    FieldChanged NVARCHAR(50) NOT NULL,
    OldValue NVARCHAR(MAX),
    NewValue NVARCHAR(MAX),
    ChangedBy INT NOT NULL,
    ChangedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TaskID) REFERENCES [Tasks](TaskID),
    FOREIGN KEY (ChangedBy) REFERENCES [Users](UserID)
);

CREATE TABLE [TimeEntries]
(
    EntryID INT PRIMARY KEY IDENTITY(1,1),
    TaskID INT NOT NULL,
    UserID INT NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME,
    Description NVARCHAR(500),
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TaskID) REFERENCES [Tasks](TaskID),
    FOREIGN KEY (UserID) REFERENCES [Users](UserID)
);



CREATE TABLE [Files]
(
    FileID INT PRIMARY KEY IDENTITY(1,1),
    FileName NVARCHAR(255) NOT NULL,
    FilePath NVARCHAR(500) NOT NULL,
    FileSize BIGINT,
    FileType NVARCHAR(100),
    ProjectID INT,
    TaskID INT,
    UploadedBy INT NOT NULL,
    UploadDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProjectID) REFERENCES [Projects](ProjectID),
    FOREIGN KEY (TaskID) REFERENCES [Tasks](TaskID),
    FOREIGN KEY (UploadedBy) REFERENCES [Users](UserID)
);

CREATE TABLE NotificaitonTypes
(
    TypeID INT PRIMARY KEY IDENTITY(1,1),
    -- 1. Task, 2. Project, 3. System
    TypeName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE [Notifications]
(
    NotificationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Title NVARCHAR(100) NOT NULL,
    Message NVARCHAR(500),
    NotificationType INT NOT NULL,
    RelatedID INT,
    -- ID of related entity (task, project, etc.)
    IsRead BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES [Users](UserID),
    FOREIGN KEY (NotificationType) REFERENCES [NotificaitonTypes](TypeID)
);

CREATE TABLE [EmailNotifications]
(
    EmailID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Subject NVARCHAR(200) NOT NULL,
    Body NVARCHAR(MAX),
    SentDate DATETIME DEFAULT GETDATE(),
    IsSuccessful BIT DEFAULT 1,
    ErrorMessage NVARCHAR(500),
    FOREIGN KEY (UserID) REFERENCES [Users](UserID)
);

CREATE TABLE [ProjectPhases]
(
    PhaseID INT PRIMARY KEY IDENTITY(1,1),
    ProjectID INT NOT NULL,
    PhaseName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    StartDate DATE,
    EndDate DATE,
    StatusID INT,
    DisplayOrder INT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProjectID) REFERENCES [Projects](ProjectID),
    FOREIGN KEY (StatusID) REFERENCES [ProjectStatus](StatusID)
);

CREATE TABLE [ReportTypes]
(
    TypeID INT PRIMARY KEY IDENTITY(1,1),
    -- Project, Task, Performance
    TypeName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [Reports]
(
    ReportID INT PRIMARY KEY IDENTITY(1,1),
    ReportName NVARCHAR(100) NOT NULL,
    ReportType INT NOT NULL,
    -- Project, Task, Performance
    Description NVARCHAR(200),
    ReportData NVARCHAR(MAX),
    -- JSON data for the report
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CreatedBy) REFERENCES [Users](UserID),
    FOREIGN KEY (ReportType) REFERENCES [ReportTypes](TypeID)
);

-- not sure how to store this data
CREATE TABLE [AuditLog]
(
    LogID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    Action NVARCHAR(50) NOT NULL,
    Module NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500),
    IPAddress NVARCHAR(50),
    LogDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES [Users](UserID)
);

CREATE TABLE [Permissions]
(
    PermissionID INT PRIMARY KEY IDENTITY(1,1),
    PermissionName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE [RolePermissions]
(
    RolePermissionID INT PRIMARY KEY IDENTITY(1,1),
    RoleID INT NOT NULL,
    PermissionID INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoleID) REFERENCES [Roles](RoleID),
    FOREIGN KEY (PermissionID) REFERENCES [Permissions](PermissionID),
    CONSTRAINT UQ_RolePermission UNIQUE (RoleID, PermissionID)
);
GO