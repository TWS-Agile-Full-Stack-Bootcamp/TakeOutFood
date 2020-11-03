namespace TakeOutFood
{
	internal class OrderItem : Item
	{
		public OrderItem(Item item, int quantity) : base(item.Id, item.Name, item.Price)
		{
			this.Quantity = quantity;
		}

		public int Quantity { get; private set; }
		public double Subtotal
		{
			get
			{
				return Price * Quantity;
			}
		}
	}
}