using System.Collections.Generic;

namespace TradeIn.Models
{
    public class Model
    {
        public Model()
        {
           
            this.Generations = new List<Generation>();
        }
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public int BrandId { get; set; }

        public Brand Brand { get; set; } = default!;
        
        public List<Generation> Generations { get; set; } = default!;
    }
}
