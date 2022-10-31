namespace TradeIn.Models
{
    public class Valuation
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Brand { get; set; } = default!;
        public int Model { get; set; } = default!;
        public int? Generation { get; set; }
        public string Category { get; set; } = default!;
        public string Condition { get; set; } = default!;
    }
}
