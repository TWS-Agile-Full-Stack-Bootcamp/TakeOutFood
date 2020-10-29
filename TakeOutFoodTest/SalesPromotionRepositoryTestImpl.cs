using System.Collections.Generic;
using TakeOutFood;

namespace TakeOutFoodTest
{
    public class SalesPromotionRepositoryTestImpl : ISalesPromotionRepository
    {
        public List<SalesPromotion> FindAll()
        {
            return TestData.ALL_SALES_PROMOTIONS;
        }
    }
}
