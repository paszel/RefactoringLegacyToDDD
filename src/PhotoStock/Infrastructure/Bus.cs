using System;
using System.Collections.Generic;
using Autofac;
using Microsoft.Extensions.Logging;
using Sales.Application;

namespace PhotoStock.Infrastructure
{
  public class Bus : ICommandBus, IEventBus
  {
    private readonly IComponentContext _container;
    private readonly ILogger _logger;

    public Bus(IComponentContext container, ILoggerFactory loggerFactory)
    {
      _container = container;
      _logger = loggerFactory.CreateLogger(GetType());
    }

    public void Send<T>(T command)
    {
      ICommandHandler<T> commandHandler;
      try
      {
        commandHandler = (ICommandHandler<T>)_container.Resolve(typeof(ICommandHandler<T>));
      }
      catch (Exception ex)
      {
        _logger.LogError($"Cannot create handler {typeof(ICommandHandler<T>)}", ex);
        throw;
      }

      commandHandler.Handle(command);
    }

    public void Publish<T>(T @event)
    {

      IEnumerable<IEventHandler<T>> eventHandlers =
        (IEnumerable<IEventHandler<T>>)_container.Resolve(typeof(IEnumerable<IEventHandler<T>>));

      foreach (var eventHandler in eventHandlers)
      {
        eventHandler.Handle(@event);
      }

    }    
  }
}
