USE ProjectManagementSystem;
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
