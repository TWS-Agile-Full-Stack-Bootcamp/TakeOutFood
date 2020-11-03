using System.Linq;
using System.Text;

namespace TakeOutFood
{
    using System;
    using System.Collections.Generic;

    public class App
    {
        private IItemRepository itemRepository;
        private ISalesPromotionRepository salesPromotionRepository;

        public App(IItemRepository itemRepository, ISalesPromotionRepository salesPromotionRepository)
        {
            this.itemRepository = itemRepository;
            this.salesPromotionRepository = salesPromotionRepository;
        }

        public string BestCharge(List<string> inputs)
        {
            //TODO: write code here

            var orderItems = inputs.Select(input =>
            {
                var splited = input.Split('x');
                var id = splited[0];
                var quantity = Convert.ToInt32(splited[1]);
                var matchedItem = this.itemRepository.FindAll().First(i => i.Id == id.Trim());
                var name = matchedItem.Name;
                var pricce = matchedItem.Price;

                return new OrderItem(id, quantity, name, pricce);
            }).ToList();

            StringBuilder sb = new StringBuilder();
            sb.Append("============= Order details =============\n");


            orderItems.ForEach(oi =>
            {
                sb.Append(string.Format($"{oi.Name} x {oi.Quantity} = {oi.Quantity * oi.Price} yuan\n"));
            });
            sb.Append("-----------------------------------\n");

            var total = orderItems.Sum(oi => oi.Quantity * oi.Price);

            sb.Append(string.Format($"Total：{total} yuan\n"));
            sb.Append("===================================");
            return sb.ToString();
        }
    }

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