using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionRuleEngine.Core.Models;
using PromotionRuleEngine.Core.Models.Common;
using PromotionRuleEngine.Interfaces;
using PromotionRuleEngine.PromotionDiscountTypes;
using System.Collections.Generic;

namespace PromotionRuleEngineTests
{
    [TestClass]
    public class PromotionRuleEngineTests
    {
        static readonly IList<Product> Products =
            new List<Product> {
                new Product { Id = 'A', Price = 50 },
                new Product { Id = 'B', Price = 30 },
                new Product { Id = 'C', Price = 20 },
                new Product { Id = 'D', Price = 15 }
            };

        static readonly IList<PromotionRule> PromotionRules =
          new List<PromotionRule> {
              new PromotionRule {
                   PromotionId = 1,
                   Type = PromotionType.Quantity,
                   ProductCode = "A",
                   Quantity = 3,
                   Price = 130,
              },
              new PromotionRule {
                   PromotionId = 2,
                   Type = PromotionType.Quantity,
                   ProductCode = "B",
                   Quantity = 2,
                   Price = 45,
              },
              new PromotionRule {
                   PromotionId = 3,
                   Type = PromotionType.Combo,
                   ProductCode = "C;D",
                   Quantity = 2,
                   Price = 30,
              }
          };

        static readonly IList<IPromotionDiscount> promotionDiscounts = new List<IPromotionDiscount>()
            {
            new QuantityDiscountType(),
            new ComboDiscountType(),
            };           

        static readonly PromotionRuleEngine.RuleEngine ruleEngine = new PromotionRuleEngine.RuleEngine(Products, PromotionRules, promotionDiscounts);

        [TestMethod]
        public void Product_Promotion_quanity_discount_A()

        {
            var order =
                new Order
                {
                    CartItems = new List<CartItem>
                    {
                        new CartItem { Id = 'A', Quantity = 1 },
                        new CartItem { Id = 'B', Quantity = 1 },
                        new CartItem { Id = 'C', Quantity = 1 },
                    }
                };

            var expectedAmount = ruleEngine.CheckOutCartItems(order);
            Assert.IsTrue(expectedAmount == 100);
        }

        [TestMethod]
        public void Product_Promotion_quanity_discount_B()

        {
            var order =
                new Order
                {
                    CartItems = new List<CartItem>
                    {
                        new CartItem { Id = 'A', Quantity = 5 },
                        new CartItem { Id = 'B', Quantity = 5 },
                        new CartItem { Id = 'C', Quantity = 1 },
                    }
                };

            var expectedAmount = ruleEngine.CheckOutCartItems(order);
            Assert.IsTrue(expectedAmount == 370);
        }

        [TestMethod]
        public void Product_Promotion_combo_discount_C_D()

        {
            var order =
                new Order
                {
                    CartItems = new List<CartItem>
                    {
                        new CartItem { Id = 'A', Quantity = 3 },
                        new CartItem { Id = 'B', Quantity = 5 },
                        new CartItem { Id = 'C', Quantity = 1 },
                        new CartItem { Id = 'D', Quantity = 1 }
                    }
                };

            var expectedAmount = ruleEngine.CheckOutCartItems(order);
            Assert.IsTrue(expectedAmount == 280);
        }
    }
}
