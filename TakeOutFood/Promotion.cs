using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TakeOutFood
{
    public class Promotion
    {
        private readonly List<OrderItem> orderItems;

        public int Saving
        {
            get { return Convert.ToInt32(this.orderItems.Sum(oi => oi.Price * oi.Quantity)) / 2; }
        }

        public List<OrderItem> OrderItems
        {
            get { return this.orderItems; }
        }

        public Promotion(List<OrderItem> orderItems)
        {
            this.orderItems = orderItems;
        }
    }
}