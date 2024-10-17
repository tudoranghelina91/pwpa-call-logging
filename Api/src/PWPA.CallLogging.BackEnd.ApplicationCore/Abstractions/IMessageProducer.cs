namespace PWPA.CallLogging.BackEnd.ApplicationCore.Abstractions;

public interface IMessageProducer<T>
{
    Task SendAsync(T message);
}