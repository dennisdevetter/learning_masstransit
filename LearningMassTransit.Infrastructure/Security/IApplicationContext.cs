namespace LearningMassTransit.Infrastructure.Security;

public interface IApplicationContext
{
    string Sub { get; set; }
    string FirstName { get; }
    string LastName { get; }
    IList<string> Roles { get; }
}

public class ApplicationContext : IApplicationContext
{
    public string Sub { get; set; }
    public string FirstName { get; }
    public string LastName { get; }
    public IList<string> Roles { get; }
}