-- Create Database
IF EXISTS(SELECT *
FROM sys.databases
WHERE name = 'ProjectManagementSystemDB')
BEGIN
	DROP DATABASE [ProjectManagementSystemDB]
END
GO

CREATE DATABASE [ProjectManagementSystemDB]
GO

USE [ProjectManagementSystemDB]
GO

CREATE TABLE [permission_types]
(
	id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL UNIQUE,
	--user permission, project permisson
    description NVARCHAR(200),
    created_date DATETIME DEFAULT GETDATE()
)

CREATE TABLE [permissions]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL UNIQUE,
	permission_type_id INT,
    description NVARCHAR(200),
    created_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (permission_type_id) REFERENCES [permission_types](id),
);
GO

CREATE TABLE [user_roles]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(50) NOT NULL UNIQUE,
    description NVARCHAR(200),
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [user_role_permissions]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    user_roles_id INT NOT NULL,
    permission_id INT NOT NULL,
    created_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_roles_id) REFERENCES [user_roles](id),
    FOREIGN KEY (permission_id) REFERENCES [permissions](id),
    CONSTRAINT UQ_UserRolePermission UNIQUE (user_roles_id, permission_id)
);
GO

CREATE TABLE [users]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    username NVARCHAR(50) NOT NULL UNIQUE,
    password NVARCHAR(128) NOT NULL,
    email NVARCHAR(100) NOT NULL UNIQUE,
    first_name NVARCHAR(50) NOT NULL,
    last_name NVARCHAR(50) NOT NULL,
    phone_number NVARCHAR(20) NOT NULL,
    address NVARCHAR(200),
    avatar NVARCHAR(255),
	user_role_id INT NOT NULL,
    last_login DATETIME,
    is_active BIT DEFAULT 1,
    is_deleted BIT DEFAULT 0,
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_role_id) REFERENCES [user_roles](id)
);
GO

CREATE TABLE [positions]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL UNIQUE,
    description NVARCHAR(255),
    is_active BIT DEFAULT 1,
    is_deleted BIT DEFAULT 0,
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [departments]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL,
    description NVARCHAR(200),
    manager_id INT,
    is_active BIT DEFAULT 1,
    is_deleted BIT DEFAULT 0,
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (manager_id) REFERENCES [users](id)
);
GO

CREATE TABLE [employees]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT NOT NULL UNIQUE,
    position_id INT NOT NULL,
    hire_date DATE,
    department_id INT,
    report_to INT,
    salary DECIMAL(18, 2),
    is_active BIT DEFAULT 1,
	is_deleted BIT DEFAULT 0,
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES [users](id),
    FOREIGN KEY (department_id) REFERENCES [departments](id),
    FOREIGN KEY (report_to) REFERENCES [employees](id),
    FOREIGN KEY (position_id) REFERENCES [positions](id),
);

CREATE TABLE [project_statuses]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(50) NOT NULL UNIQUE,
    description NVARCHAR(200),
    is_active BIT DEFAULT 1,
    is_deleted BIT DEFAULT 0,
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [project_priorities]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(50) NOT NULL UNIQUE,
    description NVARCHAR(200),
    is_active BIT DEFAULT 1,
    is_deleted BIT DEFAULT 0,
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [projects]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(100) NOT NULL,
    project_code NVARCHAR(20) UNIQUE,
    description NVARCHAR(500),
    start_date DATE,
    end_date DATE,
    budget DECIMAL(18, 2),
    status_id INT NOT NULL,
    manager_id INT NOT NULL,
    priority_id INT NOT NULL,
    percent_complete DECIMAL(5, 2) DEFAULT 0,
    is_deleted BIT DEFAULT 0,
    created_by INT NOT NULL,
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (status_id) REFERENCES [project_statuses](id),
    FOREIGN KEY (manager_id) REFERENCES [users](id),
    FOREIGN KEY (created_by) REFERENCES [users](id),
    FOREIGN KEY (priority_id) REFERENCES [project_priorities](id)
);
GO

CREATE TABLE [project_member_roles]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(50) NOT NULL UNIQUE,
    description NVARCHAR(200),
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [project_member_role_permissions]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    project_member_roles_id INT NOT NULL,
    permission_id INT NOT NULL,
    created_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (project_member_roles_id) REFERENCES [project_member_roles](id),
    FOREIGN KEY (permission_id) REFERENCES [permissions](id),
    CONSTRAINT UQ_ProjectRolePermission UNIQUE (project_member_roles_id, permission_id)
);
GO

CREATE TABLE [project_members]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    project_id INT NOT NULL,
    user_id INT NOT NULL,
    join_date DATE DEFAULT GETDATE(),
    role_in_project INT NOT NULL,
    is_confirmed BIT DEFAULT 0,
    confirmation_date DATETIME,
    is_active BIT DEFAULT 1,
    is_deleted BIT DEFAULT 0,
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (project_id) REFERENCES [projects](id),
    FOREIGN KEY (user_id) REFERENCES [users](id),
    FOREIGN KEY (role_in_project) REFERENCES [project_member_roles](id),
    CONSTRAINT UQ_project_member UNIQUE (project_id, user_id)
);
GO

CREATE TABLE [task_statuses]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(50) NOT NULL UNIQUE,
    description NVARCHAR(200),
    is_active BIT DEFAULT 1,
    is_deleted BIT DEFAULT 0,
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [task_priorities]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(50) NOT NULL UNIQUE,
    description NVARCHAR(200),
    is_active BIT DEFAULT 1,
    is_deleted BIT DEFAULT 0,
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [tasks]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    code NVARCHAR(20) UNIQUE,
    name NVARCHAR(100) NOT NULL,
    description NVARCHAR(500),
    project_id INT NOT NULL,
    assigned_user_id INT NOT NULL,  -- Ngu?i ch?u trách nhi?m chính
    status_id INT NOT NULL,
    priority_id INT NOT NULL,
    start_date DATE,
    due_date DATE,
    estimated_hours DECIMAL(8, 2),
    actual_hours DECIMAL(8, 2),
    percent_complete DECIMAL(5, 2) DEFAULT 0,
    parent_task_id INT,  -- Nhi?m v? cha (n?u có)
    last_status_change_date DATETIME,
    block_reason NVARCHAR(500),
    is_deleted BIT DEFAULT 0,
    created_by INT NOT NULL,
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (project_id) REFERENCES [projects](id),
    FOREIGN KEY (assigned_user_id) REFERENCES [users](id),
    FOREIGN KEY (status_id) REFERENCES [task_statuses](id),
    FOREIGN KEY (priority_id) REFERENCES [task_priorities](id),
    FOREIGN KEY (parent_task_id) REFERENCES [tasks](id),
    FOREIGN KEY (created_by) REFERENCES [users](id),
	CONSTRAINT CHK_PercentComplete CHECK (percent_complete BETWEEN 0 AND 100)
);
GO

CREATE TABLE [task_help_requests]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    task_id INT NOT NULL,
    requested_by INT NOT NULL,
    requested_to INT NOT NULL,
    request_message NVARCHAR(500),
    is_resolved BIT DEFAULT 0,
    resolved_date DATETIME,
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (task_id) REFERENCES [tasks](id),
    FOREIGN KEY (requested_by) REFERENCES [users](id),
    FOREIGN KEY (requested_to) REFERENCES [users](id)
);
GO

CREATE TABLE [task_dependencies]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    task_id INT NOT NULL,
    depends_on_task_id INT NOT NULL,
    dependency_type INT NOT NULL CHECK (dependency_type BETWEEN 1 AND 4),
    -- 1: Finish to Start, 2: Start to Start, 3: Finish to Finish, 4: Start to Finish
    created_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (task_id) REFERENCES [tasks](id),
    FOREIGN KEY (depends_on_task_id) REFERENCES [tasks](id),
    CONSTRAINT UQ_task_dependency UNIQUE (task_id, depends_on_task_id),
	CONSTRAINT CHK_TaskDependency CHECK (task_id != depends_on_task_id)
);
GO

CREATE TABLE [task_comments]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    task_id INT NOT NULL,
    user_id INT NOT NULL,
    comment_text NVARCHAR(1000) NOT NULL,
    is_edited BIT DEFAULT 0,
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (task_id) REFERENCES [tasks](id),
    FOREIGN KEY (user_id) REFERENCES [users](id)
);
GO

CREATE TABLE [task_history]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    task_id INT NOT NULL,
    field_changed NVARCHAR(50) NOT NULL,
    old_value NVARCHAR(MAX),
    new_value NVARCHAR(MAX),
    changed_by INT NOT NULL,
    changed_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (task_id) REFERENCES [tasks](id),
    FOREIGN KEY (changed_by) REFERENCES [users](id)
);
GO

CREATE TABLE [time_entries]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    task_id INT NOT NULL,
    user_id INT NOT NULL,
    start_time DATETIME NOT NULL,
    end_time DATETIME,
    description NVARCHAR(500),
    created_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (task_id) REFERENCES [tasks](id),
    FOREIGN KEY (user_id) REFERENCES [users](id)
);
GO

CREATE TABLE [files]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(255) NOT NULL,
    file_path NVARCHAR(500) NOT NULL,
    file_size BIGINT,
    file_type NVARCHAR(100),
    project_id INT,
    task_id INT,
    uploaded_by INT NOT NULL,
    upload_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (project_id) REFERENCES [projects](id),
    FOREIGN KEY (task_id) REFERENCES [tasks](id),
    FOREIGN KEY (uploaded_by) REFERENCES [users](id)
);
GO

CREATE TABLE [notification_types]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(50) NOT NULL,
    description NVARCHAR(200),
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [notifications]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT NOT NULL,
    title NVARCHAR(100) NOT NULL,
    message NVARCHAR(500),
    notification_type_id INT NOT NULL,
    related_id INT,
    -- ID c?a d?i tu?ng liên quan (task, project, v.v.)
    is_read BIT DEFAULT 0,
    created_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES [users](id),
    FOREIGN KEY (notification_type_id) REFERENCES [notification_types](id)
);
GO

CREATE TABLE [email_notifications]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT NOT NULL,
    subject NVARCHAR(200) NOT NULL,
    body NVARCHAR(MAX),
    sent_date DATETIME DEFAULT GETDATE(),
    is_successful BIT DEFAULT 1,
    error_message NVARCHAR(500),
    FOREIGN KEY (user_id) REFERENCES [users](id)
);
GO


CREATE TABLE [report_types]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    type_name NVARCHAR(50) NOT NULL,
    description NVARCHAR(200),
    created_date DATETIME DEFAULT GETDATE(),
    updated_date DATETIME DEFAULT GETDATE()
);
GO

CREATE TABLE [reports]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    report_name NVARCHAR(100) NOT NULL,
    report_type_id INT NOT NULL,
    description NVARCHAR(200),
    report_data NVARCHAR(MAX),
    -- JSON data for the report
    created_by INT NOT NULL,
    created_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (created_by) REFERENCES [users](id),
    FOREIGN KEY (report_type_id) REFERENCES [report_types](id)
);
GO

CREATE TABLE [audit_logs]
(
    id INT PRIMARY KEY IDENTITY(1,1),
    user_id INT,
    action NVARCHAR(50) NOT NULL,
    module NVARCHAR(50) NOT NULL,
    description NVARCHAR(500),
    ip_address NVARCHAR(50),
    log_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES [users](id)
);
GO

