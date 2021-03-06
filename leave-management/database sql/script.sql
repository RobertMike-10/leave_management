USE [aspnet-leave_management]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/4/2021 9:00:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 8/4/2021 9:00:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 8/4/2021 9:00:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 8/4/2021 9:00:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 8/4/2021 9:00:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 8/4/2021 9:00:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 8/4/2021 9:00:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[DateJoined] [datetime2](7) NULL,
	[DateOfBirth] [datetime2](7) NULL,
	[Discriminator] [nvarchar](max) NOT NULL,
	[Firstname] [nvarchar](100) NULL,
	[Lastname] [nvarchar](100) NULL,
	[TaxId] [nvarchar](100) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 8/4/2021 9:00:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaveAllocations]    Script Date: 8/4/2021 9:00:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveAllocations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumberOfDays] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[EmployeeId] [nvarchar](450) NULL,
	[LeaveTypeId] [int] NOT NULL,
	[Period] [int] NOT NULL,
 CONSTRAINT [PK_LeaveAllocations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaveHistories]    Script Date: 8/4/2021 9:00:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveHistories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RequestingEmployeeId] [nvarchar](450) NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[LeaveTypeId] [int] NOT NULL,
	[DateRequested] [datetime2](7) NOT NULL,
	[DateActioned] [datetime2](7) NULL,
	[Approved] [bit] NULL,
	[ApprovedById] [nvarchar](450) NULL,
	[RequestComments] [nvarchar](max) NULL,
	[Cancelled] [bit] NULL,
 CONSTRAINT [PK_LeaveHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaveTypes]    Script Date: 8/4/2021 9:00:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DefaultDays] [int] NOT NULL,
 CONSTRAINT [PK_LeaveTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'00000000000000_CreateIdentitySchema', N'5.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210726204258_AddedEmployeeDataPoints', N'5.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210726211951_AddedLeaveDetailsTables', N'5.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210727162414_AddRestrictions', N'5.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210728204929_AddedFields', N'5.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210730174708_LeaveHistoryChanged', N'5.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210730174919_RemoveUnusedTables', N'5.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210730223650_AddedCancelledField', N'5.0.8')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'28c023c1-48bd-40f3-94a7-6e27c215fe66', N'Administrator', N'ADMINISTRATOR', N'9a45c5bd-d307-4b5f-94a7-8b06f1f521d3')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'f29c2e7b-c0b4-4c06-a885-0b7dc21ab4ce', N'Employee', N'EMPLOYEE', N'4668f502-04a4-4fb9-aa68-9e295a3db1a5')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'32d74f3e-5323-477c-8e75-e2026fd1b0ae', N'28c023c1-48bd-40f3-94a7-6e27c215fe66')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'17b9e1fe-e66a-4262-ac68-60488a086974', N'f29c2e7b-c0b4-4c06-a885-0b7dc21ab4ce')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2e7dd468-5f5f-4fdc-8a38-666e066a7a3e', N'f29c2e7b-c0b4-4c06-a885-0b7dc21ab4ce')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f67e5363-eac6-484d-a3d8-b3a8eb9ac50a', N'f29c2e7b-c0b4-4c06-a885-0b7dc21ab4ce')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [DateJoined], [DateOfBirth], [Discriminator], [Firstname], [Lastname], [TaxId]) VALUES (N'17b9e1fe-e66a-4262-ac68-60488a086974', N'bethi@gmail.com', N'BETHI@GMAIL.COM', N'bethi@gmail.com', N'BETHI@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEF/baOHMJG/X07h0v+siMesOOLCXks16odqzqzsVzNIzlop/THMG8QBGjjZ3lIR5TA==', N'FJQ5FBMYJDJ2J6MYNDHO3DGZQ7KYJDTZ', N'690182df-6f29-4d33-832e-42e3e7a89280', NULL, 0, 0, NULL, 1, 0, CAST(N'2021-07-29T13:13:01.1906568' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'Employee', N'Beatriz', N'Martínez Gallegos', NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [DateJoined], [DateOfBirth], [Discriminator], [Firstname], [Lastname], [TaxId]) VALUES (N'2e7dd468-5f5f-4fdc-8a38-666e066a7a3e', N'esmebe@localhost.com', N'ESMEBE@LOCALHOST.COM', N'esmebe@localhost.com', N'ESMEBE@LOCALHOST.COM', 0, N'AQAAAAEAACcQAAAAEBhQiRL2oDPK58/rBvuCbbkwLoLUUCPoC+R/Xbf4LxyvZ68uno1bJOl8JaBn1CUpCg==', N'Z5B5JKEWHLK7QOKFYKEGCYNMP2FVYNXI', N'd5fe94af-5c1c-40be-8c6c-31363d5d129e', NULL, 0, 0, NULL, 1, 0, CAST(N'2021-08-02T13:12:10.8479098' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'Employee', N'Esmeralda', N'Ramírez Alonso', NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [DateJoined], [DateOfBirth], [Discriminator], [Firstname], [Lastname], [TaxId]) VALUES (N'32d74f3e-5323-477c-8e75-e2026fd1b0ae', N'admin@localhost.com', N'ADMIN@LOCALHOST.COM', N'admin@localhost.com', N'ADMIN@LOCALHOST.COM', 0, N'AQAAAAEAACcQAAAAEA0RxU7nLAH7hA/4j4cjPbAD2mypg4Ac+vq+bd8sl5CjXdv4Bv9WlVK3HLul5hy4LA==', N'674I7Z6NLKIFMKSWDYHTR3LWJSFXZFZ5', N'd1291f94-5dcb-4248-a867-548d6746435e', NULL, 0, 0, NULL, 1, 0, NULL, NULL, N'Employee', N'Zabdiel', N'García', NULL)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [DateJoined], [DateOfBirth], [Discriminator], [Firstname], [Lastname], [TaxId]) VALUES (N'f67e5363-eac6-484d-a3d8-b3a8eb9ac50a', N'robertomiguelgarcia@outlook.es', N'ROBERTOMIGUELGARCIA@OUTLOOK.ES', N'robertomiguelgarcia@outlook.es', N'ROBERTOMIGUELGARCIA@OUTLOOK.ES', 0, N'AQAAAAEAACcQAAAAEIhB9TSkXnmhaKZBIxVtnKcdJ/hQFGYYUa5D+CRaTR09lSJ/ztIrgOhlXRzSwV1DVw==', N'P2KKVCLQLMKRADED6E4LWX7LGHDB7VQK', N'1a03bdf1-5c47-479a-b9ef-f415db263e8b', NULL, 0, 0, NULL, 1, 0, CAST(N'2021-08-03T17:02:14.1784816' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'Employee', N'Esmeralda', N'Ramírez', NULL)
GO
SET IDENTITY_INSERT [dbo].[LeaveAllocations] ON 

INSERT [dbo].[LeaveAllocations] ([Id], [NumberOfDays], [DateCreated], [EmployeeId], [LeaveTypeId], [Period]) VALUES (2, 15, CAST(N'2021-07-29T15:53:05.1925664' AS DateTime2), N'17b9e1fe-e66a-4262-ac68-60488a086974', 3, 2021)
INSERT [dbo].[LeaveAllocations] ([Id], [NumberOfDays], [DateCreated], [EmployeeId], [LeaveTypeId], [Period]) VALUES (3, 20, CAST(N'2021-07-29T15:53:54.0820950' AS DateTime2), N'17b9e1fe-e66a-4262-ac68-60488a086974', 1002, 2021)
INSERT [dbo].[LeaveAllocations] ([Id], [NumberOfDays], [DateCreated], [EmployeeId], [LeaveTypeId], [Period]) VALUES (5, 55, CAST(N'2021-07-29T15:54:58.0237485' AS DateTime2), N'17b9e1fe-e66a-4262-ac68-60488a086974', 1003, 2021)
INSERT [dbo].[LeaveAllocations] ([Id], [NumberOfDays], [DateCreated], [EmployeeId], [LeaveTypeId], [Period]) VALUES (7, 15, CAST(N'2021-07-29T15:56:18.3793328' AS DateTime2), N'17b9e1fe-e66a-4262-ac68-60488a086974', 1004, 2021)
INSERT [dbo].[LeaveAllocations] ([Id], [NumberOfDays], [DateCreated], [EmployeeId], [LeaveTypeId], [Period]) VALUES (1002, -3, CAST(N'2021-08-03T12:44:03.9710644' AS DateTime2), N'2e7dd468-5f5f-4fdc-8a38-666e066a7a3e', 1003, 2021)
INSERT [dbo].[LeaveAllocations] ([Id], [NumberOfDays], [DateCreated], [EmployeeId], [LeaveTypeId], [Period]) VALUES (1003, 2, CAST(N'2021-08-03T13:27:32.6892574' AS DateTime2), N'2e7dd468-5f5f-4fdc-8a38-666e066a7a3e', 1002, 2021)
SET IDENTITY_INSERT [dbo].[LeaveAllocations] OFF
GO
SET IDENTITY_INSERT [dbo].[LeaveHistories] ON 

INSERT [dbo].[LeaveHistories] ([Id], [RequestingEmployeeId], [StartDate], [EndDate], [LeaveTypeId], [DateRequested], [DateActioned], [Approved], [ApprovedById], [RequestComments], [Cancelled]) VALUES (1, N'17b9e1fe-e66a-4262-ac68-60488a086974', CAST(N'2021-07-28T00:00:00.0000000' AS DateTime2), CAST(N'2021-07-31T00:00:00.0000000' AS DateTime2), 1002, CAST(N'2021-07-30T12:56:42.2349157' AS DateTime2), CAST(N'2021-07-30T16:11:44.9490225' AS DateTime2), 1, N'32d74f3e-5323-477c-8e75-e2026fd1b0ae', N'xxxx', NULL)
INSERT [dbo].[LeaveHistories] ([Id], [RequestingEmployeeId], [StartDate], [EndDate], [LeaveTypeId], [DateRequested], [DateActioned], [Approved], [ApprovedById], [RequestComments], [Cancelled]) VALUES (5, N'17b9e1fe-e66a-4262-ac68-60488a086974', CAST(N'2021-07-30T00:00:00.0000000' AS DateTime2), CAST(N'2021-08-04T00:00:00.0000000' AS DateTime2), 1002, CAST(N'2021-07-30T17:44:57.3754424' AS DateTime2), NULL, NULL, NULL, N'hola mundo', 1)
INSERT [dbo].[LeaveHistories] ([Id], [RequestingEmployeeId], [StartDate], [EndDate], [LeaveTypeId], [DateRequested], [DateActioned], [Approved], [ApprovedById], [RequestComments], [Cancelled]) VALUES (6, N'17b9e1fe-e66a-4262-ac68-60488a086974', CAST(N'2021-07-31T00:00:00.0000000' AS DateTime2), CAST(N'2021-08-05T00:00:00.0000000' AS DateTime2), 1003, CAST(N'2021-07-30T17:46:44.5063447' AS DateTime2), NULL, NULL, NULL, N'rgr', 1)
INSERT [dbo].[LeaveHistories] ([Id], [RequestingEmployeeId], [StartDate], [EndDate], [LeaveTypeId], [DateRequested], [DateActioned], [Approved], [ApprovedById], [RequestComments], [Cancelled]) VALUES (1006, N'2e7dd468-5f5f-4fdc-8a38-666e066a7a3e', CAST(N'2021-08-03T00:00:00.0000000' AS DateTime2), CAST(N'2021-08-31T00:00:00.0000000' AS DateTime2), 1003, CAST(N'2021-08-03T12:44:38.7142839' AS DateTime2), CAST(N'2021-08-03T12:50:52.4825808' AS DateTime2), 1, N'32d74f3e-5323-477c-8e75-e2026fd1b0ae', N'first baby', 0)
INSERT [dbo].[LeaveHistories] ([Id], [RequestingEmployeeId], [StartDate], [EndDate], [LeaveTypeId], [DateRequested], [DateActioned], [Approved], [ApprovedById], [RequestComments], [Cancelled]) VALUES (1007, N'2e7dd468-5f5f-4fdc-8a38-666e066a7a3e', CAST(N'2021-08-03T00:00:00.0000000' AS DateTime2), CAST(N'2021-08-18T00:00:00.0000000' AS DateTime2), 1003, CAST(N'2021-08-03T12:50:25.5168042' AS DateTime2), CAST(N'2021-08-03T12:51:04.1519124' AS DateTime2), 1, N'32d74f3e-5323-477c-8e75-e2026fd1b0ae', N'my child', 0)
INSERT [dbo].[LeaveHistories] ([Id], [RequestingEmployeeId], [StartDate], [EndDate], [LeaveTypeId], [DateRequested], [DateActioned], [Approved], [ApprovedById], [RequestComments], [Cancelled]) VALUES (1008, N'2e7dd468-5f5f-4fdc-8a38-666e066a7a3e', CAST(N'2021-08-03T00:00:00.0000000' AS DateTime2), CAST(N'2021-08-11T00:00:00.0000000' AS DateTime2), 1002, CAST(N'2021-08-03T13:28:13.7398423' AS DateTime2), NULL, NULL, NULL, N'go to cancun', 0)
INSERT [dbo].[LeaveHistories] ([Id], [RequestingEmployeeId], [StartDate], [EndDate], [LeaveTypeId], [DateRequested], [DateActioned], [Approved], [ApprovedById], [RequestComments], [Cancelled]) VALUES (1009, N'2e7dd468-5f5f-4fdc-8a38-666e066a7a3e', CAST(N'2021-08-03T00:00:00.0000000' AS DateTime2), CAST(N'2021-08-11T00:00:00.0000000' AS DateTime2), 1002, CAST(N'2021-08-03T13:28:42.9430745' AS DateTime2), NULL, NULL, NULL, N'go to Paris with love', 1)
INSERT [dbo].[LeaveHistories] ([Id], [RequestingEmployeeId], [StartDate], [EndDate], [LeaveTypeId], [DateRequested], [DateActioned], [Approved], [ApprovedById], [RequestComments], [Cancelled]) VALUES (1010, N'2e7dd468-5f5f-4fdc-8a38-666e066a7a3e', CAST(N'2021-08-03T00:00:00.0000000' AS DateTime2), CAST(N'2021-08-17T00:00:00.0000000' AS DateTime2), 1002, CAST(N'2021-08-03T13:29:25.2643982' AS DateTime2), CAST(N'2021-08-03T13:29:47.0110361' AS DateTime2), 1, N'32d74f3e-5323-477c-8e75-e2026fd1b0ae', N'go to paris with my love', 0)
SET IDENTITY_INSERT [dbo].[LeaveHistories] OFF
GO
SET IDENTITY_INSERT [dbo].[LeaveTypes] ON 

INSERT [dbo].[LeaveTypes] ([Id], [Name], [DateCreated], [DefaultDays]) VALUES (3, N'Sick Leave', CAST(N'2021-07-27T15:43:32.0000000' AS DateTime2), 1)
INSERT [dbo].[LeaveTypes] ([Id], [Name], [DateCreated], [DefaultDays]) VALUES (1002, N'Vacation Leave', CAST(N'2021-07-28T16:09:02.7894493' AS DateTime2), 16)
INSERT [dbo].[LeaveTypes] ([Id], [Name], [DateCreated], [DefaultDays]) VALUES (1003, N'Maternity Leave', CAST(N'2021-07-28T16:09:34.0000000' AS DateTime2), 40)
INSERT [dbo].[LeaveTypes] ([Id], [Name], [DateCreated], [DefaultDays]) VALUES (1004, N'Paternity Leave', CAST(N'2021-07-28T16:10:18.9821560' AS DateTime2), 15)
SET IDENTITY_INSERT [dbo].[LeaveTypes] OFF
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (N'') FOR [Discriminator]
GO
ALTER TABLE [dbo].[LeaveAllocations] ADD  DEFAULT ((0)) FOR [Period]
GO
ALTER TABLE [dbo].[LeaveTypes] ADD  DEFAULT ((0)) FOR [DefaultDays]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[LeaveAllocations]  WITH CHECK ADD  CONSTRAINT [FK_LeaveAllocations_AspNetUsers_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[LeaveAllocations] CHECK CONSTRAINT [FK_LeaveAllocations_AspNetUsers_EmployeeId]
GO
ALTER TABLE [dbo].[LeaveAllocations]  WITH CHECK ADD  CONSTRAINT [FK_LeaveAllocations_LeaveTypes_LeaveTypeId] FOREIGN KEY([LeaveTypeId])
REFERENCES [dbo].[LeaveTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LeaveAllocations] CHECK CONSTRAINT [FK_LeaveAllocations_LeaveTypes_LeaveTypeId]
GO
ALTER TABLE [dbo].[LeaveHistories]  WITH CHECK ADD  CONSTRAINT [FK_LeaveHistories_AspNetUsers_ApprovedById] FOREIGN KEY([ApprovedById])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[LeaveHistories] CHECK CONSTRAINT [FK_LeaveHistories_AspNetUsers_ApprovedById]
GO
ALTER TABLE [dbo].[LeaveHistories]  WITH CHECK ADD  CONSTRAINT [FK_LeaveHistories_AspNetUsers_RequestingEmployeeId] FOREIGN KEY([RequestingEmployeeId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[LeaveHistories] CHECK CONSTRAINT [FK_LeaveHistories_AspNetUsers_RequestingEmployeeId]
GO
ALTER TABLE [dbo].[LeaveHistories]  WITH CHECK ADD  CONSTRAINT [FK_LeaveHistories_LeaveTypes_LeaveTypeId] FOREIGN KEY([LeaveTypeId])
REFERENCES [dbo].[LeaveTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LeaveHistories] CHECK CONSTRAINT [FK_LeaveHistories_LeaveTypes_LeaveTypeId]
GO
