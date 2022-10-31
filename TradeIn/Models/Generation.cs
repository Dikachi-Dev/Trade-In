namespace TradeIn.Models
{
    public class Generation
    {

        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public int ModelId { get; set; }
        public Model Model { get; set; } = default!;
        
       
    }
}
