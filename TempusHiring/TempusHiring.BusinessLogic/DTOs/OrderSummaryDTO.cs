namespace TempusHiring.BusinessLogic.DTOs
{
    public class OrderSummaryDTO
    {
        public decimal SubTotal { get; set; }
        public decimal Shipping { get; set; }
        public decimal Total { get; set; }
        public int Count { get; set; }
    }
}
