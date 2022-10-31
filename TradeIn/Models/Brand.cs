namespace TradeIn.Models
{
    public class Brand
    {
        public Brand()
        {
            this.Models = new List<Model>();
        }
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public List<Model> Models { get; set; }
    }
}
