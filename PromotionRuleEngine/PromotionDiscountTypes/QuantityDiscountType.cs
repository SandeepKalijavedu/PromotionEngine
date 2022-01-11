﻿using PromotionRuleEngine.Common;
using PromotionRuleEngine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionRuleEngine.PromotionDiscountTypes
{
    public class QuantityDiscountType : IPromotionDiscount
    {
        private PromotionRule promotionRule;
        private CartItem cartItem;

        public double CalculateDiscount(Order order, IList<PromotionRule> promotionRulesForOrder, IList<Product> products)
        {
            int totalEligibleItems = cartItem.Quantity / promotionRule.Quantity;
            int remainingItems = cartItem.Quantity % promotionRule.Quantity;
            var itemPrice = products.FirstOrDefault(x => x.Id == cartItem.Id).Price;
            var finalPrice = promotionRule.Price * totalEligibleItems + remainingItems * (itemPrice);

            return finalPrice;
        }

        public bool CanExecute(CartItem item, IList<PromotionRule> promotionRulesForOrder)
        {
            cartItem = item;
            promotionRule = promotionRulesForOrder.FirstOrDefault(x => x.ProductCode == item.Id.ToString());
            if (promotionRule != null && promotionRule.Type == PromotionType.Quantity)
            {
                return true;
            }

            return false;
        }
    }
}