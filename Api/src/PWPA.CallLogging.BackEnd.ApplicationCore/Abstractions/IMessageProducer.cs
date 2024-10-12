namespace PWPA.CallLogging.BackEnd.ApplicationCore.Abstractions;

public interface IMessageProducer<T>
{
    Task Send(T message);
}