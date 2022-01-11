using PromotionRuleEngine.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionRuleEngine.Core.Models
{
    public class CartItem : IEntity
    {
        public CartItem(CartItem item)
        {
            Id = item.Id;
            Quantity = item.Quantity;
        }

        public char Id { get; set; }
        public int Quantity { get; set; }
    }
}
