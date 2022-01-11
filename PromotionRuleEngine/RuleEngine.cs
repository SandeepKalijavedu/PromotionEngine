using PromotionRuleEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionRuleEngine
{
    public class RuleEngine
    {
        private readonly IEnumerable<Product> Products;
        private readonly IEnumerable<PromotionRule> PromotionRules;

        public RuleEngine(IEnumerable<Product> products, IEnumerable<PromotionRule> promotionRules)
        {
            this.Products = products;
            this.PromotionRules = promotionRules;
        }

        public void CheckOutCartItems(Order order)
        {
            var promotionRulesForOrder = getPromotionRulesForOrder(order);
            calculatePriceForOrder(order);
            applyPromotion(order, promotionRulesForOrder);
        }

        private void applyPromotion(Order order, IList<PromotionRule> promotionRulesForOrder)
        {
            throw new NotImplementedException();
        }       

        private void calculatePriceForOrder(Order order)
        {
            foreach (var item in order.CartItems)
            {
                var itemPrice = Products.FirstOrDefault(x => x.Id == item.Id).Price;
                var quantity = item.Quantity;
                if (quantity > 0)
                    order.TotalAmt += quantity * itemPrice;
            }
        }

        private IList<PromotionRule> getPromotionRulesForOrder(Order order)
        {            

            var rules = new List<PromotionRule>();

            Parallel.ForEach(order.CartItems, cartItem =>
            {
                var rule = PromotionRules.Where(pr => pr.CartItems.Any(c => c.Id == cartItem.Id));
                if (rule != null)
                {
                    rules.AddRange(rule);
                }
            });
            return rules;
        }

    }
}
