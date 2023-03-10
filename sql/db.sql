USE [master]
GO
/****** Object:  Database [ISWildberries]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[products_parameters]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[products]    Script Date: 23.02.2023 16:20:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[price] [float] NOT NULL,
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
/****** Object:  View [dbo].[view_get_product_parametres]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[storages]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[posts]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[order_statuses]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[workers_in_orders]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[order_history]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[orders]    Script Date: 23.02.2023 16:20:48 ******/
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
	[price] [float] NULL,
	[created_at] [datetime] NOT NULL,
	[estimated_delivery_at] [datetime] NULL,
 CONSTRAINT [PK_orders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workers]    Script Date: 23.02.2023 16:20:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[lastname] [nvarchar](255) NULL,
	[firstname] [nvarchar](255) NULL,
	[patronymic] [nvarchar](255) NULL,
	[phone_number] [nvarchar](255) NULL,
	[date_of_birthday] [datetime] NULL,
	[post_id] [int] NULL,
	[worker_password] [varbinary](max) NULL,
	[is_male] [bit] NULL,
	[worker_login] [nvarchar](255) NULL,
 CONSTRAINT [PK_workers_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[view_get_workers_in_orders]    Script Date: 23.02.2023 16:20:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[view_get_workers_in_orders]
AS
SELECT workers.firstname, workers.lastname, workers.patronymic, workers.is_male, posts.title as post,
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
/****** Object:  Table [dbo].[cards]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[categories]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[countries]    Script Date: 23.02.2023 16:20:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[countries](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[deferred_products]    Script Date: 23.02.2023 16:20:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[deferred_products](
	[user_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_def_p_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[files]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[price_history]    Script Date: 23.02.2023 16:20:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[price_history](
	[product_id] [int] NOT NULL,
	[price] [float] NOT NULL,
	[price_date] [datetime] NOT NULL,
 CONSTRAINT [PK_price_history] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC,
	[price_date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products_on_storages]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[receipt_of_products_to_storages]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[reviews]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[rights]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[section_rights]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[sections]    Script Date: 23.02.2023 16:20:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sections](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](255) NULL,
	[parent_id] [int] NULL,
	[section_key] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[storage_types]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[storage_worker_shifts]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[suppliers]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[table_files]    Script Date: 23.02.2023 16:20:48 ******/
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
/****** Object:  Table [dbo].[users]    Script Date: 23.02.2023 16:20:48 ******/
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
	[order_code] [int] NULL,
	[is_male] [bit] NULL,
	[country_id] [int] NULL,
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
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (6, N'Красота и уход за собой', NULL)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (7, N'Спорт', NULL)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (8, N'Игрушки и игры', NULL)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (9, N'Книги', NULL)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (10, N'Настольные игры', 8)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (11, N'Мягкие игрушки', 8)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (12, N'Парфюмерия', 6)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (13, N'Волосы', 6)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (14, N'Женские ароматы', 12)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (15, N'Мужские ароматы', 12)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (16, N'Для роста волос', 13)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (17, N'Окрашивание', 13)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (18, N'Тренажёры', 7)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (19, N'Спортивная одежда и обувь', 7)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (20, N'Художественная литература', 9)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (21, N'Хобби и досуг', 9)
INSERT [dbo].[categories] ([id], [title], [parent_category_id]) VALUES (22, N'Философия', 9)
SET IDENTITY_INSERT [dbo].[categories] OFF
GO
SET IDENTITY_INSERT [dbo].[countries] ON 

INSERT [dbo].[countries] ([id], [title]) VALUES (1, N'Казахстан')
INSERT [dbo].[countries] ([id], [title]) VALUES (2, N'Россия')
INSERT [dbo].[countries] ([id], [title]) VALUES (3, N'Армения')
INSERT [dbo].[countries] ([id], [title]) VALUES (4, N'Беларусь')
INSERT [dbo].[countries] ([id], [title]) VALUES (5, N'Израиль')
INSERT [dbo].[countries] ([id], [title]) VALUES (6, N'Киргизия')
INSERT [dbo].[countries] ([id], [title]) VALUES (7, N'Узбекистан')
SET IDENTITY_INSERT [dbo].[countries] OFF
GO
SET IDENTITY_INSERT [dbo].[deferred_products] ON 

INSERT [dbo].[deferred_products] ([user_id], [product_id], [id]) VALUES (1, 2, 1)
INSERT [dbo].[deferred_products] ([user_id], [product_id], [id]) VALUES (1, 3, 2)
INSERT [dbo].[deferred_products] ([user_id], [product_id], [id]) VALUES (3, 3, 3)
INSERT [dbo].[deferred_products] ([user_id], [product_id], [id]) VALUES (4, 3, 4)
INSERT [dbo].[deferred_products] ([user_id], [product_id], [id]) VALUES (5, 1, 5)
SET IDENTITY_INSERT [dbo].[deferred_products] OFF
GO
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (1, CAST(N'2022-07-03T08:48:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (1, CAST(N'2022-07-03T08:53:00.000' AS DateTime), 2, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (1, CAST(N'2022-07-09T10:13:14.000' AS DateTime), 3, 5, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (1, CAST(N'2022-08-08T10:18:15.000' AS DateTime), 4, 5, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (1, CAST(N'2022-08-08T10:19:14.000' AS DateTime), 5, 5, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (1, CAST(N'2022-08-08T11:13:55.000' AS DateTime), 10, 5, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (1, CAST(N'2022-08-08T14:39:29.000' AS DateTime), 11, 5, 1)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (2, CAST(N'2022-07-03T10:24:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (2, CAST(N'2022-07-03T10:37:50.000' AS DateTime), 2, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (2, CAST(N'2022-07-05T10:11:50.000' AS DateTime), 3, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (2, CAST(N'2022-07-11T10:11:50.000' AS DateTime), 5, 7, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (2, CAST(N'2022-07-13T10:23:50.000' AS DateTime), 7, 7, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (2, CAST(N'2022-07-13T19:54:15.000' AS DateTime), 10, 7, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (2, CAST(N'2022-07-13T21:32:23.000' AS DateTime), 11, 7, 1)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (3, CAST(N'2022-07-03T10:25:00.000' AS DateTime), 1, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (3, CAST(N'2022-07-03T10:29:23.000' AS DateTime), 2, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (3, CAST(N'2022-07-03T19:12:12.000' AS DateTime), 3, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (3, CAST(N'2022-07-06T12:24:03.000' AS DateTime), 7, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (3, CAST(N'2022-07-06T14:38:26.000' AS DateTime), 8, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (3, CAST(N'2022-07-06T15:35:58.000' AS DateTime), 9, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (3, CAST(N'2022-07-06T17:09:46.000' AS DateTime), 10, 8, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (3, CAST(N'2022-07-07T03:23:48.000' AS DateTime), 11, 8, 1)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (4, CAST(N'2022-07-03T10:48:00.000' AS DateTime), 1, 5, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (4, CAST(N'2022-07-03T10:51:02.000' AS DateTime), 2, 5, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (4, CAST(N'2022-07-04T03:19:22.000' AS DateTime), 3, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (4, CAST(N'2022-07-04T15:20:27.000' AS DateTime), 11, 10, 1)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (5, CAST(N'2022-07-03T08:49:00.000' AS DateTime), 1, 5, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (5, CAST(N'2022-07-03T09:10:58.000' AS DateTime), 2, 5, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (5, CAST(N'2022-07-04T00:18:22.000' AS DateTime), 3, 5, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (5, CAST(N'2022-07-06T23:23:19.000' AS DateTime), 10, 6, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (5, CAST(N'2022-07-07T08:03:51.000' AS DateTime), 11, 6, 1)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (6, CAST(N'2022-07-03T10:47:00.000' AS DateTime), 1, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (6, CAST(N'2022-07-03T10:55:41.000' AS DateTime), 2, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (6, CAST(N'2022-07-04T07:43:57.000' AS DateTime), 3, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (6, CAST(N'2022-07-05T19:13:23.000' AS DateTime), 10, 9, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (6, CAST(N'2022-07-05T22:39:37.000' AS DateTime), 11, 9, 1)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (12, CAST(N'2022-07-06T07:28:28.000' AS DateTime), 1, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (12, CAST(N'2022-07-06T07:57:32.000' AS DateTime), 2, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (12, CAST(N'2022-07-07T01:05:12.000' AS DateTime), 3, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (12, CAST(N'2022-07-09T14:52:18.000' AS DateTime), 10, 12, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (12, CAST(N'2022-07-10T13:09:10.000' AS DateTime), 11, 12, 1)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (13, CAST(N'2022-07-06T10:35:46.000' AS DateTime), 1, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (13, CAST(N'2022-07-06T11:14:50.000' AS DateTime), 2, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (13, CAST(N'2022-07-06T19:01:52.000' AS DateTime), 3, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (13, CAST(N'2022-07-07T04:52:57.000' AS DateTime), 10, 12, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (13, CAST(N'2022-07-08T01:10:11.000' AS DateTime), 11, 12, 1)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (14, CAST(N'2022-08-06T03:29:07.000' AS DateTime), 1, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (14, CAST(N'2022-08-06T04:16:14.000' AS DateTime), 2, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (14, CAST(N'2022-08-06T08:46:36.000' AS DateTime), 3, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (14, CAST(N'2022-08-08T19:26:07.000' AS DateTime), 10, 13, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (14, CAST(N'2022-08-09T18:31:05.000' AS DateTime), 11, 13, 1)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (15, CAST(N'2022-06-06T11:24:08.000' AS DateTime), 1, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (15, CAST(N'2022-06-06T11:29:46.000' AS DateTime), 2, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (15, CAST(N'2022-06-06T22:10:56.000' AS DateTime), 3, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (15, CAST(N'2022-06-07T00:10:15.000' AS DateTime), 10, 13, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (15, CAST(N'2022-06-07T16:08:35.000' AS DateTime), 11, 13, 1)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (16, CAST(N'2022-08-06T12:48:05.000' AS DateTime), 1, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (16, CAST(N'2022-08-06T13:17:14.000' AS DateTime), 2, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (16, CAST(N'2022-08-07T07:06:33.000' AS DateTime), 3, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (16, CAST(N'2022-08-07T08:21:23.000' AS DateTime), 10, 11, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (16, CAST(N'2022-08-07T20:59:44.000' AS DateTime), 11, 11, 1)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (17, CAST(N'2022-07-06T22:27:00.000' AS DateTime), 1, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (17, CAST(N'2022-07-06T22:29:54.000' AS DateTime), 2, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (17, CAST(N'2022-07-07T04:28:49.000' AS DateTime), 3, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (17, CAST(N'2022-07-07T09:31:17.000' AS DateTime), 7, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (17, CAST(N'2022-07-07T09:39:55.000' AS DateTime), 8, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (17, CAST(N'2022-07-07T11:05:32.000' AS DateTime), 9, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (17, CAST(N'2022-07-08T04:02:28.000' AS DateTime), 10, 8, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (17, CAST(N'2022-07-08T21:37:59.000' AS DateTime), 11, 8, 1)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (18, CAST(N'2022-06-06T08:18:13.000' AS DateTime), 1, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (18, CAST(N'2022-06-06T08:44:39.000' AS DateTime), 2, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (18, CAST(N'2022-06-06T12:01:20.000' AS DateTime), 3, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (18, CAST(N'2022-06-08T03:32:18.000' AS DateTime), 7, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (18, CAST(N'2022-06-08T03:54:40.000' AS DateTime), 8, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (18, CAST(N'2022-06-08T05:12:58.000' AS DateTime), 9, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (18, CAST(N'2022-06-09T08:27:22.000' AS DateTime), 10, 8, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (18, CAST(N'2022-06-09T16:04:30.000' AS DateTime), 11, 8, 1)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (19, CAST(N'2022-08-06T18:56:51.000' AS DateTime), 1, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (19, CAST(N'2022-08-06T19:43:32.000' AS DateTime), 2, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (19, CAST(N'2022-08-07T10:53:47.000' AS DateTime), 3, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (19, CAST(N'2022-08-08T09:58:16.000' AS DateTime), 7, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (19, CAST(N'2022-08-08T13:56:11.000' AS DateTime), 8, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (19, CAST(N'2022-08-08T14:10:45.000' AS DateTime), 9, 2, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (19, CAST(N'2022-08-10T23:47:09.000' AS DateTime), 10, 9, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (19, CAST(N'2022-08-11T16:25:02.000' AS DateTime), 11, 9, 1)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (20, CAST(N'2022-06-06T23:14:14.000' AS DateTime), 1, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (20, CAST(N'2022-06-06T23:28:44.000' AS DateTime), 2, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (20, CAST(N'2022-06-07T21:51:53.000' AS DateTime), 3, 3, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (20, CAST(N'2022-06-09T13:21:06.000' AS DateTime), 7, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (20, CAST(N'2022-06-09T14:32:02.000' AS DateTime), 8, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (20, CAST(N'2022-06-09T14:51:56.000' AS DateTime), 9, 1, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (20, CAST(N'2022-06-11T16:58:06.000' AS DateTime), 10, 7, 0)
INSERT [dbo].[order_history] ([order_id], [status_changed_at], [status_id], [current_storage_id], [is_last_status]) VALUES (20, CAST(N'2022-06-11T21:56:18.000' AS DateTime), 11, 7, 1)
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

INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_at]) VALUES (1, 1, 1, 1, 5, 170, CAST(N'2022-07-03T08:48:00.000' AS DateTime), CAST(N'2022-07-17T08:48:00.000' AS DateTime))
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_at]) VALUES (2, 3, 2, 1, 7, 319, CAST(N'2022-07-03T10:24:00.000' AS DateTime), CAST(N'2022-07-17T10:24:00.000' AS DateTime))
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_at]) VALUES (3, 4, 2, 1, 8, 319, CAST(N'2022-07-03T10:25:00.000' AS DateTime), CAST(N'2022-07-17T10:25:00.000' AS DateTime))
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_at]) VALUES (4, 7, 2, 1, 10, 319, CAST(N'2022-07-03T10:48:00.000' AS DateTime), CAST(N'2022-07-17T10:48:00.000' AS DateTime))
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_at]) VALUES (5, 3, 3, 1, 6, 2156, CAST(N'2022-07-03T08:49:00.000' AS DateTime), CAST(N'2022-07-17T08:49:00.000' AS DateTime))
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_at]) VALUES (6, 5, 3, 1, 9, 2156, CAST(N'2022-07-03T10:47:00.000' AS DateTime), CAST(N'2022-07-17T10:47:00.000' AS DateTime))
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_at]) VALUES (12, 15, 6, 1, 12, 36.76, CAST(N'2022-07-06T07:28:28.000' AS DateTime), CAST(N'2022-07-20T07:28:28.000' AS DateTime))
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_at]) VALUES (13, 6, 6, 1, 12, 36.76, CAST(N'2022-07-06T10:35:46.000' AS DateTime), CAST(N'2022-07-20T10:35:46.000' AS DateTime))
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_at]) VALUES (14, 11, 8, 2, 13, 27.92, CAST(N'2022-08-06T03:29:07.000' AS DateTime), CAST(N'2022-08-20T03:29:07.000' AS DateTime))
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_at]) VALUES (15, 12, 10, 1, 13, 1859.14, CAST(N'2022-06-06T11:24:08.000' AS DateTime), CAST(N'2022-06-20T11:24:08.000' AS DateTime))
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_at]) VALUES (16, 13, 10, 1, 11, 2082.18, CAST(N'2022-08-06T12:48:05.000' AS DateTime), CAST(N'2022-08-20T12:48:05.000' AS DateTime))
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_at]) VALUES (17, 14, 11, 1, 8, 3113.92, CAST(N'2022-07-06T22:27:00.000' AS DateTime), CAST(N'2022-07-20T22:27:00.000' AS DateTime))
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_at]) VALUES (18, 16, 11, 1, 8, 2941.47, CAST(N'2022-06-06T08:18:13.000' AS DateTime), CAST(N'2022-06-20T08:18:13.000' AS DateTime))
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_at]) VALUES (19, 17, 11, 1, 9, 3352.04, CAST(N'2022-08-06T18:56:51.000' AS DateTime), CAST(N'2022-08-20T18:56:51.000' AS DateTime))
INSERT [dbo].[orders] ([id], [user_id], [product_id], [product_count], [pick_up_point_id], [price], [created_at], [estimated_delivery_at]) VALUES (20, 18, 11, 1, 7, 2941.47, CAST(N'2022-06-06T23:14:14.000' AS DateTime), CAST(N'2022-06-20T23:14:14.000' AS DateTime))
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
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1, 190, CAST(N'2022-07-06T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (2, 340, CAST(N'2022-03-26T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (2, 319, CAST(N'2022-03-30T20:12:22.870' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (2, 335, CAST(N'2022-04-12T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (2, 324, CAST(N'2022-05-13T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (2, 319, CAST(N'2022-06-14T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (2, 319, CAST(N'2022-07-14T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (3, 2156, CAST(N'2022-03-14T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (3, 2156, CAST(N'2022-03-30T20:12:32.403' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (3, 2199, CAST(N'2022-04-01T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (3, 2156, CAST(N'2022-06-30T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (3, 2156, CAST(N'2022-07-30T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (4, 67, CAST(N'2022-05-11T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (4, 70, CAST(N'2022-06-11T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (4, 65, CAST(N'2022-07-11T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (4, 63, CAST(N'2022-08-11T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (4, 65, CAST(N'2022-09-11T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (4, 71, CAST(N'2022-10-11T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (4, 68.75, CAST(N'2022-11-11T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (5, 31.91, CAST(N'2022-05-08T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (5, 33.82, CAST(N'2022-06-08T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (5, 31.79, CAST(N'2022-07-08T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (5, 35.29, CAST(N'2022-08-08T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (5, 34.94, CAST(N'2022-09-08T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (5, 34.69, CAST(N'2022-10-08T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (6, 33.52, CAST(N'2022-05-09T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (6, 36.76, CAST(N'2022-06-09T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (6, 34.08, CAST(N'2022-07-09T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (6, 31.44, CAST(N'2022-08-09T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (6, 34.4, CAST(N'2022-09-09T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (6, 31.3, CAST(N'2022-10-09T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (7, 36.99, CAST(N'2022-05-19T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (7, 33.98, CAST(N'2022-06-19T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (7, 35.47, CAST(N'2022-07-19T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (8, 16.12, CAST(N'2022-05-12T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (8, 15.17, CAST(N'2022-06-12T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (8, 13.96, CAST(N'2022-07-12T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (8, 15.02, CAST(N'2022-08-12T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (8, 16.14, CAST(N'2022-09-12T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (8, 16.75, CAST(N'2022-10-12T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (8, 16.31, CAST(N'2022-11-12T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (9, 13.11, CAST(N'2022-05-16T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (9, 11.91, CAST(N'2022-06-16T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (9, 11.76, CAST(N'2022-07-16T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (9, 14.3, CAST(N'2022-08-16T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (10, 1859.14, CAST(N'2022-05-14T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (10, 1950.48, CAST(N'2022-06-14T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (10, 2082.18, CAST(N'2022-07-14T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (10, 2050.11, CAST(N'2022-08-14T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (10, 1716.39, CAST(N'2022-09-14T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (11, 2941.47, CAST(N'2022-05-18T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (11, 3113.92, CAST(N'2022-06-18T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (11, 3352.04, CAST(N'2022-07-18T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (11, 3399.18, CAST(N'2022-08-18T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (11, 3045.19, CAST(N'2022-09-18T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (12, 2426.42, CAST(N'2022-05-18T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (12, 2245.85, CAST(N'2022-06-18T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (12, 2228.85, CAST(N'2022-07-18T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (12, 2143.51, CAST(N'2022-08-18T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (12, 2369.5, CAST(N'2022-09-18T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (12, 2202.86, CAST(N'2022-10-18T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (14, 2380.19, CAST(N'2022-05-21T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (14, 2157.15, CAST(N'2022-06-21T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (14, 1984.44, CAST(N'2022-07-21T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (14, 2108.76, CAST(N'2022-08-21T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (14, 1931.66, CAST(N'2022-09-21T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (14, 2346.71, CAST(N'2022-10-21T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (15, 540.19, CAST(N'2022-05-23T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (15, 517.79, CAST(N'2022-06-23T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (15, 547.38, CAST(N'2022-07-23T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (15, 597.64, CAST(N'2022-08-23T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (15, 657.85, CAST(N'2022-09-23T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (15, 688.65, CAST(N'2022-10-23T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (15, 544.27, CAST(N'2022-11-23T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (16, 1568.62, CAST(N'2022-05-23T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (16, 1680.37, CAST(N'2022-06-23T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (16, 1747.54, CAST(N'2022-07-23T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (16, 1697.34, CAST(N'2022-08-23T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (16, 1556.84, CAST(N'2022-09-23T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (16, 1491.92, CAST(N'2022-10-23T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (17, 2316.82, CAST(N'2022-05-24T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (17, 2476.17, CAST(N'2022-06-24T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (17, 2300.82, CAST(N'2022-07-24T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (17, 2481.41, CAST(N'2022-08-24T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (17, 2705.32, CAST(N'2022-09-24T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (17, 2118.55, CAST(N'2022-10-24T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1004, 604.24, CAST(N'2025-02-07T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1004, 642.56, CAST(N'2025-03-07T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1004, 619.66, CAST(N'2025-04-07T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1004, 649.3, CAST(N'2025-05-07T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1004, 622.75, CAST(N'2025-06-07T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1004, 614.98, CAST(N'2025-07-07T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1005, 1493.67, CAST(N'2025-02-04T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1005, 1378.53, CAST(N'2025-03-04T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1005, 1406.15, CAST(N'2025-04-04T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1006, 13166.64, CAST(N'2025-02-06T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1006, 14208.52, CAST(N'2025-03-06T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1006, 12935.83, CAST(N'2025-04-06T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1006, 13561.94, CAST(N'2025-05-06T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1006, 13642.49, CAST(N'2025-06-06T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1006, 13234.43, CAST(N'2025-07-06T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1006, 13367, CAST(N'2025-08-06T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1006, 13766.32, CAST(N'2025-09-06T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1007, 3368, CAST(N'2025-02-05T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1007, 3552.98, CAST(N'2025-03-05T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1007, 3380.86, CAST(N'2025-04-05T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1007, 3157.07, CAST(N'2025-05-05T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1007, 3519.19, CAST(N'2025-06-05T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1008, 9808.25, CAST(N'2025-02-04T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1008, 9438.32, CAST(N'2025-03-04T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1008, 9348.33, CAST(N'2025-04-04T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1009, 1179.77, CAST(N'2025-02-11T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1009, 1087.92, CAST(N'2025-03-11T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1009, 1094.83, CAST(N'2025-04-11T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1009, 1031.45, CAST(N'2025-05-11T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1009, 930.05, CAST(N'2025-06-11T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1009, 856.37, CAST(N'2025-07-11T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1009, 1087.72, CAST(N'2025-08-11T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1010, 1111.74, CAST(N'2025-02-08T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1010, 1029.68, CAST(N'2025-03-08T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1010, 1068.53, CAST(N'2025-04-08T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1010, 1044.52, CAST(N'2025-05-08T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1011, 692.53, CAST(N'2025-02-09T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1011, 646.4, CAST(N'2025-03-09T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1011, 667.24, CAST(N'2025-04-09T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1011, 647.3, CAST(N'2025-05-09T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1012, 638.09, CAST(N'2025-02-15T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1012, 685.23, CAST(N'2025-03-15T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1012, 741.09, CAST(N'2025-04-15T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1012, 773.48, CAST(N'2025-05-15T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1012, 690.19, CAST(N'2025-06-15T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1013, 3664.39, CAST(N'2025-02-16T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1013, 3545.27, CAST(N'2025-03-16T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1013, 3293.46, CAST(N'2025-04-16T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1013, 3451.4, CAST(N'2025-05-16T00:00:00.000' AS DateTime))
INSERT [dbo].[price_history] ([product_id], [price], [price_date]) VALUES (1013, 3433.3, CAST(N'2025-06-16T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[products] ON 

INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (1, N'Ручка линер', 190, 1, 2, N'Капилярная ручка с толщиной линии 0,3 мм', 95)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (2, N'Скетчбук', 319, 2, 3, N'Скетчбук для творчества формата А6', 87)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (3, N'Ценник', 2156, 3, 4, N'Ценник', 92)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (4, N'Тетрадь 48 листов', 68.75, 5, 5, N'Тетрадь 48 листов в клеточку', 98.2)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (5, N'Тетрадь 48 листов', 34.69, 5, 5, N'Тетрадь 48 листов в линию', 80.63)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (6, N'Тетрадь 96 листов', 31.3, 4, 5, N'Тетрадь 96 листов в линию', 81.67)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (7, N'Тетрадь 96 листов', 35.47, 4, 5, N'Тетрадь 96 листов в клеточку', 90.2)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (8, N'Тетрадь 12 листов', 16.31, 4, 5, N'Тетрадь 12 листов в клеточку', 78.83)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (9, N'Тетрадь 12 листов', 14.3, 4, 5, N'Тетрадь 12 листов в линию', 91.36)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (10, N'Мягкая игрушка котёнок', 1716.39, 6, 11, N'Мягкая игрушка котёнок - лучший подарок ребёнку. Сделана из исключительно безопасных и качественных метариалов. Габариты 15 см * 22 см * 30 см.', 89.57)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (11, N'Мягкая игрушка Хаги-Ваги', 3045.19, 6, 11, N'Мягкая игрушка Хаги-Ваги - прекрасный подарок ребёнку или другу. Узнаваемый образ гарантирует радость от подарка. Сделана из исключительно безопасных и качественных метариалов. Габариты 35 см * 25 см * 5 см.', 85.45)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (12, N'Настольная игра Свинтус Делюкс', 2202.86, 6, 10, N'Перед вами огромное подарочное издание, от которого любой фанат этой игры придёт в восторг. Игра прекрасно подойдёт для весёлого времяпровождения с друзьями и близкими.', 84.14)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (14, N'Духи "Элеганс"', 2346.71, 7, 14, N'Представляем "Элеганс", роскошный и утонченный аромат, сочетающий в себе чарующие ароматы жасмина и розы с богатыми древесными нотами сандала и пачули. Этот элегантный и гармоничный аромат идеально подходит для любого случая.', 76.72)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (15, N'Духи "Затмение"', 544.27, 7, 14, N'Стойкий аромат "Затмения" заставит вас чувствовать себя уверенно и маняще весь день. Гладкий и стильный дизайн флакона добавит элегантности любому туалетному столику.', 95.31)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (16, N'Парфюм "Океан"', 1491.92, 8, 15, N'"Океан": Этот суровый и мужественный аромат сочетает в себе земляные и древесные ноты орлиного дерева и кедра с острыми и пряными нотами черного перца и кардамона. Идеально подходит для современного мужчины', 84.41)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (17, N'Парфюм "Вечность"', 2118.55, 8, 15, N'«Вечность» - это классический и изысканный аромат, который сочетает в себе теплые древесные ноты амбры и ветивера с четкими и чистыми нотами бергамота и лимона. Идеально подходит для мужчин, которые хотят излучать элегантность и изысканность.', 87.8)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (1004, N'Миноксидил для волос и бороды', 614.98, 8, 16, N'Миноксидил - лосьон по уходу за волосами, стимулирующий их рост и препятствующий выпадению', 80.33)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (1005, N'Крем-краска для волос Excellence №4.54', 1406.15, 7, 17, N'Стойкая крем-краска для волос Excellence №4.54 Богатый медный', 89.11)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (1006, N'Велотренажёр', 13766.32, 10, 18, N'Данный велтренажёр предназначен для домашнего использования, помогает тренировать все мышцы тела. С его помощью можно эффективно придать спортивную форму телу.', 94.77)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (1007, N'Баскетбольные кроссовки', 3519.19, 10, 19, N'Баскетбольные кроссовки имеют прочную резиновую подошву, обеспечивающее хорошее сцепление с любой поверхностью. Идеальны для занятий спортом.', 98.47)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (1008, N'Куртка сноубордическая', 9348.33, 11, 19, N'Куртка отлично подходит как для занятий горнолыжным спортом, так и для повседневной носки.', 98.94)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (1009, N'"Властелин Колец" Дж.Р.Р.Толкин', 1087.72, 9, 20, N'Культовая трилогия "Властелин Колец", привлёкшая к себе миллионы читателец', 93.49)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (1010, N'Метро 2034', 1044.52, 9, 20, N'Вторая часть трилогии Дмитрия Глуховского, повторившая успех первой части.', 82.98)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (1011, N'Большая книга рецептов. Консервирование', 647.3, 9, 21, N'Из этой книги вы узнаете основы консервированя.', 77.07)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (1012, N'Большая книга рецептов. Салаты', 690.19, 9, 21, N'Эта книга посвящена приготовлению любимой закуски - салатов.', 90.19)
INSERT [dbo].[products] ([id], [title], [price], [supplier_id], [category_id], [description], [supplier_percent]) VALUES (1013, N'Фридрих Ницше. Так говорил Заратруста', 3433.3, 9, 22, N'Эксклюзивное подарочное издание в подарочной упаковке.', 89.71)
SET IDENTITY_INSERT [dbo].[products] OFF
GO
INSERT [dbo].[products_on_storages] ([storage_id], [product_id], [product_amount]) VALUES (1, 1, 34)
INSERT [dbo].[products_on_storages] ([storage_id], [product_id], [product_amount]) VALUES (1, 2, 256)
INSERT [dbo].[products_on_storages] ([storage_id], [product_id], [product_amount]) VALUES (1, 3, 432)
INSERT [dbo].[products_on_storages] ([storage_id], [product_id], [product_amount]) VALUES (2, 1, 234)
INSERT [dbo].[products_on_storages] ([storage_id], [product_id], [product_amount]) VALUES (2, 2, 581)
INSERT [dbo].[products_on_storages] ([storage_id], [product_id], [product_amount]) VALUES (2, 3, 352)
INSERT [dbo].[products_on_storages] ([storage_id], [product_id], [product_amount]) VALUES (3, 6, 5)
INSERT [dbo].[products_on_storages] ([storage_id], [product_id], [product_amount]) VALUES (3, 8, 5)
INSERT [dbo].[products_on_storages] ([storage_id], [product_id], [product_amount]) VALUES (3, 10, 0)
INSERT [dbo].[products_on_storages] ([storage_id], [product_id], [product_amount]) VALUES (3, 11, 12)
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
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (4, N'Формат', N'А5')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (4, N'Число листов', N'48 листов')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (5, N'Формат', N'А5')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (5, N'Число листов', N'48 листов')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (6, N'Формат', N'А5')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (6, N'Число листов', N'96 листов')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (7, N'Формат', N'А5')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (7, N'Число листов', N'96 листов')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (8, N'Формат', N'А5')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (8, N'Число листов', N'12 листов')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (9, N'Формат', N'А5')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (9, N'Число листов', N'12 листов')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (10, N'Габариты', N'15 см * 22 см * 30 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (10, N'Цвет', N'Коричневый')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (11, N'Габариты', N'35 см * 25 см * 5 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (11, N'Цвет', N'Синий')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (12, N'Вес с упаковкой', N'210 г')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (12, N'Габариты упаковки', N'13 см * 13 см * 20 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (14, N'Назначение аромата', N'женский')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (14, N'Срок годности', N'5 лет')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (15, N'Назначение аромата', N'женский')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (15, N'Срок годности', N'5 лет')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (16, N'Назначение аромата', N'мужской')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (16, N'Срок годности', N'5 лет')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (17, N'Назначение аромата', N'мужской')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (17, N'Срок годности', N'5 лет')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1005, N'Возрастные ограничения', N'14+')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1005, N'Объём', N'200 мл')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1005, N'Срок годности', N'5 лет')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1006, N'Высота упаковки', N'69 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1006, N'Длина упаковки', N'28 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1006, N'Ширина упаковки', N'60 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1007, N'Высота упаковки', N'28 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1007, N'Длина упаковки', N'10 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1007, N'Размер', N'40')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1007, N'Ширина упаковки', N'10 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1008, N'Покрой', N'прямой')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1008, N'Размер', N'52')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1008, N'Состав', N'Полиэстер')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1009, N'Высота упаковки', N'21,5 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1009, N'Длина упаковки', N'1,5 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1009, N'Количество страниц', N'576 шт')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1009, N'Ширина упаковки', N'14,5 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1010, N'Высота упаковки', N'21,5 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1010, N'Длина упаковки', N'1,5 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1010, N'Количество страниц', N'384 шт')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1010, N'Ширина упаковки', N'14,5 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1011, N'Высота упаковки', N'26 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1011, N'Количество страниц', N'512 шт')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1011, N'Ширина упаковки', N'20 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1012, N'Высота упаковки', N'26 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1012, N'Количество страниц', N'458 шт')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1012, N'Ширина упаковки', N'20 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1013, N'Высота упаковки', N'26 см')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1013, N'Количество страниц', N'384 шт')
INSERT [dbo].[products_parameters] ([product_id], [parameter_title], [parameter_value]) VALUES (1013, N'Ширина упаковки', N'20 см')
GO
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (1, 1, CAST(N'2022-01-01T00:00:00.000' AS DateTime), 5)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (1, 1, CAST(N'2022-02-01T00:00:00.000' AS DateTime), 30)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (1, 2, CAST(N'2022-01-03T00:00:00.000' AS DateTime), 234)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (2, 1, CAST(N'2022-01-01T00:00:00.000' AS DateTime), 210)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (2, 1, CAST(N'2022-02-01T00:00:00.000' AS DateTime), 48)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (2, 2, CAST(N'2022-01-04T00:00:00.000' AS DateTime), 581)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (3, 1, CAST(N'2022-02-01T00:00:00.000' AS DateTime), 434)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (3, 2, CAST(N'2022-03-01T00:00:00.000' AS DateTime), 352)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (6, 3, CAST(N'2022-08-03T10:14:00.000' AS DateTime), 7)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (8, 3, CAST(N'2022-08-03T17:14:00.000' AS DateTime), 7)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (10, 3, CAST(N'2022-08-02T13:14:00.000' AS DateTime), 2)
INSERT [dbo].[receipt_of_products_to_storages] ([product_id], [storage_id], [received_at], [amount]) VALUES (11, 3, CAST(N'2022-08-02T14:14:00.000' AS DateTime), 16)
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
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (111, 23, 1, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (112, 23, 2, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (113, 23, 3, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (114, 23, 4, 4)
INSERT [dbo].[section_rights] ([id], [section_id], [right_id], [post_id]) VALUES (115, 23, 1, 5)
SET IDENTITY_INSERT [dbo].[section_rights] OFF
GO
SET IDENTITY_INSERT [dbo].[sections] ON 

INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (1, N'Пользователи', NULL, N'users')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (2, N'Доставки', NULL, N'orders')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (3, N'Склады', NULL, N'storages')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (4, N'Товары', NULL, N'products')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (5, N'Поставщики', NULL, N'suppliers')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (6, N'Сотрудники', NULL, N'workers')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (7, N'Общие сведения', 1, N'users_general_info')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (8, N'Средние затраты пользователей', 1, N'users_avg_cost')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (9, N'Список товаров в корзине', 1, N'users_deffered_products')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (10, N'Список доставок', 2, N'order_list')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (11, N'Доставки, готовые к получению', 2, N'orders_ready_to_receive')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (12, N'История заказов', 2, N'order_history')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (13, N'Поступления на склады', 3, N'storages_receipts')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (14, N'Работа сотрудников на складе', 3, N'storages_worker_shifts')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (15, N'Товары на складах', 3, N'storages_product_amount')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (16, N'Общие сведения', 4, N'products_general_info')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (17, N'Отзывы к товару', 4, N'products_reviews')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (18, N'Категории товаров', 4, N'products_categories')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (19, N'История цен', 4, N'products_price_history')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (20, N'Общие сведения', 5, N'suppliers_general_info')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (21, N'Прибыль', 5, N'suppliers_profit')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (22, N'Список сотрудников', 6, N'workers_list')
INSERT [dbo].[sections] ([id], [title], [parent_id], [section_key]) VALUES (23, N'Общие сведения', 3, N'storages_general_info')
SET IDENTITY_INSERT [dbo].[sections] OFF
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
INSERT [dbo].[suppliers] ([id], [title]) VALUES (4, N'Notebook Nook')
INSERT [dbo].[suppliers] ([id], [title]) VALUES (5, N'Paper Lane')
INSERT [dbo].[suppliers] ([id], [title]) VALUES (6, N'Play Master')
INSERT [dbo].[suppliers] ([id], [title]) VALUES (7, N'Pretty Woman')
INSERT [dbo].[suppliers] ([id], [title]) VALUES (8, N'L''oreal')
INSERT [dbo].[suppliers] ([id], [title]) VALUES (9, N'Питер')
INSERT [dbo].[suppliers] ([id], [title]) VALUES (10, N'Драйв')
INSERT [dbo].[suppliers] ([id], [title]) VALUES (11, N'Спортмастер')
SET IDENTITY_INSERT [dbo].[suppliers] OFF
GO
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [order_code], [is_male], [country_id]) VALUES (1, N'Михаил', N'Бутуров', N'Сергеевич', N'8-953-624-01-22', CAST(N'2003-11-10T00:00:00.000' AS DateTime), N'butur@mm.ru', 142, 1, 2)
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [order_code], [is_male], [country_id]) VALUES (3, N'Полина', N'Иренко', N'Сергеевна', N'8-973-714-04-62', CAST(N'2001-01-30T00:00:00.000' AS DateTime), N'pppm@Il.ru', 772, 0, 2)
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [order_code], [is_male], [country_id]) VALUES (4, N'Петр', N'Петров', N'Ильич', N'8-953-744-04-72', CAST(N'2002-02-11T00:00:00.000' AS DateTime), N'petrbm@Il.ru', 792, 1, 2)
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [order_code], [is_male], [country_id]) VALUES (5, N'Марк', N'Дмитренко', N'Игоревич', N'8-953-684-75-57', CAST(N'2012-06-30T00:00:00.000' AS DateTime), N'markmarkmark@m.ru', 326, 1, 2)
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [order_code], [is_male], [country_id]) VALUES (6, N'Анастасия', N'Муранова', N'Алексеевна', N'8-900-532-44-71', CAST(N'2001-09-07T00:00:00.000' AS DateTime), N'nastlastcast@nast.ru', 454, 0, 1)
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [order_code], [is_male], [country_id]) VALUES (7, N'Соня', N'Дедова', N'Владимировна', N'8-921-996-32-11', CAST(N'1996-01-17T00:00:00.000' AS DateTime), N'sonyaddos@vlad.ru', 142, 0, 2)
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [order_code], [is_male], [country_id]) VALUES (11, N'Даниил', N'Петров', N'Романович', N'+7(900)506-69-88', CAST(N'1990-01-01T00:00:00.000' AS DateTime), N'johnsmith@email.com', 234, 1, 2)
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [order_code], [is_male], [country_id]) VALUES (12, N' Глеб', N'Воробьев', N' Иванович', N'+7(900)988-21-20', CAST(N'1995-05-15T00:00:00.000' AS DateTime), N'janedoe@email.com', 503, 1, 2)
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [order_code], [is_male], [country_id]) VALUES (13, N'Александр', N'Егоров ', N' Вячеславович', N'+7(900)592-14-97', CAST(N'1985-01-09T00:00:00.000' AS DateTime), N'bobjohnson@email.com', 426, 1, 2)
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [order_code], [is_male], [country_id]) VALUES (14, N'Дмитрий ', N'Волков ', N'Анатольевич', N'+7(953)425-43-42', CAST(N'2000-12-25T00:00:00.000' AS DateTime), N'samanthawilliams@email.com', 105, 1, 2)
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [order_code], [is_male], [country_id]) VALUES (15, N' Макар ', N'Филатов', N'Олегович', N'+7(953)833-96-39', CAST(N'1988-04-07T00:00:00.000' AS DateTime), N'michaeljones@email.com', 896, 1, 2)
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [order_code], [is_male], [country_id]) VALUES (16, N' Стефания', N'Герасимова', N' Данииловна', N'+7(953)176-05-30', CAST(N'1993-02-02T00:00:00.000' AS DateTime), N'emilybrown@email.com', 401, 0, 2)
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [order_code], [is_male], [country_id]) VALUES (17, N' Амина', N'Ушакова', N' Тиграновна', N'+7(953)658-25-32', CAST(N'1995-06-22T00:00:00.000' AS DateTime), N'jacobmiller@email.com', 212, 0, 2)
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [order_code], [is_male], [country_id]) VALUES (18, N' Полина ', N'Яковлева', N'Семёновна', N'+7(953)584-93-65', CAST(N'1990-03-08T00:00:00.000' AS DateTime), N'ashleymoore@email.com', 961, 0, 2)
INSERT [dbo].[users] ([id], [firstname], [lastname], [patronymic], [phone_number], [birthday], [email], [order_code], [is_male], [country_id]) VALUES (19, N'Виктория ', N'Пастухова ', N'Кирилловна', N'+7(953)080-62-04', CAST(N'1994-11-11T00:00:00.000' AS DateTime), N'brittanyanderson@email.com', 947, 0, 2)
SET IDENTITY_INSERT [dbo].[users] OFF
GO
SET IDENTITY_INSERT [dbo].[workers] ON 

INSERT [dbo].[workers] ([id], [lastname], [firstname], [patronymic], [phone_number], [date_of_birthday], [post_id], [worker_password], [is_male], [worker_login]) VALUES (1, N'Подхомутова', N'Кристина', N'Ивановна', N'8-953-624-78-41', CAST(N'1992-03-23T00:00:00.000' AS DateTime), 1, 0x0FFE1ABD1A08215353C233D6E009613E95EEC4253832A761AF28FF37AC5A150C, 0, N'podki')
INSERT [dbo].[workers] ([id], [lastname], [firstname], [patronymic], [phone_number], [date_of_birthday], [post_id], [worker_password], [is_male], [worker_login]) VALUES (2, N'Олялин', N'Кирилл', N'Валентинович', N'8-936-524-11-26', CAST(N'1988-12-09T00:00:00.000' AS DateTime), 2, 0x65E84BE33532FB784C48129675F9EFF3A682B27168C0EA744B2CF58EE02337C5, 1, N'olkv')
INSERT [dbo].[workers] ([id], [lastname], [firstname], [patronymic], [phone_number], [date_of_birthday], [post_id], [worker_password], [is_male], [worker_login]) VALUES (3, N'Замуленко', N'Сидр', N'Егорович', N'8-811-926-65-84', CAST(N'1999-11-14T00:00:00.000' AS DateTime), 1, 0x65E84BE33532FB784C48129675F9EFF3A682B27168C0EA744B2CF58EE02337C5, 1, N'zamse')
INSERT [dbo].[workers] ([id], [lastname], [firstname], [patronymic], [phone_number], [date_of_birthday], [post_id], [worker_password], [is_male], [worker_login]) VALUES (4, N'Галкин', N'Егор', N'Иванович', N'+7(921)620-30-55', CAST(N'1998-02-16T00:00:00.000' AS DateTime), 2, 0x65E84BE33532FB784C48129675F9EFF3A682B27168C0EA744B2CF58EE02337C5, 1, N'galei')
INSERT [dbo].[workers] ([id], [lastname], [firstname], [patronymic], [phone_number], [date_of_birthday], [post_id], [worker_password], [is_male], [worker_login]) VALUES (5, N'Митрофанов', N'Пётр', N'Михайлович', N'+7(921)508-60-19', CAST(N'1994-07-07T00:00:00.000' AS DateTime), 5, 0x65E84BE33532FB784C48129675F9EFF3A682B27168C0EA744B2CF58EE02337C5, 1, N'mipm')
INSERT [dbo].[workers] ([id], [lastname], [firstname], [patronymic], [phone_number], [date_of_birthday], [post_id], [worker_password], [is_male], [worker_login]) VALUES (6, N'Быков', N'Максим', N'Артёмович', N'+7(921)025-43-46', CAST(N'2000-06-12T00:00:00.000' AS DateTime), 5, 0x65E84BE33532FB784C48129675F9EFF3A682B27168C0EA744B2CF58EE02337C5, 1, N'bikma')
INSERT [dbo].[workers] ([id], [lastname], [firstname], [patronymic], [phone_number], [date_of_birthday], [post_id], [worker_password], [is_male], [worker_login]) VALUES (7, N'Артемов', N'Михаил', N'Артурович', N'+7(921)410-34-76', CAST(N'2001-03-07T00:00:00.000' AS DateTime), 2, 0x65E84BE33532FB784C48129675F9EFF3A682B27168C0EA744B2CF58EE02337C5, 1, N'arma')
INSERT [dbo].[workers] ([id], [lastname], [firstname], [patronymic], [phone_number], [date_of_birthday], [post_id], [worker_password], [is_male], [worker_login]) VALUES (8, N'Муравьев', N'Дмитрий', N'Даниилович', N'+7(921)064-02-08', CAST(N'1998-12-03T00:00:00.000' AS DateTime), 4, 0x2D7B5A2A0B6485D6EA8996622AB8EB9007672FD22A6E6D655B4C66F37ED70FBD, 1, N'mudd')
INSERT [dbo].[workers] ([id], [lastname], [firstname], [patronymic], [phone_number], [date_of_birthday], [post_id], [worker_password], [is_male], [worker_login]) VALUES (9, N'Ершова', N'Александра', N'Романовна', N'+7(921)494-71-54', CAST(N'1998-02-16T00:00:00.000' AS DateTime), 1, 0xCA00FCCFB408989EDDC401062C4D1219A6ACEB6B9B55412357F1790862E8F178, 0, N'erar')
INSERT [dbo].[workers] ([id], [lastname], [firstname], [patronymic], [phone_number], [date_of_birthday], [post_id], [worker_password], [is_male], [worker_login]) VALUES (10, N'Комарова', N'Алина', N'Марковна', N'+7(921)958-45-27', CAST(N'1994-07-07T00:00:00.000' AS DateTime), 4, 0x65E84BE33532FB784C48129675F9EFF3A682B27168C0EA744B2CF58EE02337C5, 0, N'komam')
INSERT [dbo].[workers] ([id], [lastname], [firstname], [patronymic], [phone_number], [date_of_birthday], [post_id], [worker_password], [is_male], [worker_login]) VALUES (11, N'Розанова', N'Александра', N'Артёмовна', N'+7(921)655-29-97', CAST(N'2000-06-12T00:00:00.000' AS DateTime), 5, 0x65E84BE33532FB784C48129675F9EFF3A682B27168C0EA744B2CF58EE02337C5, 0, N'rozaa')
INSERT [dbo].[workers] ([id], [lastname], [firstname], [patronymic], [phone_number], [date_of_birthday], [post_id], [worker_password], [is_male], [worker_login]) VALUES (12, N'Мартынова', N'Таисия', N'Арсентьевна', N'+7(921)798-61-71', CAST(N'2001-03-07T00:00:00.000' AS DateTime), 2, 0x65E84BE33532FB784C48129675F9EFF3A682B27168C0EA744B2CF58EE02337C5, 0, N'mata')
SET IDENTITY_INSERT [dbo].[workers] OFF
GO
INSERT [dbo].[workers_in_orders] ([order_id], [status_changed_at], [worker_id]) VALUES (1, CAST(N'2022-07-09T10:13:14.000' AS DateTime), 1)
INSERT [dbo].[workers_in_orders] ([order_id], [status_changed_at], [worker_id]) VALUES (1, CAST(N'2022-08-08T10:18:15.000' AS DateTime), 3)
INSERT [dbo].[workers_in_orders] ([order_id], [status_changed_at], [worker_id]) VALUES (1, CAST(N'2022-08-08T10:19:14.000' AS DateTime), 3)
GO
/****** Object:  Index [uc_user_id_product_id_in_def_products]    Script Date: 23.02.2023 16:20:48 ******/
ALTER TABLE [dbo].[deferred_products] ADD  CONSTRAINT [uc_user_id_product_id_in_def_products] UNIQUE NONCLUSTERED 
(
	[user_id] ASC,
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_order_history]    Script Date: 23.02.2023 16:20:48 ******/
CREATE NONCLUSTERED INDEX [IX_order_history] ON [dbo].[order_history]
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [u_sections_section_key]    Script Date: 23.02.2023 16:20:48 ******/
ALTER TABLE [dbo].[sections] ADD  CONSTRAINT [u_sections_section_key] UNIQUE NONCLUSTERED 
(
	[section_key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__table_fi__95FD4F3929D12456]    Script Date: 23.02.2023 16:20:48 ******/
ALTER TABLE [dbo].[table_files] ADD UNIQUE NONCLUSTERED 
(
	[table_name] ASC,
	[record_id] ASC,
	[f_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__table_fi__95FD4F392C75CBB1]    Script Date: 23.02.2023 16:20:48 ******/
ALTER TABLE [dbo].[table_files] ADD UNIQUE NONCLUSTERED 
(
	[table_name] ASC,
	[record_id] ASC,
	[f_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [U_workers_login]    Script Date: 23.02.2023 16:20:48 ******/
ALTER TABLE [dbo].[workers] ADD  CONSTRAINT [U_workers_login] UNIQUE NONCLUSTERED 
(
	[worker_login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[orders] ADD  CONSTRAINT [DF_orders_created_at]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[orders] ADD  CONSTRAINT [DF_orders_estimated_delivery_at]  DEFAULT (dateadd(week,(2),getdate())) FOR [estimated_delivery_at]
GO
ALTER TABLE [dbo].[products] ADD  CONSTRAINT [DF_products_supplier_percent]  DEFAULT ((0)) FOR [supplier_percent]
GO
ALTER TABLE [dbo].[reviews] ADD  CONSTRAINT [DF_reviews_created_at]  DEFAULT (sysdatetime()) FOR [created_at]
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
ALTER TABLE [dbo].[section_rights]  WITH CHECK ADD FOREIGN KEY([post_id])
REFERENCES [dbo].[posts] ([id])
GO
ALTER TABLE [dbo].[section_rights]  WITH CHECK ADD FOREIGN KEY([right_id])
REFERENCES [dbo].[rights] ([id])
GO
ALTER TABLE [dbo].[section_rights]  WITH CHECK ADD FOREIGN KEY([right_id])
REFERENCES [dbo].[rights] ([id])
GO
ALTER TABLE [dbo].[section_rights]  WITH CHECK ADD FOREIGN KEY([section_id])
REFERENCES [dbo].[sections] ([id])
GO
ALTER TABLE [dbo].[section_rights]  WITH CHECK ADD FOREIGN KEY([section_id])
REFERENCES [dbo].[sections] ([id])
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
ALTER TABLE [dbo].[table_files]  WITH CHECK ADD FOREIGN KEY([f_id])
REFERENCES [dbo].[files] ([id])
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD FOREIGN KEY([country_id])
REFERENCES [dbo].[countries] ([id])
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD FOREIGN KEY([country_id])
REFERENCES [dbo].[countries] ([id])
GO
ALTER TABLE [dbo].[workers]  WITH CHECK ADD  CONSTRAINT [FK_workers_posts] FOREIGN KEY([post_id])
REFERENCES [dbo].[posts] ([id])
GO
ALTER TABLE [dbo].[workers] CHECK CONSTRAINT [FK_workers_posts]
GO
ALTER TABLE [dbo].[workers_in_orders]  WITH CHECK ADD  CONSTRAINT [FK__workers_i__worke__656C112C] FOREIGN KEY([worker_id])
REFERENCES [dbo].[workers] ([id])
GO
ALTER TABLE [dbo].[workers_in_orders] CHECK CONSTRAINT [FK__workers_i__worke__656C112C]
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
