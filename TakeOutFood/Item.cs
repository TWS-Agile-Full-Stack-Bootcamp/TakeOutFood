namespace TakeOutFood
{
    public class Item
    {
        public Item(string id, string name, double price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }
    }
}
