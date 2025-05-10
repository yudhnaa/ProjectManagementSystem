USE [ProjectManagementSystemDB]
GO

-- Insert sample data into PermissionTypes
INSERT INTO [PermissionTypes] (Name, Description) VALUES
('User Permission', 'Permissions related to user management'),
('Project Permission', 'Permissions related to project management');

-- Insert sample data into Permissions
INSERT INTO [Permissions] (Name, PermissionTypeId, Description) VALUES
('View Users', 1, 'View user list'),
('Edit Users', 1, 'Edit user details'),
('Delete Users', 1, 'Delete users'),
('View Projects', 2, 'View project list'),
('Edit Projects', 2, 'Edit project details');

-- Insert sample data into UserRoles
INSERT INTO [UserRoles] (Name, Description) VALUES
('Admin', 'System administrator with full access'),
('Manager', 'Project manager with limited access'),
('Employee', 'Regular employee with basic access');

-- Insert sample data into UserRolePermissions
INSERT INTO [UserRolePermissions] (UserRoleId, PermissionId) VALUES
(1, 1), (1, 2), (1, 3), -- Admin permissions
(2, 4), (2, 5),         -- Manager permissions
(3, 1);                 -- Employee permissions

-- Insert sample data into Positions
INSERT INTO [Positions] (Name, Description) VALUES
('CEO', 'Chief Executive Officer'),
('Project Manager', 'Responsible for managing projects'),
('Developer', 'Responsible for software development');

-- Insert sample data into Departments first, without managers
INSERT INTO [Departments] (Name, Description, ManagerId) VALUES
('IT', 'Information Technology Department', NULL),
('HR', 'Human Resources Department', NULL),
('Finance', 'Finance Department', NULL),
('Marketing', 'Marketing Department', NULL),
('Sales', 'Sales Department', NULL);

-- Insert sample data into Users
INSERT INTO [Users] (Username, Password, Email, FirstName, LastName, PhoneNumber, Address, Avatar, UserRoleId, PositionId, DepartmentId, LastLogin, IsActive, IsDeleted, CreatedDate, UpdatedDate) VALUES
('admin', '1', 'admin@pms.com', 'System', 'Administrator', '555-0100', '123 Admin St, Admin City', '/avatars/admin.jpg', 1, 1, 1, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('jsmith', '1', 'john.smith@pms.com', 'John', 'Smith', '555-0101', '456 Manager Ave, Business City', '/avatars/jsmith.jpg', 2, 2, 1, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('awhite', '1', 'alice.white@pms.com', 'Alice', 'White', '555-0102', '789 Developer Ln, Tech Town', '/avatars/awhite.jpg', 3, 3, 1, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('bbrown', '1', 'bob.brown@pms.com', 'Bob', 'Brown', '555-0103', '101 Tester Rd, QA City', '/avatars/bbrown.jpg', 3, 3, 1, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('cjones', '1', 'carol.jones@pms.com', 'Carol', 'Jones', '555-0104', '102 Designer Blvd, UX Town', '/avatars/cjones.jpg', 2, 2, 2, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('dgreen', '1', 'david.green@pms.com', 'David', 'Green', '555-0105', '103 DevOps Ave, Cloud City', '/avatars/dgreen.jpg', 3, 3, 2, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('eblack', '1', 'emma.black@pms.com', 'Emma', 'Black', '555-0106', '104 Analyst St, Data Town', '/avatars/eblack.jpg', 3, 3, 2, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('fwilson', '1', 'frank.wilson@pms.com', 'Frank', 'Wilson', '555-0107', '105 Product Rd, Business Park', '/avatars/fwilson.jpg', 2, 2, 1, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('gmoore', '1', 'grace.moore@pms.com', 'Grace', 'Moore', '555-0108', '106 Finance St, Money City', '/avatars/gmoore.jpg', 3, 3, 3, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('hlee', '1', 'henry.lee@pms.com', 'Henry', 'Lee', '555-0109', '107 Marketing Blvd, Branding Town', '/avatars/hlee.jpg', 2, 2, 4, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('iperez', '1', 'isabel.perez@pms.com', 'Isabel', 'Perez', '555-0110', '108 Sales Ave, Revenue City', '/avatars/iperez.jpg', 3, 3, 5, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('jkim', '1', 'james.kim@pms.com', 'James', 'Kim', '555-0111', '109 IT Pkwy, Server Town', '/avatars/jkim.jpg', 3, 3, 1, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('knguyen', '1', 'kelly.nguyen@pms.com', 'Kelly', 'Nguyen', '555-0112', '110 HR Lane, Recruitment City', '/avatars/knguyen.jpg', 3, 3, 2, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('lgarcia', '1', 'luis.garcia@pms.com', 'Luis', 'Garcia', '555-0113', '111 Finance Dr, Budget Town', '/avatars/lgarcia.jpg', 2, 2, 3, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('mwang', '1', 'mia.wang@pms.com', 'Mia', 'Wang', '555-0114', '112 Marketing St, Creative City', '/avatars/mwang.jpg', 3, 3, 4, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('nsingh', '1', 'noah.singh@pms.com', 'Noah', 'Singh', '555-0115', '113 Sales Blvd, Client Town', '/avatars/nsingh.jpg', 3, 3, 5, GETDATE(), 1, 0, GETDATE(), GETDATE());

-- Finally, update Departments with manager references after Users exist
UPDATE [Departments] SET ManagerId = 2 WHERE Name = 'IT';
UPDATE [Departments] SET ManagerId = 5 WHERE Name = 'HR';

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

-- Insert sample data into Projects
INSERT INTO [Projects] (Name, ProjectCode, Description, StartDate, EndDate, Budget, StatusId, ManagerId, PriorityId, PercentComplete, CreatedBy) VALUES
('Project Alpha', 'PA001', 'First project description', '2025-01-01', '2025-06-30', 100000.00, 2, 2, 1, 50.00, 1),
('Project Beta', 'PB002', 'Second project description', '2025-02-01', '2025-07-31', 200000.00, 1, 2, 2, 0.00, 1);

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

-- Insert sample data into ProjectMemberRoles
INSERT INTO [ProjectMemberRoles] (Name, Description, CreatedDate, UpdatedDate) VALUES
('Manager', 'Responsible for managing the project', GETDATE(), GETDATE()),
('Developer', 'Responsible for developing project tasks', GETDATE(), GETDATE()),
('Tester', 'Responsible for testing project deliverables', GETDATE(), GETDATE()),
('Designer', 'Responsible for designing project assets', GETDATE(), GETDATE());

-- Insert sample data into ProjectMembers
INSERT INTO [ProjectMembers] (ProjectId, UserId, JoinDate, RoleInProject, IsConfirmed, ConfirmationDate, IsActive, IsDeleted, CreatedDate, UpdatedDate) VALUES
(1, 2, '2025-01-01', 1, 1, '2025-01-02', 1, 0, GETDATE(), GETDATE()), -- Manager for Project Alpha
(1, 3, '2025-01-03', 2, 1, '2025-01-04', 1, 0, GETDATE(), GETDATE()), -- Developer for Project Alpha
(2, 2, '2025-02-01', 1, 1, '2025-02-02', 1, 0, GETDATE(), GETDATE()), -- Manager for Project Beta
(2, 3, '2025-02-03', 2, 1, '2025-02-04', 1, 0, GETDATE(), GETDATE()); -- Developer for Project Beta


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