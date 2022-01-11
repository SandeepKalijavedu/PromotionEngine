using System;
using System.Collections.Generic;
using System.Text;
using PromotionRuleEngine.Core.Models.Common;

namespace PromotionRuleEngine.Core.Models
{
    public class Product : EntityBase, IEntity
    {
        public char Id { get; set; }
        public double Price { get; set; }
    }
}
