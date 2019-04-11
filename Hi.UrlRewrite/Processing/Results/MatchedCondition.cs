using Hi.UrlRewrite.Entities.Conditions;

namespace Hi.UrlRewrite.Processing.Results
{
    public class MatchedCondition
    {
        public Condition Condition { get; }
        public string ConditionInput { get; }

        public MatchedCondition()
        {
                
        }

        public MatchedCondition(Condition condition, string conditionInput)
        {
            Condition = condition;
            ConditionInput = conditionInput;
        }
    }
}