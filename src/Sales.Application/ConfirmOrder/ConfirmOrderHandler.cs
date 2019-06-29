using System.IO;
using System.Xml.Serialization;
using PhotoStock.BusInterfaces;
using Sales.Domain;
using Sales.Domain.Discount;
using Sales.Domain.Product;
using Sales.Domain.Purchase;

namespace Sales.Application.ConfirmOrder
{
  class ConfirmOrderHandler : ICommandHandler<ConfirmOrderCommand>
  {
    private readonly IOrderRepository _repository;
    private readonly IProductRepository _productRepository;
    private readonly IDiscountCalculator _discountCalculator;
    private readonly IClientRepository _clientRepository;
    private readonly IPurchaseFactory _purchaseFactory;
    private readonly IPurchaseRepository _purchaseRepository;

    public ConfirmOrderHandler(IOrderRepository repository, IProductRepository productRepository, IDiscountCalculator discountCalculator, IClientRepository clientRepository, IPurchaseFactory purchaseFactory, IPurchaseRepository purchaseRepository)
    {
      _repository = repository;
      _productRepository = productRepository;
      _discountCalculator = discountCalculator;
      _clientRepository = clientRepository;
      _purchaseFactory = purchaseFactory;
      _purchaseRepository = purchaseRepository;
    }

    public void Handle(ConfirmOrderCommand command)
    {
      Order o = _repository.Get(command.OrderId);
      o.Confirm(command.SeenOffer, _discountCalculator, _productRepository);

      Client client = _clientRepository.Get(command.SeenOffer.ClientId);
      client.Charge(command.SeenOffer.TotalCost);
      _clientRepository.Save(client);

      Purchase purchase = _purchaseFactory.Create(command.OrderId, command.SeenOffer);
      _purchaseRepository.Save(purchase);     
    }
  }
}