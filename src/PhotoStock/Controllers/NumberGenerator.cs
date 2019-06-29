using System;
using Microsoft.Extensions.Configuration;
using PhotoStock.Infrastructure;

namespace PhotoStock.Controllers
{
  public class NumberGenerator : INumberGenerator
  {
    private readonly IConfiguration _configuration;
    private readonly IDateTimeProvider _dateTimeProvider;

    public NumberGenerator(IConfiguration configuration, IDateTimeProvider dateTimeProvider)
    {
      _configuration = configuration;
      _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateNumber()
    {
      if (_configuration["Environment"] != "PROD")
      {
        return _configuration["Environment"] + "/Or/" + _dateTimeProvider.Now;
      }

      return "Or/" + _dateTimeProvider.Now;
    }
  }
}