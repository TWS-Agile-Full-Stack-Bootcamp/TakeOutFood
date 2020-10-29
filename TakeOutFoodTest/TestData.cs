using System.Collections.Generic;
using TakeOutFood;

namespace TakeOutFoodTest
{
    public class TestData
    {
        public static readonly List<Item> ALL_ITEMS = new List<Item>()
        {
            new Item("ITEM0001", "Braised chicken", 18.00),
            new Item("ITEM0013", "Chinese hamburger", 6.00),
            new Item("ITEM0022", "Cold noodles", 8.00),
            new Item("ITEM0030", "coca-cola", 2.00),
        };

        public static readonly List<SalesPromotion> ALL_SALES_PROMOTIONS = new List<SalesPromotion>()
        {
                new SalesPromotion("BUY_30_SAVE_6_YUAN", "Deduct 6 yuan when the order reaches 30 yuan", new List<string>()),
                new SalesPromotion("50%_DISCOUNT_ON_SPECIFIED_ITEMS", "Half price for certain dishes", new List<string>()
                    {
                        "ITEM0001",
                        "ITEM0022",
                    }),
        };
    }
}
