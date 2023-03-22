using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System;

namespace LearningMassTransit.Infrastructure.EntityFramework;

public class ContextDesignTimeFactory<T> : IDesignTimeDbContextFactory<T> where T : DbContext
{
    public const string ParamConnectionString = "connectionString";
    public const string ParamSchema = "schema";

    private string _connectionString;
    private string _schema;

    public T CreateDbContext(string[] args)
    {
        ParseArguments(args);

        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new InvalidOperationException("connectionString cannot be null or empty");
        }

        var optionsBuilder = new DbContextOptionsBuilder<T>();
        optionsBuilder.UseNpgsql(_connectionString, options =>
        {
            options.MigrationsHistoryTable("__EFMigrationsHistory", _schema);
        });

        var contextType = typeof(T);
        return (T)Activator.CreateInstance(contextType, optionsBuilder.Options);
    }

    private void ParseArguments(string[] args)
    {
        var matches = new Regex(@"(?<key>\w*)=(?<value>.*)").Matches(string.Join("\n", args));

        foreach (Match match in matches)
        {
            var groups = match.Groups;
            switch (groups["key"].Value)
            {
                case ParamConnectionString:
                    _connectionString = groups["value"].Value;
                    break;
                case ParamSchema:
                    _schema = groups["value"].Value;
                    break;
            }
        }
    }
}