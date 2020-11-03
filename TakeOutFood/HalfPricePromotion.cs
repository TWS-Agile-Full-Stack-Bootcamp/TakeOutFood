using System.Collections.Generic;
using System.Linq;

namespace TakeOutFood
{
	internal class HalfPricePromotion : IPromotion
	{
		private const double HALF_DISCOUNT = 0.5;

		public double CalculateSaving(IEnumerable<OrderItem> orderItems)
		{
			return orderItems.Select(_ => _.Subtotal).Sum() * HALF_DISCOUNT;
		}
	}
}