namespace TakeOutFood
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

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
            List<KeyValuePair<Item, int>> itemAndCountPairs = DecodeOrders(inputs);

            return Render(itemAndCountPairs);
        }

        private string Render(List<KeyValuePair<Item, int>> itemAndCountPairs)
        {
            PromotionItem promotionItem = CalculatePromotion(itemAndCountPairs);

            string header = "============= Order details =============\n";
            string orderDetailsText = RenderOrderDetails(itemAndCountPairs);
            string promotionText = promotionItem.Saving != 0 ? RenderPromotion(promotionItem) : String.Empty;
            string totalText = RenderTotal(itemAndCountPairs, promotionItem.Saving);
            string footer = "===================================";

            return header + orderDetailsText + promotionText + totalText + footer;

        }

        private string RenderPromotion(PromotionItem promotionItem)
        {
            StringBuilder promotionText = new StringBuilder("-----------------------------------\nPromotion used:\n");
            promotionText.Append(String.Format("Half price for certain dishes ({0}), saving {1} yuan\n",
                string.Join(", ", promotionItem.PromotionedItemNames), promotionItem.Saving));
            return promotionText.ToString();
        }

        private PromotionItem CalculatePromotion(List<KeyValuePair<Item, int>> itemAndCountPairs)
        {
            PromotionItem promotionItem = new PromotionItem();

            List<SalesPromotion> promotions = salesPromotionRepository.FindAll();
            itemAndCountPairs.ForEach(itemAndCountPair =>
            {
                SalesPromotion promotion = promotions.Find(promotion => promotion.RelatedItems.Contains(itemAndCountPair.Key.Id));
                if (promotion != null)
                {
                    promotionItem.PromotionedItemNames.Add(itemAndCountPair.Key.Name);
                    promotionItem.Saving += (int) (itemAndCountPair.Key.Price * itemAndCountPair.Value * 0.5);
                }
            });

            return promotionItem;
        }

        private string RenderTotal(List<KeyValuePair<Item, int>> itemIdAndCountPairs, int promotion)
        {
            StringBuilder totalText = new StringBuilder("-----------------------------------\n");
            int total = (int)itemIdAndCountPairs.Sum(pair => pair.Key.Price * pair.Value) - promotion;
            totalText.Append(String.Format("Total：{0} yuan\n", total));
            return totalText.ToString();
        }

        private string RenderOrderDetails(List<KeyValuePair<Item, int>> itemAndCountPairs)
        {
            StringBuilder orderDetails = new StringBuilder();
            itemAndCountPairs.ForEach(itemAndCountPair =>
            {
                orderDetails.Append(String.Format("{0} x {1} = {2} yuan\n", itemAndCountPair.Key.Name,
                    itemAndCountPair.Value, itemAndCountPair.Key.Price * itemAndCountPair.Value));
            });

            return orderDetails.ToString();
        }

        private List<KeyValuePair<Item, int>> DecodeOrders(List<string> orders)
        {
            List<KeyValuePair<Item, int>> itemAndCountPairs = new List<KeyValuePair<Item, int>>();
            List<Item> allItems = itemRepository.FindAll();

            orders.ForEach(order =>
            {
                KeyValuePair<string, int> itemIdAndCountPairs = DecodeOrder(order);
                Item matchItem = allItems.Find(item => item.Id == itemIdAndCountPairs.Key);
                if (matchItem != null)
                {
                    itemAndCountPairs.Add(new KeyValuePair<Item, int>(matchItem, itemIdAndCountPairs.Value));
                }
            });
            return itemAndCountPairs;
        }

        private KeyValuePair<string, int> DecodeOrder(string order)
        {
            string[] splittedOrder = order.Split("x");
            string itemId = splittedOrder[0].Trim();
            int count = Int32.Parse(splittedOrder[1].Trim());

            return new KeyValuePair<string, int>(itemId, count);
        }
    }
}
