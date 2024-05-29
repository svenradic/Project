using Microsoft.AspNetCore.Mvc;

namespace Stocks.WebAPI
{
    public static class StockRepository
    {
        private static ICollection<Stock> Stocks = new List<Stock>()
        {
            new Stock{ Id = 1, Symbol = "AAPL", CompanyName = "Apple Inc", CurrentPrice = 189.99, MarketCap = 32143154325 },
            new Stock{ Id = 2, Symbol = "MSFT", CompanyName = "Microsoft", CurrentPrice = 430.32, MarketCap = 51135545498 },

        };

        public static ICollection<Stock> GetAll()
        {
            return Stocks;
        }

        public static Stock? Get(int id)
        {
            var stock = Stocks.FirstOrDefault(s => s.Id == id);
            return stock;
        }

        public static Stock? Add(Stock stock)
        {
            int newId = Stocks.Any() ? Stocks.Count() + 1 : 1;
            stock.Id = newId;
            Stocks.Add(stock);
            return stock;
        }

        public static Stock? Update(Stock stock, int id)
        {
            var stockToUpdate = Stocks.FirstOrDefault(s => s.Id == id);
            if (stockToUpdate != null)
            {
                stockToUpdate.Symbol = stock.Symbol;
                stockToUpdate.CompanyName = stock.CompanyName;
                stockToUpdate.MarketCap = stock.MarketCap;
                stockToUpdate.CurrentPrice = stock.CurrentPrice;
            }
            return stockToUpdate;
        }
        public static int Delete(int id) 
        {
            var stockToDelete = Stocks.FirstOrDefault(s => s.Id ==id);
            if (stockToDelete != null)
            {
                Stocks.Remove(stockToDelete);
                return 1;
            }
            return 0;
        }

    }
}
