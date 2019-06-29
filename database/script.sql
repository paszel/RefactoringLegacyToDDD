USE [master]
GO
/****** Object:  Database [PhotoStock]    Script Date: 17.06.2019 12:21:22 ******/
CREATE DATABASE [PhotoStock]
go

USE [PhotoStock]
GO

/****** Object:  Table [dbo].[Client]    Script Date: 17.06.2019 12:21:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[id] [varchar](128) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[credit] [money] NOT NULL,
	[invoiceType] [int] NOT NULL,
	[creditLeft] [money] NOT NULL,
	[email] [varchar](128) NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 17.06.2019 12:21:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[orderId] [varchar](40) NOT NULL,
	[net_amount] [money] NOT NULL,
	[tax_amount] [money] NOT NULL,
	[number] [varchar](50) NOT NULL,
	[net_currency] [nvarchar](255) NOT NULL,
	[tax_currency] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceItem]    Script Date: 17.06.2019 12:21:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceItem](
	[invoiceId] [varchar](40) NOT NULL,
	[productName] [varchar](128) NOT NULL,
	[net_amount] [money] NOT NULL,
	[tax_amount] [money] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[net_currency] [nvarchar](255) NOT NULL,
	[tax_currency] [nvarchar](255) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceType]    Script Date: 17.06.2019 12:21:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceType](
	[id] [int] NOT NULL,
	[description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_InvoiceTypes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LastInvoiceNumber]    Script Date: 17.06.2019 12:21:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LastInvoiceNumber](
	[number] [int] NOT NULL,
	[invoiceType] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 17.06.2019 12:21:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[id] [varchar](40) NOT NULL,
	[clientId] [varchar](128) NOT NULL,
	[status] [int] NULL,
	[discountPrice] [money] NOT NULL,
	[number] [varchar](128) NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 17.06.2019 12:21:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[orderId] [varchar](40) NOT NULL,
	[productId] [varchar](40) NOT NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC,
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderSnapshot]    Script Date: 17.06.2019 12:21:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderSnapshot](
	[orderId] [varchar](40) NOT NULL,
	[orderData] [xml] NOT NULL,
 CONSTRAINT [PK_Ordernapshots] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 17.06.2019 12:21:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[id] [int] NOT NULL,
	[description] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Ordertatus] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 17.06.2019 12:21:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[id] [varchar](40) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[price] [money] NOT NULL,
	[aviable] [bit] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shipment]    Script Date: 17.06.2019 12:21:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shipment](
	[orderId] [varchar](40) NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_Shipements] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VersionInfo]    Script Date: 17.06.2019 12:21:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VersionInfo](
	[Version] [bigint] NOT NULL,
	[AppliedOn] [datetime] NULL,
	[Description] [nvarchar](1024) NULL
) ON [PRIMARY]
GO
/****** Object:  Index [UC_Version]    Script Date: 17.06.2019 12:21:22 ******/
CREATE UNIQUE CLUSTERED INDEX [UC_Version] ON [dbo].[VersionInfo]
(
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
INSERT [dbo].[Client] ([id], [name], [credit], [invoiceType], [creditLeft], [email]) VALUES (N'aaa111', N'Ala makotowska', 100.0000, 1, 100.0000, N'ala.makotowska@example.com')
INSERT [dbo].[InvoiceType] ([id], [description]) VALUES (1, N'Polish')
INSERT [dbo].[InvoiceType] ([id], [description]) VALUES (2, N'EU')
INSERT [dbo].[InvoiceType] ([id], [description]) VALUES (3, N'Other')
INSERT [dbo].[LastInvoiceNumber] ([number], [invoiceType]) VALUES (0, 1)
INSERT [dbo].[LastInvoiceNumber] ([number], [invoiceType]) VALUES (0, 2)
INSERT [dbo].[LastInvoiceNumber] ([number], [invoiceType]) VALUES (0, 3)
INSERT [dbo].[Order] ([id], [clientId], [status], [discountPrice], [number]) VALUES (N'12', N'client123', 1, 0.0000, N'123')
SET IDENTITY_INSERT [dbo].[OrderItem] ON 

INSERT [dbo].[OrderItem] ([id], [orderId], [productId]) VALUES (1, N'12', N'P1')
INSERT [dbo].[OrderItem] ([id], [orderId], [productId]) VALUES (2, N'12', N'P2')
SET IDENTITY_INSERT [dbo].[OrderItem] OFF
INSERT [dbo].[OrderStatus] ([id], [description]) VALUES (1, N'New')
INSERT [dbo].[OrderStatus] ([id], [description]) VALUES (2, N'Confirmed')
INSERT [dbo].[OrderStatus] ([id], [description]) VALUES (3, N'Paid')
INSERT [dbo].[Product] ([id], [name], [price], [aviable]) VALUES (N'P1', N'Rysunek1', 10.0000, 1)
INSERT [dbo].[Product] ([id], [name], [price], [aviable]) VALUES (N'P2', N'Rysunek2', 11.0000, 1)
INSERT [dbo].[Product] ([id], [name], [price], [aviable]) VALUES (N'P3', N'Rysunek3', 4.0000, 1)
INSERT [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201901010810, CAST(N'2019-06-16T21:33:30.000' AS DateTime), N'_1_InitMigration')
INSERT [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201901011000, CAST(N'2019-06-16T21:33:30.000' AS DateTime), N'TestSeeding')
INSERT [dbo].[VersionInfo] ([Version], [AppliedOn], [Description]) VALUES (201902010810, CAST(N'2019-06-16T21:33:30.000' AS DateTime), N'_2_RowVersion')
ALTER TABLE [dbo].[Client] ADD  CONSTRAINT [DF_Client_amountLeft]  DEFAULT ((0)) FOR [credit]
GO
ALTER TABLE [dbo].[Client] ADD  CONSTRAINT [DF_Client_creditLeft]  DEFAULT ((0)) FOR [creditLeft]
GO
ALTER TABLE [dbo].[Invoice] ADD  CONSTRAINT [DF_Invoice_net_currency]  DEFAULT (N'PLN') FOR [net_currency]
GO
ALTER TABLE [dbo].[Invoice] ADD  CONSTRAINT [DF_Invoice_tax_currency]  DEFAULT (N'PLN') FOR [tax_currency]
GO
ALTER TABLE [dbo].[InvoiceItem] ADD  CONSTRAINT [DF_InvoiceItem_net_currency]  DEFAULT (N'PLN') FOR [net_currency]
GO
ALTER TABLE [dbo].[InvoiceItem] ADD  CONSTRAINT [DF_InvoiceItem_tax_currency]  DEFAULT (N'PLN') FOR [tax_currency]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_discountPrice]  DEFAULT ((0)) FOR [discountPrice]
GO
ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_aviable]  DEFAULT ((1)) FOR [aviable]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Clients] FOREIGN KEY([id])
REFERENCES [dbo].[Client] ([id])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Clients]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_InvoiceTypes] FOREIGN KEY([invoiceType])
REFERENCES [dbo].[InvoiceType] ([id])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_InvoiceTypes]
GO
ALTER TABLE [dbo].[InvoiceItem]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceItem_Invoice] FOREIGN KEY([invoiceId])
REFERENCES [dbo].[Invoice] ([orderId])
GO
ALTER TABLE [dbo].[InvoiceItem] CHECK CONSTRAINT [FK_InvoiceItem_Invoice]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Ordertatus] FOREIGN KEY([status])
REFERENCES [dbo].[OrderStatus] ([id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Ordertatus]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Order] FOREIGN KEY([orderId])
REFERENCES [dbo].[Order] ([id])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Order]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Product] FOREIGN KEY([productId])
REFERENCES [dbo].[Product] ([id])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Product]
GO
ALTER TABLE [dbo].[OrderSnapshot]  WITH CHECK ADD  CONSTRAINT [FK_Ordernapshot_Order] FOREIGN KEY([orderId])
REFERENCES [dbo].[Order] ([id])
GO
ALTER TABLE [dbo].[OrderSnapshot] CHECK CONSTRAINT [FK_Ordernapshot_Order]
GO
ALTER TABLE [dbo].[OrderStatus]  WITH CHECK ADD  CONSTRAINT [FK_Ordertatus_Ordertatus] FOREIGN KEY([id])
REFERENCES [dbo].[OrderStatus] ([id])
GO
ALTER TABLE [dbo].[OrderStatus] CHECK CONSTRAINT [FK_Ordertatus_Ordertatus]
GO
ALTER TABLE [dbo].[Shipment]  WITH CHECK ADD  CONSTRAINT [FK_Shipements_Order] FOREIGN KEY([orderId])
REFERENCES [dbo].[Order] ([id])
GO
ALTER TABLE [dbo].[Shipment] CHECK CONSTRAINT [FK_Shipements_Order]
GO
USE [master]
GO
ALTER DATABASE [PhotoStock] SET  READ_WRITE 
GO
