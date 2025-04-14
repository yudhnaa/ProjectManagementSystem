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
INSERT INTO [Users] (Username, Password, Email, FirstName, LastName, PhoneNumber, Address, Avatar, UserRoleId, PositionId, HireDate, DepartmentId, ReportTo, Salary, LastLogin, IsActive, IsDeleted, CreatedDate, UpdatedDate) VALUES
('admin', 'hashed_password_123', 'admin@pms.com', 'System', 'Administrator', '555-0100', '123 Admin St, Admin City', '/avatars/admin.jpg', 1, 1, '2022-01-01', 1, NULL, 8000.00, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('jsmith', 'hashed_password_456', 'john.smith@pms.com', 'John', 'Smith', '555-0101', '456 Manager Ave, Business City', '/avatars/jsmith.jpg', 2, 2, '2022-03-15', 1, 1, 6000.00, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('awhite', 'hashed_password_789', 'alice.white@pms.com', 'Alice', 'White', '555-0102', '789 Developer Ln, Tech Town', '/avatars/awhite.jpg', 3, 3, '2022-06-10', 1, 2, 4500.00, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('bbrown', 'hashed_password_101', 'bob.brown@pms.com', 'Bob', 'Brown', '555-0103', '101 Tester Rd, QA City', '/avatars/bbrown.jpg', 3, 3, '2023-01-20', 1, 2, 4200.00, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('cjones', 'hashed_password_102', 'carol.jones@pms.com', 'Carol', 'Jones', '555-0104', '102 Designer Blvd, UX Town', '/avatars/cjones.jpg', 2, 2, '2023-05-15', 2, 1, 5500.00, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('dgreen', 'hashed_password_103', 'david.green@pms.com', 'David', 'Green', '555-0105', '103 DevOps Ave, Cloud City', '/avatars/dgreen.jpg', 3, 3, '2023-08-22', 2, 5, 4800.00, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('eblack', 'hashed_password_104', 'emma.black@pms.com', 'Emma', 'Black', '555-0106', '104 Analyst St, Data Town', '/avatars/eblack.jpg', 3, 3, '2024-01-10', 2, 5, 4300.00, GETDATE(), 1, 0, GETDATE(), GETDATE()),
('fwilson', 'hashed_password_105', 'frank.wilson@pms.com', 'Frank', 'Wilson', '555-0107', '105 Product Rd, Business Park', '/avatars/fwilson.jpg', 2, 2, '2024-03-05', 1, 1, 5800.00, GETDATE(), 1, 0, GETDATE(), GETDATE());

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
('T008', 'Task 8', 'Second task description', 2, 3, 1, 2, '2025-02-16', '2025-03-15', 60.00, 0.00, 1);



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