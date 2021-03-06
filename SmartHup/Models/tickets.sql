USE [master]
GO
/****** Object:  Database [Tickets]    Script Date: 11/12/2018 09:19:43 ص ******/
CREATE DATABASE [Tickets]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Tickets', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Tickets.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Tickets_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Tickets_log.ldf' , SIZE = 10176KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Tickets] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Tickets].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Tickets] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Tickets] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Tickets] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Tickets] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Tickets] SET ARITHABORT OFF 
GO
ALTER DATABASE [Tickets] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Tickets] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Tickets] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Tickets] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Tickets] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Tickets] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Tickets] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Tickets] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Tickets] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Tickets] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Tickets] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Tickets] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Tickets] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Tickets] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Tickets] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Tickets] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Tickets] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Tickets] SET RECOVERY FULL 
GO
ALTER DATABASE [Tickets] SET  MULTI_USER 
GO
ALTER DATABASE [Tickets] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Tickets] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Tickets] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Tickets] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Tickets] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Tickets', N'ON'
GO
USE [Tickets]
GO
/****** Object:  Table [dbo].[Action]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Action](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](64) NULL,
	[Ename] [nvarchar](64) NULL,
	[label] [nvarchar](64) NULL,
	[Elabel] [nvarchar](64) NULL,
	[pageType] [nvarchar](64) NULL,
	[moduleId] [bigint] NOT NULL,
	[creationDate] [datetime] NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NULL,
	[deletedDate] [datetime] NULL,
	[entityStatus_systemId] [int] NULL,
	[deletedBy] [bigint] NULL,
 CONSTRAINT [PK_Action] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Announcement]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Announcement](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[AnnouncementTitle] [nvarchar](max) NOT NULL,
	[AnnouncementArTitle] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[StartDate] [datetime] NULL,
	[UserGroup_ID] [bigint] NULL,
	[Company_ID] [bigint] NOT NULL,
	[creationDate] [datetime] NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NULL,
	[entityStatus_systemId] [int] NULL,
 CONSTRAINT [PK_Announcement] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Attachements]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachements](
	[systemId] [bigint] NOT NULL,
	[Referance] [bigint] NOT NULL,
	[FileContent] [image] NOT NULL,
	[FileName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Attachements] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AuditingLog]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditingLog](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[userId] [bigint] NULL,
	[actionId] [bigint] NOT NULL,
	[moduleId] [bigint] NOT NULL,
	[oldData] [nvarchar](max) NULL,
	[dateCreated] [datetime] NOT NULL,
	[clientData] [nvarchar](max) NULL,
	[creationDate] [datetime] NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NULL,
	[entityStatus_systemId] [int] NULL,
 CONSTRAINT [PK_AuditingLog] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[systemId] [bigint] NOT NULL,
	[CategoryName] [nvarchar](32) NOT NULL,
	[CategoryArName] [nvarchar](32) NOT NULL,
	[CategoryType] [int] NULL,
	[isChild] [bit] NULL,
	[ParentCategory] [bigint] NULL,
	[entityStatus_systemId] [int] NOT NULL,
	[creationDate] [datetime] NOT NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NOT NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CategoryType]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryType](
	[systemId] [int] NOT NULL,
	[CategoryType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CategoryType] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChatQueue]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatQueue](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[ChatQueueName] [nvarchar](32) NOT NULL,
	[ChatQueueArName] [nvarchar](32) NOT NULL,
	[IdleTime] [int] NULL,
	[CloseTime] [int] NULL,
	[UserGroup_ID] [bigint] NULL,
	[creationDate] [datetime] NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NULL,
	[entityStatus_systemId] [int] NULL,
 CONSTRAINT [PK_ChatQueue] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChatQueueMessages]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatQueueMessages](
	[systemId] [bigint] NOT NULL,
	[Title] [nvarchar](64) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[ChatQueue] [bigint] NULL,
 CONSTRAINT [PK_ChatQueueMessages] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChatSessions]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatSessions](
	[systemId] [bigint] NOT NULL,
	[SubmitterUser] [bigint] NOT NULL,
	[TicketId] [bigint] NULL,
	[ChatQueue] [bigint] NOT NULL,
	[AssignedUser] [bigint] NULL,
	[start_time] [datetime] NULL,
	[end_time] [datetime] NULL,
	[modified_time] [datetime] NULL,
	[acceptTime] [datetime] NULL,
 CONSTRAINT [PK_ChatSessions] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Company]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[systemId] [bigint] NOT NULL,
	[CompanyName] [nvarchar](max) NOT NULL,
	[CompanyArName] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Address] [nchar](10) NULL,
	[Phone] [nvarchar](30) NULL,
	[Email] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[SLAId] [bigint] NULL,
	[createdBy] [bigint] NOT NULL,
	[modifiedBy] [bigint] NULL,
	[creationDate] [datetime] NOT NULL,
	[modificationDate] [datetime] NULL,
	[entityStatus_systemId] [int] NOT NULL,
	[version] [int] NOT NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Company_Announcement]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company_Announcement](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[Company_ID] [bigint] NOT NULL,
	[Announcement_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_Company_Announcement] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CurrentMeasurementLists]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrentMeasurementLists](
	[systemId] [bigint] NOT NULL,
	[SLAId] [bigint] NULL,
	[MeasurementId] [bigint] NULL,
	[List] [bigint] NULL,
	[TicletId] [bigint] NULL,
	[list_value] [float] NULL,
	[run_date] [datetime] NULL,
 CONSTRAINT [PK_CurrentMeasurementLists] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CurrentSlaResults]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CurrentSlaResults](
	[systemId] [bigint] NOT NULL,
	[MeasurementId] [bigint] NULL,
	[currenetResult] [float] NULL,
	[SLAGrade] [float] NULL,
	[internalGrade] [float] NULL,
	[runDate] [datetime] NULL,
	[finalResult] [bit] NULL,
 CONSTRAINT [PK_CurrentSlaResults] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Department]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](max) NOT NULL,
	[DepartmentArName] [nvarchar](max) NOT NULL,
	[Descrtiption] [nvarchar](max) NULL,
	[CompanyId] [bigint] NULL,
	[DepartmentHead] [bigint] NULL,
	[modifiedBy] [bigint] NULL,
	[deletedBy] [bigint] NULL,
	[creationDate] [datetime] NOT NULL,
	[modificationDate] [datetime] NULL,
	[deletedDate] [datetime] NULL,
	[entityStatus_systemId] [int] NOT NULL,
	[version] [int] NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DueDateRule]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DueDateRule](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[DueDateRuleName] [nvarchar](50) NOT NULL,
	[Category_ID] [bigint] NULL,
	[Urgancy_ID] [bigint] NULL,
	[Priority_ID] [bigint] NULL,
	[SLA_ID] [bigint] NULL,
	[DueDate] [int] NULL,
	[creationDate] [datetime] NOT NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NOT NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NULL,
	[entityStatus_systemId] [int] NULL,
	[SubCategory] [bigint] NULL,
 CONSTRAINT [PK_DueDateRule] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EntityStatus]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EntityStatus](
	[systemId] [int] IDENTITY(1,1) NOT NULL,
	[EntityStatusName] [nvarchar](50) NULL,
 CONSTRAINT [PK_EntityStatus] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EscalationRule]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EscalationRule](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[EscalationName] [nvarchar](32) NOT NULL,
	[EscalationArName] [nvarchar](32) NOT NULL,
	[Category_ID] [bigint] NULL,
	[Sub_Category_ID] [bigint] NULL,
	[SLA_ID] [bigint] NULL,
	[Status_ID] [bigint] NULL,
	[Priority_ID] [bigint] NULL,
	[ChangePriority] [bigint] NULL,
	[ChangeStatus] [bigint] NULL,
	[ReAssignUser] [bigint] NULL,
	[AssignedUser_ID] [bigint] NULL,
	[SubmitterUSerID] [bigint] NULL,
	[EscalationLevel] [int] NULL,
	[AssignedGroup] [bigint] NULL,
	[creationDate] [datetime] NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NULL,
	[entityStatus_systemId] [int] NULL,
 CONSTRAINT [PK_Escalation] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroupType]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupType](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupTypeName] [nvarchar](30) NULL,
 CONSTRAINT [PK_GroupType] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Impact]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Impact](
	[systemId] [bigint] NOT NULL,
	[ImpactName] [nvarchar](32) NOT NULL,
	[ImpactArName] [nvarchar](32) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[DescriptionAr] [nvarchar](max) NULL,
	[createdBy] [bigint] NOT NULL,
	[modifiedBy] [bigint] NULL,
	[creationDate] [datetime] NOT NULL,
	[modificationDate] [datetime] NULL,
	[entityStatus_systemId] [int] NOT NULL,
	[version] [int] NOT NULL,
	[isDefault] [bit] NULL,
 CONSTRAINT [PK_Impact] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KnowledgeBase]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KnowledgeBase](
	[systemId] [bigint] NOT NULL,
	[TopicName] [nvarchar](max) NOT NULL,
	[TopicArName] [nvarchar](max) NOT NULL,
	[Category_ID] [bigint] NULL,
	[Sub_Category_ID] [bigint] NULL,
	[User_ID] [bigint] NULL,
	[Quetion] [nvarchar](max) NULL,
	[Answer] [nvarchar](max) NULL,
	[Attachment] [image] NULL,
	[Publish] [bit] NULL,
	[createdBy] [bigint] NULL,
	[modifiedBy] [bigint] NULL,
	[creationDate] [datetime] NULL,
	[modificationDate] [datetime] NULL,
	[entityStatus_systemId] [int] NULL,
	[version] [int] NULL,
 CONSTRAINT [PK_Topic] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Measurment]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Measurment](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[MeasurmentName] [nvarchar](255) NOT NULL,
	[MeasurmentArName] [nvarchar](255) NOT NULL,
	[Unit] [nvarchar](64) NULL,
	[Formula] [nvarchar](32) NULL,
	[SLAId] [bigint] NULL,
	[MeasurmentList_ID] [bigint] NULL,
	[TestInterval] [int] NULL,
	[CurrentValue] [nvarchar](32) NULL,
	[creationDate] [datetime] NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NULL,
	[entityStatus_systemId] [int] NULL,
	[MeasurmentList_ID2] [bigint] NULL,
	[critical_grade] [int] NULL,
	[warning_grade] [int] NULL,
	[optimum_grade] [int] NULL,
	[goal_critical] [int] NULL,
	[goal_warning] [int] NULL,
	[goal_optimum] [int] NULL,
	[sla_critical] [int] NULL,
	[sla_warning] [int] NULL,
	[sla_optimum] [int] NULL,
	[calculated] [bit] NULL,
 CONSTRAINT [PK_Measurment] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MeasurmentList]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeasurmentList](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[MeasurmentListName] [nvarchar](32) NOT NULL,
	[MeasurmentListArName] [nvarchar](32) NOT NULL,
	[creationDate] [datetime] NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NULL,
	[Status] [int] NULL,
	[dateField] [nvarchar](50) NULL,
	[statusType] [bigint] NULL,
	[fieldName] [nvarchar](50) NULL,
	[entityStatus_systemId] [int] NULL,
 CONSTRAINT [PK_MeasurmentList] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Module]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Module](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](64) NULL,
	[Ename] [nvarchar](64) NULL,
	[parentId] [bigint] NULL,
	[creationDate] [datetime] NOT NULL,
	[modificationDate] [datetime] NULL,
	[deletedDate] [datetime] NULL,
	[status] [int] NOT NULL,
	[version] [int] NOT NULL,
	[entityStatus_systemId] [int] NULL,
	[createdBy] [bigint] NOT NULL,
	[modifiedBy] [bigint] NULL,
	[deletedBy] [bigint] NULL,
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OnlineUsers]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnlineUsers](
	[systemId] [bigint] NOT NULL,
	[userId] [bigint] NOT NULL,
	[sessionId] [bigint] NOT NULL,
	[ipAddress] [nchar](10) NOT NULL,
 CONSTRAINT [PK_OnlineUsers] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Priority]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Priority](
	[systemId] [bigint] NOT NULL,
	[PriorityName] [nvarchar](50) NOT NULL,
	[PriorityArName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[PriorityLevel] [int] NOT NULL,
	[entityStatus_systemId] [int] NOT NULL,
	[createdBy] [bigint] NULL,
	[modifiedBy] [bigint] NULL,
	[creationDate] [datetime] NULL,
	[modificationDate] [datetime] NULL,
	[version] [int] NULL,
	[isDefault] [bit] NULL,
 CONSTRAINT [PK_Priority] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PriorityMatrix]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriorityMatrix](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[Impact_ID] [bigint] NOT NULL,
	[Urgancy_ID] [bigint] NOT NULL,
	[Priority_ID] [bigint] NOT NULL,
	[creationDate] [datetime] NOT NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NOT NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NULL,
	[entityStatus_systemId] [int] NULL,
	[SLAId] [bigint] NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_PriorityMatrix] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PriorityRule]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriorityRule](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[PriorityRuleName] [nvarchar](32) NOT NULL,
	[Section_ID] [bigint] NULL,
	[UserGroup_ID] [bigint] NULL,
	[Priority_ID] [bigint] NULL,
	[SLA_ID] [bigint] NOT NULL,
	[UserId] [bigint] NULL,
	[creationDate] [datetime] NOT NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NOT NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NOT NULL,
	[entityStatus_systemId] [int] NULL,
	[UsingPriorityMatrix] [bit] NOT NULL,
	[PriorityMatrix] [bigint] NULL,
 CONSTRAINT [PK_PriorityRule] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reminder]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reminder](
	[systemId] [bigint] NULL,
	[ReminderName] [nvarchar](64) NULL,
	[Notification] [nvarchar](64) NULL,
	[alertBefore] [int] NULL,
	[alertTime] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](64) NULL,
	[RoleArName] [nvarchar](64) NOT NULL,
	[UserTypeId] [bigint] NOT NULL,
	[createdBy] [bigint] NOT NULL,
	[modifiedBy] [bigint] NULL,
	[deletedBy] [bigint] NULL,
	[creationDate] [datetime] NOT NULL,
	[modificationDate] [datetime] NULL,
	[deletedDate] [datetime] NULL,
	[entityStatus_systemId] [int] NULL,
	[version] [int] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleActions]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleActions](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleId] [bigint] NOT NULL,
	[actionId] [bigint] NOT NULL,
	[creationDate] [datetime] NOT NULL,
	[modificationDate] [datetime] NULL,
	[deletedDate] [datetime] NULL,
	[version] [int] NOT NULL,
	[entityStatus_systemId] [int] NULL,
	[createdBy] [bigint] NOT NULL,
	[modifiedBy] [bigint] NULL,
	[deletedBy] [bigint] NULL,
 CONSTRAINT [PK_Role_Permission] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoutingRule]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoutingRule](
	[systemId] [bigint] NOT NULL,
	[RoutingRuleName] [nvarchar](32) NOT NULL,
	[RoutingRuleArName] [nvarchar](32) NOT NULL,
	[SubmitterGroupID] [bigint] NULL,
	[AssignedGroupID] [bigint] NULL,
	[CategoryID] [bigint] NULL,
	[SLAID] [bigint] NULL,
	[creationDate] [datetime] NOT NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NOT NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NOT NULL,
	[entityStatus_systemId] [int] NULL,
	[SubCategory] [bigint] NULL,
	[AssignedUser] [bigint] NULL,
 CONSTRAINT [PK_RoutingRule] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Section]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Section](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[SectionName] [nvarchar](max) NOT NULL,
	[SectionArName] [nvarchar](max) NOT NULL,
	[DepartmentId] [bigint] NULL,
	[companyId] [bigint] NULL,
	[createdBy] [bigint] NOT NULL,
	[modifiedBy] [bigint] NULL,
	[deletedBy] [bigint] NULL,
	[creationDate] [datetime] NOT NULL,
	[modificationDate] [datetime] NULL,
	[deletedDate] [datetime] NULL,
	[entityStatus_systemId] [int] NOT NULL,
	[version] [int] NOT NULL,
 CONSTRAINT [PK_Secction] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShareTopic]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShareTopic](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[ShareTopicName] [nvarchar](32) NOT NULL,
	[ShareTopicArName] [nvarchar](32) NOT NULL,
	[Method] [nvarchar](32) NULL,
	[Topic_ID] [bigint] NULL,
	[UserGroup_ID] [bigint] NULL,
	[Subject] [nvarchar](32) NULL,
	[Message] [nvarchar](32) NULL,
	[Attachment] [image] NULL,
	[creationDate] [datetime] NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_ShareTopic] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SLA]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SLA](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[SLA_Name] [nvarchar](32) NOT NULL,
	[SLA_ArName] [nvarchar](32) NOT NULL,
	[isDefault] [bit] NOT NULL,
	[creationDate] [datetime] NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NULL,
	[entityStatus_systemId] [int] NULL,
 CONSTRAINT [PK_SLA] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Status]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](32) NOT NULL,
	[StatusArName] [nvarchar](32) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[StatusType_ID] [bigint] NOT NULL,
	[IsDefault] [bit] NULL,
	[Tickets] [bit] NULL,
	[Tasks] [bit] NULL,
	[createdBy] [bigint] NOT NULL,
	[modifiedBy] [bigint] NULL,
	[deletedBy] [bigint] NULL,
	[creationDate] [datetime] NOT NULL,
	[modificationDate] [datetime] NULL,
	[deletedDate] [datetime] NULL,
	[entityStatus_systemId] [int] NOT NULL,
	[version] [int] NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StatusTimer]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusTimer](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[Time] [datetime] NOT NULL,
	[Status_ID] [bigint] NOT NULL,
	[Timer_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_StatusTimer] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StatusType]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusType](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[StatusTypeName] [nvarchar](32) NOT NULL,
	[StatusTypeArName] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_StatusType] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Task]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[TaskName] [nvarchar](32) NOT NULL,
	[TaskArName] [nvarchar](32) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Ticket_ID] [bigint] NULL,
	[EstimatedStartDate] [datetime] NULL,
	[EstimatedEndDate] [datetime] NULL,
	[Total] [nvarchar](32) NULL,
	[AssignedUser] [bigint] NULL,
	[Status_ID] [bigint] NULL,
	[creationDate] [datetime] NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NULL,
	[entityStatus_systemId] [int] NULL,
	[Dependent] [bigint] NULL,
	[Note] [nvarchar](max) NULL,
	[Category] [bigint] NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TicketLog]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketLog](
	[systemId] [bigint] NOT NULL,
	[TicketId] [bigint] NOT NULL,
	[LogTime] [datetime] NOT NULL,
	[logType] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[UserId] [bigint] NOT NULL,
 CONSTRAINT [PK_TicketLog] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TicketMsg]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TicketMsg](
	[systemId] [bigint] NOT NULL,
	[msgTime] [datetime] NOT NULL,
	[fromUser] [nvarchar](64) NOT NULL,
	[toUser] [nvarchar](255) NOT NULL,
	[method] [nvarchar](64) NULL,
	[subject] [nvarchar](64) NULL,
	[Body] [nvarchar](max) NULL,
 CONSTRAINT [PK_TicketMsg] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[TicketTitle] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
	[SubmitterUser] [bigint] NULL,
	[CategoryID] [bigint] NULL,
	[SubCategoryID] [bigint] NULL,
	[StatusID] [bigint] NULL,
	[Impact_ID] [bigint] NULL,
	[Urgancy_ID] [bigint] NULL,
	[Priority_ID] [bigint] NULL,
	[AssignedUser] [bigint] NULL,
	[AssignedGroup] [bigint] NULL,
	[Attachment] [image] NULL,
	[Solution] [nvarchar](max) NULL,
	[Resolution] [nvarchar](max) NULL,
	[StratDate] [datetime] NULL,
	[CloseDate] [datetime] NULL,
	[version] [int] NULL,
	[KnowledgeBase] [bigint] NULL,
	[DueDate] [datetime] NULL,
	[Timer] [bigint] NULL,
	[SLAId] [bigint] NULL,
	[creationDate] [datetime] NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NULL,
	[modifiedBy] [bigint] NULL,
	[entityStatus_systemId] [int] NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Timer]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Timer](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[TimerName] [nvarchar](32) NOT NULL,
	[TimerArName] [nvarchar](32) NOT NULL,
	[creationDate] [datetime] NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NULL,
	[entityStatus_systemId] [int] NULL,
	[statusId] [bigint] NULL,
 CONSTRAINT [PK_Timer] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Type]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](32) NOT NULL,
	[TypeArName] [nvarchar](32) NOT NULL,
	[entityStatus_systemId] [int] NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Urgency]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Urgency](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[UrgancyName] [nvarchar](32) NOT NULL,
	[UrgancyArName] [nvarchar](32) NOT NULL,
	[entityStatus_systemId] [int] NULL,
	[creationDate] [datetime] NULL,
	[modificationDate] [datetime] NULL,
	[createdBy] [bigint] NULL,
	[modifiedBy] [bigint] NULL,
	[version] [int] NULL,
 CONSTRAINT [PK_Urgancy] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserGroup]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGroup](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserGroupName] [nvarchar](50) NOT NULL,
	[UserGroupArName] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[GroupTypeId] [bigint] NOT NULL,
	[createdBy] [bigint] NOT NULL,
	[modifiedBy] [bigint] NULL,
	[deletedBy] [bigint] NULL,
	[creationDate] [datetime] NOT NULL,
	[modificationDate] [datetime] NULL,
	[deletedDate] [datetime] NULL,
	[version] [int] NOT NULL,
	[entityStatus_systemId] [int] NOT NULL,
 CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[IDRole_Id] [bigint] NULL,
	[UserId] [bigint] NOT NULL,
	[RoleId] [bigint] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRouting]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRouting](
	[systemId] [bigint] NOT NULL,
	[userfrom] [bigint] NULL,
	[userTo] [bigint] NULL,
 CONSTRAINT [PK_UserRouting] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[LoginName] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[departmentId] [bigint] NULL,
	[companyId] [bigint] NULL,
	[SectionID] [bigint] NULL,
	[GroupID] [bigint] NULL,
	[RoleID] [bigint] NULL,
	[createdBy] [bigint] NOT NULL,
	[modifiedBy] [bigint] NULL,
	[deletedBy] [bigint] NULL,
	[creationDate] [datetime] NULL,
	[modificationDate] [datetime] NULL,
	[deletedDate] [datetime] NULL,
	[entityStatus_systemId] [int] NULL,
	[version] [int] NULL,
	[EmailConfirmed] [bit] NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NULL,
	[AccessFailedCount] [int] NULL,
	[LockoutEnabled] [bit] NULL,
	[status] [int] NOT NULL CONSTRAINT [DF_Users_status]  DEFAULT ((1)),
	[LockoutEndDateUtc] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserType]    Script Date: 11/12/2018 09:19:43 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[systemId] [bigint] IDENTITY(1,1) NOT NULL,
	[TypeName] [nvarchar](50) NOT NULL,
	[TypeArName] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Action] ON 

INSERT [dbo].[Action] ([systemId], [name], [Ename], [label], [Elabel], [pageType], [moduleId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [deletedDate], [entityStatus_systemId], [deletedBy]) VALUES (1, N'Index', NULL, NULL, NULL, N'NormalPage', 1, CAST(N'2018-12-08 21:42:14.250' AS DateTime), NULL, 10019, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Action] ([systemId], [name], [Ename], [label], [Elabel], [pageType], [moduleId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [deletedDate], [entityStatus_systemId], [deletedBy]) VALUES (3, N'Create', NULL, N'Create Role', NULL, N'FunctionPage', 1, CAST(N'2018-04-05 10:00:12.337' AS DateTime), NULL, NULL, 1, 0, NULL, 0, NULL)
INSERT [dbo].[Action] ([systemId], [name], [Ename], [label], [Elabel], [pageType], [moduleId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [deletedDate], [entityStatus_systemId], [deletedBy]) VALUES (4, N'Index', N'Roles', N'Roles', NULL, N'NormalPage', 9, CAST(N'2018-04-05 10:58:37.553' AS DateTime), NULL, NULL, 1, 0, NULL, 1, NULL)
INSERT [dbo].[Action] ([systemId], [name], [Ename], [label], [Elabel], [pageType], [moduleId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [deletedDate], [entityStatus_systemId], [deletedBy]) VALUES (5, N'AssignPages', N'AssignPages', N'AssignPages', NULL, N'FunctionPage', 9, CAST(N'2018-04-05 10:59:30.157' AS DateTime), NULL, NULL, 1, 0, NULL, 1, NULL)
INSERT [dbo].[Action] ([systemId], [name], [Ename], [label], [Elabel], [pageType], [moduleId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [deletedDate], [entityStatus_systemId], [deletedBy]) VALUES (6, N'Edit', N'Edit Role', N'Edit Role', NULL, N'FunctionPage', 9, CAST(N'2018-04-08 10:06:57.907' AS DateTime), NULL, NULL, 1, 0, NULL, 1, NULL)
INSERT [dbo].[Action] ([systemId], [name], [Ename], [label], [Elabel], [pageType], [moduleId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [deletedDate], [entityStatus_systemId], [deletedBy]) VALUES (7, N'Details', N'Role Details', N'Role Details', NULL, N'FunctionPage', 9, CAST(N'2018-04-08 10:08:25.017' AS DateTime), NULL, NULL, 1, 0, NULL, 1, NULL)
INSERT [dbo].[Action] ([systemId], [name], [Ename], [label], [Elabel], [pageType], [moduleId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [deletedDate], [entityStatus_systemId], [deletedBy]) VALUES (8, N'Delete', N'Delete Role', N'Delete Role', NULL, N'FunctionPage', 9, CAST(N'2018-04-08 10:15:57.423' AS DateTime), NULL, NULL, 1, 0, NULL, 1, NULL)
INSERT [dbo].[Action] ([systemId], [name], [Ename], [label], [Elabel], [pageType], [moduleId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [deletedDate], [entityStatus_systemId], [deletedBy]) VALUES (9, N'Create', NULL, N'Create User', NULL, N'FunctionPage', 7, CAST(N'2018-04-05 10:00:12.337' AS DateTime), NULL, NULL, 1, 0, NULL, 0, NULL)
INSERT [dbo].[Action] ([systemId], [name], [Ename], [label], [Elabel], [pageType], [moduleId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [deletedDate], [entityStatus_systemId], [deletedBy]) VALUES (10, N'Index', N'Users', N'User', NULL, N'NormalPage', 7, CAST(N'2018-04-05 10:58:37.553' AS DateTime), NULL, NULL, 1, 0, NULL, 1, NULL)
INSERT [dbo].[Action] ([systemId], [name], [Ename], [label], [Elabel], [pageType], [moduleId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [deletedDate], [entityStatus_systemId], [deletedBy]) VALUES (12, N'Edit', N'Edit User', N'Edit User', NULL, N'FunctionPage', 7, CAST(N'2018-04-08 10:06:57.907' AS DateTime), NULL, NULL, 1, 0, NULL, 1, NULL)
INSERT [dbo].[Action] ([systemId], [name], [Ename], [label], [Elabel], [pageType], [moduleId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [deletedDate], [entityStatus_systemId], [deletedBy]) VALUES (13, N'Details', N'User Details', N'User Details', NULL, N'FunctionPage', 7, CAST(N'2018-04-08 10:08:25.017' AS DateTime), NULL, NULL, 1, 0, NULL, 1, NULL)
INSERT [dbo].[Action] ([systemId], [name], [Ename], [label], [Elabel], [pageType], [moduleId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [deletedDate], [entityStatus_systemId], [deletedBy]) VALUES (14, N'Delete', N'Delete User', N'Delete User', NULL, N'FunctionPage', 7, CAST(N'2018-04-08 10:15:57.423' AS DateTime), NULL, NULL, 1, 0, NULL, 1, NULL)
INSERT [dbo].[Action] ([systemId], [name], [Ename], [label], [Elabel], [pageType], [moduleId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [deletedDate], [entityStatus_systemId], [deletedBy]) VALUES (15, N'adduser', NULL, NULL, NULL, N'NormalPage', 7, CAST(N'2018-12-09 12:52:36.177' AS DateTime), NULL, 10019, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Action] ([systemId], [name], [Ename], [label], [Elabel], [pageType], [moduleId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [deletedDate], [entityStatus_systemId], [deletedBy]) VALUES (10009, N'Index', N'UserGroups', N'User Groups', NULL, N'NormalPage', 10003, CAST(N'2018-12-09 14:19:16.347' AS DateTime), NULL, 10019, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Action] ([systemId], [name], [Ename], [label], [Elabel], [pageType], [moduleId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [deletedDate], [entityStatus_systemId], [deletedBy]) VALUES (10010, N'Create', NULL, NULL, NULL, N'NormalPage', 10003, CAST(N'2018-12-09 14:29:45.083' AS DateTime), NULL, 10019, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Action] ([systemId], [name], [Ename], [label], [Elabel], [pageType], [moduleId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [deletedDate], [entityStatus_systemId], [deletedBy]) VALUES (10011, N'Edit', NULL, NULL, NULL, N'NormalPage', 10003, CAST(N'2018-12-09 14:30:12.710' AS DateTime), NULL, 10019, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Action] OFF
SET IDENTITY_INSERT [dbo].[AuditingLog] ON 

INSERT [dbo].[AuditingLog] ([systemId], [userId], [actionId], [moduleId], [oldData], [dateCreated], [clientData], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [entityStatus_systemId]) VALUES (1, 10019, 4, 9, N'{"Type":"System.Web.HttpCompileException","message":"C:  Users  mac5  Documents  Visual Studio 2015  Projects  ATS  SmartHup  Views  Roles  Index.cshtml(25): error CS1061: ''Role'' does not contain a definition for ''Name'' and no extension method ''Name'' accepting a first argument of type ''Role'' could be found (are you missing a using directive or an assembly reference?)","InnerException":""}', CAST(N'2018-12-08 22:21:28.910' AS DateTime), N'{"host":"::1","ip":"::1","agent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:64.0) Gecko/20100101 Firefox/64.0","url":"/Roles"}', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditingLog] ([systemId], [userId], [actionId], [moduleId], [oldData], [dateCreated], [clientData], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [entityStatus_systemId]) VALUES (2, 10019, 5, 9, N'{"Type":"System.Data.Entity.Infrastructure.DbUpdateException","message":"An error occurred while updating the entries. See the inner exception for details.","InnerException":"System.Data.Entity.Core.UpdateException: An error occurred while updating the entries. See the inner exception for details. ---> System.Data.SqlClient.SqlException: Cannot insert explicit value for identity column in table ''RoleActions'' when IDENTITY_INSERT is set to OFF. r n   at System.Data.SqlClient.SqlConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction) r n   at System.Data.SqlClient.SqlInternalConnection.OnError(SqlException exception, Boolean breakConnection, Action`1 wrapCloseInAction) r n   at System.Data.SqlClient.TdsParser.ThrowExceptionAndWarning(TdsParserStateObject stateObj, Boolean callerHasConnectionLock, Boolean asyncClose) r n   at System.Data.SqlClient.TdsParser.TryRun(RunBehavior runBehavior, SqlCommand cmdHandler, SqlDataReader dataStream, BulkCopySimpleResultSet bulkCopyHandler, TdsParserStateObject stateObj, Boolean& dataReady) r n   at System.Data.SqlClient.SqlCommand.FinishExecuteReader(SqlDataReader ds, RunBehavior runBehavior, String resetOptionsString, Boolean isInternal, Boolean forDescribeParameterEncryption, Boolean shouldCacheForAlwaysEncrypted) r n   at System.Data.SqlClient.SqlCommand.RunExecuteReaderTds(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, Boolean async, Int32 timeout, Task& task, Boolean asyncWrite, Boolean inRetry, SqlDataReader ds, Boolean describeParameterEncryptionRequest) r n   at System.Data.SqlClient.SqlCommand.RunExecuteReader(CommandBehavior cmdBehavior, RunBehavior runBehavior, Boolean returnStream, String method, TaskCompletionSource`1 completion, Int32 timeout, Task& task, Boolean& usedCache, Boolean asyncWrite, Boolean inRetry) r n   at System.Data.SqlClient.SqlCommand.InternalExecuteNonQuery(TaskCompletionSource`1 completion, String methodName, Boolean sendToPipe, Int32 timeout, Boolean& usedCache, Boolean asyncWrite, Boolean inRetry) r n   at System.Data.SqlClient.SqlCommand.ExecuteNonQuery() r n   at System.Data.Entity.Infrastructure.Interception.DbCommandDispatcher.<NonQuery>b__0(DbCommand t, DbCommandInterceptionContext`1 c) r n   at System.Data.Entity.Infrastructure.Interception.InternalDispatcher`1.Dispatch[TTarget,TInterceptionContext,TResult](TTarget target, Func`3 operation, TInterceptionContext interceptionContext, Action`3 executing, Action`3 executed) r n   at System.Data.Entity.Infrastructure.Interception.DbCommandDispatcher.NonQuery(DbCommand command, DbCommandInterceptionContext interceptionContext) r n   at System.Data.Entity.Internal.InterceptableDbCommand.ExecuteNonQuery() r n   at System.Data.Entity.Core.Mapping.Update.Internal.DynamicUpdateCommand.Execute(Dictionary`2 identifierValues, List`1 generatedValues) r n   at System.Data.Entity.Core.Mapping.Update.Internal.UpdateTranslator.Update() r n   --- End of inner exception stack trace --- r n   at System.Data.Entity.Core.Mapping.Update.Internal.UpdateTranslator.Update() r n   at System.Data.Entity.Core.EntityClient.Internal.EntityAdapter.<Update>b__2(UpdateTranslator ut) r n   at System.Data.Entity.Core.EntityClient.Internal.EntityAdapter.Update[T](T noChangesResult, Func`2 updateFunction) r n   at System.Data.Entity.Core.EntityClient.Internal.EntityAdapter.Update() r n   at System.Data.Entity.Core.Objects.ObjectContext.<SaveChangesToStore>b__35() r n   at System.Data.Entity.Core.Objects.ObjectContext.ExecuteInTransaction[T](Func`1 func, IDbExecutionStrategy executionStrategy, Boolean startLocalTransaction, Boolean releaseConnectionOnSuccess) r n   at System.Data.Entity.Core.Objects.ObjectContext.SaveChangesToStore(SaveOptions options, IDbExecutionStrategy executionStrategy, Boolean startLocalTransaction) r n   at System.Data.Entity.Core.Objects.ObjectContext.<>c__DisplayClass2a.<SaveChangesInternal>b__27() r n   at System.Data.Entity.SqlServer.DefaultSqlExecutionStrategy.Execute[TResult](Func`1 operation) r n   at System.Data.Entity.Core.Objects.ObjectContext.SaveChangesInternal(SaveOptions options, Boolean executeInExistingTransaction) r n   at System.Data.Entity.Core.Objects.ObjectContext.SaveChanges(SaveOptions options) r n   at System.Data.Entity.Internal.InternalContext.SaveChanges()"}', CAST(N'2018-12-09 12:08:51.163' AS DateTime), N'{"host":"127.0.0.1","ip":"127.0.0.1","agent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:64.0) Gecko/20100101 Firefox/64.0","url":"/Roles/AssignPages/3"}', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditingLog] ([systemId], [userId], [actionId], [moduleId], [oldData], [dateCreated], [clientData], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [entityStatus_systemId]) VALUES (3, 10019, 10, 7, N'{"Type":"System.NullReferenceException","message":"Object reference not set to an instance of an object.","InnerException":""}', CAST(N'2018-12-09 12:39:30.273' AS DateTime), N'{"host":"127.0.0.1","ip":"127.0.0.1","agent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:64.0) Gecko/20100101 Firefox/64.0","url":"/Users"}', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditingLog] ([systemId], [userId], [actionId], [moduleId], [oldData], [dateCreated], [clientData], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [entityStatus_systemId]) VALUES (4, 10019, 10, 7, N'{"Type":"System.NullReferenceException","message":"Object reference not set to an instance of an object.","InnerException":""}', CAST(N'2018-12-09 12:39:36.743' AS DateTime), N'{"host":"127.0.0.1","ip":"127.0.0.1","agent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:64.0) Gecko/20100101 Firefox/64.0","url":"/Users"}', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditingLog] ([systemId], [userId], [actionId], [moduleId], [oldData], [dateCreated], [clientData], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [entityStatus_systemId]) VALUES (5, 10019, 10, 7, N'{"Type":"System.Web.HttpCompileException","message":"C:  Users  mac5  AppData  Local  Temp  Temporary ASP.NET Files  vs  26fafae7  1cbd6621  App_Web_index.cshtml.d629674c.f3s-0krh.0.cs(37): error CS0234: The type or namespace name ''User'' does not exist in the namespace ''SmartHup.Models'' (are you missing an assembly reference?)","InnerException":""}', CAST(N'2018-12-09 12:45:20.200' AS DateTime), N'{"host":"127.0.0.1","ip":"127.0.0.1","agent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:64.0) Gecko/20100101 Firefox/64.0","url":"/Users"}', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditingLog] ([systemId], [userId], [actionId], [moduleId], [oldData], [dateCreated], [clientData], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [entityStatus_systemId]) VALUES (6, 10019, 10, 7, N'{"Type":"System.Web.HttpCompileException","message":"C:  Users  mac5  AppData  Local  Temp  Temporary ASP.NET Files  vs  26fafae7  1cbd6621  App_Web_index.cshtml.d629674c.akr4ohya.0.cs(37): error CS0234: The type or namespace name ''User'' does not exist in the namespace ''SmartHup.Models'' (are you missing an assembly reference?)","InnerException":""}', CAST(N'2018-12-09 12:47:55.893' AS DateTime), N'{"host":"127.0.0.1","ip":"127.0.0.1","agent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:64.0) Gecko/20100101 Firefox/64.0","url":"/Users"}', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditingLog] ([systemId], [userId], [actionId], [moduleId], [oldData], [dateCreated], [clientData], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [entityStatus_systemId]) VALUES (7, 10019, 10, 7, N'{"Type":"System.Web.HttpCompileException","message":"C:  Users  mac5  Documents  Visual Studio 2015  Projects  ATS  SmartHup  Views  Users  Index.cshtml(44): error CS1061: ''Users'' does not contain a definition for ''serviceProviderId'' and no extension method ''serviceProviderId'' accepting a first argument of type ''Users'' could be found (are you missing a using directive or an assembly reference?)","InnerException":""}', CAST(N'2018-12-09 12:49:42.217' AS DateTime), N'{"host":"127.0.0.1","ip":"127.0.0.1","agent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:64.0) Gecko/20100101 Firefox/64.0","url":"/Users"}', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditingLog] ([systemId], [userId], [actionId], [moduleId], [oldData], [dateCreated], [clientData], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [entityStatus_systemId]) VALUES (8, 10019, 10, 7, N'{"Type":"System.Web.HttpCompileException","message":"C:  Users  mac5  Documents  Visual Studio 2015  Projects  ATS  SmartHup  Views  Users  Index.cshtml(75): error CS1061: ''Role'' does not contain a definition for ''Name'' and no extension method ''Name'' accepting a first argument of type ''Role'' could be found (are you missing a using directive or an assembly reference?)","InnerException":""}', CAST(N'2018-12-09 12:50:30.930' AS DateTime), N'{"host":"127.0.0.1","ip":"127.0.0.1","agent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:64.0) Gecko/20100101 Firefox/64.0","url":"/Users"}', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditingLog] ([systemId], [userId], [actionId], [moduleId], [oldData], [dateCreated], [clientData], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [entityStatus_systemId]) VALUES (9, 10019, 15, 7, N'{"Type":"System.InvalidOperationException","message":"There is no ViewData item of type ''IEnumerable<SelectListItem>'' that has the key ''serviceProviderId''.","InnerException":""}', CAST(N'2018-12-09 13:07:05.450' AS DateTime), N'{"host":"127.0.0.1","ip":"127.0.0.1","agent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:64.0) Gecko/20100101 Firefox/64.0","url":"/Users/AddUser"}', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditingLog] ([systemId], [userId], [actionId], [moduleId], [oldData], [dateCreated], [clientData], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [entityStatus_systemId]) VALUES (10, 10019, 15, 7, N'{"Type":"System.InvalidOperationException","message":"There is no ViewData item of type ''IEnumerable<SelectListItem>'' that has the key ''RoleId''.","InnerException":""}', CAST(N'2018-12-09 13:08:08.550' AS DateTime), N'{"host":"127.0.0.1","ip":"127.0.0.1","agent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:64.0) Gecko/20100101 Firefox/64.0","url":"/Users/AddUser"}', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[AuditingLog] ([systemId], [userId], [actionId], [moduleId], [oldData], [dateCreated], [clientData], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [entityStatus_systemId]) VALUES (11, 10019, 15, 7, N'{"Type":"System.InvalidOperationException","message":"There is no ViewData item of type ''IEnumerable<SelectListItem>'' that has the key ''RoleID''.","InnerException":""}', CAST(N'2018-12-09 13:09:11.293' AS DateTime), N'{"host":"127.0.0.1","ip":"127.0.0.1","agent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:64.0) Gecko/20100101 Firefox/64.0","url":"/Users/AddUser"}', NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[AuditingLog] OFF
INSERT [dbo].[Company] ([systemId], [CompanyName], [CompanyArName], [Description], [Address], [Phone], [Email], [Note], [SLAId], [createdBy], [modifiedBy], [creationDate], [modificationDate], [entityStatus_systemId], [version]) VALUES (1, N'Com1', N'Com', N'--', N'---       ', N'012346789', N'mo7ammed.ftta7@gmail.com', N'--', 2, 1, NULL, CAST(N'2018-01-01 00:00:00.000' AS DateTime), NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([systemId], [DepartmentName], [DepartmentArName], [Descrtiption], [CompanyId], [DepartmentHead], [modifiedBy], [deletedBy], [creationDate], [modificationDate], [deletedDate], [entityStatus_systemId], [version]) VALUES (5, N'1', N'1', N'1', 1, 1, 1, 2, CAST(N'2018-01-01 00:00:00.000' AS DateTime), NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[Department] OFF
SET IDENTITY_INSERT [dbo].[EntityStatus] ON 

INSERT [dbo].[EntityStatus] ([systemId], [EntityStatusName]) VALUES (1, N'active')
SET IDENTITY_INSERT [dbo].[EntityStatus] OFF
SET IDENTITY_INSERT [dbo].[GroupType] ON 

INSERT [dbo].[GroupType] ([systemId], [GroupTypeName]) VALUES (1, N'Group 1')
SET IDENTITY_INSERT [dbo].[GroupType] OFF
SET IDENTITY_INSERT [dbo].[Module] ON 

INSERT [dbo].[Module] ([systemId], [name], [Ename], [parentId], [creationDate], [modificationDate], [deletedDate], [status], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (1, N'Home', NULL, NULL, CAST(N'2018-12-08 21:42:14.077' AS DateTime), NULL, NULL, 1, 0, NULL, 10019, NULL, NULL)
INSERT [dbo].[Module] ([systemId], [name], [Ename], [parentId], [creationDate], [modificationDate], [deletedDate], [status], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (3, N'Users Management', N'Users Management', NULL, CAST(N'2018-04-05 00:00:00.000' AS DateTime), NULL, NULL, 1, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[Module] ([systemId], [name], [Ename], [parentId], [creationDate], [modificationDate], [deletedDate], [status], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (7, N'Users', N'', 3, CAST(N'2018-04-05 10:04:28.250' AS DateTime), NULL, NULL, 1, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[Module] ([systemId], [name], [Ename], [parentId], [creationDate], [modificationDate], [deletedDate], [status], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (9, N'Roles', N'', 3, CAST(N'2018-04-05 10:00:12.007' AS DateTime), NULL, NULL, 1, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[Module] ([systemId], [name], [Ename], [parentId], [creationDate], [modificationDate], [deletedDate], [status], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (10003, N'UserGroups', N'User Groups', 3, CAST(N'2018-04-05 10:00:12.007' AS DateTime), NULL, NULL, 1, 0, NULL, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Module] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([systemId], [RoleName], [RoleArName], [UserTypeId], [createdBy], [modifiedBy], [deletedBy], [creationDate], [modificationDate], [deletedDate], [entityStatus_systemId], [version]) VALUES (3, N'role 1', N'role 1', 1, 1, NULL, NULL, CAST(N'2018-01-01 00:00:00.000' AS DateTime), NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[RoleActions] ON 

INSERT [dbo].[RoleActions] ([systemId], [RoleId], [actionId], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (1, 3, 1, CAST(N'2018-01-01 00:00:00.000' AS DateTime), NULL, NULL, 0, 1, 1, NULL, NULL)
INSERT [dbo].[RoleActions] ([systemId], [RoleId], [actionId], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (2, 3, 3, CAST(N'2018-01-01 00:00:00.000' AS DateTime), NULL, NULL, 0, 1, 1, NULL, NULL)
INSERT [dbo].[RoleActions] ([systemId], [RoleId], [actionId], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (3, 3, 4, CAST(N'2018-01-01 00:00:00.000' AS DateTime), NULL, NULL, 0, 1, 1, NULL, NULL)
INSERT [dbo].[RoleActions] ([systemId], [RoleId], [actionId], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (4, 3, 5, CAST(N'2018-01-01 00:00:00.000' AS DateTime), NULL, NULL, 0, 1, 1, NULL, NULL)
INSERT [dbo].[RoleActions] ([systemId], [RoleId], [actionId], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (5, 3, 6, CAST(N'2018-01-01 00:00:00.000' AS DateTime), NULL, NULL, 0, 1, 1, NULL, NULL)
INSERT [dbo].[RoleActions] ([systemId], [RoleId], [actionId], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (6, 3, 7, CAST(N'2018-01-01 00:00:00.000' AS DateTime), NULL, NULL, 0, 1, 1, NULL, NULL)
INSERT [dbo].[RoleActions] ([systemId], [RoleId], [actionId], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (7, 3, 8, CAST(N'2018-01-01 00:00:00.000' AS DateTime), NULL, NULL, 0, 1, 1, NULL, NULL)
INSERT [dbo].[RoleActions] ([systemId], [RoleId], [actionId], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (8, 3, 9, CAST(N'2018-12-09 12:21:47.187' AS DateTime), NULL, NULL, 0, NULL, 10019, NULL, NULL)
INSERT [dbo].[RoleActions] ([systemId], [RoleId], [actionId], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (9, 3, 10, CAST(N'2018-12-09 12:21:48.097' AS DateTime), NULL, NULL, 0, NULL, 10019, NULL, NULL)
INSERT [dbo].[RoleActions] ([systemId], [RoleId], [actionId], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (10, 3, 12, CAST(N'2018-12-09 12:21:48.110' AS DateTime), NULL, NULL, 0, NULL, 10019, NULL, NULL)
INSERT [dbo].[RoleActions] ([systemId], [RoleId], [actionId], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (11, 3, 13, CAST(N'2018-12-09 12:21:48.117' AS DateTime), NULL, NULL, 0, NULL, 10019, NULL, NULL)
INSERT [dbo].[RoleActions] ([systemId], [RoleId], [actionId], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (12, 3, 14, CAST(N'2018-12-09 12:21:48.140' AS DateTime), NULL, NULL, 0, NULL, 10019, NULL, NULL)
INSERT [dbo].[RoleActions] ([systemId], [RoleId], [actionId], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (13, 3, 15, CAST(N'2018-12-09 12:53:56.843' AS DateTime), NULL, NULL, 0, NULL, 10019, NULL, NULL)
INSERT [dbo].[RoleActions] ([systemId], [RoleId], [actionId], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (10004, 3, 10009, CAST(N'2018-12-09 14:21:48.233' AS DateTime), NULL, NULL, 0, NULL, 10019, NULL, NULL)
INSERT [dbo].[RoleActions] ([systemId], [RoleId], [actionId], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (10005, 3, 10010, CAST(N'2018-12-09 14:30:24.633' AS DateTime), NULL, NULL, 0, NULL, 10019, NULL, NULL)
INSERT [dbo].[RoleActions] ([systemId], [RoleId], [actionId], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId], [createdBy], [modifiedBy], [deletedBy]) VALUES (10006, 3, 10011, CAST(N'2018-12-09 14:30:24.643' AS DateTime), NULL, NULL, 0, NULL, 10019, NULL, NULL)
SET IDENTITY_INSERT [dbo].[RoleActions] OFF
SET IDENTITY_INSERT [dbo].[Section] ON 

INSERT [dbo].[Section] ([systemId], [SectionName], [SectionArName], [DepartmentId], [companyId], [createdBy], [modifiedBy], [deletedBy], [creationDate], [modificationDate], [deletedDate], [entityStatus_systemId], [version]) VALUES (10007, N'1', N'1', 5, 1, 1, NULL, NULL, CAST(N'2018-01-01 00:00:00.000' AS DateTime), NULL, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[Section] OFF
SET IDENTITY_INSERT [dbo].[SLA] ON 

INSERT [dbo].[SLA] ([systemId], [SLA_Name], [SLA_ArName], [isDefault], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version], [entityStatus_systemId]) VALUES (2, N'--', N'---', 0, CAST(N'2018-01-01 00:00:00.000' AS DateTime), NULL, 1, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[SLA] OFF
SET IDENTITY_INSERT [dbo].[Urgency] ON 

INSERT [dbo].[Urgency] ([systemId], [UrgancyName], [UrgancyArName], [entityStatus_systemId], [creationDate], [modificationDate], [createdBy], [modifiedBy], [version]) VALUES (5, N'123', N'lamees', NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Urgency] OFF
SET IDENTITY_INSERT [dbo].[UserGroup] ON 

INSERT [dbo].[UserGroup] ([systemId], [UserGroupName], [UserGroupArName], [Description], [GroupTypeId], [createdBy], [modifiedBy], [deletedBy], [creationDate], [modificationDate], [deletedDate], [version], [entityStatus_systemId]) VALUES (2, N'Gorup name', N'U', N'--', 1, 1, NULL, NULL, CAST(N'2018-01-01 00:00:00.000' AS DateTime), NULL, NULL, 0, 1)
SET IDENTITY_INSERT [dbo].[UserGroup] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([systemId], [UserName], [LoginName], [PasswordHash], [Email], [PhoneNumber], [departmentId], [companyId], [SectionID], [GroupID], [RoleID], [createdBy], [modifiedBy], [deletedBy], [creationDate], [modificationDate], [deletedDate], [entityStatus_systemId], [version], [EmailConfirmed], [SecurityStamp], [PhoneNumberConfirmed], [AccessFailedCount], [LockoutEnabled], [status], [LockoutEndDateUtc]) VALUES (10019, N'admin', N'22', N'AJFe1oeWC/mnE4EDsgDYZR6Oqm0S1eBB/mBebpSvuNP8iC9AsdDth6WCsghacfNLmQ==', N'Admin@amarouse.com', N'0', 2, 1, 10007, 2, 3, 2, 2, 2, CAST(N'2018-01-01 00:00:00.000' AS DateTime), NULL, NULL, 1, 0, 1, N'5ed2ea05-51c3-4d1a-96b7-5c7c6d131608', 1, NULL, 0, 1, CAST(N'2018-06-25 14:00:13.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
SET IDENTITY_INSERT [dbo].[UserType] ON 

INSERT [dbo].[UserType] ([systemId], [TypeName], [TypeArName]) VALUES (1, N'type 1', N'type 1')
SET IDENTITY_INSERT [dbo].[UserType] OFF
/****** Object:  Index [IX_UserType]    Script Date: 11/12/2018 09:19:44 ص ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserType] ON [dbo].[UserType]
(
	[systemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Action]  WITH CHECK ADD  CONSTRAINT [FK_Action_Module] FOREIGN KEY([moduleId])
REFERENCES [dbo].[Module] ([systemId])
GO
ALTER TABLE [dbo].[Action] CHECK CONSTRAINT [FK_Action_Module]
GO
ALTER TABLE [dbo].[Announcement]  WITH CHECK ADD  CONSTRAINT [FK_Announcement_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[Announcement] CHECK CONSTRAINT [FK_Announcement_EntityStatus]
GO
ALTER TABLE [dbo].[Announcement]  WITH CHECK ADD  CONSTRAINT [FK_Announcement_UserGroup] FOREIGN KEY([UserGroup_ID])
REFERENCES [dbo].[UserGroup] ([systemId])
GO
ALTER TABLE [dbo].[Announcement] CHECK CONSTRAINT [FK_Announcement_UserGroup]
GO
ALTER TABLE [dbo].[AuditingLog]  WITH NOCHECK ADD  CONSTRAINT [FK_AuditingLog_Action1] FOREIGN KEY([actionId])
REFERENCES [dbo].[Action] ([systemId])
GO
ALTER TABLE [dbo].[AuditingLog] CHECK CONSTRAINT [FK_AuditingLog_Action1]
GO
ALTER TABLE [dbo].[AuditingLog]  WITH CHECK ADD  CONSTRAINT [FK_AuditingLog_Module] FOREIGN KEY([moduleId])
REFERENCES [dbo].[Module] ([systemId])
GO
ALTER TABLE [dbo].[AuditingLog] CHECK CONSTRAINT [FK_AuditingLog_Module]
GO
ALTER TABLE [dbo].[AuditingLog]  WITH CHECK ADD  CONSTRAINT [FK_AuditingLog_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([systemId])
GO
ALTER TABLE [dbo].[AuditingLog] CHECK CONSTRAINT [FK_AuditingLog_Users]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_Category] FOREIGN KEY([ParentCategory])
REFERENCES [dbo].[Category] ([systemId])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_Category]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_CategoryType] FOREIGN KEY([CategoryType])
REFERENCES [dbo].[CategoryType] ([systemId])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_CategoryType]
GO
ALTER TABLE [dbo].[ChatQueue]  WITH CHECK ADD  CONSTRAINT [FK_ChatQueue_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[ChatQueue] CHECK CONSTRAINT [FK_ChatQueue_EntityStatus]
GO
ALTER TABLE [dbo].[ChatQueue]  WITH CHECK ADD  CONSTRAINT [FK_ChatQueue_UserGroup] FOREIGN KEY([UserGroup_ID])
REFERENCES [dbo].[UserGroup] ([systemId])
GO
ALTER TABLE [dbo].[ChatQueue] CHECK CONSTRAINT [FK_ChatQueue_UserGroup]
GO
ALTER TABLE [dbo].[ChatQueueMessages]  WITH CHECK ADD  CONSTRAINT [FK_ChatQueueMessages_ChatQueue] FOREIGN KEY([ChatQueue])
REFERENCES [dbo].[ChatQueue] ([systemId])
GO
ALTER TABLE [dbo].[ChatQueueMessages] CHECK CONSTRAINT [FK_ChatQueueMessages_ChatQueue]
GO
ALTER TABLE [dbo].[ChatSessions]  WITH CHECK ADD  CONSTRAINT [FK_ChatSessions_ChatQueue] FOREIGN KEY([ChatQueue])
REFERENCES [dbo].[ChatQueue] ([systemId])
GO
ALTER TABLE [dbo].[ChatSessions] CHECK CONSTRAINT [FK_ChatSessions_ChatQueue]
GO
ALTER TABLE [dbo].[ChatSessions]  WITH CHECK ADD  CONSTRAINT [FK_ChatSessions_Ticket] FOREIGN KEY([TicketId])
REFERENCES [dbo].[Tickets] ([systemId])
GO
ALTER TABLE [dbo].[ChatSessions] CHECK CONSTRAINT [FK_ChatSessions_Ticket]
GO
ALTER TABLE [dbo].[ChatSessions]  WITH CHECK ADD  CONSTRAINT [FK_ChatSessions_Users] FOREIGN KEY([SubmitterUser])
REFERENCES [dbo].[Users] ([systemId])
GO
ALTER TABLE [dbo].[ChatSessions] CHECK CONSTRAINT [FK_ChatSessions_Users]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_EntityStatus]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_SLA] FOREIGN KEY([SLAId])
REFERENCES [dbo].[SLA] ([systemId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_SLA]
GO
ALTER TABLE [dbo].[Company_Announcement]  WITH CHECK ADD  CONSTRAINT [FK_Company_Announcement_Company] FOREIGN KEY([Company_ID])
REFERENCES [dbo].[Company] ([systemId])
GO
ALTER TABLE [dbo].[Company_Announcement] CHECK CONSTRAINT [FK_Company_Announcement_Company]
GO
ALTER TABLE [dbo].[CurrentMeasurementLists]  WITH CHECK ADD  CONSTRAINT [FK_CurrentMeasurementLists_Measurment] FOREIGN KEY([MeasurementId])
REFERENCES [dbo].[Measurment] ([systemId])
GO
ALTER TABLE [dbo].[CurrentMeasurementLists] CHECK CONSTRAINT [FK_CurrentMeasurementLists_Measurment]
GO
ALTER TABLE [dbo].[CurrentMeasurementLists]  WITH CHECK ADD  CONSTRAINT [FK_CurrentMeasurementLists_MeasurmentList] FOREIGN KEY([List])
REFERENCES [dbo].[MeasurmentList] ([systemId])
GO
ALTER TABLE [dbo].[CurrentMeasurementLists] CHECK CONSTRAINT [FK_CurrentMeasurementLists_MeasurmentList]
GO
ALTER TABLE [dbo].[CurrentMeasurementLists]  WITH CHECK ADD  CONSTRAINT [FK_CurrentMeasurementLists_SLA] FOREIGN KEY([SLAId])
REFERENCES [dbo].[SLA] ([systemId])
GO
ALTER TABLE [dbo].[CurrentMeasurementLists] CHECK CONSTRAINT [FK_CurrentMeasurementLists_SLA]
GO
ALTER TABLE [dbo].[CurrentMeasurementLists]  WITH CHECK ADD  CONSTRAINT [FK_CurrentMeasurementLists_Tickets] FOREIGN KEY([TicletId])
REFERENCES [dbo].[Tickets] ([systemId])
GO
ALTER TABLE [dbo].[CurrentMeasurementLists] CHECK CONSTRAINT [FK_CurrentMeasurementLists_Tickets]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([systemId])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Company]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_EntityStatus]
GO
ALTER TABLE [dbo].[DueDateRule]  WITH CHECK ADD  CONSTRAINT [FK_DueDateRule_Category] FOREIGN KEY([Category_ID])
REFERENCES [dbo].[Category] ([systemId])
GO
ALTER TABLE [dbo].[DueDateRule] CHECK CONSTRAINT [FK_DueDateRule_Category]
GO
ALTER TABLE [dbo].[DueDateRule]  WITH CHECK ADD  CONSTRAINT [FK_DueDateRule_Category1] FOREIGN KEY([SubCategory])
REFERENCES [dbo].[Category] ([systemId])
GO
ALTER TABLE [dbo].[DueDateRule] CHECK CONSTRAINT [FK_DueDateRule_Category1]
GO
ALTER TABLE [dbo].[DueDateRule]  WITH CHECK ADD  CONSTRAINT [FK_DueDateRule_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[DueDateRule] CHECK CONSTRAINT [FK_DueDateRule_EntityStatus]
GO
ALTER TABLE [dbo].[DueDateRule]  WITH CHECK ADD  CONSTRAINT [FK_DueDateRule_Priority] FOREIGN KEY([Priority_ID])
REFERENCES [dbo].[Priority] ([systemId])
GO
ALTER TABLE [dbo].[DueDateRule] CHECK CONSTRAINT [FK_DueDateRule_Priority]
GO
ALTER TABLE [dbo].[DueDateRule]  WITH CHECK ADD  CONSTRAINT [FK_DueDateRule_SLA] FOREIGN KEY([SLA_ID])
REFERENCES [dbo].[SLA] ([systemId])
GO
ALTER TABLE [dbo].[DueDateRule] CHECK CONSTRAINT [FK_DueDateRule_SLA]
GO
ALTER TABLE [dbo].[DueDateRule]  WITH CHECK ADD  CONSTRAINT [FK_DueDateRule_Urgancy] FOREIGN KEY([Urgancy_ID])
REFERENCES [dbo].[Urgency] ([systemId])
GO
ALTER TABLE [dbo].[DueDateRule] CHECK CONSTRAINT [FK_DueDateRule_Urgancy]
GO
ALTER TABLE [dbo].[EscalationRule]  WITH CHECK ADD  CONSTRAINT [FK_EscalationRule_Category] FOREIGN KEY([Category_ID])
REFERENCES [dbo].[Category] ([systemId])
GO
ALTER TABLE [dbo].[EscalationRule] CHECK CONSTRAINT [FK_EscalationRule_Category]
GO
ALTER TABLE [dbo].[EscalationRule]  WITH CHECK ADD  CONSTRAINT [FK_EscalationRule_Category1] FOREIGN KEY([Sub_Category_ID])
REFERENCES [dbo].[Category] ([systemId])
GO
ALTER TABLE [dbo].[EscalationRule] CHECK CONSTRAINT [FK_EscalationRule_Category1]
GO
ALTER TABLE [dbo].[EscalationRule]  WITH CHECK ADD  CONSTRAINT [FK_EscalationRule_Priority] FOREIGN KEY([Priority_ID])
REFERENCES [dbo].[Priority] ([systemId])
GO
ALTER TABLE [dbo].[EscalationRule] CHECK CONSTRAINT [FK_EscalationRule_Priority]
GO
ALTER TABLE [dbo].[EscalationRule]  WITH CHECK ADD  CONSTRAINT [FK_EscalationRule_Priority1] FOREIGN KEY([ChangePriority])
REFERENCES [dbo].[Priority] ([systemId])
GO
ALTER TABLE [dbo].[EscalationRule] CHECK CONSTRAINT [FK_EscalationRule_Priority1]
GO
ALTER TABLE [dbo].[EscalationRule]  WITH CHECK ADD  CONSTRAINT [FK_EscalationRule_Status] FOREIGN KEY([Status_ID])
REFERENCES [dbo].[Status] ([systemId])
GO
ALTER TABLE [dbo].[EscalationRule] CHECK CONSTRAINT [FK_EscalationRule_Status]
GO
ALTER TABLE [dbo].[EscalationRule]  WITH CHECK ADD  CONSTRAINT [FK_EscalationRule_Status1] FOREIGN KEY([ChangeStatus])
REFERENCES [dbo].[Status] ([systemId])
GO
ALTER TABLE [dbo].[EscalationRule] CHECK CONSTRAINT [FK_EscalationRule_Status1]
GO
ALTER TABLE [dbo].[EscalationRule]  WITH CHECK ADD  CONSTRAINT [FK_EscalationRule_UserGroup] FOREIGN KEY([AssignedGroup])
REFERENCES [dbo].[UserGroup] ([systemId])
GO
ALTER TABLE [dbo].[EscalationRule] CHECK CONSTRAINT [FK_EscalationRule_UserGroup]
GO
ALTER TABLE [dbo].[EscalationRule]  WITH CHECK ADD  CONSTRAINT [FK_EscalationRule_Users] FOREIGN KEY([AssignedUser_ID])
REFERENCES [dbo].[Users] ([systemId])
GO
ALTER TABLE [dbo].[EscalationRule] CHECK CONSTRAINT [FK_EscalationRule_Users]
GO
ALTER TABLE [dbo].[EscalationRule]  WITH CHECK ADD  CONSTRAINT [FK_EscalationRule_Users1] FOREIGN KEY([SubmitterUSerID])
REFERENCES [dbo].[Users] ([systemId])
GO
ALTER TABLE [dbo].[EscalationRule] CHECK CONSTRAINT [FK_EscalationRule_Users1]
GO
ALTER TABLE [dbo].[EscalationRule]  WITH CHECK ADD  CONSTRAINT [FK_EscalationRule_Users2] FOREIGN KEY([ReAssignUser])
REFERENCES [dbo].[Users] ([systemId])
GO
ALTER TABLE [dbo].[EscalationRule] CHECK CONSTRAINT [FK_EscalationRule_Users2]
GO
ALTER TABLE [dbo].[Impact]  WITH CHECK ADD  CONSTRAINT [FK_Impact_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[Impact] CHECK CONSTRAINT [FK_Impact_EntityStatus]
GO
ALTER TABLE [dbo].[KnowledgeBase]  WITH CHECK ADD  CONSTRAINT [FK_KnowledgeBase_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[KnowledgeBase] CHECK CONSTRAINT [FK_KnowledgeBase_EntityStatus]
GO
ALTER TABLE [dbo].[KnowledgeBase]  WITH CHECK ADD  CONSTRAINT [FK_Topic_Category] FOREIGN KEY([Category_ID])
REFERENCES [dbo].[Category] ([systemId])
GO
ALTER TABLE [dbo].[KnowledgeBase] CHECK CONSTRAINT [FK_Topic_Category]
GO
ALTER TABLE [dbo].[KnowledgeBase]  WITH CHECK ADD  CONSTRAINT [FK_Topic_Users] FOREIGN KEY([User_ID])
REFERENCES [dbo].[Users] ([systemId])
GO
ALTER TABLE [dbo].[KnowledgeBase] CHECK CONSTRAINT [FK_Topic_Users]
GO
ALTER TABLE [dbo].[Measurment]  WITH CHECK ADD  CONSTRAINT [FK_Measurment_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[Measurment] CHECK CONSTRAINT [FK_Measurment_EntityStatus]
GO
ALTER TABLE [dbo].[Measurment]  WITH CHECK ADD  CONSTRAINT [FK_Measurment_MeasurmentList] FOREIGN KEY([MeasurmentList_ID])
REFERENCES [dbo].[MeasurmentList] ([systemId])
GO
ALTER TABLE [dbo].[Measurment] CHECK CONSTRAINT [FK_Measurment_MeasurmentList]
GO
ALTER TABLE [dbo].[Measurment]  WITH CHECK ADD  CONSTRAINT [FK_Measurment_MeasurmentList1] FOREIGN KEY([MeasurmentList_ID2])
REFERENCES [dbo].[MeasurmentList] ([systemId])
GO
ALTER TABLE [dbo].[Measurment] CHECK CONSTRAINT [FK_Measurment_MeasurmentList1]
GO
ALTER TABLE [dbo].[Measurment]  WITH CHECK ADD  CONSTRAINT [FK_Measurment_SLA] FOREIGN KEY([SLAId])
REFERENCES [dbo].[SLA] ([systemId])
GO
ALTER TABLE [dbo].[Measurment] CHECK CONSTRAINT [FK_Measurment_SLA]
GO
ALTER TABLE [dbo].[MeasurmentList]  WITH CHECK ADD  CONSTRAINT [FK_MeasurmentList_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[MeasurmentList] CHECK CONSTRAINT [FK_MeasurmentList_EntityStatus]
GO
ALTER TABLE [dbo].[MeasurmentList]  WITH CHECK ADD  CONSTRAINT [FK_MeasurmentList_StatusType] FOREIGN KEY([statusType])
REFERENCES [dbo].[StatusType] ([systemId])
GO
ALTER TABLE [dbo].[MeasurmentList] CHECK CONSTRAINT [FK_MeasurmentList_StatusType]
GO
ALTER TABLE [dbo].[Module]  WITH CHECK ADD  CONSTRAINT [FK_Module_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[Module] CHECK CONSTRAINT [FK_Module_EntityStatus]
GO
ALTER TABLE [dbo].[Module]  WITH CHECK ADD  CONSTRAINT [FK_Module_Module] FOREIGN KEY([parentId])
REFERENCES [dbo].[Module] ([systemId])
GO
ALTER TABLE [dbo].[Module] CHECK CONSTRAINT [FK_Module_Module]
GO
ALTER TABLE [dbo].[OnlineUsers]  WITH CHECK ADD  CONSTRAINT [FK_OnlineUsers_ChatSessions] FOREIGN KEY([sessionId])
REFERENCES [dbo].[ChatSessions] ([systemId])
GO
ALTER TABLE [dbo].[OnlineUsers] CHECK CONSTRAINT [FK_OnlineUsers_ChatSessions]
GO
ALTER TABLE [dbo].[OnlineUsers]  WITH CHECK ADD  CONSTRAINT [FK_OnlineUsers_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([systemId])
GO
ALTER TABLE [dbo].[OnlineUsers] CHECK CONSTRAINT [FK_OnlineUsers_Users]
GO
ALTER TABLE [dbo].[Priority]  WITH CHECK ADD  CONSTRAINT [FK_Priority_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[Priority] CHECK CONSTRAINT [FK_Priority_EntityStatus]
GO
ALTER TABLE [dbo].[PriorityMatrix]  WITH CHECK ADD  CONSTRAINT [FK_PriorityMatrix_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[PriorityMatrix] CHECK CONSTRAINT [FK_PriorityMatrix_EntityStatus]
GO
ALTER TABLE [dbo].[PriorityMatrix]  WITH CHECK ADD  CONSTRAINT [FK_PriorityMatrix_Impact] FOREIGN KEY([Impact_ID])
REFERENCES [dbo].[Impact] ([systemId])
GO
ALTER TABLE [dbo].[PriorityMatrix] CHECK CONSTRAINT [FK_PriorityMatrix_Impact]
GO
ALTER TABLE [dbo].[PriorityMatrix]  WITH CHECK ADD  CONSTRAINT [FK_PriorityMatrix_Priority] FOREIGN KEY([Priority_ID])
REFERENCES [dbo].[Priority] ([systemId])
GO
ALTER TABLE [dbo].[PriorityMatrix] CHECK CONSTRAINT [FK_PriorityMatrix_Priority]
GO
ALTER TABLE [dbo].[PriorityMatrix]  WITH CHECK ADD  CONSTRAINT [FK_PriorityMatrix_SLA] FOREIGN KEY([SLAId])
REFERENCES [dbo].[SLA] ([systemId])
GO
ALTER TABLE [dbo].[PriorityMatrix] CHECK CONSTRAINT [FK_PriorityMatrix_SLA]
GO
ALTER TABLE [dbo].[PriorityMatrix]  WITH CHECK ADD  CONSTRAINT [FK_PriorityMatrix_Urgancy] FOREIGN KEY([Urgancy_ID])
REFERENCES [dbo].[Urgency] ([systemId])
GO
ALTER TABLE [dbo].[PriorityMatrix] CHECK CONSTRAINT [FK_PriorityMatrix_Urgancy]
GO
ALTER TABLE [dbo].[PriorityRule]  WITH CHECK ADD  CONSTRAINT [FK_PriorityRule_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[PriorityRule] CHECK CONSTRAINT [FK_PriorityRule_EntityStatus]
GO
ALTER TABLE [dbo].[PriorityRule]  WITH CHECK ADD  CONSTRAINT [FK_PriorityRule_Priority] FOREIGN KEY([Priority_ID])
REFERENCES [dbo].[Priority] ([systemId])
GO
ALTER TABLE [dbo].[PriorityRule] CHECK CONSTRAINT [FK_PriorityRule_Priority]
GO
ALTER TABLE [dbo].[PriorityRule]  WITH CHECK ADD  CONSTRAINT [FK_PriorityRule_PriorityMatrix] FOREIGN KEY([PriorityMatrix])
REFERENCES [dbo].[PriorityMatrix] ([systemId])
GO
ALTER TABLE [dbo].[PriorityRule] CHECK CONSTRAINT [FK_PriorityRule_PriorityMatrix]
GO
ALTER TABLE [dbo].[PriorityRule]  WITH CHECK ADD  CONSTRAINT [FK_PriorityRule_Section] FOREIGN KEY([Section_ID])
REFERENCES [dbo].[Section] ([systemId])
GO
ALTER TABLE [dbo].[PriorityRule] CHECK CONSTRAINT [FK_PriorityRule_Section]
GO
ALTER TABLE [dbo].[PriorityRule]  WITH CHECK ADD  CONSTRAINT [FK_PriorityRule_SLA] FOREIGN KEY([SLA_ID])
REFERENCES [dbo].[SLA] ([systemId])
GO
ALTER TABLE [dbo].[PriorityRule] CHECK CONSTRAINT [FK_PriorityRule_SLA]
GO
ALTER TABLE [dbo].[PriorityRule]  WITH CHECK ADD  CONSTRAINT [FK_PriorityRule_UserGroup] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([systemId])
GO
ALTER TABLE [dbo].[PriorityRule] CHECK CONSTRAINT [FK_PriorityRule_UserGroup]
GO
ALTER TABLE [dbo].[PriorityRule]  WITH CHECK ADD  CONSTRAINT [FK_PriorityRule_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([systemId])
GO
ALTER TABLE [dbo].[PriorityRule] CHECK CONSTRAINT [FK_PriorityRule_Users]
GO
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_EntityStatus]
GO
ALTER TABLE [dbo].[Role]  WITH CHECK ADD  CONSTRAINT [FK_Role_UserType] FOREIGN KEY([UserTypeId])
REFERENCES [dbo].[UserType] ([systemId])
GO
ALTER TABLE [dbo].[Role] CHECK CONSTRAINT [FK_Role_UserType]
GO
ALTER TABLE [dbo].[RoleActions]  WITH NOCHECK ADD  CONSTRAINT [FK_RoleActions_Action] FOREIGN KEY([actionId])
REFERENCES [dbo].[Action] ([systemId])
GO
ALTER TABLE [dbo].[RoleActions] CHECK CONSTRAINT [FK_RoleActions_Action]
GO
ALTER TABLE [dbo].[RoutingRule]  WITH CHECK ADD  CONSTRAINT [FK_RoutingRule_Category] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[RoutingRule] CHECK CONSTRAINT [FK_RoutingRule_Category]
GO
ALTER TABLE [dbo].[RoutingRule]  WITH CHECK ADD  CONSTRAINT [FK_RoutingRule_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[RoutingRule] CHECK CONSTRAINT [FK_RoutingRule_EntityStatus]
GO
ALTER TABLE [dbo].[RoutingRule]  WITH CHECK ADD  CONSTRAINT [FK_RoutingRule_SLA1] FOREIGN KEY([SLAID])
REFERENCES [dbo].[SLA] ([systemId])
GO
ALTER TABLE [dbo].[RoutingRule] CHECK CONSTRAINT [FK_RoutingRule_SLA1]
GO
ALTER TABLE [dbo].[RoutingRule]  WITH CHECK ADD  CONSTRAINT [FK_RoutingRule_UserGroup] FOREIGN KEY([AssignedGroupID])
REFERENCES [dbo].[UserGroup] ([systemId])
GO
ALTER TABLE [dbo].[RoutingRule] CHECK CONSTRAINT [FK_RoutingRule_UserGroup]
GO
ALTER TABLE [dbo].[RoutingRule]  WITH CHECK ADD  CONSTRAINT [FK_RoutingRule_UserGroup1] FOREIGN KEY([SubmitterGroupID])
REFERENCES [dbo].[UserGroup] ([systemId])
GO
ALTER TABLE [dbo].[RoutingRule] CHECK CONSTRAINT [FK_RoutingRule_UserGroup1]
GO
ALTER TABLE [dbo].[RoutingRule]  WITH NOCHECK ADD  CONSTRAINT [FK_RoutingRule_Users] FOREIGN KEY([AssignedUser])
REFERENCES [dbo].[Users] ([systemId])
GO
ALTER TABLE [dbo].[RoutingRule] CHECK CONSTRAINT [FK_RoutingRule_Users]
GO
ALTER TABLE [dbo].[Section]  WITH CHECK ADD  CONSTRAINT [FK_Secction_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([systemId])
GO
ALTER TABLE [dbo].[Section] CHECK CONSTRAINT [FK_Secction_Department]
GO
ALTER TABLE [dbo].[Section]  WITH CHECK ADD  CONSTRAINT [FK_Section_Company] FOREIGN KEY([companyId])
REFERENCES [dbo].[Company] ([systemId])
GO
ALTER TABLE [dbo].[Section] CHECK CONSTRAINT [FK_Section_Company]
GO
ALTER TABLE [dbo].[Section]  WITH CHECK ADD  CONSTRAINT [FK_Section_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[Section] CHECK CONSTRAINT [FK_Section_EntityStatus]
GO
ALTER TABLE [dbo].[ShareTopic]  WITH CHECK ADD  CONSTRAINT [FK_ShareTopic_ShareTopic] FOREIGN KEY([UserGroup_ID])
REFERENCES [dbo].[UserGroup] ([systemId])
GO
ALTER TABLE [dbo].[ShareTopic] CHECK CONSTRAINT [FK_ShareTopic_ShareTopic]
GO
ALTER TABLE [dbo].[SLA]  WITH CHECK ADD  CONSTRAINT [FK_SLA_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[SLA] CHECK CONSTRAINT [FK_SLA_EntityStatus]
GO
ALTER TABLE [dbo].[Status]  WITH CHECK ADD  CONSTRAINT [FK_Status_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[Status] CHECK CONSTRAINT [FK_Status_EntityStatus]
GO
ALTER TABLE [dbo].[Status]  WITH CHECK ADD  CONSTRAINT [FK_Status_StatusType] FOREIGN KEY([StatusType_ID])
REFERENCES [dbo].[StatusType] ([systemId])
GO
ALTER TABLE [dbo].[Status] CHECK CONSTRAINT [FK_Status_StatusType]
GO
ALTER TABLE [dbo].[StatusTimer]  WITH CHECK ADD  CONSTRAINT [FK_StatusTimer_Status] FOREIGN KEY([Status_ID])
REFERENCES [dbo].[Status] ([systemId])
GO
ALTER TABLE [dbo].[StatusTimer] CHECK CONSTRAINT [FK_StatusTimer_Status]
GO
ALTER TABLE [dbo].[StatusTimer]  WITH CHECK ADD  CONSTRAINT [FK_StatusTimer_StatusTimer] FOREIGN KEY([Timer_ID])
REFERENCES [dbo].[Timer] ([systemId])
GO
ALTER TABLE [dbo].[StatusTimer] CHECK CONSTRAINT [FK_StatusTimer_StatusTimer]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Category] FOREIGN KEY([Category])
REFERENCES [dbo].[Category] ([systemId])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Category]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_EntityStatus]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Status] FOREIGN KEY([Status_ID])
REFERENCES [dbo].[Status] ([systemId])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Status]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Task] FOREIGN KEY([Dependent])
REFERENCES [dbo].[Task] ([systemId])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Task]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Ticket] FOREIGN KEY([Ticket_ID])
REFERENCES [dbo].[Tickets] ([systemId])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Ticket]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_UserGroup] FOREIGN KEY([AssignedUser])
REFERENCES [dbo].[Users] ([systemId])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_UserGroup]
GO
ALTER TABLE [dbo].[TicketLog]  WITH CHECK ADD  CONSTRAINT [FK_TicketLog_Tickets] FOREIGN KEY([TicketId])
REFERENCES [dbo].[Tickets] ([systemId])
GO
ALTER TABLE [dbo].[TicketLog] CHECK CONSTRAINT [FK_TicketLog_Tickets]
GO
ALTER TABLE [dbo].[TicketLog]  WITH CHECK ADD  CONSTRAINT [FK_TicketLog_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([systemId])
GO
ALTER TABLE [dbo].[TicketLog] CHECK CONSTRAINT [FK_TicketLog_Users]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([systemId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Ticket_Category]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Category1] FOREIGN KEY([SubCategoryID])
REFERENCES [dbo].[Category] ([systemId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Ticket_Category1]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Impact] FOREIGN KEY([Impact_ID])
REFERENCES [dbo].[Impact] ([systemId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Ticket_Impact]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Priority] FOREIGN KEY([Priority_ID])
REFERENCES [dbo].[Priority] ([systemId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Ticket_Priority]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Status] FOREIGN KEY([StatusID])
REFERENCES [dbo].[Status] ([systemId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Ticket_Status]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Ticket] FOREIGN KEY([systemId])
REFERENCES [dbo].[Tickets] ([systemId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Ticket_Ticket]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Urgancy] FOREIGN KEY([Urgancy_ID])
REFERENCES [dbo].[Urgency] ([systemId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Ticket_Urgancy]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_User] FOREIGN KEY([AssignedUser])
REFERENCES [dbo].[Users] ([systemId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Ticket_User]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Users] FOREIGN KEY([SubmitterUser])
REFERENCES [dbo].[Users] ([systemId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Ticket_Users]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_EntityStatus]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_KnowledgeBase] FOREIGN KEY([KnowledgeBase])
REFERENCES [dbo].[KnowledgeBase] ([systemId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_KnowledgeBase]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_SLA] FOREIGN KEY([SLAId])
REFERENCES [dbo].[SLA] ([systemId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_SLA]
GO
ALTER TABLE [dbo].[Timer]  WITH CHECK ADD  CONSTRAINT [FK_Timer_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[Timer] CHECK CONSTRAINT [FK_Timer_EntityStatus]
GO
ALTER TABLE [dbo].[Timer]  WITH CHECK ADD  CONSTRAINT [FK_Timer_Timer] FOREIGN KEY([statusId])
REFERENCES [dbo].[Status] ([systemId])
GO
ALTER TABLE [dbo].[Timer] CHECK CONSTRAINT [FK_Timer_Timer]
GO
ALTER TABLE [dbo].[Urgency]  WITH CHECK ADD  CONSTRAINT [FK_Urgency_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[Urgency] CHECK CONSTRAINT [FK_Urgency_EntityStatus]
GO
ALTER TABLE [dbo].[UserGroup]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[UserGroup] CHECK CONSTRAINT [FK_UserGroup_EntityStatus]
GO
ALTER TABLE [dbo].[UserGroup]  WITH CHECK ADD  CONSTRAINT [FK_UserGroup_GroupType] FOREIGN KEY([GroupTypeId])
REFERENCES [dbo].[GroupType] ([systemId])
GO
ALTER TABLE [dbo].[UserGroup] CHECK CONSTRAINT [FK_UserGroup_GroupType]
GO
ALTER TABLE [dbo].[UserRoles]  WITH NOCHECK ADD  CONSTRAINT [FK_UserRoles_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([systemId])
GO
ALTER TABLE [dbo].[UserRoles] NOCHECK CONSTRAINT [FK_UserRoles_Role]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([systemId])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Company] FOREIGN KEY([companyId])
REFERENCES [dbo].[Company] ([systemId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Company]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_EntityStatus] FOREIGN KEY([entityStatus_systemId])
REFERENCES [dbo].[EntityStatus] ([systemId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_EntityStatus]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([systemId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Role]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Section] FOREIGN KEY([SectionID])
REFERENCES [dbo].[Section] ([systemId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Section]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserGroup] FOREIGN KEY([GroupID])
REFERENCES [dbo].[UserGroup] ([systemId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserGroup]
GO
USE [master]
GO
ALTER DATABASE [Tickets] SET  READ_WRITE 
GO
