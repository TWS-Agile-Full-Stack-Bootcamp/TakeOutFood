using System.Collections.Generic;

namespace TakeOutFood
{
	internal interface IFooterRender
	{
		string Render(IEnumerable<OrderItem> orderItems);
	}
}