﻿using Autofac;
using Sales.Application;
using Sales.Application.AddProduct;
using Sales.Application.CalculateOffer;
using Sales.Application.CreateOrder;
using Sales.Domain;
using Sales.Domain.Discount;
using Sales.Domain.Offer;

namespace Sales.Infrastructure
{
  public class AutofacSalesModule : Module
  {
    private readonly string _connectionString;

    public AutofacSalesModule(string connectionString)
    {
      _connectionString = connectionString;
    }

    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<NumberGenerator>().AsImplementedInterfaces();
      builder.RegisterType<DiscountCalculator>().AsImplementedInterfaces();
      builder.RegisterType<DateTimeProvider>().AsImplementedInterfaces();
      builder.RegisterType<CalculateOfferHandler>().AsImplementedInterfaces();
      builder.RegisterType<ConfigurationAdapter>().AsImplementedInterfaces();
      builder.RegisterType<ProductRepository>().AsImplementedInterfaces();
      builder.RegisterType<DiscountSpecificationFactory>().AsImplementedInterfaces();

      builder.RegisterType<CreateOrderHandler>().AsImplementedInterfaces();
      builder.RegisterType<AddProductHandler>().AsImplementedInterfaces();
      builder.RegisterType<CalculateOfferHandler>().AsImplementedInterfaces();
      builder.RegisterType<OrderFactory>().AsImplementedInterfaces();
      builder.RegisterType<OrderRepository>().AsImplementedInterfaces();
      builder.RegisterType<EventPublisherAdapter>().AsImplementedInterfaces();
      builder.RegisterType<ConfirmOrderHandler>().AsImplementedInterfaces();
      builder.RegisterType<OrderContext>().WithParameter("connectionString", _connectionString);
    }
  }
}
