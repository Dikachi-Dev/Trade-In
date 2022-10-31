using System.ComponentModel.DataAnnotations;

namespace TradeIn.Models
{
    public class EstimateRef
    {
        public int Id { get; set; }

        //[Required]
        //public string? title { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? PhoneNumber { get; set; }

        public string? Refnumber { get; set; }
        public DateTime Estimatedate { get; set; }
        public string? Category { get; set; }

        [Required]
        public int Brand { get; set; }

        [Required]
        public int Model { get; set; }

        [Required]
        public string? OtherItem { get; set; }

        public int Generation { get; set; }

        [Required]
        public string Condition { get; set; } = default!;

        public double Amount { get; set; }
    }
}
