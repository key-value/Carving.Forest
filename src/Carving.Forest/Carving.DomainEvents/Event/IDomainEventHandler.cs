// Carving.Domain.Core IDomainEventHandler.cs
// writer sundy
// Last Update Time 2015-05-06-21:20
// Create Time 2015-05-04-14:44

using Carving.Domain.Events;

namespace Carving.Domain.Core.Event
{
    /// <summary>
    /// 表示继承于该接口的类型是领域事件处理器类型。
    /// </summary>
    /// <typeparam name="TDomainEvent">领域事件处理器所能处理的领域事件的类型。</typeparam>
    public interface IDomainEventHandler<TDomainEvent> : IEventHandler<TDomainEvent>
        where TDomainEvent : class, IDomainEvent
    {
    }
}
