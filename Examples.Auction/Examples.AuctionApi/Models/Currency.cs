using System.Collections.Generic;
using System.Globalization;

namespace Examples.AuctionApi.Models
{
    public class Currency
    {
        public Currency(string currency)
        {
            Code = CurrencyCodesBySymbol[currency[0]];
            Value = double.Parse(currency.Substring(1), new CultureInfo("en-Us"));
        }

        public static IDictionary<char, string> CurrencyCodesBySymbol = new Dictionary<char, string>
        {
            {'€', "EUR"},
            {'$', "USD"},
        };
        public string Code { get; }
        public double Value { get; }

        public override string ToString()
        {
            return Code + Value;
        }
    }
}