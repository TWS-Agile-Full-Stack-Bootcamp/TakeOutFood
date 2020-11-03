using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TakeOutFood
{
    public class Promotion
    {
        private readonly List<OrderItem> _orderItems;

        public int Saving
        {
            get { return Convert.ToInt32(this._orderItems.Sum(oi => oi.Price * oi.Quantity)) / 2; }
        }

        public List<OrderItem> OrderItems
        {
            get { return this._orderItems; }
        }

        public Promotion(List<OrderItem> orderItems)
        {
            _orderItems = orderItems;
        }

        public string RenderPromotion()
        {
            StringBuilder sb = new StringBuilder();
            if (this._orderItems != null && this._orderItems.Count > 0)
            {
                sb.Append("-----------------------------------\n");
                sb.Append("Promotion used:\n");
                var names = String.Join(", ", this._orderItems.Select(oi => oi.Name).ToList());
                sb.Append(String.Format($"Half price for certain dishes ({names}), saving {Saving} yuan\n"));
            }

            return sb.ToString();
        }
    }
}