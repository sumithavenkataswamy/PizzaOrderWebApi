namespace PizzaOrderWebApi.Models
{
    public class PizzaOrder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int PizzaId { get; set; }
        public string Size { get; set; }
        public string Toppings { get; set; }
        public decimal Price { get; set; }

        public Customer Customer { get; set; }
        public Pizza Pizza { get; set; }
    }
}
