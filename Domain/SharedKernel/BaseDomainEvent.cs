using MediatR;

namespace Domain.SharedKernel
{
    public abstract class BaseDomainEvent : INotification
    {
        public DateTimeOffset DateOccurred
        { get; protected set; } = DateTimeOffset.UtcNow;
    }
}
