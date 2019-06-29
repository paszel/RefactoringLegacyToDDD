﻿namespace Sales.Application
{
  public interface IOrderService
  {
    void CreateOrder(string orderId, string clientId);
    void AddProduct(string orderId, string productId);
  }
}