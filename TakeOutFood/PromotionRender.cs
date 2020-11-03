using System.Collections.Generic;
using System.Linq;

namespace TakeOutFood
{
	internal class PromotionRender : NormalRender
	{
		private List<SalesPromotion> promotions;
		private double saving;

		public PromotionRender(List<SalesPromotion> promotions)
		{
			this.promotions = promotions;
		}

		public override string Render(IEnumerable<OrderItem> orderItems)
		{
			this.saving = 0;
			var renderedPromotions = promotions.Select(promotion =>
			{
				var displayName = promotion.DisplayName;
				IEnumerable<OrderItem> promotedOrderItems = orderItems
					.Where(orderItem => promotion.RelatedItems.Contains(orderItem.Id));
				var itemNames = promotedOrderItems.Select(_ => _.Name);
				double saving = new HalfPricePromotion().CalculateSaving(promotedOrderItems);
				this.saving += saving;
				return $"{displayName} ({string.Join(", ", itemNames)}), saving {saving} yuan";
			});
			return "Promotion used:\n" +
				string.Join("\n", renderedPromotions) +
				"\n-----------------------------------\n" +
				base.Render(orderItems);
		}

		protected override double CalculateTotal(IEnumerable<OrderItem> orderItems)
		{
			return orderItems.Select(_ => _.Subtotal).Sum() - this.saving;
		}
	}
}