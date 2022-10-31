namespace TradeIn.Models
{
    public class UserInfo
    {
        public Guid Id { get; set; }

        //public string? title { get; set; }
        //public string? FirstName { get; set; }
        //public string? LastName { get; set; }
        public string? Email { get; set; }

        public string? Paypal { get; set; }

        public string? PhoneNumber { get; set; }
        public string? PostCode { get; set; }
        public string? Address { get; set; }
        public string? State { get; set; }
        public string? County { get; set; }
    }
}
