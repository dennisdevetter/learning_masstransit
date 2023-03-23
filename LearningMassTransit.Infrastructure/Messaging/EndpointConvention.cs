using System;
using System.Linq;
using System.Reflection;

namespace LearningMassTransit.Infrastructure.Messaging;

public static class EndpointConvention
{
    public static void RegisterServiceBusEndpoints(Uri uri, params Assembly[] assemblies)
    {
        foreach (var type in assemblies.SelectMany(a => a.GetTypes().Where(type => type.IsClass && !type.IsGenericType)))
        {
            var method = typeof(MassTransit.EndpointConvention).GetMethod("Map", new[] { typeof(Uri) });
            var generic = method.MakeGenericMethod(type);
            generic.Invoke(null, new object?[] { uri });
        }
    }
}