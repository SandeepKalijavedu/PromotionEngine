using PromotionRuleEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionRuleEngine.Common
{
    public interface IPromotionDiscount
    {
        public bool CanExecute(CartItem item, IList<PromotionRule> promotionRulesForOrder);
        public double CalculateDiscount(ref Order order, IList<PromotionRule> promotionRulesForOrder, IList<Product> products);
    }
}
