namespace Stocks.WebAPI
{
    public class Trader
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Stock>? Stocks { get; set; }
    }
}
