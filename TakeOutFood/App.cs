using System.Linq;
using System.Text;
using Microsoft.VisualBasic;

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

            var promotionOrderItems = orderItems.Where(oi =>
                this.salesPromotionRepository.FindAll().Exists(p => p.RelatedItems.Contains(oi.Id))).ToList();

            var promotion = new Promotion(promotionOrderItems);
            StringBuilder sb = new StringBuilder();
            sb.Append("============= Order details =============\n");
            RenderOrderItems(orderItems, sb);
            sb.Append(promotion.RenderPromotion());
            sb.Append("-----------------------------------\n");
            RenderTotal(orderItems, promotion, sb);
            sb.Append("===================================");
            return sb.ToString();
        }

        private static void RenderTotal(List<OrderItem> orderItems, Promotion promotion, StringBuilder sb)
        {
            var total = orderItems.Sum(oi => oi.Quantity * oi.Price);
            total = total - promotion.Saving;
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
                var id = splitStr[0].Trim();
                var quantity = Convert.ToInt32(splitStr[1]);
                var matchedItem = this.itemRepository.FindAll().First(i => i.Id == id);
                var name = matchedItem.Name;
                var price = matchedItem.Price;

                return new OrderItem(id, quantity, name, price);
            }).ToList();
        }
    }
}