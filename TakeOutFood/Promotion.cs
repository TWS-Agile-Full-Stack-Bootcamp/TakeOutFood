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
    }
}