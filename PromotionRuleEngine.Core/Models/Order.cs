using PromotionRuleEngine.Core.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionRuleEngine.Core.Models
{
    public class Order : EntityBase
    {
        public List<CartItem> CartItems { get; set; }
        public double TotalAmt { get; set; }
    }
}
