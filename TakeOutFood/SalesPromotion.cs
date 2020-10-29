using System.Collections.Generic;

namespace TakeOutFood
{
    public class SalesPromotion
    {
        public SalesPromotion(string type, string displayName, List<string> relatedItems)
        {
            this.Type = type;
            this.DisplayName = displayName;
            this.RelatedItems = relatedItems;
        }

        public string Type { get; private set; }
        public string DisplayName { get; private set; }
        public List<string> RelatedItems { get; private set; }
    }
}
