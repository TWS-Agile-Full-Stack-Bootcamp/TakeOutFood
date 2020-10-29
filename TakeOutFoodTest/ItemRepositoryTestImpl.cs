using System.Collections.Generic;
using TakeOutFood;

namespace TakeOutFoodTest
{
    public class ItemRepositoryTestImpl : IItemRepository
    {
        public List<Item> FindAll()
        {
            return TestData.ALL_ITEMS;
        }
    }
}
