USE ProjectManagementSystem;
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