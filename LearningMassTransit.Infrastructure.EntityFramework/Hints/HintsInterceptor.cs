using System.Data.Common;
using LearningMassTransit.Infrastructure.Database.Hints;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LearningMassTransit.Infrastructure.EntityFramework.Hints
{
    public class HintsInterceptor : DbCommandInterceptor
    {
        public HintsInterceptor()
        {
            _hints = new HintsCollection();
        }

        private readonly HintsCollection _hints;

        private string BuildQuery(string query)
        {
            if (_hints.Count != 0)
            {
                string sTHints;
                string sQHints;

                sTHints = _hints.GenerateString<TableHint>();
                sQHints = _hints.GenerateString<QueryHint>();


                if (sQHints != string.Empty)
                {
                    query = QueryHint.BuildQuery(query, sQHints);
                }

                if (sTHints != string.Empty)
                {
                    query = TableHint.BuildQuery(query, sTHints);
                }
            }

            return query;
        }

        public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> result)
        {
            command.CommandText = BuildQuery(command.CommandText);
            return result;
        }

        public override ValueTask<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result, CancellationToken cancellationToken = default)
        {
            command.CommandText = BuildQuery(command.CommandText);
            return new ValueTask<InterceptionResult<object>>(result);
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            command.CommandText = BuildQuery(command.CommandText);
            return result;
        }

        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            command.CommandText = BuildQuery(command.CommandText);
            return new ValueTask<InterceptionResult<DbDataReader>>(result);
        }

        public void Add(HintBase hint)
        {
            _hints.Add(hint);
        }

        public void Clear()
        {
            _hints.Clear();
        }

        public HintsCollection Hints => _hints;
    }
}
