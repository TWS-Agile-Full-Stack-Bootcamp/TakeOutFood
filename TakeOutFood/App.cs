namespace TakeOutFood
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class App
	{
		private IItemRepository itemRepository;
		private ISalesPromotionRepository salesPromotionRepository;

		public App(IItemRepository itemRepository, ISalesPromotionRepository salesPromotionRepository)
		{
			this.itemRepository = itemRepository;
			this.salesPromotionRepository = salesPromotionRepository;
		}

		public string BestCharge(List<string> inputs)
		{
			IEnumerable<OrderItem> orderItems = this.DecodeInputs(inputs);
			IFooterRender footerRender = HasPromotionItem(orderItems)
				? new PromotionRender(this.salesPromotionRepository.FindAll())
				: new NormalRender();
			return "============= Order details =============\n" +
					this.RenderItems(orderItems) +
					"\n-----------------------------------\n" +
					footerRender.Render(orderItems) +
					"===================================";
		}

		private bool HasPromotionItem(IEnumerable<OrderItem> orderItems)
		{
			IEnumerable<string> promotionItemIds = this.salesPromotionRepository.FindAll().SelectMany(_ => _.RelatedItems);
			IEnumerable<string> orderItemIdsInPromotion = orderItems.Select(_ => _.Id).Intersect(promotionItemIds);
			bool hasPromotionItem = orderItemIdsInPromotion.Any();
			return hasPromotionItem;
		}

		private IEnumerable<OrderItem> DecodeInputs(IEnumerable<string> inputs)
		{
			return inputs.Select(input =>
			{
				var splitted = input.Split(" x ");
				var id = splitted[0];
				int quantity = Int32.Parse(splitted[1]);
				var item = this.itemRepository.FindAll().Find(item => item.Id == id);
				return new OrderItem(item, quantity);
			});
		}

		private string RenderItems(IEnumerable<OrderItem> orderItems)
		{
			var renderedItems = orderItems.Select(orderItem =>
			{
				var name = orderItem.Name;
				var quantity = orderItem.Quantity;
				var subtotal = orderItem.Subtotal;
				return $"{name} x {quantity} = {subtotal} yuan";
			});
			return string.Join("\n", renderedItems);
		}
	}
}
