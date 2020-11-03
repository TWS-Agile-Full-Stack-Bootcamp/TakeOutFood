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

            var orderItems = GenerateOrderItems(inputs);

            StringBuilder sb = new StringBuilder();
            sb.Append("============= Order details =============\n");
            RenderOrderItems(orderItems, sb);
            sb.Append("-----------------------------------\n");
            RenderTotal(orderItems, sb);
            sb.Append("===================================");
            return sb.ToString();
        }

        private static void RenderTotal(List<OrderItem> orderItems, StringBuilder sb)
        {
            var total = orderItems.Sum(oi => oi.Quantity * oi.Price);
            sb.Append(string.Format($"Total：{total} yuan\n"));
        }

        private static void RenderOrderItems(List<OrderItem> orderItems, StringBuilder sb)
        {
            orderItems.ForEach(oi =>
            {
                sb.Append(string.Format($"{oi.Name} x {oi.Quantity} = {oi.Quantity * oi.Price} yuan\n"));
            });
        }

        private static double CalculateTotal(List<OrderItem> orderItems)
        {
            return orderItems.Sum(oi => oi.Quantity * oi.Price);
        }

        private List<OrderItem> GenerateOrderItems(List<string> inputs)
        {
            return inputs.Select(input =>
            {
                var splitStr = input.Split('x');
                var id = splitStr[0];
                var quantity = Convert.ToInt32(splitStr[1]);
                var matchedItem = this.itemRepository.FindAll().First(i => i.Id == id.Trim());
                var name = matchedItem.Name;
                var price = matchedItem.Price;

                return new OrderItem(id, quantity, name, price);
            }).ToList();
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