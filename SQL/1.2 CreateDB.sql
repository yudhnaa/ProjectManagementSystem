USE [ProjectManagementSystemDB]
GO
/****** Object:  Table [dbo].[AuditLogs]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Action] [nvarchar](50) NOT NULL,
	[Module] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[IpAddress] [nvarchar](50) NULL,
	[LogDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[ManagerId] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailNotifications]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailNotifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Subject] [nvarchar](200) NOT NULL,
	[Body] [nvarchar](max) NULL,
	[SentDate] [datetime] NULL,
	[IsSuccessful] [bit] NULL,
	[ErrorMessage] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PositionId] [int] NOT NULL,
	[HireDate] [date] NULL,
	[DepartmentId] [int] NULL,
	[ReportTo] [int] NULL,
	[Salary] [decimal](18, 2) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Files]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Files](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[FilePath] [nvarchar](500) NOT NULL,
	[FileSize] [bigint] NULL,
	[FileType] [nvarchar](100) NULL,
	[ProjectId] [int] NULL,
	[TaskId] [int] NULL,
	[UploadedBy] [int] NOT NULL,
	[UploadDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Message] [nvarchar](500) NULL,
	[NotificationTypeId] [int] NOT NULL,
	[RelatedId] [int] NULL,
	[IsRead] [bit] NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationTypes]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[PermissionTypeId] [int] NULL,
	[Description] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionTypes]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Positions]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Positions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectMemberRolePermissions]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectMemberRolePermissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectMemberRoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_ProjectRolePermission] UNIQUE NONCLUSTERED 
(
	[ProjectMemberRoleId] ASC,
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectMemberRoles]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectMemberRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectMembers]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectMembers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[JoinDate] [date] NULL,
	[RoleInProject] [int] NOT NULL,
	[IsConfirmed] [bit] NULL,
	[ConfirmationDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_project_member] UNIQUE NONCLUSTERED 
(
	[ProjectId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectPriorities]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectPriorities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ProjectCode] [nvarchar](20) NULL,
	[Description] [nvarchar](500) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Budget] [decimal](18, 2) NULL,
	[StatusId] [int] NOT NULL,
	[ManagerId] [int] NOT NULL,
	[PriorityId] [int] NOT NULL,
	[PercentComplete] [decimal](5, 2) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ProjectCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectStatuses]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reports]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reports](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReportName] [nvarchar](100) NOT NULL,
	[ReportTypeId] [int] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[ReportData] [nvarchar](max) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportTypes]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskComments]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskComments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CommentText] [nvarchar](1000) NOT NULL,
	[IsEdited] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskDependencies]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskDependencies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NOT NULL,
	[DependsOnTaskId] [int] NOT NULL,
	[DependencyType] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_task_dependency] UNIQUE NONCLUSTERED 
(
	[TaskId] ASC,
	[DependsOnTaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskHelpRequests]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskHelpRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NOT NULL,
	[RequestedBy] [int] NOT NULL,
	[RequestedTo] [int] NOT NULL,
	[RequestMessage] [nvarchar](500) NULL,
	[IsResolved] [bit] NULL,
	[ResolvedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskHistory]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NOT NULL,
	[FieldChanged] [nvarchar](50) NOT NULL,
	[OldValue] [nvarchar](max) NULL,
	[NewValue] [nvarchar](max) NULL,
	[ChangedBy] [int] NOT NULL,
	[ChangedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskPriorities]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskPriorities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](20) NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[ProjectId] [int] NOT NULL,
	[AssignedUserId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[PriorityId] [int] NOT NULL,
	[StartDate] [date] NULL,
	[DueDate] [date] NULL,
	[EstimatedHours] [decimal](8, 2) NULL,
	[ActualHours] [decimal](8, 2) NULL,
	[PercentComplete] [decimal](5, 2) NULL,
	[ParentTaskId] [int] NULL,
	[LastStatusChangeDate] [datetime] NULL,
	[BlockReason] [nvarchar](500) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskStatuses]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeEntries]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeEntries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NULL,
	[Description] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRolePermissions]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRolePermissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserRoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_UserRolePermission] UNIQUE NONCLUSTERED 
(
	[UserRoleId] ASC,
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 26/03/2025 4:02:12 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[Address] [nvarchar](200) NULL,
	[Avatar] [nvarchar](255) NULL,
	[UserRoleId] [int] NOT NULL,
	[LastLogin] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AuditLogs] ADD  DEFAULT (getdate()) FOR [LogDate]
GO
ALTER TABLE [dbo].[Departments] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Departments] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Departments] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Departments] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[EmailNotifications] ADD  DEFAULT (getdate()) FOR [SentDate]
GO
ALTER TABLE [dbo].[EmailNotifications] ADD  DEFAULT ((1)) FOR [IsSuccessful]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Employees] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[Files] ADD  DEFAULT (getdate()) FOR [UploadDate]
GO
ALTER TABLE [dbo].[Notifications] ADD  DEFAULT ((0)) FOR [IsRead]
GO
ALTER TABLE [dbo].[Notifications] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[NotificationTypes] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[NotificationTypes] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[Permissions] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[PermissionTypes] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Positions] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Positions] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Positions] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Positions] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[ProjectMemberRolePermissions] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ProjectMemberRoles] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ProjectMemberRoles] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[ProjectMembers] ADD  DEFAULT (getdate()) FOR [JoinDate]
GO
ALTER TABLE [dbo].[ProjectMembers] ADD  DEFAULT ((0)) FOR [IsConfirmed]
GO
ALTER TABLE [dbo].[ProjectMembers] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ProjectMembers] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ProjectMembers] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ProjectMembers] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[ProjectPriorities] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ProjectPriorities] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ProjectPriorities] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ProjectPriorities] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT ((0)) FOR [PercentComplete]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Projects] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[ProjectStatuses] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ProjectStatuses] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[ProjectStatuses] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ProjectStatuses] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[Reports] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ReportTypes] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[ReportTypes] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[TaskComments] ADD  DEFAULT ((0)) FOR [IsEdited]
GO
ALTER TABLE [dbo].[TaskComments] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TaskComments] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[TaskDependencies] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TaskHelpRequests] ADD  DEFAULT ((0)) FOR [IsResolved]
GO
ALTER TABLE [dbo].[TaskHelpRequests] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TaskHelpRequests] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[TaskHistory] ADD  DEFAULT (getdate()) FOR [ChangedDate]
GO
ALTER TABLE [dbo].[TaskPriorities] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[TaskPriorities] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TaskPriorities] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TaskPriorities] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[Tasks] ADD  DEFAULT ((0)) FOR [PercentComplete]
GO
ALTER TABLE [dbo].[Tasks] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Tasks] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Tasks] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[TaskStatuses] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[TaskStatuses] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[TaskStatuses] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[TaskStatuses] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[TimeEntries] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserRolePermissions] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserRoles] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserRoles] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[AuditLogs]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Departments]  WITH CHECK ADD FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[EmailNotifications]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD FOREIGN KEY([PositionId])
REFERENCES [dbo].[Positions] ([Id])
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD FOREIGN KEY([ReportTo])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Files]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[Files]  WITH CHECK ADD FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([Id])
GO
ALTER TABLE [dbo].[Files]  WITH CHECK ADD FOREIGN KEY([UploadedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD FOREIGN KEY([NotificationTypeId])
REFERENCES [dbo].[NotificationTypes] ([Id])
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD FOREIGN KEY([PermissionTypeId])
REFERENCES [dbo].[PermissionTypes] ([Id])
GO
ALTER TABLE [dbo].[ProjectMemberRolePermissions]  WITH CHECK ADD FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permissions] ([Id])
GO
ALTER TABLE [dbo].[ProjectMemberRolePermissions]  WITH CHECK ADD FOREIGN KEY([ProjectMemberRoleId])
REFERENCES [dbo].[ProjectMemberRoles] ([Id])
GO
ALTER TABLE [dbo].[ProjectMembers]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectMembers]  WITH CHECK ADD FOREIGN KEY([RoleInProject])
REFERENCES [dbo].[ProjectMemberRoles] ([Id])
GO
ALTER TABLE [dbo].[ProjectMembers]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD FOREIGN KEY([PriorityId])
REFERENCES [dbo].[ProjectPriorities] ([Id])
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD FOREIGN KEY([StatusId])
REFERENCES [dbo].[ProjectStatuses] ([Id])
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD FOREIGN KEY([ReportTypeId])
REFERENCES [dbo].[ReportTypes] ([Id])
GO
ALTER TABLE [dbo].[TaskComments]  WITH CHECK ADD FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([Id])
GO
ALTER TABLE [dbo].[TaskComments]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[TaskDependencies]  WITH CHECK ADD FOREIGN KEY([DependsOnTaskId])
REFERENCES [dbo].[Tasks] ([Id])
GO
ALTER TABLE [dbo].[TaskDependencies]  WITH CHECK ADD FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([Id])
GO
ALTER TABLE [dbo].[TaskHelpRequests]  WITH CHECK ADD FOREIGN KEY([RequestedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[TaskHelpRequests]  WITH CHECK ADD FOREIGN KEY([RequestedTo])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[TaskHelpRequests]  WITH CHECK ADD FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([Id])
GO
ALTER TABLE [dbo].[TaskHistory]  WITH CHECK ADD FOREIGN KEY([ChangedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[TaskHistory]  WITH CHECK ADD FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([Id])
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([AssignedUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([ParentTaskId])
REFERENCES [dbo].[Tasks] ([Id])
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([PriorityId])
REFERENCES [dbo].[TaskPriorities] ([Id])
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([StatusId])
REFERENCES [dbo].[TaskStatuses] ([Id])
GO
ALTER TABLE [dbo].[TimeEntries]  WITH CHECK ADD FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([Id])
GO
ALTER TABLE [dbo].[TimeEntries]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserRolePermissions]  WITH CHECK ADD FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permissions] ([Id])
GO
ALTER TABLE [dbo].[UserRolePermissions]  WITH CHECK ADD FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[UserRoles] ([Id])
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[UserRoles] ([Id])
GO
ALTER TABLE [dbo].[TaskDependencies]  WITH CHECK ADD  CONSTRAINT [CHK_TaskDependency] CHECK  (([TaskId]<>[DependsOnTaskId]))
GO
ALTER TABLE [dbo].[TaskDependencies] CHECK CONSTRAINT [CHK_TaskDependency]
GO
ALTER TABLE [dbo].[TaskDependencies]  WITH CHECK ADD CHECK  (([DependencyType]>=(1) AND [DependencyType]<=(4)))
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [CHK_PercentComplete] CHECK  (([PercentComplete]>=(0) AND [PercentComplete]<=(100)))
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [CHK_PercentComplete]
GO
