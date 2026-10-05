USE [master]
GO
/****** Object:  Database [RoleManagement]    Script Date: 05-10-2026 16:13:44 ******/
CREATE DATABASE [RoleManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RoleManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLSERVER1\MSSQL\DATA\RoleManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RoleManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLSERVER1\MSSQL\DATA\RoleManagement_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [RoleManagement] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RoleManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RoleManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RoleManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RoleManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RoleManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RoleManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [RoleManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RoleManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RoleManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RoleManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RoleManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RoleManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RoleManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RoleManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RoleManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RoleManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RoleManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RoleManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RoleManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RoleManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RoleManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RoleManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RoleManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RoleManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [RoleManagement] SET  MULTI_USER 
GO
ALTER DATABASE [RoleManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RoleManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RoleManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RoleManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RoleManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RoleManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'RoleManagement', N'ON'
GO
ALTER DATABASE [RoleManagement] SET QUERY_STORE = ON
GO
ALTER DATABASE [RoleManagement] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [RoleManagement]
GO
/****** Object:  Table [dbo].[CustomerList]    Script Date: 05-10-2026 16:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerList](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](max) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[Amount] [int] NOT NULL,
 CONSTRAINT [PK_CustomerList] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeList]    Script Date: 05-10-2026 16:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeList](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [nvarchar](max) NOT NULL,
	[DepartmentName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_EmployeeList] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PageNames]    Script Date: 05-10-2026 16:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageNames](
	[PageId] [int] IDENTITY(1,1) NOT NULL,
	[PageName] [nvarchar](max) NOT NULL,
	[Controller] [nvarchar](max) NULL,
	[Action] [nvarchar](max) NULL,
 CONSTRAINT [PK_PageName] PRIMARY KEY CLUSTERED 
(
	[PageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolePermission]    Script Date: 05-10-2026 16:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PageId] [int] NOT NULL,
	[Add] [bit] NOT NULL,
	[Edit] [bit] NOT NULL,
	[Delete] [bit] NOT NULL,
	[View] [bit] NOT NULL,
	[EditPermission] [bit] NOT NULL,
 CONSTRAINT [PK_RolePermission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentList]    Script Date: 05-10-2026 16:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentList](
	[StudentId] [int] IDENTITY(1,1) NOT NULL,
	[StudentName] [nvarchar](max) NOT NULL,
	[RollNumber] [int] NOT NULL,
	[Class] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_StudentList] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 05-10-2026 16:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 05-10-2026 16:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Role] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CustomerList] ON 
GO
INSERT [dbo].[CustomerList] ([CustomerId], [CustomerName], [ProductName], [Amount]) VALUES (1, N'jay', N'cd', 22)
GO
SET IDENTITY_INSERT [dbo].[CustomerList] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeList] ON 
GO
INSERT [dbo].[EmployeeList] ([EmployeeId], [EmployeeName], [DepartmentName]) VALUES (3, N'emp1', N'a')
GO
SET IDENTITY_INSERT [dbo].[EmployeeList] OFF
GO
SET IDENTITY_INSERT [dbo].[PageNames] ON 
GO
INSERT [dbo].[PageNames] ([PageId], [PageName], [Controller], [Action]) VALUES (1, N'StudentList', N'StudentList', N'StudentList')
GO
INSERT [dbo].[PageNames] ([PageId], [PageName], [Controller], [Action]) VALUES (2, N'EmployeeList', N'EmployeeList', N'EmployeeList')
GO
INSERT [dbo].[PageNames] ([PageId], [PageName], [Controller], [Action]) VALUES (3, N'CustomerList', N'CustomerList', N'CustomerList')
GO
INSERT [dbo].[PageNames] ([PageId], [PageName], [Controller], [Action]) VALUES (4, N'UserList', N'UserList', N'UserList')
GO
INSERT [dbo].[PageNames] ([PageId], [PageName], [Controller], [Action]) VALUES (5, N'UserRole', N'UserRole', N'UserRole')
GO
SET IDENTITY_INSERT [dbo].[PageNames] OFF
GO
SET IDENTITY_INSERT [dbo].[RolePermission] ON 
GO
INSERT [dbo].[RolePermission] ([Id], [UserId], [PageId], [Add], [Edit], [Delete], [View], [EditPermission]) VALUES (1, 1, 1, 1, 1, 1, 1, 0)
GO
INSERT [dbo].[RolePermission] ([Id], [UserId], [PageId], [Add], [Edit], [Delete], [View], [EditPermission]) VALUES (2, 1, 2, 1, 1, 1, 1, 0)
GO
INSERT [dbo].[RolePermission] ([Id], [UserId], [PageId], [Add], [Edit], [Delete], [View], [EditPermission]) VALUES (3, 1, 3, 1, 1, 1, 1, 0)
GO
INSERT [dbo].[RolePermission] ([Id], [UserId], [PageId], [Add], [Edit], [Delete], [View], [EditPermission]) VALUES (4, 2, 1, 1, 0, 1, 1, 0)
GO
INSERT [dbo].[RolePermission] ([Id], [UserId], [PageId], [Add], [Edit], [Delete], [View], [EditPermission]) VALUES (5, 2, 2, 0, 1, 0, 1, 0)
GO
INSERT [dbo].[RolePermission] ([Id], [UserId], [PageId], [Add], [Edit], [Delete], [View], [EditPermission]) VALUES (6, 2, 3, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[RolePermission] ([Id], [UserId], [PageId], [Add], [Edit], [Delete], [View], [EditPermission]) VALUES (52, 18, 1, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[RolePermission] ([Id], [UserId], [PageId], [Add], [Edit], [Delete], [View], [EditPermission]) VALUES (53, 18, 2, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[RolePermission] ([Id], [UserId], [PageId], [Add], [Edit], [Delete], [View], [EditPermission]) VALUES (54, 18, 3, 0, 0, 0, 1, 1)
GO
INSERT [dbo].[RolePermission] ([Id], [UserId], [PageId], [Add], [Edit], [Delete], [View], [EditPermission]) VALUES (55, 1, 4, 1, 1, 1, 1, 1)
GO
INSERT [dbo].[RolePermission] ([Id], [UserId], [PageId], [Add], [Edit], [Delete], [View], [EditPermission]) VALUES (56, 2, 4, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[RolePermission] ([Id], [UserId], [PageId], [Add], [Edit], [Delete], [View], [EditPermission]) VALUES (57, 18, 4, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[RolePermission] ([Id], [UserId], [PageId], [Add], [Edit], [Delete], [View], [EditPermission]) VALUES (66, 1, 5, 1, 1, 1, 1, 1)
GO
INSERT [dbo].[RolePermission] ([Id], [UserId], [PageId], [Add], [Edit], [Delete], [View], [EditPermission]) VALUES (67, 18, 5, 0, 0, 0, 0, 0)
GO
INSERT [dbo].[RolePermission] ([Id], [UserId], [PageId], [Add], [Edit], [Delete], [View], [EditPermission]) VALUES (68, 2, 5, 0, 0, 0, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[RolePermission] OFF
GO
SET IDENTITY_INSERT [dbo].[StudentList] ON 
GO
INSERT [dbo].[StudentList] ([StudentId], [StudentName], [RollNumber], [Class]) VALUES (1, N'denish', 123, N'a')
GO
SET IDENTITY_INSERT [dbo].[StudentList] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoles] ON 
GO
INSERT [dbo].[UserRoles] ([RoleId], [RoleName]) VALUES (1, N'admin')
GO
INSERT [dbo].[UserRoles] ([RoleId], [RoleName]) VALUES (4, N'user')
GO
INSERT [dbo].[UserRoles] ([RoleId], [RoleName]) VALUES (5, N'manager')
GO
SET IDENTITY_INSERT [dbo].[UserRoles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [IsActive], [Role]) VALUES (1, N'admin', N'admin', N'admin@gmail.com', 1, N'admin')
GO
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [IsActive], [Role]) VALUES (2, N'user1', N'user1', N'user1@gmail.com', 1, N'User')
GO
INSERT [dbo].[Users] ([UserId], [Username], [Password], [Email], [IsActive], [Role]) VALUES (18, N'user2', N'user2', N'user2@gmail.com', 1, N'User')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[RolePermission]  WITH CHECK ADD  CONSTRAINT [fk_RolePermission_PageId_PageNames_PageId] FOREIGN KEY([PageId])
REFERENCES [dbo].[PageNames] ([PageId])
GO
ALTER TABLE [dbo].[RolePermission] CHECK CONSTRAINT [fk_RolePermission_PageId_PageNames_PageId]
GO
ALTER TABLE [dbo].[RolePermission]  WITH CHECK ADD  CONSTRAINT [fk_RolePermission_UserId_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[RolePermission] CHECK CONSTRAINT [fk_RolePermission_UserId_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [RoleManagement] SET  READ_WRITE 
GO
