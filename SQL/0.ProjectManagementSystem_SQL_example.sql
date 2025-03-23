-- Tạo cơ sở dữ liệu Project Management System
CREATE DATABASE ProjectManagementSystem;
GO

USE ProjectManagementSystem;
GO

-- Bảng [Roles] - Lưu trữ các vai trò trong hệ thống
CREATE TABLE [Roles]
(
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);

-- Bảng [Users] - Lưu trữ thông tin người dùng
CREATE TABLE [Users]
(
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(128) NOT NULL,
    PasswordSalt NVARCHAR(128) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    FullName NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20),
    Address NVARCHAR(200),
    ProfilePicture VARBINARY(MAX),
    RoleID INT NOT NULL,
    LastLogin DATETIME,
    IsActive BIT DEFAULT 1,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (RoleID) REFERENCES [Roles](RoleID)
);

-- Bảng [Departments] - Lưu trữ các phòng ban
CREATE TABLE [Departments]
(
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(200),
    ManagerID INT,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ManagerID) REFERENCES [Users](UserID)
);

-- Bảng [Employees] - Lưu trữ thông tin cụ thể về nhân viên
CREATE TABLE [Employees]
(
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL UNIQUE,
    Position NVARCHAR(100),
    HireDate DATE,
    DepartmentID INT,
    ReportsTo INT,
    Salary DECIMAL(18, 2),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES [Users](UserID),
    FOREIGN KEY (DepartmentID) REFERENCES [Departments](DepartmentID),
    FOREIGN KEY (ReportsTo) REFERENCES [Employees](EmployeeID)
);

-- Bảng [ProjectStatus] - Lưu trữ trạng thái dự án
CREATE TABLE [ProjectStatus]
(
    StatusID INT PRIMARY KEY IDENTITY(1,1),
    StatusName NVARCHAR(50) NOT NULL,
    Description NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);

-- Bảng [Projects] - Lưu trữ thông tin dự án
CREATE TABLE [Projects]
(
    ProjectID INT PRIMARY KEY IDENTITY(1,1),
    ProjectName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    StartDate DATE,
    EndDate DATE,
    Budget DECIMAL(18, 2),
    StatusID INT NOT NULL,
    ManagerID INT NOT NULL,
    Priority INT DEFAULT 0,
    -- 0: Low, 1: Medium, 2: High
    PercentComplete DECIMAL(5, 2) DEFAULT 0,
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (StatusID) REFERENCES [ProjectStatus](StatusID),
    FOREIGN KEY (ManagerID) REFERENCES [Users](UserID),
    FOREIGN KEY (CreatedBy) REFERENCES [Users](UserID)
);

-- Bảng [ProjectMembers] - Lưu trữ thành viên của dự án
CREATE TABLE [ProjectMembers]
(
    ProjectMemberID INT PRIMARY KEY IDENTITY(1,1),
    ProjectID INT NOT NULL,
    UserID INT NOT NULL,
    JoinDate DATE DEFAULT GETDATE(),
    RoleInProject NVARCHAR(100),
    IsConfirmed BIT DEFAULT 0,
    ConfirmationDate DATETIME,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProjectID) REFERENCES [Projects](ProjectID),
    FOREIGN KEY (UserID) REFERENCES [Users](UserID),
    CONSTRAINT UQ_ProjectMember UNIQUE (ProjectID, UserID)
);

-- Bảng [TaskStatus] - Lưu trữ trạng thái nhiệm vụ
CREATE TABLE [TaskStatus]
(
    StatusID INT PRIMARY KEY IDENTITY(1,1),
    StatusName NVARCHAR(50) NOT NULL,
    -- New, In Progress, Blocked, Done, etc.
    Description NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);

-- Bảng [TaskPriority] - Lưu trữ mức độ ưu tiên nhiệm vụ
CREATE TABLE [TaskPriority]
(
    PriorityID INT PRIMARY KEY IDENTITY(1,1),
    PriorityName NVARCHAR(50) NOT NULL,
    -- Low, Medium, High, Urgent
    Description NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE()
);

-- Bảng [Tasks] - Lưu trữ thông tin nhiệm vụ
CREATE TABLE [Tasks]
(
    TaskID INT PRIMARY KEY IDENTITY(1,1),
    TaskName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    ProjectID INT NOT NULL,
    AssignedTo INT,
    StatusID INT NOT NULL,
    PriorityID INT NOT NULL,
    StartDate DATE,
    DueDate DATE,
    EstimatedHours DECIMAL(8, 2),
    ActualHours DECIMAL(8, 2),
    PercentComplete DECIMAL(5, 2) DEFAULT 0,
    ParentTaskID INT,
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProjectID) REFERENCES [Projects](ProjectID),
    FOREIGN KEY (AssignedTo) REFERENCES [Users](UserID),
    FOREIGN KEY (StatusID) REFERENCES [TaskStatus](StatusID),
    FOREIGN KEY (PriorityID) REFERENCES [TaskPriority](PriorityID),
    FOREIGN KEY (ParentTaskID) REFERENCES [Tasks](TaskID),
    FOREIGN KEY (CreatedBy) REFERENCES [Users](UserID)
);

-- Bảng [TaskComments] - Lưu trữ bình luận về nhiệm vụ
CREATE TABLE [TaskComments]
(
    CommentID INT PRIMARY KEY IDENTITY(1,1),
    TaskID INT NOT NULL,
    UserID INT NOT NULL,
    CommentText NVARCHAR(1000) NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (TaskID) REFERENCES [Tasks](TaskID),
    FOREIGN KEY (UserID) REFERENCES [Users](UserID)
);

-- Bảng [TaskHistory] - Lưu trữ lịch sử thay đổi nhiệm vụ
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

-- Bảng [HelpRequests] - Lưu trữ yêu cầu trợ giúp
CREATE TABLE [HelpRequests]
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

-- Bảng [Files] - Lưu trữ tập tin
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

-- Bảng [Notifications] - Lưu trữ thông báo
CREATE TABLE [Notifications]
(
    NotificationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    Title NVARCHAR(100) NOT NULL,
    Message NVARCHAR(500),
    IsRead BIT DEFAULT 0,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES [Users](UserID)
);

-- Bảng [EmailNotifications] - Lưu trữ thông tin email đã gửi
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

-- Bảng [ProjectPhases] - Lưu trữ các giai đoạn dự án
CREATE TABLE [ProjectPhases]
(
    PhaseID INT PRIMARY KEY IDENTITY(1,1),
    ProjectID INT NOT NULL,
    PhaseName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    StartDate DATE,
    EndDate DATE,
    StatusID INT,
    CreatedDate DATETIME DEFAULT GETDATE(),
    UpdatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProjectID) REFERENCES [Projects](ProjectID),
    FOREIGN KEY (StatusID) REFERENCES [ProjectStatus](StatusID)
);

-- Bảng [Reports] - Lưu trữ các báo cáo
CREATE TABLE [Reports]
(
    ReportID INT PRIMARY KEY IDENTITY(1,1),
    ReportName NVARCHAR(100) NOT NULL,
    ReportType NVARCHAR(50) NOT NULL,
    -- Project, Task, Performance
    Description NVARCHAR(200),
    ReportData NVARCHAR(MAX),
    -- JSON data for the report
    CreatedBy INT NOT NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CreatedBy) REFERENCES [Users](UserID)
);

-- Bảng [AuditLog] - Lưu trữ nhật ký hoạt động trong hệ thống
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

-- Bảng [UserPermissions] - Lưu trữ quyền của người dùng
CREATE TABLE [Permissions]
(
    PermissionID INT PRIMARY KEY IDENTITY(1,1),
    PermissionName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(200),
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- Bảng trung gian giữa vai trò và quyền
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

-- View để xem thống kê dự án
CREATE VIEW [ProjectStatistics]
AS
    SELECT
        p.ProjectID,
        p.ProjectName,
        p.StartDate,
        p.EndDate,
        p.Budget,
        ps.StatusName AS ProjectStatus,
        u.FullName AS ProjectManager,
        COUNT(DISTINCT pm.UserID) AS MemberCount,
        COUNT(DISTINCT t.TaskID) AS TaskCount,
        AVG(t.PercentComplete) AS AvgTaskCompletion,
        p.PercentComplete AS ProjectCompletion
    FROM
        [Projects] p
        INNER JOIN [ProjectStatus] ps ON p.StatusID = ps.StatusID
        INNER JOIN [Users] u ON p.ManagerID = u.UserID
        LEFT JOIN [ProjectMembers] pm ON p.ProjectID = pm.ProjectID
        LEFT JOIN [Tasks] t ON p.ProjectID = t.ProjectID
    GROUP BY
    p.ProjectID, p.ProjectName, p.StartDate, p.EndDate, p.Budget,
    ps.StatusName, u.FullName, p.PercentComplete;
GO
-- View để xem thống kê nhiệm vụ
CREATE VIEW [TaskStatistics]
AS
    SELECT
        t.TaskID,
        t.TaskName,
        p.ProjectName,
        t.StartDate,
        t.DueDate,
        ts.StatusName AS TaskStatus,
        tp.PriorityName AS Priority,
        u.FullName AS AssignedTo,
        t.EstimatedHours,
        t.ActualHours,
        t.PercentComplete,
        DATEDIFF(DAY, t.StartDate, t.DueDate) AS Duration,
        CASE
        WHEN t.DueDate < GETDATE() AND t.PercentComplete < 100 THEN 'Overdue'
        WHEN t.DueDate = GETDATE() AND t.PercentComplete < 100 THEN 'Due Today'
        WHEN DATEDIFF(DAY, GETDATE(), t.DueDate) <= 3 AND t.PercentComplete < 100 THEN 'Upcoming'
        ELSE 'On Track'
    END AS TimeStatus
    FROM
        [Tasks] t
        INNER JOIN [Projects] p ON t.ProjectID = p.ProjectID
        INNER JOIN [TaskStatus] ts ON t.StatusID = ts.StatusID
        INNER JOIN [TaskPriority] tp ON t.PriorityID = tp.PriorityID
        LEFT JOIN [Users] u ON t.AssignedTo = u.UserID;
GO
-- View để xem thống kê người dùng
CREATE VIEW [UserStatistics]
AS
    SELECT
        u.UserID,
        u.FullName,
        r.RoleName,
        d.DepartmentName,
        COUNT(DISTINCT pm.ProjectID) AS ProjectCount,
        COUNT(DISTINCT t.TaskID) AS TaskCount,
        AVG(t.PercentComplete) AS AvgTaskCompletion,
        COUNT(CASE WHEN t.DueDate < GETDATE() AND t.PercentComplete < 100 THEN 1 END) AS OverdueTasks
    FROM
        [Users] u
        INNER JOIN [Roles] r ON u.RoleID = r.RoleID
        LEFT JOIN [Employees] e ON u.UserID = e.UserID
        LEFT JOIN [Departments] d ON e.DepartmentID = d.DepartmentID
        LEFT JOIN [ProjectMembers] pm ON u.UserID = pm.UserID
        LEFT JOIN [Tasks] t ON u.UserID = t.AssignedTo
    GROUP BY
    u.UserID, u.FullName, r.RoleName, d.DepartmentName;
GO

-- Chèn dữ liệu mẫu vào bảng [Roles]
INSERT INTO [Roles]
    (RoleName, Description)
VALUES
    ('Admin', N'Quản trị viên hệ thống'),
    ('ProjectManager', N'Quản lý dự án'),
    ('Employee', N'Nhân viên');

-- Chèn dữ liệu mẫu vào bảng [ProjectStatus]
INSERT INTO [ProjectStatus]
    (StatusName, Description)
VALUES
    ('Pending', N'Dự án đang chờ bắt đầu'),
    ('Active', N'Dự án đang hoạt động'),
    ('On Hold', N'Dự án tạm dừng'),
    ('Completed', N'Dự án đã hoàn thành'),
    ('Cancelled', N'Dự án đã hủy');

-- Chèn dữ liệu mẫu vào bảng [TaskStatus]
INSERT INTO [TaskStatus]
    (StatusName, Description)
VALUES
    ('New', N'Nhiệm vụ mới'),
    ('In Progress', N'Đang tiến hành'),
    ('On Hold', N'Tạm dừng'),
    ('Blocked', N'Bị chặn/Không thể tiếp tục'),
    ('Review', N'Đang kiểm tra'),
    ('Completed', N'Đã hoàn thành');

-- Chèn dữ liệu mẫu vào bảng [TaskPriority]
INSERT INTO [TaskPriority]
    (PriorityName, Description)
VALUES
    ('Low', N'Ưu tiên thấp'),
    ('Medium', N'Ưu tiên trung bình'),
    ('High', N'Ưu tiên cao'),
    ('Urgent', N'Khẩn cấp');
GO

-- Tạo Stored Procedure để tạo tài khoản người dùng
CREATE PROCEDURE [usp_CreateUser]
    @Username NVARCHAR(50),
    @Password NVARCHAR(100),
    @Email NVARCHAR(100),
    @FullName NVARCHAR(100),
    @PhoneNumber NVARCHAR(20) = NULL,
    @Address NVARCHAR(200) = NULL,
    @RoleID INT,
    @UserID INT OUTPUT
AS
BEGIN
    DECLARE @PasswordSalt NVARCHAR(128) = CONVERT(NVARCHAR(128), NEWID());
    DECLARE @PasswordHash NVARCHAR(128) = CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', @Password + @PasswordSalt));

    INSERT INTO [Users]
        (Username, PasswordHash, PasswordSalt, Email, FullName, PhoneNumber, Address, RoleID)
    VALUES
        (@Username, @PasswordHash, @PasswordSalt, @Email, @FullName, @PhoneNumber, @Address, @RoleID);

    SET @UserID = SCOPE_IDENTITY();
END;
GO

-- Tạo Stored Procedure để xác thực đăng nhập
CREATE PROCEDURE [usp_AuthenticateUser]
    @Username NVARCHAR(50),
    @Password NVARCHAR(100),
    @IsAuthenticated BIT OUTPUT,
    @UserID INT OUTPUT
AS
BEGIN
    DECLARE @PasswordSalt NVARCHAR(128);
    DECLARE @StoredPasswordHash NVARCHAR(128);
    DECLARE @CalcPasswordHash NVARCHAR(128);

    SELECT
        @UserID = UserID,
        @PasswordSalt = PasswordSalt,
        @StoredPasswordHash = PasswordHash
    FROM [Users]
    WHERE Username = @Username AND IsActive = 1;

    IF @UserID IS NULL
    BEGIN
        SET @IsAuthenticated = 0;
        RETURN;
    END

    SET @CalcPasswordHash = CONVERT(NVARCHAR(128), HASHBYTES('SHA2_512', @Password + @PasswordSalt));

    IF @StoredPasswordHash = @CalcPasswordHash
    BEGIN
        SET @IsAuthenticated = 1;
        UPDATE [Users] SET LastLogin = GETDATE() WHERE UserID = @UserID;
    END
    ELSE
    BEGIN
        SET @IsAuthenticated = 0;
    END
END;
GO

-- Tạo Stored Procedure để thêm/cập nhật dự án
CREATE PROCEDURE [usp_SaveProject]
    @ProjectID INT = NULL,
    @ProjectName NVARCHAR(100),
    @Description NVARCHAR(500) = NULL,
    @StartDate DATE = NULL,
    @EndDate DATE = NULL,
    @Budget DECIMAL(18, 2) = NULL,
    @StatusID INT,
    @ManagerID INT,
    @Priority INT = 0,
    @CreatedBy INT,
    @NewProjectID INT OUTPUT
AS
BEGIN
    IF @ProjectID IS NULL
    BEGIN
        -- Thêm mới dự án
        INSERT INTO [Projects]
            (ProjectName, Description, StartDate, EndDate, Budget, StatusID, ManagerID, Priority, CreatedBy)
        VALUES
            (@ProjectName, @Description, @StartDate, @EndDate, @Budget, @StatusID, @ManagerID, @Priority, @CreatedBy);

        SET @NewProjectID = SCOPE_IDENTITY();

        -- Tự động thêm quản lý dự án vào thành viên dự án
        INSERT INTO [ProjectMembers]
            (ProjectID, UserID, JoinDate, RoleInProject, IsConfirmed)
        VALUES
            (@NewProjectID, @ManagerID, GETDATE(), 'Project Manager', 1);
    END
    ELSE
    BEGIN
        -- Cập nhật dự án
        UPDATE [Projects]
        SET ProjectName = @ProjectName,
            Description = @Description,
            StartDate = @StartDate,
            EndDate = @EndDate,
            Budget = @Budget,
            StatusID = @StatusID,
            ManagerID = @ManagerID,
            Priority = @Priority,
            UpdatedDate = GETDATE()
        WHERE ProjectID = @ProjectID;

        SET @NewProjectID = @ProjectID;
    END
END;
GO

-- Tạo Stored Procedure để thêm/cập nhật nhiệm vụ
CREATE PROCEDURE [usp_SaveTask]
    @TaskID INT = NULL,
    @TaskName NVARCHAR(100),
    @Description NVARCHAR(500) = NULL,
    @ProjectID INT,
    @AssignedTo INT = NULL,
    @StatusID INT,
    @PriorityID INT,
    @StartDate DATE = NULL,
    @DueDate DATE = NULL,
    @EstimatedHours DECIMAL(8, 2) = NULL,
    @ParentTaskID INT = NULL,
    @CreatedBy INT,
    @NewTaskID INT OUTPUT
AS
BEGIN
    IF @TaskID IS NULL
    BEGIN
        -- Thêm mới nhiệm vụ
        INSERT INTO [Tasks]
            (TaskName, Description, ProjectID, AssignedTo, StatusID, PriorityID, StartDate, DueDate, EstimatedHours, ParentTaskID, CreatedBy)
        VALUES
            (@TaskName, @Description, @ProjectID, @AssignedTo, @StatusID, @PriorityID, @StartDate, @DueDate, @EstimatedHours, @ParentTaskID, @CreatedBy);

        SET @NewTaskID = SCOPE_IDENTITY();

        -- Tạo thông báo cho người được giao nhiệm vụ
        IF @AssignedTo IS NOT NULL
        BEGIN
            INSERT INTO [Notifications]
                (UserID, Title, Message)
            VALUES
                (@AssignedTo, N'Nhiệm vụ mới', N'Bạn đã được giao một nhiệm vụ mới: ' + @TaskName);
        END
    END
    ELSE
    BEGIN
        -- Lưu trữ giá trị hiện tại
        DECLARE @OldAssignedTo INT, @OldStatusID INT;
        SELECT @OldAssignedTo = AssignedTo, @OldStatusID = StatusID
        FROM [Tasks]
        WHERE TaskID = @TaskID;

        -- Cập nhật nhiệm vụ
        UPDATE [Tasks]
        SET TaskName = @TaskName,
            Description = @Description,
            ProjectID = @ProjectID,
            AssignedTo = @AssignedTo,
            StatusID = @StatusID,
            PriorityID = @PriorityID,
            StartDate = @StartDate,
            DueDate = @DueDate,
            EstimatedHours = @EstimatedHours,
            ParentTaskID = @ParentTaskID,
            UpdatedDate = GETDATE()
        WHERE TaskID = @TaskID;

        SET @NewTaskID = @TaskID;

        -- Lưu lịch sử thay đổi
        IF @OldStatusID <> @StatusID
        BEGIN
            DECLARE @OldStatusName NVARCHAR(50), @NewStatusName NVARCHAR(50);
            SELECT @OldStatusName = StatusName
            FROM [TaskStatus]
            WHERE StatusID = @OldStatusID;
            SELECT @NewStatusName = StatusName
            FROM [TaskStatus]
            WHERE StatusID = @StatusID;

            INSERT INTO [TaskHistory]
                (TaskID, FieldChanged, OldValue, NewValue, ChangedBy)
            VALUES
                (@TaskID, 'Status', @OldStatusName, @NewStatusName, @CreatedBy);
        END

        -- Tạo thông báo khi người được giao thay đổi
        IF @OldAssignedTo <> @AssignedTo AND @AssignedTo IS NOT NULL
        BEGIN
            INSERT INTO [Notifications]
                (UserID, Title, Message)
            VALUES
                (@AssignedTo, N'Nhiệm vụ mới được giao', N'Bạn đã được giao nhiệm vụ: ' + @TaskName);
        END
    END
END;
GO

-- Tạo Stored Procedure để cập nhật tiến độ của nhiệm vụ
CREATE PROCEDURE [usp_UpdateTaskProgress]
    @TaskID INT,
    @PercentComplete DECIMAL(5, 2),
    @ActualHours DECIMAL(8, 2) = NULL,
    @StatusID INT = NULL,
    @UpdatedBy INT
AS
BEGIN
    DECLARE @OldPercentComplete DECIMAL(5, 2), @OldStatusID INT;
    SELECT @OldPercentComplete = PercentComplete, @OldStatusID = StatusID
    FROM [Tasks]
    WHERE TaskID = @TaskID;

    -- Cập nhật tiến độ
    UPDATE [Tasks]
    SET PercentComplete = @PercentComplete,
        ActualHours = ISNULL(@ActualHours, ActualHours),
        StatusID = ISNULL(@StatusID, StatusID),
        UpdatedDate = GETDATE()
    WHERE TaskID = @TaskID;

    -- Thêm lịch sử
    INSERT INTO [TaskHistory]
        (TaskID, FieldChanged, OldValue, NewValue, ChangedBy)
    VALUES
        (@TaskID, 'PercentComplete', CAST(@OldPercentComplete AS NVARCHAR), CAST(@PercentComplete AS NVARCHAR), @UpdatedBy);

    -- Nếu trạng thái thay đổi, thêm vào lịch sử
    IF @StatusID IS NOT NULL AND @OldStatusID <> @StatusID
    BEGIN
        DECLARE @OldStatusName NVARCHAR(50), @NewStatusName NVARCHAR(50);
        SELECT @OldStatusName = StatusName
        FROM [TaskStatus]
        WHERE StatusID = @OldStatusID;
        SELECT @NewStatusName = StatusName
        FROM [TaskStatus]
        WHERE StatusID = @StatusID;

        INSERT INTO [TaskHistory]
            (TaskID, FieldChanged, OldValue, NewValue, ChangedBy)
        VALUES
            (@TaskID, 'Status', @OldStatusName, @NewStatusName, @UpdatedBy);
    END

    -- Cập nhật tiến độ dự án
    DECLARE @ProjectID INT;
    SELECT @ProjectID = ProjectID
    FROM [Tasks]
    WHERE TaskID = @TaskID;

    UPDATE [Projects]
    SET PercentComplete = (
        SELECT AVG(PercentComplete)
    FROM [Tasks]
    WHERE ProjectID = @ProjectID
    ),
    UpdatedDate = GETDATE()
    WHERE ProjectID = @ProjectID;
END;
GO

-- Tạo Stored Procedure để gửi yêu cầu trợ giúp
CREATE PROCEDURE [usp_RequestHelp]
    @TaskID INT,
    @RequestedBy INT,
    @RequestedTo INT,
    @RequestMessage NVARCHAR(500),
    @RequestID INT OUTPUT
AS
BEGIN
    INSERT INTO [HelpRequests]
        (TaskID, RequestedBy, RequestedTo, RequestMessage)
    VALUES
        (@TaskID, @RequestedBy, @RequestedTo, @RequestMessage);

    SET @RequestID = SCOPE_IDENTITY();

    -- Tạo thông báo cho người được yêu cầu trợ giúp
    DECLARE @TaskName NVARCHAR(100), @RequesterName NVARCHAR(100);
    SELECT @TaskName = TaskName
    FROM [Tasks]
    WHERE TaskID = @TaskID;
    SELECT @RequesterName = FullName
    FROM [Users]
    WHERE UserID = @RequestedBy;

    INSERT INTO [Notifications]
        (UserID, Title, Message)
    VALUES
        (
            @RequestedTo,
            N'Yêu cầu trợ giúp',
            @RequesterName + N' cần sự trợ giúp của bạn với nhiệm vụ: ' + @TaskName
    );
END;
GO

-- Tạo Stored Procedure để cập nhật nhiều trạng thái dự án
CREATE PROCEDURE [usp_UpdateProjectStatistics]
AS
BEGIN
    -- Cập nhật tiến độ cho tất cả các dự án
    UPDATE p
    SET p.PercentComplete = ISNULL(t.AvgCompletion, 0),
        p.UpdatedDate = GETDATE()
    FROM [Projects] p
        LEFT JOIN (
        SELECT
            ProjectID,
            AVG(PercentComplete) AS AvgCompletion
        FROM [Tasks]
        GROUP BY ProjectID
    ) t ON p.ProjectID = t.ProjectID;

    -- Cập nhật trạng thái dự án nếu tất cả nhiệm vụ hoàn thành
    UPDATE p
    SET p.StatusID = (SELECT StatusID
    FROM [ProjectStatus]
    WHERE StatusName = 'Completed'),
        p.UpdatedDate = GETDATE()
    FROM [Projects] p
    WHERE p.PercentComplete = 100
        AND p.StatusID <> (SELECT StatusID
        FROM [ProjectStatus]
        WHERE StatusName = 'Completed');
END;
GO

-- Tạo Stored Procedure để lấy tất cả dự án của người dùng
CREATE PROCEDURE [usp_GetUserProjects]
    @UserID INT
AS
BEGIN
    SELECT
        p.ProjectID,
        p.ProjectName,
        p.Description,
        p.StartDate,
        p.EndDate,
        p.PercentComplete,
        ps.StatusName,
        pm.RoleInProject,
        pm.IsConfirmed
    FROM
        [Projects] p
        INNER JOIN
        [ProjectMembers] pm ON p.ProjectID = pm.ProjectID
        INNER JOIN
        [ProjectStatus] ps ON p.StatusID = ps.StatusID
    WHERE
        pm.UserID = @UserID
    ORDER BY
        p.EndDate ASC, p.StartDate ASC;
END;
GO

-- Tạo Stored Procedure để lấy tất cả nhiệm vụ của người dùng
CREATE PROCEDURE [usp_GetUserTasks]
    @UserID INT
AS
BEGIN
    SELECT
        t.TaskID,
        t.TaskName,
        t.Description,
        p.ProjectName,
        t.StartDate,
        t.DueDate,
        t.PercentComplete,
        ts.StatusName,
        tp.PriorityName,
        t.EstimatedHours,
        t.ActualHours,
        CASE
            WHEN t.DueDate < GETDATE() AND t.PercentComplete < 100 THEN 'Overdue'
            WHEN t.DueDate = GETDATE() AND t.PercentComplete < 100 THEN 'Due Today'
            WHEN DATEDIFF(DAY, GETDATE(), t.DueDate) <= 3 AND t.PercentComplete < 100 THEN 'Upcoming'
            ELSE 'On Track'
        END AS TimeStatus
    FROM
        [Tasks] t
        INNER JOIN
        [Projects] p ON t.ProjectID = p.ProjectID
        INNER JOIN
        [TaskStatus] ts ON t.StatusID = ts.StatusID
        INNER JOIN
        [TaskPriority] tp ON t.PriorityID = tp.PriorityID
    WHERE
        t.AssignedTo = @UserID
    ORDER BY
        TimeStatus, t.DueDate ASC;
END;
GO

-- Tạo Stored Procedure để xác nhận tham gia dự án
CREATE PROCEDURE [usp_ConfirmProjectParticipation]
    @ProjectMemberID INT,
    @UserID INT
AS
BEGIN
    DECLARE @MemberUserID INT, @ProjectID INT, @ProjectName NVARCHAR(100);

    SELECT @MemberUserID = UserID, @ProjectID = ProjectID
    FROM [ProjectMembers]
    WHERE ProjectMemberID = @ProjectMemberID;

    SELECT @ProjectName = ProjectName
    FROM [Projects]
    WHERE ProjectID = @ProjectID;

    -- Kiểm tra xem người dùng có phải là người được mời tham gia không
    IF @MemberUserID = @UserID
    BEGIN
        UPDATE [ProjectMembers]
        SET IsConfirmed = 1,
            ConfirmationDate = GETDATE(),
            UpdatedDate = GETDATE()
        WHERE ProjectMemberID = @ProjectMemberID;

        -- Thông báo cho quản lý dự án
        DECLARE @ManagerID INT, @UserName NVARCHAR(100);
        SELECT @ManagerID = ManagerID
        FROM [Projects]
        WHERE ProjectID = @ProjectID;
        SELECT @UserName = FullName
        FROM [Users]
        WHERE UserID = @UserID;

        INSERT INTO [Notifications]
            (UserID, Title, Message)
        VALUES
            (
                @ManagerID,
                N'Xác nhận tham gia dự án',
                @UserName + N' đã xác nhận tham gia dự án: ' + @ProjectName
        );

        SELECT 'Success' AS Result;
    END
    ELSE
    BEGIN
        SELECT 'Unauthorized' AS Result;
    END
END;
GO

-- Tạo Stored Procedure để kiểm tra hoàn thành task
CREATE PROCEDURE [usp_ReviewTaskCompletion]
    @TaskID INT,
    @IsApproved BIT,
    @ReviewerID INT,
    @Comments NVARCHAR(500) = NULL
AS
BEGIN
    DECLARE @ProjectID INT, @AssignedTo INT, @TaskName NVARCHAR(100);
    SELECT
        @ProjectID = ProjectID,
        @AssignedTo = AssignedTo,
        @TaskName = TaskName
    FROM [Tasks]
    WHERE TaskID = @TaskID;

    -- Kiểm tra xem người review có phải là quản lý dự án hay không
    DECLARE @ManagerID INT, @AssigneeName NVARCHAR(100);
    SELECT @ManagerID = ManagerID
    FROM [Projects]
    WHERE ProjectID = @ProjectID;
    SELECT @AssigneeName = FullName
    FROM [Users]
    WHERE UserID = @AssignedTo;

    IF @ReviewerID = @ManagerID OR EXISTS (SELECT 1
        FROM [Users]
        WHERE UserID = @ReviewerID AND RoleID = 1) -- Admin hoặc PM
    BEGIN
        IF @IsApproved = 1
        BEGIN
            -- Cập nhật trạng thái task thành hoàn thành
            UPDATE [Tasks]
            SET StatusID = (SELECT StatusID
            FROM [TaskStatus]
            WHERE StatusName = 'Completed'),
                PercentComplete = 100,
                UpdatedDate = GETDATE()
            WHERE TaskID = @TaskID;

            -- Ghi lại lịch sử
            INSERT INTO [TaskHistory]
                (TaskID, FieldChanged, OldValue, NewValue, ChangedBy)
            VALUES
                (
                    @TaskID,
                    'Status',
                    'Review',
                    'Completed',
                    @ReviewerID
            );

            -- Thông báo cho người được giao nhiệm vụ
            INSERT INTO [Notifications]
                (UserID, Title, Message)
            VALUES
                (
                    @AssignedTo,
                    N'Task đã được phê duyệt',
                    N'Nhiệm vụ "' + @TaskName + N'" của bạn đã được phê duyệt hoàn thành.'
            );
        END
        ELSE
        BEGIN
            -- Cập nhật trạng thái task về InProgress
            UPDATE [Tasks]
            SET StatusID = (SELECT StatusID
            FROM [TaskStatus]
            WHERE StatusName = 'In Progress'),
                UpdatedDate = GETDATE()
            WHERE TaskID = @TaskID;

            -- Ghi lại lịch sử
            INSERT INTO [TaskHistory]
                (TaskID, FieldChanged, OldValue, NewValue, ChangedBy)
            VALUES
                (
                    @TaskID,
                    'Status',
                    'Review',
                    'In Progress',
                    @ReviewerID
            );

            -- Thêm comment
            IF @Comments IS NOT NULL
            BEGIN
                INSERT INTO [TaskComments]
                    (TaskID, UserID, CommentText)
                VALUES
                    (@TaskID, @ReviewerID, @Comments);
            END

            -- Thông báo cho người được giao nhiệm vụ
            INSERT INTO [Notifications]
                (UserID, Title, Message)
            VALUES
                (
                    @AssignedTo,
                    N'Task cần chỉnh sửa thêm',
                    N'Nhiệm vụ "' + @TaskName + N'" cần được chỉnh sửa: ' + ISNULL(@Comments, N'Không có mô tả chi tiết')
            );
        END

        SELECT 'Success' AS Result;
    END
    ELSE
    BEGIN
        SELECT 'Unauthorized' AS Result;
    END
END;
GO

-- Tạo Stored Procedure để tìm kiếm nâng cao
CREATE PROCEDURE [usp_SearchItems]
    @SearchText NVARCHAR(100),
    @ItemType NVARCHAR(20),
    -- 'Project', 'Task', 'User'
    @UserID INT
AS
BEGIN
    IF @ItemType = 'Project'
    BEGIN
        -- Tìm kiếm dự án
        SELECT
            p.ProjectID,
            p.ProjectName,
            p.Description,
            p.StartDate,
            p.EndDate,
            ps.StatusName,
            u.FullName AS Manager,
            p.PercentComplete
        FROM
            [Projects] p
            INNER JOIN
            [Users] u ON p.ManagerID = u.UserID
            INNER JOIN
            [ProjectStatus] ps ON p.StatusID = ps.StatusID
            INNER JOIN
            [ProjectMembers] pm ON p.ProjectID = pm.ProjectID
        WHERE
            (p.ProjectName LIKE '%' + @SearchText + '%' OR
            p.Description LIKE '%' + @SearchText + '%') AND
            pm.UserID = @UserID
        ORDER BY
            p.StartDate DESC;
    END
    ELSE IF @ItemType = 'Task'
    BEGIN
        -- Tìm kiếm nhiệm vụ
        SELECT
            t.TaskID,
            t.TaskName,
            t.Description,
            p.ProjectName,
            ts.StatusName,
            tp.PriorityName,
            t.StartDate,
            t.DueDate,
            t.PercentComplete
        FROM
            [Tasks] t
            INNER JOIN
            [Projects] p ON t.ProjectID = p.ProjectID
            INNER JOIN
            [TaskStatus] ts ON t.StatusID = ts.StatusID
            INNER JOIN
            [TaskPriority] tp ON t.PriorityID = tp.PriorityID
        WHERE
            (t.TaskName LIKE '%' + @SearchText + '%' OR
            t.Description LIKE '%' + @SearchText + '%') AND
            (t.AssignedTo = @UserID OR
            p.ManagerID = @UserID OR
            EXISTS (SELECT 1
            FROM [Users]
            WHERE UserID = @UserID AND RoleID = 1))
        -- Admin
        ORDER BY
            t.DueDate ASC;
    END
    ELSE IF @ItemType = 'User'
    BEGIN
        -- Tìm kiếm người dùng
        SELECT
            u.UserID,
            u.FullName,
            u.Email,
            r.RoleName,
            d.DepartmentName
        FROM
            [Users] u
            INNER JOIN
            [Roles] r ON u.RoleID = r.RoleID
            LEFT JOIN
            [Employees] e ON u.UserID = e.UserID
            LEFT JOIN
            [Departments] d ON e.DepartmentID = d.DepartmentID
        WHERE
            u.FullName LIKE '%' + @SearchText + '%' OR
            u.Email LIKE '%' + @SearchText + '%' OR
            u.Username LIKE '%' + @SearchText + '%'
        ORDER BY
            u.FullName;
    END
END;
GO

-- Tạo Stored Procedure để tạo báo cáo project
CREATE PROCEDURE [usp_GenerateProjectReport]
    @ProjectID INT,
    @ReportType NVARCHAR(50),
    -- 'Summary', 'Detailed', 'Timeline'
    @CreatedBy INT,
    @ReportID INT OUTPUT
AS
BEGIN
    DECLARE @ProjectName NVARCHAR(100), @ReportName NVARCHAR(100), @ReportData NVARCHAR(MAX);

    SELECT @ProjectName = ProjectName
    FROM [Projects]
    WHERE ProjectID = @ProjectID;
    SET @ReportName = @ProjectName + ' - ' + @ReportType + ' Report';

    IF @ReportType = 'Summary'
    BEGIN
        -- Tạo báo cáo tóm tắt dự án dạng JSON
        SET @ReportData = (
            SELECT
            p.ProjectID,
            p.ProjectName,
            p.Description,
            p.StartDate,
            p.EndDate,
            p.Budget,
            ps.StatusName,
            u.FullName AS Manager,
            p.PercentComplete,
            (
                    SELECT COUNT(*)
            FROM [ProjectMembers]
            WHERE ProjectID = p.ProjectID
                ) AS MemberCount,
            (
                    SELECT COUNT(*)
            FROM [Tasks]
            WHERE ProjectID = p.ProjectID
                ) AS TaskCount,
            (
                    SELECT COUNT(*)
            FROM [Tasks]
            WHERE ProjectID = p.ProjectID
                AND StatusID = (SELECT StatusID
                FROM [TaskStatus]
                WHERE StatusName = 'Completed')
                ) AS CompletedTaskCount,
            (
                    SELECT COUNT(*)
            FROM [Tasks]
            WHERE ProjectID = p.ProjectID
                AND DueDate < GETDATE()
                AND StatusID <> (SELECT StatusID
                FROM [TaskStatus]
                WHERE StatusName = 'Completed')
                ) AS OverdueTaskCount
        FROM
            [Projects] p
            INNER JOIN
            [Users] u ON p.ManagerID = u.UserID
            INNER JOIN
            [ProjectStatus] ps ON p.StatusID = ps.StatusID
        WHERE
                p.ProjectID = @ProjectID
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        );
    END
    ELSE IF @ReportType = 'Detailed'
    BEGIN
        -- Tạo báo cáo chi tiết dự án dạng JSON
        SET @ReportData = (
            SELECT
            p.ProjectID,
            p.ProjectName,
            p.Description,
            p.StartDate,
            p.EndDate,
            p.Budget,
            ps.StatusName,
            u.FullName AS Manager,
            p.PercentComplete,
            (
                    SELECT
                pm.UserID,
                u.FullName,
                pm.RoleInProject,
                pm.JoinDate,
                pm.IsConfirmed
            FROM
                [ProjectMembers] pm
                INNER JOIN
                [Users] u ON pm.UserID = u.UserID
            WHERE
                        pm.ProjectID = p.ProjectID
            FOR JSON PATH
                ) AS Members,
            (
                    SELECT
                t.TaskID,
                t.TaskName,
                t.Description,
                ts.StatusName,
                tp.PriorityName,
                t.StartDate,
                t.DueDate,
                t.PercentComplete,
                t.EstimatedHours,
                t.ActualHours,
                u.FullName AS AssignedTo
            FROM
                [Tasks] t
                INNER JOIN
                [TaskStatus] ts ON t.StatusID = ts.StatusID
                INNER JOIN
                [TaskPriority] tp ON t.PriorityID = tp.PriorityID
                LEFT JOIN
                [Users] u ON t.AssignedTo = u.UserID
            WHERE
                        t.ProjectID = p.ProjectID
            FOR JSON PATH
                ) AS Tasks,
            (
                    SELECT
                pp.PhaseID,
                pp.PhaseName,
                pp.Description,
                pp.StartDate,
                pp.EndDate,
                ps.StatusName
            FROM
                [ProjectPhases] pp
                INNER JOIN
                [ProjectStatus] ps ON pp.StatusID = ps.StatusID
            WHERE
                        pp.ProjectID = p.ProjectID
            FOR JSON PATH
                ) AS Phases
        FROM
            [Projects] p
            INNER JOIN
            [Users] u ON p.ManagerID = u.UserID
            INNER JOIN
            [ProjectStatus] ps ON p.StatusID = ps.StatusID
        WHERE
                p.ProjectID = @ProjectID
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        );
    END
    ELSE IF @ReportType = 'Timeline'
    BEGIN
        -- Tạo báo cáo lịch trình dự án dạng JSON
        SET @ReportData = (
            SELECT
            p.ProjectID,
            p.ProjectName,
            p.StartDate,
            p.EndDate,
            (
                    SELECT
                t.TaskID,
                t.TaskName,
                t.StartDate,
                t.DueDate,
                t.PercentComplete,
                ts.StatusName,
                u.FullName AS AssignedTo
            FROM
                [Tasks] t
                INNER JOIN
                [TaskStatus] ts ON t.StatusID = ts.StatusID
                LEFT JOIN
                [Users] u ON t.AssignedTo = u.UserID
            WHERE
                        t.ProjectID = p.ProjectID
            ORDER BY
                        t.StartDate, t.DueDate
            FOR JSON PATH
                ) AS Timeline
        FROM
            [Projects] p
        WHERE
                p.ProjectID = @ProjectID
        FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        );
    END

    -- Lưu báo cáo vào bảng Reports
    INSERT INTO [Reports]
        (ReportName, ReportType, Description, ReportData, CreatedBy)
    VALUES
        (@ReportName, @ReportType, 'Report for project: ' + @ProjectName, @ReportData, @CreatedBy);

    SET @ReportID = SCOPE_IDENTITY();
END;
GO

-- Tạo Index để tối ưu hiệu suất truy vấn
CREATE NONCLUSTERED INDEX IX_Projects_ManagerID ON [Projects] (ManagerID);
CREATE NONCLUSTERED INDEX IX_Projects_StatusID ON [Projects] (StatusID);
CREATE NONCLUSTERED INDEX IX_Tasks_ProjectID ON [Tasks] (ProjectID);
CREATE NONCLUSTERED INDEX IX_Tasks_AssignedTo ON [Tasks] (AssignedTo);
CREATE NONCLUSTERED INDEX IX_Tasks_StatusID ON [Tasks] (StatusID);
CREATE NONCLUSTERED INDEX IX_Tasks_PriorityID ON [Tasks] (PriorityID);
CREATE NONCLUSTERED INDEX IX_ProjectMembers_ProjectID ON [ProjectMembers] (ProjectID);
CREATE NONCLUSTERED INDEX IX_ProjectMembers_UserID ON [ProjectMembers] (UserID);
CREATE NONCLUSTERED INDEX IX_Users_RoleID ON [Users] (RoleID);
CREATE NONCLUSTERED INDEX IX_Notifications_UserID ON [Notifications] (UserID);
CREATE NONCLUSTERED INDEX IX_HelpRequests_TaskID ON [HelpRequests] (TaskID);
CREATE NONCLUSTERED INDEX IX_Files_ProjectID ON [Files] (ProjectID);
CREATE NONCLUSTERED INDEX IX_Files_TaskID ON [Files] (TaskID);
GO

-- Tạo trigger để tự động cập nhật tiến độ dự án khi nhiệm vụ được cập nhật
CREATE TRIGGER trg_UpdateProjectProgress
ON [Tasks]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Lấy danh sách ProjectID bị ảnh hưởng
    DECLARE @AffectedProjects TABLE (ProjectID INT);

    -- Từ INSERT và UPDATE
    INSERT INTO @AffectedProjects
        (ProjectID)
    SELECT DISTINCT ProjectID
    FROM inserted;

    -- Từ DELETE
    INSERT INTO @AffectedProjects
        (ProjectID)
    SELECT DISTINCT ProjectID
    FROM deleted
    WHERE ProjectID NOT IN (SELECT ProjectID
    FROM @AffectedProjects);

    -- Cập nhật tiến độ cho mỗi dự án bị ảnh hưởng
    UPDATE p
    SET p.PercentComplete = ISNULL(t.AvgCompletion, 0),
        p.UpdatedDate = GETDATE()
    FROM [Projects] p
        INNER JOIN @AffectedProjects ap ON p.ProjectID = ap.ProjectID
        LEFT JOIN (
        SELECT
            ProjectID,
            AVG(PercentComplete) AS AvgCompletion
        FROM [Tasks]
        GROUP BY ProjectID
    ) t ON p.ProjectID = t.ProjectID;
END;
GO

-- Tạo trigger để tự động gửi thông báo khi nhiệm vụ gần hạn
CREATE TRIGGER trg_TaskDueReminder
ON [Tasks]
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Tìm các nhiệm vụ sắp đến hạn (3 ngày)
    INSERT INTO [Notifications]
        (UserID, Title, Message)
    SELECT
        t.AssignedTo,
        N'Nhiệm vụ sắp đến hạn',
        N'Nhiệm vụ "' + t.TaskName + N'" sẽ đến hạn trong ' +
        CAST(DATEDIFF(DAY, GETDATE(), t.DueDate) AS NVARCHAR) + N' ngày nữa.'
    FROM
        inserted t
    WHERE
        t.AssignedTo IS NOT NULL AND
        t.DueDate IS NOT NULL AND
        DATEDIFF(DAY, GETDATE(), t.DueDate) BETWEEN 1 AND 3 AND
        t.StatusID <> (SELECT StatusID
        FROM [TaskStatus]
        WHERE StatusName = 'Completed');

    -- Tìm các nhiệm vụ quá hạn
    INSERT INTO [Notifications]
        (UserID, Title, Message)
    SELECT
        t.AssignedTo,
        N'Nhiệm vụ quá hạn',
        N'Nhiệm vụ "' + t.TaskName + N'" đã quá hạn ' +
        CAST(ABS(DATEDIFF(DAY, GETDATE(), t.DueDate)) AS NVARCHAR) + N' ngày.'
    FROM
        inserted t
    WHERE
        t.AssignedTo IS NOT NULL AND
        t.DueDate IS NOT NULL AND
        t.DueDate < GETDATE() AND
        t.StatusID <> (SELECT StatusID
        FROM [TaskStatus]
        WHERE StatusName = 'Completed') AND
        NOT EXISTS (
            SELECT 1
        FROM [Notifications]
        WHERE UserID = t.AssignedTo AND
            Title = N'Nhiệm vụ quá hạn' AND
            Message LIKE N'Nhiệm vụ "' + t.TaskName + N'%" đã quá hạn%' AND
            DATEDIFF(DAY, CreatedDate, GETDATE()) = 0
        );
END;
GO

-- Tạo trigger để ghi lại hành động người dùng
CREATE TRIGGER trg_AuditUserActivity
ON [Users]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Action NVARCHAR(50);

    -- Xác định hành động
    IF EXISTS (SELECT *
        FROM inserted) AND EXISTS (SELECT *
        FROM deleted)
        SET @Action = 'UPDATE';
    ELSE IF EXISTS (SELECT *
    FROM inserted)
        SET @Action = 'INSERT';
    ELSE
        SET @Action = 'DELETE';

    -- Ghi lại hành động
    IF @Action = 'INSERT'
    BEGIN
        INSERT INTO [AuditLog]
            (UserID, Action, Module, Description, IPAddress)
        SELECT
            i.UserID,
            'CREATE',
            'Users',
            'Created user ' + i.Username,
            '::1'
        -- Thay bằng cách lấy IP thực tế
        FROM
            inserted i;
    END
    ELSE IF @Action = 'UPDATE'
    BEGIN
        INSERT INTO [AuditLog]
            (UserID, Action, Module, Description, IPAddress)
        SELECT
            i.UserID,
            'UPDATE',
            'Users',
            'Updated user ' + i.Username,
            '::1'
        -- Thay bằng cách lấy IP thực tế
        FROM
            inserted i
            INNER JOIN
            deleted d ON i.UserID = d.UserID;
    END
    ELSE -- DELETE
    BEGIN
        INSERT INTO [AuditLog]
            (UserID, Action, Module, Description, IPAddress)
        VALUES
            (
                NULL, -- Không có UserID vì người dùng bị xóa
                'DELETE',
                'Users',
                'Deleted user(s)',
                '::1' -- Thay bằng cách lấy IP thực tế
        );
    END
END;
GO

-- Chèn quyền mẫu
INSERT INTO [Permissions]
    (PermissionName, Description)
VALUES
    ('VIEW_PROJECTS', N'Xem dự án'),
    ('CREATE_PROJECT', N'Tạo dự án mới'),
    ('EDIT_PROJECT', N'Chỉnh sửa dự án'),
    ('DELETE_PROJECT', N'Xóa dự án'),
    ('VIEW_TASKS', N'Xem nhiệm vụ'),
    ('CREATE_TASK', N'Tạo nhiệm vụ mới'),
    ('EDIT_TASK', N'Chỉnh sửa nhiệm vụ'),
    ('DELETE_TASK', N'Xóa nhiệm vụ'),
    ('VIEW_USERS', N'Xem người dùng'),
    ('CREATE_USER', N'Tạo người dùng mới'),
    ('EDIT_USER', N'Chỉnh sửa người dùng'),
    ('DELETE_USER', N'Xóa người dùng'),
    ('GENERATE_REPORTS', N'Tạo báo cáo'),
    ('APPROVE_TASKS', N'Duyệt nhiệm vụ'),
    ('MANAGE_DEPARTMENTS', N'Quản lý phòng ban'),
    ('VIEW_REPORTS', N'Xem báo cáo');

-- Phân quyền cho các vai trò
-- Admin có tất cả quyền
INSERT INTO [RolePermissions]
    (RoleID, PermissionID)
SELECT 1, PermissionID
FROM [Permissions];

-- Project Manager
INSERT INTO [RolePermissions]
    (RoleID, PermissionID)
SELECT 2, PermissionID
FROM [Permissions]
WHERE PermissionName IN (
    'VIEW_PROJECTS', 'CREATE_PROJECT', 'EDIT_PROJECT',
    'VIEW_TASKS', 'CREATE_TASK', 'EDIT_TASK', 'DELETE_TASK',
    'VIEW_USERS', 'GENERATE_REPORTS', 'APPROVE_TASKS', 'VIEW_REPORTS'
);

-- Employee
INSERT INTO [RolePermissions]
    (RoleID, PermissionID)
SELECT 3, PermissionID
FROM [Permissions]
WHERE PermissionName IN (
    'VIEW_PROJECTS', 'VIEW_TASKS', 'EDIT_TASK', 'VIEW_USERS'
);

-- Tạo tài khoản admin mặc định (Username: admin, Password: Admin@123)
DECLARE @AdminID INT;
EXEC [usp_CreateUser]
    @Username = 'admin',
    @Password = 'Admin@123',
    @Email = 'admin@example.com',
    @FullName = N'Quản trị viên',
    @PhoneNumber = '0123456789',
    @RoleID = 1,
    @UserID = @AdminID OUTPUT;