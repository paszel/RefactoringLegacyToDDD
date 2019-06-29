using System;
using PhotoStock.BusInterfaces;

namespace Processes
{
  public class ProcessOrderConfirmationHandler : ICommandHandler<ProcessOrderConfirmationCommand>
  {
    private readonly ICommandBus _commandBus;

    public ProcessOrderConfirmationHandler(ICommandBus commandBus)
    {
      _commandBus = commandBus;
    }

    public void Handle(ProcessOrderConfirmationCommand command)
    {
      _commandBus.Send(new ConfirmOrderCommand());
    }
  }
}
