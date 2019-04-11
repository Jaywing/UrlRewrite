using System.Collections.Generic;

namespace Hi.UrlRewrite.Entities.Conditions
{
    public interface IConditionsProperties : IConditionLogicalGrouping
    {
        IEnumerable<Condition> Conditions { get; set; }
    }
}