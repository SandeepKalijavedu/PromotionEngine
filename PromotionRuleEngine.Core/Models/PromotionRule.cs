using PromotionRuleEngine.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionRuleEngine.Core.Models
{
    public class PromotionRule
    {
        public int PromotionId { get; set; }
        public int Quantity { get; set; }
        public PromotionType Type { get; set; }
        public string ProductCode { get; set; }
        public double Price { get; set; }
    }
}
