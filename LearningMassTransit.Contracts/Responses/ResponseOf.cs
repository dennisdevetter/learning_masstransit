namespace LearningMassTransit.Contracts.Responses;

public abstract class ResponseOf<T>
{
    public T Result { get; set; }

    protected ResponseOf(T result)
    {
        Result = result;
    }
}