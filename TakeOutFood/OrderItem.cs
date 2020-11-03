namespace TakeOutFood
{
    public class OrderItem
    {
        public string Id { get; private set; }
        public int Quantity { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }

        public OrderItem(string id, int quantity, string name, double price)
        {
            this.Id = id;
            this.Quantity = quantity;
            this.Name = name;
            this.Price = price;
        }
    }
}