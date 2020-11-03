using System.Collections.Generic;
using System.Linq;

namespace TakeOutFood
{
	internal class NormalRender : IFooterRender
	{
		public virtual string Render(IEnumerable<OrderItem> orderItems)
		{
			return $"Total：{this.CalculateTotal(orderItems)} yuan\n";
		}

		protected virtual double CalculateTotal(IEnumerable<OrderItem> orderItems)
		{
			return orderItems.Select(_ => _.Subtotal).Sum();
		}
	}
}