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
            string header = "============= Order details =============\n";
            string orderDetailsText = RenderOrderDetails(itemAndCountPairs);
            string totalText = RenderTotal(itemAndCountPairs);
            string footer = "===================================";


            return header + orderDetailsText + totalText + footer;

        }

        private string RenderTotal(List<KeyValuePair<Item, int>> itemIdAndCountPairs)
        {
            StringBuilder totalText = new StringBuilder("-----------------------------------\n");
            int total = (int)itemIdAndCountPairs.Sum(pair => pair.Key.Price * pair.Value);
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
