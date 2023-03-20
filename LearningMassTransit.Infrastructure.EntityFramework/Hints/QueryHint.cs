using LearningMassTransit.Infrastructure.Database.Hints;

namespace LearningMassTransit.Infrastructure.EntityFramework.Hints
{
    public abstract class QueryHint : HintBase
    {
        public static string BuildQuery(string Query, string hints)
        {
            return Query + " OPTION (" + hints + ")";
        }
    }
}
