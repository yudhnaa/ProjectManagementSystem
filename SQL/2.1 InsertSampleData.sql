USE [ProjectManagementSystemDB]
GO

-- Thêm dữ liệu vào bảng PermissionTypes
INSERT INTO [PermissionTypes] (Name, Description) VALUES
('User Permission', 'Quyền liên quan đến người dùng'),
('Project Permission', 'Quyền liên quan đến dự án');

-- Thêm dữ liệu vào bảng Permissions
INSERT INTO [Permissions] (Name, PermissionTypeId, Description) VALUES
('View Users', 1, 'Xem danh sách người dùng'),
('Edit Users', 1, 'Chỉnh sửa thông tin người dùng'),
('Delete Users', 1, 'Xóa người dùng'),
('View Projects', 2, 'Xem danh sách dự án'),
('Edit Projects', 2, 'Chỉnh sửa dự án');

-- Thêm dữ liệu vào bảng UserRoles
INSERT INTO [UserRoles] (Name, Description) VALUES
('Admin', 'Quản trị viên toàn hệ thống'),
('Manager', 'Người quản lý dự án'),
('Employee', 'Nhân viên thông thường');

-- Thêm dữ liệu vào bảng UserRolePermissions
INSERT INTO [UserRolePermissions] (UserRoleId, PermissionId) VALUES
(1, 1), (1, 2), (1, 3), -- Admin có quyền quản lý Users
(2, 4), (2, 5), -- Manager có quyền quản lý Projects
(3, 1); -- Employee chỉ có quyền xem Users

-- Thêm dữ liệu vào bảng Users
INSERT INTO [Users] (Username, Password, Email, FirstName, LastName, PhoneNumber, Address, UserRoleId) VALUES
('admin', '1', 'admin@example.com', 'Admin', 'User', '0123456789', 'Hà Nội', 1),
('manager1', '1', 'manager@example.com', 'Manager', 'One', '0987654321', 'TP HCM', 2),
('employee1', '1', 'employee@example.com', 'Employee', 'One', '0912345678', 'Đà Nẵng', 3);

-- Thêm dữ liệu vào bảng Positions
INSERT INTO [Positions] (Name, Description) VALUES
('CEO', 'Tổng giám đốc'),
('Project Manager', 'Quản lý dự án'),
('Developer', 'Lập trình viên');

-- Thêm dữ liệu vào bảng Departments
INSERT INTO [Departments] (Name, Description, ManagerId) VALUES
('IT', 'Phòng công nghệ thông tin', 2),
('HR', 'Phòng nhân sự', NULL);

-- Thêm dữ liệu vào bảng Employees
INSERT INTO [Employees] (UserId, PositionId, HireDate, DepartmentId, ReportTo, Salary) VALUES
(1, 1, '2022-01-01', 1, NULL, 5000.00),
(2, 2, '2023-05-10', 1, 1, 3000.00),
(3, 3, '2024-03-15', 1, 2, 1500.00);
