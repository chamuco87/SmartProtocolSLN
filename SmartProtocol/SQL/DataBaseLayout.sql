USE [DB_A57E75_chamucolol87]
GO
/****** Object:  Schema [Protocol]    Script Date: 4/19/2020 12:56:25 PM ******/
CREATE SCHEMA [Protocol]
GO
/****** Object:  Table [Protocol].[Address]    Script Date: 4/19/2020 12:56:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Protocol].[Address](
	[AddressId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[AddressTypeId] [int] NOT NULL,
	[Address1] [varchar](80) NOT NULL,
	[Address2] [varchar](80) NULL,
	[City] [varchar](80) NOT NULL,
	[State] [varchar](80) NOT NULL,
	[Country] [varchar](80) NOT NULL,
	[ZipCode] [varchar](80) NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Protocol].[AddressType]    Script Date: 4/19/2020 12:56:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Protocol].[AddressType](
	[AddressTypeId] [int] IDENTITY(1,1) NOT NULL,
	[AddressTypeName] [varchar](20) NOT NULL,
	[AddressTypeDescription] [varchar](80) NULL,
 CONSTRAINT [PK_AddressType] PRIMARY KEY CLUSTERED 
(
	[AddressTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Protocol].[AlertType]    Script Date: 4/19/2020 12:56:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Protocol].[AlertType](
	[AlertTypeId] [int] IDENTITY(1,1) NOT NULL,
	[AlertTypeName] [varchar](20) NOT NULL,
	[AlertTypeDescription] [varchar](80) NULL,
 CONSTRAINT [PK_AlertType] PRIMARY KEY CLUSTERED 
(
	[AlertTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Protocol].[Email]    Script Date: 4/19/2020 12:56:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Protocol].[Email](
	[EmailId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[EmailAddress] [varchar](50) NOT NULL UNIQUE,
	[IsPrimary] [bit] NOT NULL,
	[IsVerified] [bit] NOT NULL,
 CONSTRAINT [PK_Email1] PRIMARY KEY CLUSTERED 
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Protocol].[Flow]    Script Date: 4/19/2020 12:56:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Protocol].[Flow](
	[FlowId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[FlowName] [varchar](50) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Flow] PRIMARY KEY CLUSTERED 
(
	[FlowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Protocol].[Login]    Script Date: 4/19/2020 12:56:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Protocol].[Login](
	[LoginId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[EmailId] [bigint] NOT NULL,
	[Password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[LoginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Protocol].[Step]    Script Date: 4/19/2020 12:56:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Protocol].[Step](
	[StepId] [bigint] IDENTITY(1,1) NOT NULL,
	[FlowId] [bigint] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[StepTypeId] [int] NOT NULL,
	[StepObject] [varchar](max) NOT NULL,
	[StepName] [varchar](80) NOT NULL,
	[StepStatus] [varchar](80) NOT NULL,
	[StepStartedOn] [datetime] NULL,
	[StepCompletedOn] [datetime] NULL,
	[ClaimedBy] [datetime] NULL,
	[TriggerDateTime] [datetime] NOT NULL,
	[RetryCount] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Step] PRIMARY KEY CLUSTERED 
(
	[StepId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Protocol].[StepAlert]    Script Date: 4/19/2020 12:56:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Protocol].[StepAlert](
	[StepAlertId] [int] IDENTITY(1,1) NOT NULL,
	[StepId] [bigint] NOT NULL,
	[AlertTypeId] [int] NOT NULL,
	[StepAlertStatus] [varchar](80) NOT NULL,
	[StepStartedOn] [datetime] NULL,
	[StepCompletedOn] [datetime] NULL,
	[RetryCount] [int] NOT NULL,
 CONSTRAINT [PK_StepAlert] PRIMARY KEY CLUSTERED 
(
	[StepAlertId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Protocol].[StepType]    Script Date: 4/19/2020 12:56:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Protocol].[StepType](
	[StepTypeId] [int] IDENTITY(1,1) NOT NULL,
	[StepTypeName] [varchar](20) NOT NULL,
	[StepTypeDescription] [varchar](80) NULL,
 CONSTRAINT [PK_StepType] PRIMARY KEY CLUSTERED 
(
	[StepTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Protocol].[Telephone]    Script Date: 4/19/2020 12:56:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Protocol].[Telephone](
	[TelephoneId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[TelephoneTypeId] [int] NOT NULL,
	[TelephoneNumber] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Telephone] PRIMARY KEY CLUSTERED 
(
	[TelephoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Protocol].[TelephoneType]    Script Date: 4/19/2020 12:56:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Protocol].[TelephoneType](
	[TelephoneTypeId] [int] IDENTITY(1,1) NOT NULL,
	[TelephoneTypeName] [varchar](20) NOT NULL,
	[TelephoneTypeDescription] [varchar](80) NULL,
 CONSTRAINT [PK_TelephoneType] PRIMARY KEY CLUSTERED 
(
	[TelephoneTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Protocol].[User]    Script Date: 4/19/2020 12:56:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Protocol].[User](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[First] [varchar](50) NULL,
	[Last] [varchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [Protocol].[Address]  WITH CHECK ADD FOREIGN KEY([AddressTypeId])
REFERENCES [Protocol].[AddressType] ([AddressTypeId])
ON DELETE CASCADE
GO
ALTER TABLE [Protocol].[Address]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [Protocol].[User] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [Protocol].[Email]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [Protocol].[User] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [Protocol].[Flow]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [Protocol].[User] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [Protocol].[Login]  WITH CHECK ADD FOREIGN KEY([EmailId])
REFERENCES [Protocol].[Email] ([EmailId])
ON DELETE CASCADE
GO
ALTER TABLE [Protocol].[Login]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [Protocol].[User] ([UserId])
GO
ALTER TABLE [Protocol].[Step]  WITH CHECK ADD FOREIGN KEY([FlowId])
REFERENCES [Protocol].[Flow] ([FlowId])
ON DELETE CASCADE
GO
ALTER TABLE [Protocol].[Step]  WITH CHECK ADD FOREIGN KEY([StepTypeId])
REFERENCES [Protocol].[StepType] ([StepTypeId])
ON DELETE CASCADE
GO
ALTER TABLE [Protocol].[Step]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [Protocol].[User] ([UserId])
GO
ALTER TABLE [Protocol].[StepAlert]  WITH CHECK ADD FOREIGN KEY([AlertTypeId])
REFERENCES [Protocol].[AlertType] ([AlertTypeId])
ON DELETE CASCADE
GO
ALTER TABLE [Protocol].[StepAlert]  WITH CHECK ADD FOREIGN KEY([StepId])
REFERENCES [Protocol].[Step] ([StepId])
ON DELETE CASCADE
GO
ALTER TABLE [Protocol].[Telephone]  WITH CHECK ADD FOREIGN KEY([TelephoneTypeId])
REFERENCES [Protocol].[TelephoneType] ([TelephoneTypeId])
ON DELETE CASCADE
GO
ALTER TABLE [Protocol].[Telephone]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [Protocol].[User] ([UserId])
ON DELETE CASCADE
GO
