USE ProjectManagementSystem;
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
