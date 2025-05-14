USE ProjectManagementSystem;
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