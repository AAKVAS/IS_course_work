USE [master]
GO
/****** Object:  Database [ISWildberries]    Script Date: 18.01.2023 9:36:07 ******/
CREATE DATABASE [ISWildberries]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'cours_work', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\is_wildberries.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'cours_work_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\is_wildberries_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ISWildberries] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ISWildberries].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ISWildberries] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ISWildberries] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ISWildberries] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ISWildberries] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ISWildberries] SET ARITHABORT OFF 
GO
ALTER DATABASE [ISWildberries] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ISWildberries] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ISWildberries] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ISWildberries] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ISWildberries] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ISWildberries] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ISWildberries] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ISWildberries] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ISWildberries] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ISWildberries] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ISWildberries] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ISWildberries] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ISWildberries] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ISWildberries] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ISWildberries] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ISWildberries] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ISWildberries] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ISWildberries] SET RECOVERY FULL 
GO
ALTER DATABASE [ISWildberries] SET  MULTI_USER 
GO
ALTER DATABASE [ISWildberries] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ISWildberries] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ISWildberries] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ISWildberries] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ISWildberries] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ISWildberries] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ISWildberries', N'ON'
GO
ALTER DATABASE [ISWildberries] SET QUERY_STORE = OFF
GO
USE [ISWildberries]
GO
/****** Object:  Table [dbo].[products]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[price] [int] NOT NULL,
	[supplier_id] [int] NOT NULL,
	[category_id] [int] NOT NULL,
	[description] [nvarchar](255) NOT NULL,
	[supplier_percent] [float] NULL,
 CONSTRAINT [PK_products_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products_parameters]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products_parameters](
	[product_id] [int] NOT NULL,
	[parameter_title] [nvarchar](255) NOT NULL,
	[parameter_value] [nvarchar](255) NULL,
 CONSTRAINT [PK_pr_par_id] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[parameter_title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[view_get_product_parametres]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_get_product_parametres] 
AS
SELECT products.title, parameter_title, parameter_value
FROM products
LEFT JOIN products_parameters
ON products_parameters.product_id = products.id;
GO
/****** Object:  Table [dbo].[orders]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[product_count] [int] NULL,
	[pick_up_point_id] [int] NOT NULL,
	[price] [int] NULL,
	[created_at] [datetime] NOT NULL,
	[estimated_delivery_time] [nvarchar](255) NULL,
 CONSTRAINT [PK_orders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[storages]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[storages](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[country] [nvarchar](50) NULL,
	[federal_subject] [nvarchar](255) NULL,
	[locality] [nvarchar](255) NULL,
	[street] [nvarchar](255) NULL,
	[house_number] [nvarchar](255) NULL,
	[storage_type] [int] NULL,
 CONSTRAINT [PK_storages_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[posts]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[posts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NULL,
 CONSTRAINT [PK_posts_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workers]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstname] [nvarchar](255) NULL,
	[lastname] [nvarchar](255) NULL,
	[patronymic] [nvarchar](255) NULL,
	[phone_number] [nvarchar](255) NULL,
	[date_of_birthday] [datetime] NULL,
	[sex] [nvarchar](255) NULL,
	[post] [int] NULL,
	[password] [varchar](255) NULL,
 CONSTRAINT [PK_workers_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_statuses]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_statuses](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workers_in_orders]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workers_in_orders](
	[order_id] [int] NOT NULL,
	[status_changed_at] [datetime] NOT NULL,
	[worker_id] [int] NOT NULL,
 CONSTRAINT [PK_workers_in_orders] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC,
	[status_changed_at] ASC,
	[worker_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_history]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_history](
	[order_id] [int] NOT NULL,
	[status_changed_at] [datetime] NOT NULL,
	[status_id] [int] NULL,
	[current_storage_id] [int] NULL,
	[is_last_status] [bit] NULL,
 CONSTRAINT [PK_order_history] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC,
	[status_changed_at] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[view_get_workers_in_orders]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[view_get_workers_in_orders]
AS
SELECT workers.firstname, workers.lastname, workers.patronymic, workers.sex, posts.title as post,
products.title as product_name, order_statuses.description as 'status', storages.locality,
storages.street, storages.house_number, order_history.status_changed_at 
	FROM workers
		JOIN posts
		ON posts.id = workers.post
		JOIN workers_in_orders
		ON workers.id = workers_in_orders.worker_id
		JOIN order_history
		ON order_history.order_id = workers_in_orders.order_id
			AND workers_in_orders.status_changed_at = order_history.status_changed_at
		JOIN orders
		ON order_history.order_id = orders.id
		JOIN products
		ON products.id = orders.product_id
		JOIN order_statuses
		ON order_statuses.id = order_history.status_id
		JOIN storages
		ON
		storages.id = order_history.current_storage_id;
GO
/****** Object:  Table [dbo].[cards]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cards](
	[user_id] [int] NOT NULL,
	[card_number] [nvarchar](255) NOT NULL,
	[validity] [nvarchar](255) NULL,
	[card_owner] [nvarchar](255) NULL,
	[cvc] [int] NULL,
 CONSTRAINT [PK_cards] PRIMARY KEY CLUSTERED 
(
	[card_number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[categories]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NULL,
	[parent_category_id] [int] NULL,
 CONSTRAINT [PK_categories_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deferred_products]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deferred_products](
	[user_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
 CONSTRAINT [PK_def_p_id] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC,
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[files]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[files](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[file_title] [varchar](255) NULL,
	[file_path] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[price_history]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[price_history](
	[product_id] [int] NOT NULL,
	[price] [int] NOT NULL,
	[price_date] [datetime] NOT NULL,
 CONSTRAINT [PK_price_history] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[price_date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products_on_storages]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products_on_storages](
	[storage_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[product_amount] [int] NULL,
 CONSTRAINT [PK_pos_id] PRIMARY KEY CLUSTERED 
(
	[storage_id] ASC,
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[receipt_of_products_to_storages]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[receipt_of_products_to_storages](
	[product_id] [int] NOT NULL,
	[storage_id] [int] NOT NULL,
	[received_at] [datetime] NOT NULL,
	[amount] [int] NOT NULL,
 CONSTRAINT [PK_receipt_of_products_to_storages] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[storage_id] ASC,
	[received_at] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[reviews]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reviews](
	[order_id] [int] NOT NULL,
	[review_text] [nvarchar](255) NOT NULL,
	[stars] [int] NULL,
	[created_at] [datetime] NOT NULL,
 CONSTRAINT [PK_reviews] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[rights]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[rights](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](60) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[section]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[section](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](255) NULL,
	[parent_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[section_rights]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[section_rights](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[section_id] [int] NULL,
	[right_id] [int] NULL,
	[post_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[storage_types]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[storage_types](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[storage_worker_shifts]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[storage_worker_shifts](
	[worker_id] [int] NOT NULL,
	[storage_id] [int] NOT NULL,
	[started_shift_at] [datetime] NOT NULL,
	[finished_shift_at] [datetime] NULL,
 CONSTRAINT [PK_w_a_s_id] PRIMARY KEY CLUSTERED 
(
	[worker_id] ASC,
	[storage_id] ASC,
	[started_shift_at] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[suppliers]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[suppliers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NULL,
 CONSTRAINT [PK_supliers_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[table_files]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[table_files](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[table_name] [varchar](255) NULL,
	[record_id] [int] NULL,
	[f_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 18.01.2023 9:36:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstname] [nvarchar](255) NOT NULL,
	[lastname] [nvarchar](255) NOT NULL,
	[patronymic] [nvarchar](255) NOT NULL,
	[phone_number] [nvarchar](255) NOT NULL,
	[birthday] [datetime] NOT NULL,
	[email] [nvarchar](255) NULL,
	[sex] [nvarchar](255) NULL,
	[order_code] [int] NULL,
	[country] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[categories] ON 

INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (1, N'Канцтовары', NULL)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (2, N'Письменные принадлежности', 1)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (3, N'Бумажная продукция', 1)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (4, N'Торговые принадлежности', 1)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (5, N'Тетради', 3)
SET IDENTITY_INSERT [dbo].[categories] OFF
GO
INSERT [dbo].[deferred_products] ([user_id], [product_id]) VALUES (1, 2)
INSERT [dbo].[deferred_products] ([user_id], [product_id]) VALUES (1, 3)
INSERT [dbo].[deferred_products] ([user_id], [product_id]) VALUES (3, 3)
INSERT [dbo].[deferred_products] ([user_id], [product_id]) VALUES (4, 3)
INSERT [dbo].[deferred_products] ([user_id], [product_id]) VALUES (5, 1)
GO
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (1, CAST(N'2022-07-03T08:48:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (1, CAST(N'2022-08-08T10:18:15.000' AS DateTime), 4, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (1, CAST(N'2022-08-08T10:19:14.000' AS DateTime), 5, 2, 1)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (1, CAST(N'2022-09-07T10:13:14.000' AS DateTime), 3, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (2, CAST(N'2022-03-07T00:00:00.000' AS DateTime), 2, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (2, CAST(N'2022-04-07T00:00:00.000' AS DateTime), 3, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (2, CAST(N'2022-04-08T00:12:00.000' AS DateTime), 6, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (2, CAST(N'2022-07-03T08:48:00.000' AS DateTime), 4, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (2, CAST(N'2022-07-04T00:00:00.000' AS DateTime), 5, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (2, CAST(N'2022-08-04T00:12:00.000' AS DateTime), 7, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[order_statuses] ON 

INSERT [dbo].[order_statuses] ([id], [description]) VALUES (1, N'Оформлен')
INSERT [dbo].[order_statuses] ([id], [description]) VALUES (2, N'Отправлен на сборку')
INSERT [dbo].[order_statuses] ([id], [description]) VALUES (3, N'Собран на складе')
INSERT [dbo].[order_statuses] ([id], [description]) VALUES (4, N'Отсортирован в сортировачном центре')
INSERT [dbo].[order_statuses] ([id], [description]) VALUES (5, N'Доставлен СЦ/РЦ')
INSERT [dbo].[order_statuses] ([id], [description]) VALUES (6, N'Принят в сортировачном центре')
INSERT [dbo].[order_statuses] ([id], [description]) VALUES (7, N'Поступил в распределительный центр')
INSERT [dbo].[order_statuses] ([id], [description]) VALUES (8, N'Подготовлен к отгрузке в распределительном центре')
INSERT [dbo].[order_statuses] ([id], [description]) VALUES (9, N'В пути на пункт выдачи')
INSERT [dbo].[order_statuses] ([id], [description]) VALUES (10, N'Прибыл на пункт выдачи')
INSERT [dbo].[order_statuses] ([id], [description]) VALUES (11, N'Готов к полунию')
INSERT [dbo].[order_statuses] ([id], [description]) VALUES (12, N'Получен')
INSERT [dbo].[order_statuses] ([id], [description]) VALUES (13, N'Оформлен возрат')
INSERT [dbo].[order_statuses] ([id], [description]) VALUES (14, N'Возвращён')
SET IDENTITY_INSERT [dbo].[order_statuses] OFF
GO
SET IDENTITY_INSERT [dbo].[orders] ON 

INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_time]) VALUES (1, 1, 1, 1, 5, 170, CAST(N'2022-07-03T08:48:00.000' AS DateTime), N' 9-12 марта')
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_time]) VALUES (2, 3, 2, 1, 7, 319, CAST(N'2022-07-03T10:24:00.000' AS DateTime), N' 9-12 марта')
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_time]) VALUES (3, 4, 2, 1, 8, 319, CAST(N'2022-07-03T10:25:00.000' AS DateTime), N' 9-12 марта')
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_time]) VALUES (4, 7, 2, 1, 10, 319, CAST(N'2022-07-03T10:48:00.000' AS DateTime), N' 9-12 марта')
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_time]) VALUES (5, 3, 3, 1, 6, 2156, CAST(N'2022-07-03T08:49:00.000' AS DateTime), N' 9-11 марта')
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_time]) VALUES (6, 5, 3, 1, 9, 2156, CAST(N'2022-07-03T10:47:00.000' AS DateTime), N' 9-12 марта')
SET IDENTITY_INSERT [dbo].[orders] OFF
GO
SET IDENTITY_INSERT [dbo].[posts] ON 

INSERT [dbo].[posts] ([id], [title]) VALUES (1, N'сортировщик')
INSERT [dbo].[posts] ([id], [title]) VALUES (2, N'сотрудник пункта выдачи заказов')
INSERT [dbo].[posts] ([id], [title]) VALUES (3, N'уборщик')
INSERT [dbo].[posts] ([id], [title]) VALUES (4, N'администратор')
INSERT [dbo].[posts] ([id], [title]) VALUES (5, N'аналитик')
SET IDENTITY_INSERT [dbo].[posts] OFF
GO
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1, 167, CAST(N'2022-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1, 169, CAST(N'2022-01-03T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1, 180, CAST(N'2022-03-14T18:08:20.277' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1, 180, CAST(N'2022-03-14T18:20:54.933' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1, 190, CAST(N'2022-03-19T15:57:54.863' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1, 190, CAST(N'2022-03-30T20:12:15.810' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1, 170, CAST(N'2022-06-06T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (2, 340, CAST(N'2022-03-26T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (2, 319, CAST(N'2022-03-30T20:12:22.870' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (2, 335, CAST(N'2022-04-12T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (2, 324, CAST(N'2022-05-13T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (2, 319, CAST(N'2022-06-14T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (3, 2156, CAST(N'2022-03-14T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (3, 2156, CAST(N'2022-03-30T20:12:32.403' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (3, 2199, CAST(N'2022-04-01T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (3, 2156, CAST(N'2022-06-30T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[products] ON 

INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (1, N'Ручка линер', 190, 1, 2, N'Капилярная ручка с толщиной линии 0,3 мм', 95)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (2, N'Скетчбук', 319, 2, 3, N'Скетчбук для творчества формата А6', 87)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (3, N'Ценник', 2156, 3, 4, N'Ценник', 92)
SET IDENTITY_INSERT [dbo].[products] OFF
GO
INSERT [dbo].[products_on_storages] ([storage_id], [product_id], [product_amount]) VALUES (1, 1, 34)
INSERT [dbo].[products_on_storages] ([storage_id], [product_id], [product_amount]) VALUES (1, 2, 256)
INSERT [dbo].[products_on_storages] ([storage_id], [product_id], [product_amount]) VALUES (1, 3, 432)
INSERT [dbo].[products_on_storages] ([storage_id], [product_id], [product_amount]) VALUES (2, 1, 234)
INSERT [dbo].[products_on_storages] ([storage_id], [product_id], [product_amount]) VALUES (2, 2, 581)
INSERT [dbo].[products_on_storages] ([storage_id], [product_id], [product_amount]) VALUES (2, 3, 352)
GO
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1, N'Вес', N'20 г')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1, N'Длина', N'16,2 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1, N'Материал', N'пластик')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1, N'Толщина линии', N'0,3 мм')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1, N'Цвет', N'черный')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (2, N'Вес', N'420 г')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (2, N'Высота', N'1,4 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (2, N'Формат', N'А5')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (2, N'Цвет', N'синий')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (3, N'Количество', N'1000 шт')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (3, N'Цвет', N'оранжевый')
GO
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (1, 1, CAST(N'2022-01-01T00:00:00.000' AS DateTime), 5)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (1, 1, CAST(N'2022-02-01T00:00:00.000' AS DateTime), 30)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (1, 2, CAST(N'2022-01-03T00:00:00.000' AS DateTime), 234)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (2, 1, CAST(N'2022-01-01T00:00:00.000' AS DateTime), 210)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (2, 1, CAST(N'2022-02-01T00:00:00.000' AS DateTime), 48)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (2, 2, CAST(N'2022-01-04T00:00:00.000' AS DateTime), 581)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (3, 1, CAST(N'2022-02-01T00:00:00.000' AS DateTime), 434)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (3, 2, CAST(N'2022-03-01T00:00:00.000' AS DateTime), 352)
GO
INSERT [dbo].[reviews] ([order_id], [review_text], [stars], [created_at]) VALUES (1, N'Всё супер', 5, CAST(N'2022-03-18T00:00:00.000' AS DateTime))
INSERT [dbo].[reviews] ([order_id], [review_text], [stars], [created_at]) VALUES (2, N'Долго везли', 4, CAST(N'2022-03-18T00:00:00.000' AS DateTime))
INSERT [dbo].[reviews] ([order_id], [review_text], [stars], [created_at]) VALUES (3, N'Сыну понравилось', 5, CAST(N'2022-03-18T00:00:00.000' AS DateTime))
INSERT [dbo].[reviews] ([order_id], [review_text], [stars], [created_at]) VALUES (4, N'Муж доволен', 5, CAST(N'2022-03-18T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[rights] ON 

INSERT [dbo].[rights] ([id], [title]) VALUES (1, N'Просмотр')
INSERT [dbo].[rights] ([id], [title]) VALUES (2, N'Вставка')
INSERT [dbo].[rights] ([id], [title]) VALUES (3, N'Изменение')
INSERT [dbo].[rights] ([id], [title]) VALUES (4, N'Удаление')
SET IDENTITY_INSERT [dbo].[rights] OFF
GO
SET IDENTITY_INSERT [dbo].[section] ON 

INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (1, N'Пользователи', NULL)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (2, N'Доставки', NULL)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (3, N'Склады', NULL)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (4, N'Товары', NULL)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (5, N'Поставщики', NULL)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (6, N'Сотрудники', NULL)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (7, N'Общие сведения', 1)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (8, N'Средние затраты пользователей', 1)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (9, N'Список товаров в корзине', 1)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (10, N'Список доставок', 2)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (11, N'Доставки по ПВ, ФИО, коду', 2)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (12, N'История заказов', 2)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (13, N'Поступления на склады', 3)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (14, N'Работа сотрудников на складе', 3)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (15, N'Товары на складах', 3)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (16, N'Общие сведения', 4)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (17, N'Отзывы к товару', 4)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (18, N'Категории товаров', 4)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (19, N'История цен', 4)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (20, N'История поставок', 5)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (21, N'Прибыль', 5)
INSERT [dbo].[section] ([id], [title], [parent_id]) VALUES (22, N'Список сотрудников', 6)
SET IDENTITY_INSERT [dbo].[section] OFF
GO
SET IDENTITY_INSERT [dbo].[section_rights] ON 

INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (1, 1, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (2, 2, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (3, 3, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (4, 4, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (5, 5, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (6, 6, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (7, 7, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (8, 9, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (9, 10, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (10, 11, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (11, 12, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (12, 13, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (13, 14, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (14, 15, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (15, 16, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (16, 17, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (17, 18, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (18, 19, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (19, 20, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (20, 22, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (21, 1, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (22, 2, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (23, 3, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (24, 4, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (25, 5, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (26, 6, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (27, 7, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (28, 9, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (29, 10, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (30, 11, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (31, 12, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (32, 13, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (33, 14, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (34, 15, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (35, 16, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (36, 17, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (37, 18, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (38, 19, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (39, 20, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (40, 22, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (41, 1, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (42, 2, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (43, 3, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (44, 4, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (45, 5, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (46, 6, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (47, 7, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (48, 9, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (49, 10, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (50, 11, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (51, 12, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (52, 13, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (53, 14, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (54, 15, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (55, 16, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (56, 17, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (57, 18, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (58, 19, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (59, 20, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (60, 22, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (61, 1, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (62, 2, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (63, 3, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (64, 4, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (65, 5, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (66, 6, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (67, 7, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (68, 9, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (69, 10, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (70, 11, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (71, 12, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (72, 13, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (73, 14, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (74, 15, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (75, 16, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (76, 17, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (77, 18, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (78, 19, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (79, 20, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (80, 22, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (81, 8, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (82, 21, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (83, 1, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (84, 2, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (85, 3, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (86, 4, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (87, 5, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (88, 6, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (89, 7, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (90, 8, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (91, 9, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (92, 10, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (93, 11, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (94, 12, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (95, 13, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (96, 14, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (97, 15, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (98, 16, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (99, 17, 1, 5)
GO
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (100, 18, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (101, 19, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (102, 20, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (103, 21, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (104, 22, 1, 5)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (105, 13, 1, 1)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (106, 13, 3, 1)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (107, 15, 1, 1)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (108, 15, 3, 1)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (109, 11, 1, 2)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (110, 11, 3, 2)
SET IDENTITY_INSERT [dbo].[section_rights] OFF
GO
SET IDENTITY_INSERT [dbo].[storage_types] ON 

INSERT [dbo].[storage_types] ([id], [title]) VALUES (1, N'пункт выдачи')
INSERT [dbo].[storage_types] ([id], [title]) VALUES (2, N'распределительный склад')
INSERT [dbo].[storage_types] ([id], [title]) VALUES (3, N'сортировачный склад')
SET IDENTITY_INSERT [dbo].[storage_types] OFF
GO
INSERT [dbo].[storage_worker_shifts] ([worker_id], [storage_id], [started_shift_at], [finished_shift_at]) VALUES (1, 1, CAST(N'2022-01-03T08:00:00.000' AS DateTime), CAST(N'2022-01-03T15:48:00.000' AS DateTime))
INSERT [dbo].[storage_worker_shifts] ([worker_id], [storage_id], [started_shift_at], [finished_shift_at]) VALUES (2, 1, CAST(N'2022-01-03T10:50:00.000' AS DateTime), CAST(N'2022-01-03T18:50:00.000' AS DateTime))
INSERT [dbo].[storage_worker_shifts] ([worker_id], [storage_id], [started_shift_at], [finished_shift_at]) VALUES (3, 2, CAST(N'2022-01-03T16:00:00.000' AS DateTime), CAST(N'2022-01-03T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[storages] ON 

INSERT [dbo].[storages] ([id], [country], [federal_subject], [locality], [street], [house_number], [storage_type]) VALUES (1, N'Россия', N'Вологодская область', N'Вологда', N'Клубова', N'44а', 2)
INSERT [dbo].[storages] ([id], [country], [federal_subject], [locality], [street], [house_number], [storage_type]) VALUES (2, N'Россия', N'Вологодская область', N'Череповец', N'Гагарина', N'39', 2)
INSERT [dbo].[storages] ([id], [country], [federal_subject], [locality], [street], [house_number], [storage_type]) VALUES (3, N'Россия', N'Ярославская область', N'Ярославль', N'Чайковского', N'3', 3)
INSERT [dbo].[storages] ([id], [country], [federal_subject], [locality], [street], [house_number], [storage_type]) VALUES (4, N'Россия', N'Ярославская область', N'Ростов', N'Спартаковская', N'51', 3)
INSERT [dbo].[storages] ([id], [country], [federal_subject], [locality], [street], [house_number], [storage_type]) VALUES (5, N'Россия', N'Вологодская область', N'Вологда', N'Герцена', N'118', 1)
INSERT [dbo].[storages] ([id], [country], [federal_subject], [locality], [street], [house_number], [storage_type]) VALUES (6, N'Россия', N'Вологодская область', N'Вологда', N'Щетинина', N'15А', 1)
INSERT [dbo].[storages] ([id], [country], [federal_subject], [locality], [street], [house_number], [storage_type]) VALUES (7, N'Россия', N'Вологодская область', N'Вологда', N'Возрождения', N'72', 1)
INSERT [dbo].[storages] ([id], [country], [federal_subject], [locality], [street], [house_number], [storage_type]) VALUES (8, N'Россия', N'Вологодская область', N'Череповец', N'Ленинградская', N'60', 1)
INSERT [dbo].[storages] ([id], [country], [federal_subject], [locality], [street], [house_number], [storage_type]) VALUES (9, N'Россия', N'Вологодская область', N'Череповец', N'Наседкина', N'21', 1)
INSERT [dbo].[storages] ([id], [country], [federal_subject], [locality], [street], [house_number], [storage_type]) VALUES (10, N'Россия', N'Вологодская область', N'Череповец', N'Проспект Победы', N'162', 1)
INSERT [dbo].[storages] ([id], [country], [federal_subject], [locality], [street], [house_number], [storage_type]) VALUES (11, N'Россия', N'Ярославская область', N'Ярославль', N'улица Свободы', N'62б', 1)
INSERT [dbo].[storages] ([id], [country], [federal_subject], [locality], [street], [house_number], [storage_type]) VALUES (12, N'Россия', N'Ярославская область', N'Ярославль', N'Калиниа', N'23', 1)
INSERT [dbo].[storages] ([id], [country], [federal_subject], [locality], [street], [house_number], [storage_type]) VALUES (13, N'Россия', N'Ярославская область', N'Ярославль', N'Лермонтова', N'9к2', 1)
SET IDENTITY_INSERT [dbo].[storages] OFF
GO
SET IDENTITY_INSERT [dbo].[suppliers] ON 

INSERT [dbo].[suppliers] ([id], [title]) VALUES (1, N'Малевич')
INSERT [dbo].[suppliers] ([id], [title]) VALUES (2, N'Dialog')
INSERT [dbo].[suppliers] ([id], [title]) VALUES (3, N'Finn-Lux')
SET IDENTITY_INSERT [dbo].[suppliers] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [sex], [order_code], [country]) VALUES (1, N'Михаил', N'Бутуров', N'Сергеевич', N'8-953-624-01-22', CAST(N'2003-11-10T00:00:00.000' AS DateTime), N'butur@mm.ru', N'мужской', 142, N'Россия')
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [sex], [order_code], [country]) VALUES (3, N'Полина', N'Иренко', N'Сергеевна', N'8-973-714-04-62', CAST(N'2001-01-30T00:00:00.000' AS DateTime), N'pppm@Il.ru', N'женский', 772, N'Россия')
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [sex], [order_code], [country]) VALUES (4, N'Петр', N'Петров', N'Ильич', N'8-953-744-04-72', CAST(N'2002-02-11T00:00:00.000' AS DateTime), N'petrbm@Il.ru', N'мужской', 792, N'Россия')
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [sex], [order_code], [country]) VALUES (5, N'Марк', N'Дмитренко', N'Игоревич', N'8-953-684-75-57', CAST(N'2012-06-30T00:00:00.000' AS DateTime), N'markmarkmark@m.ru', N'мужской', 326, N'Россия')
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [sex], [order_code], [country]) VALUES (6, N'Анастасия', N'Муранова', N'Алексеевна', N'8-900-532-44-71', CAST(N'2001-09-07T00:00:00.000' AS DateTime), N'nastlastcast@nast.ru', N'женский', 454, N'Казахстан')
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [sex], [order_code], [country]) VALUES (7, N'Соня', N'Дедова', N'Владимировна', N'8-921-996-32-11', CAST(N'1996-01-17T00:00:00.000' AS DateTime), N'sonyaddos@vlad.ru', N'женский', 142, N'Россия')
SET IDENTITY_INSERT [dbo].[users] OFF
GO
SET IDENTITY_INSERT [dbo].[workers] ON 

INSERT [dbo].[workers] ([id], [firstname], [lastname], [patronymic], [phone_number], [date_of_birthday], [sex], [post], [password]) VALUES (1, N'Подхомутова', N'Кристина', N'Ивановна', N'8-953-624-78-41', CAST(N'1992-03-23T00:00:00.000' AS DateTime), N'женский', 1, NULL)
INSERT [dbo].[workers] ([id], [firstname], [lastname], [patronymic], [phone_number], [date_of_birthday], [sex], [post], [password]) VALUES (2, N'Кирилл', N'Олялин', N'Валентинович', N'8-936-524-11-26', CAST(N'1988-12-09T00:00:00.000' AS DateTime), N'мужской', 2, NULL)
INSERT [dbo].[workers] ([id], [firstname], [lastname], [patronymic], [phone_number], [date_of_birthday], [sex], [post], [password]) VALUES (3, N'Сидр', N'Замуленко', N'Егорович', N'8-811-926-65-84', CAST(N'1999-11-14T00:00:00.000' AS DateTime), N'мужской', 1, NULL)
SET IDENTITY_INSERT [dbo].[workers] OFF
GO
INSERT [dbo].[workers_in_orders] ([order_id], [status_changed_at], [worker_id]) VALUES (1, CAST(N'2022-08-08T10:18:15.000' AS DateTime), 3)
INSERT [dbo].[workers_in_orders] ([order_id], [status_changed_at], [worker_id]) VALUES (1, CAST(N'2022-08-08T10:19:14.000' AS DateTime), 3)
INSERT [dbo].[workers_in_orders] ([order_id], [status_changed_at], [worker_id]) VALUES (1, CAST(N'2022-09-07T10:13:14.000' AS DateTime), 1)
GO
/****** Object:  Index [IX_order_history]    Script Date: 18.01.2023 9:36:07 ******/
CREATE NONCLUSTERED INDEX [IX_order_history] ON [dbo].[order_history]
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__table_fi__95FD4F3929D12456]    Script Date: 18.01.2023 9:36:07 ******/
ALTER TABLE [dbo].[table_files] ADD UNIQUE NONCLUSTERED 
(
	[table_name] ASC,
	[record_id] ASC,
	[f_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[orders] ADD  CONSTRAINT [DF_orders_created_at]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[products] ADD  CONSTRAINT [DF_products_supplier_percent]  DEFAULT ((0)) FOR [supplier_percent]
GO
ALTER TABLE [dbo].[cards]  WITH CHECK ADD  CONSTRAINT [FK_cards_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[cards] CHECK CONSTRAINT [FK_cards_users]
GO
ALTER TABLE [dbo].[categories]  WITH CHECK ADD  CONSTRAINT [FK_categories_categories] FOREIGN KEY([parent_category_id])
REFERENCES [dbo].[categories] ([id])
GO
ALTER TABLE [dbo].[categories] CHECK CONSTRAINT [FK_categories_categories]
GO
ALTER TABLE [dbo].[deferred_products]  WITH CHECK ADD  CONSTRAINT [FK_deferred_products_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[deferred_products] CHECK CONSTRAINT [FK_deferred_products_products]
GO
ALTER TABLE [dbo].[deferred_products]  WITH CHECK ADD  CONSTRAINT [FK_deferred_products_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[deferred_products] CHECK CONSTRAINT [FK_deferred_products_users]
GO
ALTER TABLE [dbo].[order_history]  WITH CHECK ADD  CONSTRAINT [FK_order_history_order_statuses] FOREIGN KEY([status_id])
REFERENCES [dbo].[order_statuses] ([id])
GO
ALTER TABLE [dbo].[order_history] CHECK CONSTRAINT [FK_order_history_order_statuses]
GO
ALTER TABLE [dbo].[order_history]  WITH CHECK ADD  CONSTRAINT [FK_order_history_orders] FOREIGN KEY([order_id])
REFERENCES [dbo].[orders] ([id])
GO
ALTER TABLE [dbo].[order_history] CHECK CONSTRAINT [FK_order_history_orders]
GO
ALTER TABLE [dbo].[order_history]  WITH CHECK ADD  CONSTRAINT [FK_order_history_storages] FOREIGN KEY([current_storage_id])
REFERENCES [dbo].[storages] ([id])
GO
ALTER TABLE [dbo].[order_history] CHECK CONSTRAINT [FK_order_history_storages]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_ORDERS_PICK_UP_POINT_ID] FOREIGN KEY([pick_up_point_id])
REFERENCES [dbo].[storages] ([id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_ORDERS_PICK_UP_POINT_ID]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_products]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_users]
GO
ALTER TABLE [dbo].[price_history]  WITH CHECK ADD  CONSTRAINT [FK_price_history_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[price_history] CHECK CONSTRAINT [FK_price_history_products]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK_products_categories1] FOREIGN KEY([category_id])
REFERENCES [dbo].[categories] ([id])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK_products_categories1]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK_products_suppliers] FOREIGN KEY([supplier_id])
REFERENCES [dbo].[suppliers] ([id])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK_products_suppliers]
GO
ALTER TABLE [dbo].[products_on_storages]  WITH CHECK ADD  CONSTRAINT [FK_products_on_storages_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[products_on_storages] CHECK CONSTRAINT [FK_products_on_storages_products]
GO
ALTER TABLE [dbo].[products_on_storages]  WITH CHECK ADD  CONSTRAINT [FK_products_on_storages_storages] FOREIGN KEY([storage_id])
REFERENCES [dbo].[storages] ([id])
GO
ALTER TABLE [dbo].[products_on_storages] CHECK CONSTRAINT [FK_products_on_storages_storages]
GO
ALTER TABLE [dbo].[products_parameters]  WITH CHECK ADD  CONSTRAINT [FK_products_parameters_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[products_parameters] CHECK CONSTRAINT [FK_products_parameters_products]
GO
ALTER TABLE [dbo].[receipt_of_products_to_storages]  WITH CHECK ADD  CONSTRAINT [FK__receipt_o__stora__7FEAFD3E] FOREIGN KEY([storage_id])
REFERENCES [dbo].[storages] ([id])
GO
ALTER TABLE [dbo].[receipt_of_products_to_storages] CHECK CONSTRAINT [FK__receipt_o__stora__7FEAFD3E]
GO
ALTER TABLE [dbo].[receipt_of_products_to_storages]  WITH CHECK ADD  CONSTRAINT [FK_receipt_of_products_to_storages_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[receipt_of_products_to_storages] CHECK CONSTRAINT [FK_receipt_of_products_to_storages_products]
GO
ALTER TABLE [dbo].[reviews]  WITH CHECK ADD  CONSTRAINT [FK_reviews_orders] FOREIGN KEY([order_id])
REFERENCES [dbo].[orders] ([id])
GO
ALTER TABLE [dbo].[reviews] CHECK CONSTRAINT [FK_reviews_orders]
GO
ALTER TABLE [dbo].[section_rights]  WITH CHECK ADD FOREIGN KEY([post_id])
REFERENCES [dbo].[posts] ([id])
GO
ALTER TABLE [dbo].[section_rights]  WITH CHECK ADD FOREIGN KEY([right_id])
REFERENCES [dbo].[rights] ([id])
GO
ALTER TABLE [dbo].[section_rights]  WITH CHECK ADD FOREIGN KEY([section_id])
REFERENCES [dbo].[section] ([id])
GO
ALTER TABLE [dbo].[storage_worker_shifts]  WITH CHECK ADD  CONSTRAINT [FK_workers_at_storages_storages] FOREIGN KEY([storage_id])
REFERENCES [dbo].[storages] ([id])
GO
ALTER TABLE [dbo].[storage_worker_shifts] CHECK CONSTRAINT [FK_workers_at_storages_storages]
GO
ALTER TABLE [dbo].[storage_worker_shifts]  WITH CHECK ADD  CONSTRAINT [FK_workers_at_storages_workers] FOREIGN KEY([worker_id])
REFERENCES [dbo].[workers] ([id])
GO
ALTER TABLE [dbo].[storage_worker_shifts] CHECK CONSTRAINT [FK_workers_at_storages_workers]
GO
ALTER TABLE [dbo].[storages]  WITH CHECK ADD  CONSTRAINT [fk_storage_type] FOREIGN KEY([storage_type])
REFERENCES [dbo].[storage_types] ([id])
GO
ALTER TABLE [dbo].[storages] CHECK CONSTRAINT [fk_storage_type]
GO
ALTER TABLE [dbo].[table_files]  WITH CHECK ADD FOREIGN KEY([f_id])
REFERENCES [dbo].[files] ([id])
GO
ALTER TABLE [dbo].[workers]  WITH CHECK ADD  CONSTRAINT [FK_workers_posts] FOREIGN KEY([post])
REFERENCES [dbo].[posts] ([id])
GO
ALTER TABLE [dbo].[workers] CHECK CONSTRAINT [FK_workers_posts]
GO
ALTER TABLE [dbo].[workers_in_orders]  WITH CHECK ADD FOREIGN KEY([worker_id])
REFERENCES [dbo].[workers] ([id])
GO
ALTER TABLE [dbo].[workers_in_orders]  WITH CHECK ADD  CONSTRAINT [FK_workers_in_orders_order_history] FOREIGN KEY([order_id], [status_changed_at])
REFERENCES [dbo].[order_history] ([order_id], [status_changed_at])
GO
ALTER TABLE [dbo].[workers_in_orders] CHECK CONSTRAINT [FK_workers_in_orders_order_history]
GO
USE [master]
GO
ALTER DATABASE [ISWildberries] SET  READ_WRITE 
GO
