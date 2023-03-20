using System.Text.RegularExpressions;
using LearningMassTransit.Infrastructure.Database.Hints;

namespace LearningMassTransit.Infrastructure.EntityFramework.Hints
{
    public abstract class TableHint : HintBase
    {
        private static readonly Regex TableHRegex = new(@"(?<table>FROM \[?\w*\]?.\[?\w*\]? AS \[?\w\]?)", RegexOptions.Multiline | RegexOptions.IgnoreCase);

        public static string BuildQuery(string query, string hints)
        {
            return TableHRegex.Replace(query, $"${{table}} WITH ({hints})");
        }
    }
}
