using FluentMigrator;

namespace PhotoStock.Migrations
{
  [Migration(201901010810)]
  public class _1_InitMigration : ForwardOnlyMigration
  {
    public override void Up()
    {
      Execute.Sql(@"
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
/****** Object:  Table [dbo].[Invoice]    Script Date: 15.06.2019 21:53:08 ******/
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
/****** Object:  Table [dbo].[InvoiceItem]    Script Date: 15.06.2019 21:53:08 ******/
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
/****** Object:  Table [dbo].[InvoiceType]    Script Date: 15.06.2019 21:53:08 ******/
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
/****** Object:  Table [dbo].[LastInvoiceNumber]    Script Date: 15.06.2019 21:53:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LastInvoiceNumber](
	[number] [int] NOT NULL,
	[invoiceType] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 15.06.2019 21:53:08 ******/
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
/****** Object:  Table [dbo].[OrderItem]    Script Date: 15.06.2019 21:53:08 ******/
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
/****** Object:  Table [dbo].[OrderSnapshot]    Script Date: 15.06.2019 21:53:08 ******/
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
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 15.06.2019 21:53:08 ******/
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
/****** Object:  Table [dbo].[Product]    Script Date: 15.06.2019 21:53:08 ******/
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
/****** Object:  Table [dbo].[Shipment]    Script Date: 15.06.2019 21:53:08 ******/
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
");

      Insert.IntoTable("InvoiceType").Row(new { id = 1, description = "Polish" });
      Insert.IntoTable("InvoiceType").Row(new { id = 2, description = "EU" });
      Insert.IntoTable("InvoiceType").Row(new { id = 3, description = "Other" });

      Insert.IntoTable("OrderStatus").Row(new { id = 1, description = "New" });
      Insert.IntoTable("OrderStatus").Row(new { id = 2, description = "Confirmed" });
      Insert.IntoTable("OrderStatus").Row(new { id = 3, description = "Paid" });

      Insert.IntoTable("LastInvoiceNumber").Row(new { invoiceType = 1, number = 0 });
      Insert.IntoTable("LastInvoiceNumber").Row(new { invoiceType = 2, number = 0 });
      Insert.IntoTable("LastInvoiceNumber").Row(new { invoiceType = 3, number = 0 });

    }
  }
}
