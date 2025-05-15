USE [ProjectManagementSystemDB]
GO

-- Insert sample data into UserRoles
INSERT INTO [UserRoles] (Name, Description) VALUES
('Admin', 'System administrator with full access'),
('Manager', 'Project manager with limited access'),
('Employee', 'Regular employee with basic access');

-- Insert sample data into Departments first, without managers
INSERT INTO [Departments] (Name, Description, ManagerId) VALUES
('IT', 'Information Technology Department', NULL),
('HR', 'Human Resources Department', NULL),
('Finance', 'Finance Department', NULL),
('Marketing', 'Marketing Department', NULL),
('Sales', 'Sales Department', NULL);

-- Insert sample data into ProjectPriorities
INSERT INTO [ProjectPriorities] (Name, Description) VALUES
('High', 'High priority project'),
('Medium', 'Medium priority project'),
('Low', 'Low priority project');


-- Insert sample data into ProjectStatuses
INSERT INTO [ProjectStatuses] (Name, Description) VALUES
('Not Started', 'Project has not started yet'),
('In Progress', 'Project is currently in progress'),
('Completed', 'Project has been completed'),
('Cancelled', 'Task has been cancelled');

-- Insert sample data into TaskStatuses
INSERT INTO [TaskStatuses] (Name, Description) VALUES
('Not Started', 'Task has not started yet'),
('In Progress', 'Task is currently in progress'),
('Completed', 'Task has been completed'),
('Cancelled', 'Task has been cancelled');

-- Insert sample data into TaskPriorities
INSERT INTO [TaskPriorities] (Name, Description) VALUES
('Critical', 'Tasks that require immediate attention and resolution'),
('High', 'Tasks that are important and should be completed soon'),
('Medium', 'Tasks that are of moderate importance'),
('Low', 'Tasks that are less important and can be addressed later');



-- Insert sample data into TaskDependencyTypes
INSERT INTO [TaskDependencyTypes] (Name, Description, CreatedDate, UpdatedDate) VALUES
('Finish to Start', 'The dependent task cannot start until the predecessor task is finished', GETDATE(), GETDATE()),
('Start to Start', 'The dependent task cannot start until the predecessor task starts', GETDATE(), GETDATE()),
('Finish to Finish', 'The dependent task cannot finish until the predecessor task finishes', GETDATE(), GETDATE()),
('Start to Finish', 'The dependent task cannot finish until the predecessor task starts', GETDATE(), GETDATE()),
('Mandatory', 'The dependency must be respected and cannot be changed', GETDATE(), GETDATE()),
('Discretionary', 'The dependency is a recommendation but can be changed if needed', GETDATE(), GETDATE()),
('External', 'The dependency is related to a factor outside the project scope', GETDATE(), GETDATE());


-- Insert sample data into ProjectMemberRoles
INSERT INTO [ProjectMemberRoles] (Name, Description, CreatedDate, UpdatedDate) VALUES
('Manager', 'Responsible for managing the project', GETDATE(), GETDATE()),
('Developer', 'Responsible for developing project tasks', GETDATE(), GETDATE()),
('Tester', 'Responsible for testing project deliverables', GETDATE(), GETDATE()),
('Designer', 'Responsible for designing project assets', GETDATE(), GETDATE());

-- Insert sample data into NotificationTypes
INSERT INTO [NotificationTypes] (Name, Description) VALUES
('Task Help Request', 'Notification when someone requests help with a task'),
('Project Invitation', 'Notification when a user is invited to join a project'),
('Task Expiring', 'Notification for tasks that are nearing their due date'),
('Task Assignment', 'Notification when a task is assigned to a user'),
('Comment Added', 'Notification when a comment is added to a task');



-- Insert sample data into Users
INSERT INTO [Users] (Username, Password, Email, FirstName, LastName, PhoneNumber, Address, Avatar, UserRoleId, PositionId, DepartmentId, LastLogin, IsActive, IsDeleted, CreatedDate, UpdatedDate) VALUES
('admin', '$2a$11$iOWBeF/3TD9rifgLeDDDBO6CRAmMJrCZAr2Uc9Ko3IpUIU4FZjYpO', 'admin@pms.com', 'System', 'Administrator', '555-0100', '123 Admin St, Admin City', '/avatars/admin.jpg', 1, 1, 1, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('jsmith', '$2a$11$ev2iDZh8fybFMp5oe0736u0LlzU4zeZi0lSVhjOVZaDXMTwYIKq8a', 'john.smith@pms.com', 'John', 'Smith', '555-0101', '456 Manager Ave, Business City', '/avatars/jsmith.jpg', 3, 2, 1, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('awhite', '$2a$11$x29nVzo9EFE7dVWDBiqeRukVGNf9sd0SzIBZryE1X2z.vRObbjx.K', 'alice.white@pms.com', 'Alice', 'White', '555-0102', '789 Developer Ln, Tech Town', '/avatars/awhite.jpg', 3, 3, 1, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('bbrown', '$2a$11$qPxRR4d2hRe2PqkANNHi7uwAYCVnvUrhbgH98JHdHyLCwuIUQCk8m', 'bob.brown@pms.com', 'Bob', 'Brown', '555-0103', '101 Tester Rd, QA City', '/avatars/bbrown.jpg', 3, 3, 3, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('cjones', '$2a$11$WTzKBSYdQKOhDd5vd7/vFeVL9Ud0Wzx6D.sYfHBxPgkWUwo/d95BG', 'carol.jones@pms.com', 'Carol', 'Jones', '555-0104', '102 Designer Blvd, UX Town', '/avatars/cjones.jpg', 3, 2, 2, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('dgreen', '$2a$11$fTlY1zINdtul.6l94ucTYOS3k23tkgohvs4ca0wRWGBVFJwLnb0ZO', 'david.green@pms.com', 'David', 'Green', '555-0105', '103 DevOps Ave, Cloud City', '/avatars/dgreen.jpg', 3, 3, 2, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('eblack', '$2a$11$tWDkNTwJSYylO/7SES15juRA1cYb9m.7tYhp641B7IOcUmtxmNDWK', 'emma.black@pms.com', 'Emma', 'Black', '555-0106', '104 Analyst St, Data Town', '/avatars/eblack.jpg', 3, 3, 2, GETDATE(), 1, 0, GETDATE(), GETDATE());

-- Finally, update Departments with manager references after Users exist
UPDATE [Departments] SET ManagerId = 2 WHERE Name = 'IT';
UPDATE [Departments] SET ManagerId = 3 WHERE Name = 'HR';
UPDATE [Departments] SET ManagerId = 4 WHERE Name = 'Finance';
UPDATE [Departments] SET ManagerId = 5 WHERE Name = 'Marketing';
UPDATE [Departments] SET ManagerId = 6 WHERE Name = 'Sales';


-- Insert sample data into Projects
INSERT INTO [Projects] (Name, ProjectCode, Description, StartDate, EndDate, Budget, StatusId, ManagerId, PriorityId, PercentComplete, CreatedBy) VALUES
('Store Management System', 'PA001', 'First project description', '2025-01-01', '2025-06-30', 100000.00, 2, 2, 1, 50.00, 1),
('Blood Bank Management System', 'PB002', 'Second project description', '2025-02-01', '2025-07-31', 200000.00, 1, 2, 2, 0.00, 1);

-- Insert sample data into Tasks
INSERT INTO [Tasks] (Code, Name, Description, ProjectId, AssignedUserId, StatusId, PriorityId, StartDate, DueDate, EstimatedHours, PercentComplete, CreatedBy) VALUES
('T001', 'Task 1', 'First task description', 1, 2, 2, 1, '2025-01-15', '2025-02-15', 40.00, 25.00, 1),
('T002', 'Task 2', 'Second task description', 1, 2, 1, 2, '2025-02-16', '2025-03-15', 60.00, 0.00, 1),
('T003', 'Task 3', 'First task description', 1, 3, 2, 1, '2025-01-15', '2025-02-15', 40.00, 25.00, 1),
('T004', 'Task 4', 'Second task description', 1, 3, 1, 2, '2025-02-16', '2025-03-15', 60.00, 0.00, 1),
('T007', 'Task 7', 'Second task description', 2, 2, 1, 2, '2025-02-16', '2025-03-15', 60.00, 0.00, 1),
('T008', 'Task 8', 'Second task description', 2, 3, 1, 2, '2025-02-16', '2025-03-15', 60.00, 0.00, 1),
('T009', 'Task 9', '9th task description', 2, 3, 1, 2, '2025-02-16', '2025-03-15', 60.00, 0.00, 1),
('T0010', 'Task 10', '10th task description', 2, 3, 1, 3, '2026-01-11', '2026-03-15', 10.00, 0.00, 1);

-- Insert sample data into ProjectMembers
INSERT INTO [ProjectMembers] (ProjectId, UserId, JoinDate, RoleInProject, IsConfirmed, ConfirmationDate, IsActive, IsDeleted, CreatedDate, UpdatedDate) VALUES
(1, 4, '2025-01-01', 2, 1, '2025-01-02', 1, 0, GETDATE(), GETDATE()), 
(1, 3, '2025-01-03', 3, 1, '2025-01-04', 1, 0, GETDATE(), GETDATE()), 
(2, 6, '2025-02-01', 4, 1, '2025-02-02', 1, 0, GETDATE(), GETDATE()), 
(2, 3, '2025-02-03', 2, 1, '2025-02-04', 1, 0, GETDATE(), GETDATE());


-- Insert sample data into TaskComments table
INSERT INTO [dbo].[TaskComments] ([TaskId], [UserId], [CommentText], [IsEdited], [CreatedDate], [UpdatedDate])
VALUES
    (1, 1, 'Initial comment on the task.', 0, GETDATE(), GETDATE()),
    (1, 2, 'Follow-up comment with additional details.', 0, GETDATE(), GETDATE()),
    (2, 3, 'Requesting clarification on the task requirements.', 0, GETDATE(), GETDATE()),
    (3, 1, 'Updated the task description based on feedback.', 1, GETDATE(), GETDATE()),
    (3, 2, 'Task is on track for completion.', 0, GETDATE(), GETDATE()),
    (3, 3, 'Need to review the task with the team.', 0, GETDATE(), GETDATE()),
    (3, 1, 'Task has been updated with new requirements.', 0, GETDATE(), GETDATE()),
    (3, 2, 'Completed the first phase of the task.', 0, GETDATE(), GETDATE()),
    (3, 3, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', 0, GETDATE(), GETDATE()),
    (3, 3, 'Awaiting confirmation from the project manager.', 0, GETDATE(), GETDATE()),
    (4, 1, 'Task is under review by the team lead.', 0, GETDATE(), GETDATE()),
    (4, 2, 'Finalizing the task for submission.', 0, GETDATE(), GETDATE()),
    (4, 3, 'Need to discuss the task with the team.', 0, GETDATE(), GETDATE()),
    (4, 1, 'Finalizing the task details before submission.', 0, GETDATE(), GETDATE()),
    (4, 2, 'Task has been completed successfully.', 0, GETDATE(), GETDATE()),
    (4, 3, 'Awaiting approval for the completed task.', 0, GETDATE(), GETDATE()),
    (4, 4, 'Marked this task as completed.', 0, GETDATE(), GETDATE()),
    (5, 1, 'Task is in progress and on schedule.', 0, GETDATE(), GETDATE()),
    (5, 2, 'Need to address some issues with the task.', 0, GETDATE(), GETDATE()),
    (5, 3, 'Task is behind schedule due to unforeseen circumstances.', 0, GETDATE(), GETDATE()),
    (5, 1, 'Task is progressing well and on track.', 0, GETDATE(), GETDATE()),
    (5, 2, 'Need to allocate more resources to this task.', 0, GETDATE(), GETDATE()),
    (5, 3, 'Task is nearing completion.', 0, GETDATE(), GETDATE()),
    (6, 1, 'Task has been assigned to the development team.', 0, GETDATE(), GETDATE()),
    (6, 2, 'Need to review the task with the project manager.', 0, GETDATE(), GETDATE()),
    (6, 3, 'Task is in the testing phase.', 0, GETDATE(), GETDATE()),
    (6, 1, 'Task is ready for review by the QA team.', 0, GETDATE(), GETDATE()),
    (6, 2, 'Need to finalize the task details before submission.', 0, GETDATE(), GETDATE()),
    (6, 3, 'Awaiting feedback from the QA team.', 0, GETDATE(), GETDATE()),
    (7, 1, 'Task is in progress and on schedule.', 0, GETDATE(), GETDATE()),
    (7, 2, 'Need to address some issues with the task.', 0, GETDATE(), GETDATE()),
    (7, 3, 'Task is behind schedule due to unforeseen circumstances.', 0, GETDATE(), GETDATE()),
    (7, 1, 'Task is progressing well and on track.', 0, GETDATE(), GETDATE()),
    (7, 2, 'Need to allocate more resources to this task.', 0, GETDATE(), GETDATE()),
    (7, 3, 'Task is nearing completion.', 0, GETDATE(), GETDATE()),
    (7, 1, 'Task is in the final stages of development.', 0, GETDATE(), GETDATE()),
    (7, 2, 'Need to finalize the task details before submission.', 0, GETDATE(), GETDATE()),
    (7, 3, 'Awaiting feedback from the project manager.', 0, GETDATE(), GETDATE()),
    (8, 1, 'Task has been assigned to the development team.', 0, GETDATE(), GETDATE()),
    (8, 2, 'Need to review the task with the project manager.', 0, GETDATE(), GETDATE()),
    (8, 3, 'Task is in the testing phase.', 0, GETDATE(), GETDATE()),
    (8, 1, 'Task is ready for review by the QA team.', 0, GETDATE(), GETDATE()),
    (8, 2, 'Need to finalize the task details before submission.', 0, GETDATE(), GETDATE()),
    (8, 3, 'Awaiting feedback from the QA team.', 0, GETDATE(), GETDATE());

-- Insert sample data into Notifications
INSERT INTO [Notifications] (UserId, Title, Message, NotificationTypeId, RelatedId, IsRead, CreatedDate) VALUES
-- Task Help Request notifications
(2, 'Help Requested', 'Bob Brown has requested your help with task "Task 2"', 1, 2, 0, DATEADD(day, -2, GETDATE())),
(1, 'Help Requested', 'John Smith needs assistance with task "Task 1"', 1, 1, 1, DATEADD(day, -5, GETDATE())),
(4, 'Help Requested', 'Alice White has requested your help with task "Task 3"', 1, 3, 0, DATEADD(day, -1, GETDATE())),
(3, 'Help Requested', 'David Green needs assistance with task "Task 8"', 1, 8, 0, DATEADD(day, -3, GETDATE())),

-- Project Invitation notifications
(4, 'Project Invitation', 'You have been invited to join Project Alpha', 2, 1, 0, DATEADD(day, -10, '2025-01-01')),
(6, 'Project Invitation', 'You have been invited to join Project Beta', 2, 2, 1, DATEADD(day, -5, '2025-02-01')),
(3, 'Project Invitation', 'You have been invited to join Project Alpha', 2, 1, 1, DATEADD(day, -12, '2025-01-03')),
(3, 'Project Invitation', 'You have been invited to join Project Beta', 2, 2, 1, DATEADD(day, -7, '2025-02-03')),

-- Task Expiring notifications
(2, 'Task Approaching Due Date', 'Task "Task 1" is due in 3 days', 3, 1, 0, DATEADD(day, -3, '2025-02-15')),
(3, 'Task Approaching Due Date', 'Task "Task 3" is due tomorrow', 3, 3, 0, DATEADD(day, -1, '2025-02-15')),
(3, 'Task Overdue', 'Task "Task 4" is now overdue by 2 days', 3, 4, 1, DATEADD(day, 2, '2025-03-15')),
(2, 'Task Approaching Due Date', 'Task "Task 7" is due in 5 days', 3, 7, 0, DATEADD(day, -5, '2025-03-15')),

-- Task Assignment notifications
(2, 'Task Assigned', 'You have been assigned to task "Task 1"', 4, 1, 1, DATEADD(day, -2, '2025-01-15')),
(3, 'Task Assigned', 'You have been assigned to task "Task 3"', 4, 3, 1, DATEADD(day, -2, '2025-01-15')),
(2, 'Task Assigned', 'You have been assigned to task "Task 7"', 4, 7, 0, DATEADD(day, -2, '2025-02-16')),
(3, 'Task Assigned', 'You have been assigned to task "Task 9"', 4, 9, 0, DATEADD(day, -2, '2025-02-16')),

-- Comment Added comment notifications
(2, 'New Comment', 'System Administrator commented on task "Task 1"', 5, 1, 0, DATEADD(day, -4, GETDATE())),
(1, 'New Comment', 'John Smith commented on task "Task 3"', 5, 3, 1, DATEADD(day, -2, GETDATE())),
(3, 'New Comment', 'Alice White commented on task "Task 4"', 5, 4, 0, DATEADD(day, -1, GETDATE())),
(2, 'New Comment', 'Bob Brown commented on task "Task 7"', 5, 7, 0, DATEADD(hour, -12, GETDATE()));