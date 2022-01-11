using System;


namespace PromotionRuleEngine.Core.Models.Common
{
    public abstract class EntityBase
    {
        public EntityBase()
        {

        }

        public string CreatedBy { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
