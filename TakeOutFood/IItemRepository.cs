namespace TakeOutFood
{
    using System.Collections.Generic;

    public interface IItemRepository
    {
        List<Item> FindAll();
    }
}
