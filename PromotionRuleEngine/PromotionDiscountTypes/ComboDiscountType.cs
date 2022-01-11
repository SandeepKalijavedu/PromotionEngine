using PromotionRuleEngine.Common;
using PromotionRuleEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionRuleEngine.PromotionDiscountTypes
{
    public class ComboDiscountType : IPromotionDiscount
    {
        private PromotionRule promotionRule;
        List<CartItem> cartItems;

        public double CalculateDiscount(Order order, IList<PromotionRule> promotionRulesForOrder, IList<Product> products)
        {
            cartItems = new List<CartItem>();

            double finalPrice = 0;

            cartItems = order.CartItems;

            int quantity_first = 0;
            int quantity_second = 0;
            if (cartItems.Count > 1)
            {
                quantity_first = cartItems[0].Quantity;
                quantity_second = cartItems[1].Quantity;
            }              
                
                
            if (quantity_first == quantity_second)
            {
                finalPrice = promotionRule.Price * quantity_first;
            }
            else if (quantity_first > quantity_second)
            {
                int additionalItems = quantity_first - quantity_second;
                var cartItemPrice = products.FirstOrDefault(x => x.Id == cartItems[0].Id).Price;
                finalPrice = (cartItemPrice * additionalItems) + (promotionRule.Price * quantity_second);
            }
            else if (quantity_first < quantity_second)
            {
                int additionalItems = quantity_second - quantity_first;
                var cartItemPrice = products.FirstOrDefault(x => x.Id == cartItems[1].Id).Price;
                finalPrice = (cartItemPrice * additionalItems) + (promotionRule.Price * quantity_first);
            }

            return finalPrice;
        }

        public bool CanExecute(CartItem item, IList<PromotionRule> promotionRulesForOrder)
        {
            promotionRule = promotionRulesForOrder.Where(x => x.ProductCode.Split(';').Contains(item.Id.ToString())).FirstOrDefault();
            if (promotionRule != null && promotionRule.Type == PromotionType.Combo)
            {
                return true;
            }

            return false;

        }
    }
}
