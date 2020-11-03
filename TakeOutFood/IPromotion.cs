using System.Collections.Generic;

namespace TakeOutFood
{
	internal interface IPromotion
	{
		double CalculateSaving(IEnumerable<OrderItem> orderItems);
	}
}