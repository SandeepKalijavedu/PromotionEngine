using PromotionRuleEngine.Interfaces;
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

        public double CheckOutCartItems(Order order)
        {
            Order appliedPromotionOffer = new Order();

            foreach (CartItem item in order.CartItems)
            {
                if (item.Quantity > 0)
                {
                    foreach (var promotionDiscount in promotionDiscounts)
                    {
                        if (promotionDiscount.CanExecute(item, PromotionRules))
                        {
                            item.FinalPrice = promotionDiscount.CalculateDiscount(ref order, PromotionRules, Products);
                            appliedPromotionOffer.TotalAmt += item.FinalPrice;
                            break;
                        }
                    }
                }
            }
            //appliedPromotionOffer.CartItems = order.CartItems;

            return appliedPromotionOffer.TotalAmt;
        }
    }
}
