
CREATE TABLE [dbo].[FunctionInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[ParentId] [uniqueidentifier] NOT NULL,
	[Order] [int] NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,
	[Path] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FUNCTIONINFO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FunctionInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FunctionInfo', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FunctionInfo', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FunctionInfo', @level2type=N'COLUMN',@level2name=N'ParentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FunctionInfo', @level2type=N'COLUMN',@level2name=N'Order'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FunctionInfo', @level2type=N'COLUMN',@level2name=N'Remark'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FunctionInfo', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FunctionInfo', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FunctionInfo', @level2type=N'COLUMN',@level2name=N'AddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FunctionInfo', @level2type=N'COLUMN',@level2name=N'AddUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FunctionInfo', @level2type=N'COLUMN',@level2name=N'UpdateUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FunctionInfo', @level2type=N'COLUMN',@level2name=N'AddUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FunctionInfo', @level2type=N'COLUMN',@level2name=N'UpdateUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统功能' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'FunctionInfo'
GO



CREATE TABLE [dbo].[RoleInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[ParentId] [uniqueidentifier] NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ROLEINFO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleInfo', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleInfo', @level2type=N'COLUMN',@level2name=N'ParentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleInfo', @level2type=N'COLUMN',@level2name=N'Remark'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleInfo', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleInfo', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleInfo', @level2type=N'COLUMN',@level2name=N'AddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleInfo', @level2type=N'COLUMN',@level2name=N'AddUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleInfo', @level2type=N'COLUMN',@level2name=N'UpdateUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleInfo', @level2type=N'COLUMN',@level2name=N'AddUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleInfo', @level2type=N'COLUMN',@level2name=N'UpdateUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleInfo'
GO



CREATE TABLE [dbo].[RolePermissionInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[FunctionId] [uniqueidentifier] NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ROLEPERMISSIONINFO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePermissionInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePermissionInfo', @level2type=N'COLUMN',@level2name=N'RoleId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePermissionInfo', @level2type=N'COLUMN',@level2name=N'FunctionId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePermissionInfo', @level2type=N'COLUMN',@level2name=N'Remark'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePermissionInfo', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePermissionInfo', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePermissionInfo', @level2type=N'COLUMN',@level2name=N'AddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePermissionInfo', @level2type=N'COLUMN',@level2name=N'AddUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePermissionInfo', @level2type=N'COLUMN',@level2name=N'UpdateUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePermissionInfo', @level2type=N'COLUMN',@level2name=N'AddUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePermissionInfo', @level2type=N'COLUMN',@level2name=N'UpdateUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePermissionInfo'
GO


CREATE TABLE [dbo].[SysDictionary](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Type] [int] NOT NULL,
	[ParentId] [uniqueidentifier] NOT NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SYSDICTIONARY] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'Code'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'Type'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父级别Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'ParentId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本 0表示基础类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'AddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'AddUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'UpdateUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'AddUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary', @level2type=N'COLUMN',@level2name=N'UpdateUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统字典' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysDictionary'
GO


CREATE TABLE [dbo].[SysLog](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Type] [int] NOT NULL,
	[Message] [nvarchar](2000) NOT NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SYSLOG] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLog', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLog', @level2type=N'COLUMN',@level2name=N'Name'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLog', @level2type=N'COLUMN',@level2name=N'Type'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLog', @level2type=N'COLUMN',@level2name=N'Message'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLog', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLog', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLog', @level2type=N'COLUMN',@level2name=N'AddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLog', @level2type=N'COLUMN',@level2name=N'AddUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLog', @level2type=N'COLUMN',@level2name=N'UpdateUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLog', @level2type=N'COLUMN',@level2name=N'AddUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLog', @level2type=N'COLUMN',@level2name=N'UpdateUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统日志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SysLog'
GO


CREATE TABLE [dbo].[UserDataPermissionInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ToUserId] [uniqueidentifier] NOT NULL,
	[Type] [smallint] NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_USERDATAPERMISSIONINFO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'允许访问的用户Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'ToUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型 0：可以访问 1：不能访问' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'Type'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'Remark'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'AddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'AddUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'UpdateUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'AddUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'UpdateUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户数据权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserDataPermissionInfo'
GO


CREATE TABLE [dbo].[UserInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NOT NULL,
	[RealName] [nvarchar](50) NULL,
	[RealNamePin] [nvarchar](200) NULL,
	[Qq] [nvarchar](50) NULL,
	[WxNo] [nvarchar](50) NULL,
	[TelPhone] [nvarchar](50) NULL,
	[PositionName] [nvarchar](50) NULL,
	[Province] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Area] [nvarchar](50) NULL,
	[Address] [nvarchar](200) NULL,
	[HeadImageUrl] [nvarchar](200) NULL,
	[Status] [smallint] NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,
	[Type] [int] NOT NULL, 
	[ExpiredTime] [datetime] NOT NULL,
	[BirthDay] [datetime] NULL,
	[IdCard] [nvarchar](50) NULL,
	[Sex] [int] NOT NULL,
	[WxOpenId] [nvarchar](50) NULL,
	[WxUnionId] [nvarchar](50) NULL,
 CONSTRAINT [PK_USERINFO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserInfo] ADD  CONSTRAINT [DF_UserInfo_Sex]  DEFAULT ((0)) FOR [Sex]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'UserName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机号码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Phone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Email'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Password'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'真实姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'RealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'真实姓名拼音' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'RealNamePin'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'qq号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Qq'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'微信号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'WxNo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'座机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'TelPhone'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职位名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'PositionName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'省' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Province'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'市' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'City'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'区' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Area'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'详细地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Address'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'头像' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'HeadImageUrl'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态：0：启用 1：停用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Status'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Remark'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'AddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'AddUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'UpdateUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'AddUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo', @level2type=N'COLUMN',@level2name=N'UpdateUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserInfo'
GO


CREATE TABLE [dbo].[UserPermissionInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[FunctionId] [uniqueidentifier] NOT NULL,
	[Type] [smallint] NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_USERPERMISSIONINFO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPermissionInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPermissionInfo', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'功能Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPermissionInfo', @level2type=N'COLUMN',@level2name=N'FunctionId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型：0增加 1：减少' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPermissionInfo', @level2type=N'COLUMN',@level2name=N'Type'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPermissionInfo', @level2type=N'COLUMN',@level2name=N'Remark'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPermissionInfo', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPermissionInfo', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPermissionInfo', @level2type=N'COLUMN',@level2name=N'AddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPermissionInfo', @level2type=N'COLUMN',@level2name=N'AddUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPermissionInfo', @level2type=N'COLUMN',@level2name=N'UpdateUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPermissionInfo', @level2type=N'COLUMN',@level2name=N'AddUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPermissionInfo', @level2type=N'COLUMN',@level2name=N'UpdateUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserPermissionInfo'
GO


CREATE TABLE [dbo].[UserRoleDataPermissionInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_USERROLEDATAPERMISSIONINFO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'RoleId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'Remark'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'AddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'AddUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'UpdateUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'AddUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleDataPermissionInfo', @level2type=N'COLUMN',@level2name=N'UpdateUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户部门数据权限' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleDataPermissionInfo'
GO


CREATE TABLE [dbo].[UserRoleInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_USERROLEINFO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleInfo', @level2type=N'COLUMN',@level2name=N'Id'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleInfo', @level2type=N'COLUMN',@level2name=N'UserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleInfo', @level2type=N'COLUMN',@level2name=N'RoleId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleInfo', @level2type=N'COLUMN',@level2name=N'Remark'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'版本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleInfo', @level2type=N'COLUMN',@level2name=N'Version'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleInfo', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddTime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleInfo', @level2type=N'COLUMN',@level2name=N'AddTime'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleInfo', @level2type=N'COLUMN',@level2name=N'AddUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserId' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleInfo', @level2type=N'COLUMN',@level2name=N'UpdateUserId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AddUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleInfo', @level2type=N'COLUMN',@level2name=N'AddUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'UpdateUserRealName' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleInfo', @level2type=N'COLUMN',@level2name=N'UpdateUserRealName'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户角色' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRoleInfo'
GO





CREATE TABLE [dbo].[Hospital](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](200) NULL,	 
	[Province] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Area] [nvarchar](50) NULL,
	[Address] [nvarchar](200) NULL,
	[HeadImageUrl] [nvarchar](200) NULL,
	[Status] [smallint] NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,

 
 CONSTRAINT [PK_HOSPITAL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[Epartmene](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[HospitalId] [uniqueidentifier] NOT NULL,     
	[Location] [nvarchar](200) NULL,
	[Status] [int] NOT NULL,
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,

 
 CONSTRAINT [PK_EPARTMENE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[Doctor](
	[Id] [uniqueidentifier] NOT NULL,	 
	[BelongHospitalId] [uniqueidentifier] NOT NULL,	
	[EpartmeneId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier]  NOT NULL,     
    [Professional] [nvarchar](50)  NULL, 
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,

 
 CONSTRAINT [PK_DOCTOR] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[Patient](
	[Id] [uniqueidentifier] NOT NULL,
    [BelongHospitalId] [uniqueidentifier] NOT NULL,	
	[BelongDoctorId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier]  NOT NULL,     
    [IllnessDesc] [nvarchar](500)  NULL, 
	[From] [int]  NOT NULL DEFAULT 1,--1:门诊 2：住院 3：急诊 
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,

 
 CONSTRAINT [PK_PATIENT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[TransferConsultation](
	[Id] [uniqueidentifier] NOT NULL,	 
	[PatientId] [uniqueidentifier] NOT NULL,
	[TransferConsultationId] [uniqueidentifier] NOT NULL,	
	[From] [int]  NULL DEFAULT 1,--1:门诊 2：住院 3：急诊 
	[FromHospitalId] [uniqueidentifier]  NULL,
	[FromEpartmeneId] [uniqueidentifier]  NULL,
	[FromDoctorId] [uniqueidentifier]  NULL,
	[FromIllnessDesc] [nvarchar](500)  NULL,
	
    [TransferHospitalId] [uniqueidentifier]  NULL,
	[TransferEpartmeneId] [uniqueidentifier]  NULL,
	[TransferDoctorId] [uniqueidentifier]  NULL,
    [Status] [int]  NOT NULL DEFAULT 1,--1:待转诊 2：已确认接诊 4：已接诊 8：已住院 16：已完成 32：拒绝接诊
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,

 
 CONSTRAINT [PK_TRANSFERCONSULTATION] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[TransferConsultationRecord](
	[Id] [uniqueidentifier] NOT NULL,	 
	[PatientId] [uniqueidentifier] NOT NULL,
	[From] [int]  NULL DEFAULT 1,--1:门诊 2：住院 3：急诊 
	[FromHospitalId] [uniqueidentifier]  NULL,
	[FromEpartmeneId] [uniqueidentifier]  NULL,
	[FromDoctorId] [uniqueidentifier]  NULL,
	[FromIllnessDesc] [nvarchar](500)  NULL,
	
	[TransferHospitalId] [uniqueidentifier]  NULL,
	[TransferEpartmeneId] [uniqueidentifier]  NULL,
	[TransferDoctorId] [uniqueidentifier]  NULL,
    
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,

 
 CONSTRAINT [PK_TRANSFERCONSULTATIONRECORD] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ItemType](
	[Id] [uniqueidentifier] NOT NULL,	 
	[Code] [nvarchar](50)  NOT NULL,
    [Name] [nvarchar](50)  NOT NULL,
	[HospitalId] [uniqueidentifier] NOT NULL,	
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,

 
 CONSTRAINT [PK_ITEMTYPE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[Item](
	[Id] [uniqueidentifier] NOT NULL,
	[HospitalId] [uniqueidentifier] NOT NULL,	
    [Name] [nvarchar](50)  NOT NULL,
	[ItemTypeId] [uniqueidentifier] NOT NULL,
	[Price] decimal(10,2)  NOT NULL,
	[ImageUrl] [nvarchar](200) NULL,
	[Status] [int] NULL,--1:上架 2：下架
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,

 
 CONSTRAINT [PK_ITEM] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



CREATE TABLE [dbo].[Order](
	[Id] [uniqueidentifier] NOT NULL,
	[HospitalId] [uniqueidentifier] NOT NULL,	
    [Amount] decimal(10,2)  NOT NULL,
	[Status] [int] NULL,--1:已开单 2：已付款 4：已取消
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,

 
 CONSTRAINT [PK_ORDER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[OrderItem](
	[Id] [uniqueidentifier] NOT NULL,
	[HospitalId] [uniqueidentifier] NOT NULL,
    [OrderId] [uniqueidentifier] NOT NULL,	
    [ItemId] [uniqueidentifier] NOT NULL,	
    [ItemName] [nvarchar](50)  NOT NULL,	
	[Quantity] int  NOT NULL,
	[Price] decimal(10,2)  NOT NULL,	 
	[Remark] [nvarchar](200) NULL,
	[Version] [timestamp] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[AddUserId] [uniqueidentifier] NOT NULL,
	[UpdateUserId] [uniqueidentifier] NOT NULL,
	[AddUserRealName] [nvarchar](50) NOT NULL,
	[UpdateUserRealName] [nvarchar](50) NOT NULL,

 
 CONSTRAINT [PK_ORDERITEM] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


