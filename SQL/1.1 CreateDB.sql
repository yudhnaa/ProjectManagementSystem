-- Create Database
IF EXISTS(SELECT *
FROM sys.databases
WHERE name = 'ProjectManagementSystemDB')
BEGIN
	DROP DATABASE [ProjectManagementSystemDB]
END
GO

CREATE DATABASE [ProjectManagementSystemDB]
GO

USE [ProjectManagementSystemDB]
GO

CREATE TABLE [PermissionTypes]
(
	Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE()
)

CREATE TABLE [Permissions]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE,
	PermissionTypeId INT,
    Description NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (PermissionTypeId) REFERENCES [PermissionTypes](Id)
);
GO

CREATE TABLE [UserRoles]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [UserRolePermissions]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserRoleId INT NOT NULL,
    PermissionId INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserRoleId) REFERENCES [UserRoles](Id),
    FOREIGN KEY (PermissionId) REFERENCES [Permissions](Id),
    CONSTRAINT UQ_UserRolePermission UNIQUE (UserRoleId, PermissionId)
);
GO

CREATE TABLE [Users]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(128) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Address NVARCHAR(200),
    Avatar NVARCHAR(255),
	UserRoleId INT NOT NULL,
    LastLogin DATETIME,
    IsActive BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserRoleId) REFERENCES [UserRoles](Id)
);
GO

CREATE TABLE [Positions]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(255),
    IsActive BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [Departments]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(200),
    ManagerId INT,
    IsActive BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (ManagerId) REFERENCES [Users](Id)
);
GO

CREATE TABLE [Employees]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL UNIQUE,
    PositionId INT NOT NULL,
    HireDate DATE,
    DepartmentId INT,
    ReportTo INT,
    Salary DECIMAL(18, 2),
    IsActive BIT DEFAULT 1,
	IsDeleted BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES [Users](Id),
    FOREIGN KEY (DepartmentId) REFERENCES [Departments](Id),
    FOREIGN KEY (ReportTo) REFERENCES [Employees](Id),
    FOREIGN KEY (PositionId) REFERENCES [Positions](Id)
);

CREATE TABLE [ProjectStatuses]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(200),
    IsActive BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [ProjectPriorities]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(200),
    IsActive BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [Projects]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    ProjectCode NVARCHAR(20) UNIQUE,
    Description NVARCHAR(500),
    StartDate DATE,
    EndDate DATE,
    Budget DECIMAL(18, 2),
    StatusId INT NOT NULL,
    ManagerId INT NOT NULL,
    PriorityId INT NOT NULL,
    PercentComplete DECIMAL(5, 2) DEFAULT 0,
    IsDeleted BIT DEFAULT 0,
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (StatusId) REFERENCES [ProjectStatuses](Id),
    FOREIGN KEY (ManagerId) REFERENCES [Users](Id),
    FOREIGN KEY (CreatedBy) REFERENCES [Users](Id),
    FOREIGN KEY (PriorityId) REFERENCES [ProjectPriorities](Id)
);
GO

CREATE TABLE [ProjectMemberRoles]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [ProjectMemberRolePermissions]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProjectMemberRoleId INT NOT NULL,
    PermissionId INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProjectMemberRoleId) REFERENCES [ProjectMemberRoles](Id),
    FOREIGN KEY (PermissionId) REFERENCES [Permissions](Id),
    CONSTRAINT UQ_ProjectRolePermission UNIQUE (ProjectMemberRoleId, PermissionId)
);
GO

CREATE TABLE [ProjectMembers]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProjectId INT NOT NULL,
    UserId INT NOT NULL,
    JoinDate DATE DEFAULT GETDATE(),
    RoleInProject INT NOT NULL,
    IsConfirmed BIT DEFAULT 0,
    ConfirmationDate DATETIME,
    IsActive BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProjectId) REFERENCES [Projects](Id),
    FOREIGN KEY (UserId) REFERENCES [Users](Id),
    FOREIGN KEY (RoleInProject) REFERENCES [ProjectMemberRoles](Id),
    CONSTRAINT UQ_project_member UNIQUE (ProjectId, UserId)
);
GO

CREATE TABLE [TaskStatuses]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(200),
    IsActive BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [TaskPriorities]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(200),
    IsActive BIT DEFAULT 1,
    IsDeleted BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [Tasks]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Code NVARCHAR(20) UNIQUE,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    ProjectId INT NOT NULL,
    AssignedUserId INT NOT NULL,
    StatusId INT NOT NULL,
    PriorityId INT NOT NULL,
    StartDate DATE,
    DueDate DATE,
    EstimatedHours DECIMAL(8, 2),
    ActualHours DECIMAL(8, 2),
    PercentComplete DECIMAL(5, 2) DEFAULT 0,
    ParentTaskId INT,
    LastStatusChangeDate DATETIME,
    BlockReason NVARCHAR(500),
    IsDeleted BIT DEFAULT 0,
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProjectId) REFERENCES [Projects](Id),
    FOREIGN KEY (AssignedUserId) REFERENCES [Users](Id),
    FOREIGN KEY (StatusId) REFERENCES [TaskStatuses](Id),
    FOREIGN KEY (PriorityId) REFERENCES [TaskPriorities](Id),
    FOREIGN KEY (ParentTaskId) REFERENCES [Tasks](Id),
    FOREIGN KEY (CreatedBy) REFERENCES [Users](Id),
	CONSTRAINT CHK_PercentComplete CHECK (PercentComplete BETWEEN 0 AND 100)
);
GO

CREATE TABLE [TaskHelpRequests]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    TaskId INT NOT NULL,
    RequestedBy INT NOT NULL,
    RequestedTo INT NOT NULL,
    RequestMessage NVARCHAR(500),
    IsResolved BIT DEFAULT 0,
    ResolvedDate DATETIME,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TaskId) REFERENCES [Tasks](Id),
    FOREIGN KEY (RequestedBy) REFERENCES [Users](Id),
    FOREIGN KEY (RequestedTo) REFERENCES [Users](Id)
);
GO

CREATE TABLE [TaskDependencies]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    TaskId INT NOT NULL,
    DependsOnTaskId INT NOT NULL,
    DependencyType INT NOT NULL CHECK (DependencyType BETWEEN 1 AND 4),
    -- 1: Finish to Start, 2: Start to Start, 3: Finish to Finish, 4: Start to Finish
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TaskId) REFERENCES [Tasks](Id),
    FOREIGN KEY (DependsOnTaskId) REFERENCES [Tasks](Id),
    CONSTRAINT UQ_task_dependency UNIQUE (TaskId, DependsOnTaskId),
	CONSTRAINT CHK_TaskDependency CHECK (TaskId != DependsOnTaskId)
);
GO

CREATE TABLE [TaskComments]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    TaskId INT NOT NULL,
    UserId INT NOT NULL,
    CommentText NVARCHAR(1000) NOT NULL,
    IsEdited BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TaskId) REFERENCES [Tasks](Id),
    FOREIGN KEY (UserId) REFERENCES [Users](Id)
);
GO

CREATE TABLE [TaskHistory]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    TaskId INT NOT NULL,
    FieldChanged NVARCHAR(50) NOT NULL,
    OldValue NVARCHAR(MAX),
    NewValue NVARCHAR(MAX),
    ChangedBy INT NOT NULL,
    ChangedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TaskId) REFERENCES [Tasks](Id),
    FOREIGN KEY (ChangedBy) REFERENCES [Users](Id)
);
GO

CREATE TABLE [TimeEntries]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    TaskId INT NOT NULL,
    UserId INT NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME,
    Description NVARCHAR(500),
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TaskId) REFERENCES [Tasks](Id),
    FOREIGN KEY (UserId) REFERENCES [Users](Id)
);
GO

CREATE TABLE [Files]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(255) NOT NULL,
    FilePath NVARCHAR(500) NOT NULL,
    FileSize BIGINT,
    FileType NVARCHAR(100),
    ProjectId INT,
    TaskId INT,
    UploadedBy INT NOT NULL,
    UploadDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProjectId) REFERENCES [Projects](Id),
    FOREIGN KEY (TaskId) REFERENCES [Tasks](Id),
    FOREIGN KEY (UploadedBy) REFERENCES [Users](Id)
);
GO

CREATE TABLE [NotificationTypes]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [Notifications]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    Title NVARCHAR(100) NOT NULL,
    Message NVARCHAR(500),
    NotificationTypeId INT NOT NULL,
    RelatedId INT,
    -- ID c?a d?i tu?ng li�n quan (task, project, v.v.)
    IsRead BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES [Users](Id),
    FOREIGN KEY (NotificationTypeId) REFERENCES [NotificationTypes](Id)
);
GO

CREATE TABLE [EmailNotifications]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    Subject NVARCHAR(200) NOT NULL,
    Body NVARCHAR(MAX),
    SentDate DATETIME DEFAULT GETDATE(),
    IsSuccessful BIT DEFAULT 1,
    ErrorMessage NVARCHAR(500),
    FOREIGN KEY (UserId) REFERENCES [Users](Id)
);
GO


CREATE TABLE [ReportTypes]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    TypeName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [Reports]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    ReportName NVARCHAR(100) NOT NULL,
    ReportTypeId INT NOT NULL,
    Description NVARCHAR(200),
    ReportData NVARCHAR(MAX),
    -- JSON data for the report
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CreatedBy) REFERENCES [Users](Id),
    FOREIGN KEY (ReportTypeId) REFERENCES [ReportTypes](Id)
);
GO

CREATE TABLE [AuditLogs]
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT,
    Action NVARCHAR(50) NOT NULL,
    Module NVARCHAR(50) NOT NULL,
    Description NVARCHAR(500),
    IpAddress NVARCHAR(50),
    LogDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserId) REFERENCES [Users](Id)
);
GO

