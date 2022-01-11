using PromotionRuleEngine.Common;
using PromotionRuleEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionRuleEngine
{
    public class RuleEngine
    {
        private readonly IList<Product> Products;
        private readonly IList<PromotionRule> PromotionRules;
        private readonly IList<IPromotionDiscount> promotionDiscounts;

        public RuleEngine(IList<Product> products, IList<PromotionRule> promotionRules, IList<IPromotionDiscount> promotionDiscounts)
        {
            this.Products = products;
            this.PromotionRules = promotionRules;
            this.promotionDiscounts = promotionDiscounts;
        }

        public void CheckOutCartItems(Order order)
        {            

            foreach (CartItem item in order.CartItems)
            {
                if (item.Quantity > 0)
                {
                    foreach (var promotionDiscount in promotionDiscounts)
                    {
                        if (promotionDiscount.CanExecute(item, PromotionRules))
                        {
                            var finalItemPrice = promotionDiscount.CalculateDiscount(order, PromotionRules, Products);
                            order.TotalAmt += finalItemPrice;
                            break;
                        }
                    }
                }
            }
        }
    }
}
