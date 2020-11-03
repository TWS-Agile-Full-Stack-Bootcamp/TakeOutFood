using System;
using System.Collections.Generic;

namespace TakeOutFood
{
    public class PromotionItem
    {
        public PromotionItem()
        {
        }

        public List<string> PromotionedItemNames { get; set; } = new List<string>();
        public int Saving { get; set; }

    }
}
