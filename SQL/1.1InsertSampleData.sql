USE ProjectManagementSystem;
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