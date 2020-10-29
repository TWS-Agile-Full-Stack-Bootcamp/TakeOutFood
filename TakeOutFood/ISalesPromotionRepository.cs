namespace TakeOutFood
{
    using System.Collections.Generic;

    public interface ISalesPromotionRepository
    {
        List<SalesPromotion> FindAll();
    }
}
