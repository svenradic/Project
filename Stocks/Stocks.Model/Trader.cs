namespace Stocks.Model
{
    public class Trader
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        public ICollection<Stock>? Stocks { get; set; }
    }
}
