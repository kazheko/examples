using System.Collections.Generic;

namespace Examples.AuctionApi.Models
{
    public class Currency
    {
        public Currency(string currency)
        {
            Code = CurrencyCodesBySymbol[currency[0]];
            Value = double.Parse(currency.Substring(1));
        }

        public static IDictionary<char, string> CurrencyCodesBySymbol = new Dictionary<char, string>
        {
            {'€', "EUR"},
            {'$', "USD"},
        };
        public string Code { get; private set; }
        public double Value { get; private set; }
    }
}