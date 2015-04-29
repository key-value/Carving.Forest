// Carving.DomainEvents IEventHandler.cs
// writer sundy
// Last Update Time 2015-04-29-21:38
// Create Time 2015-04-29-21:38
namespace Carving.DomainEvents
{
    /// <summary>
    /// 表示实现该接口的类型为事件处理器。
    /// </summary>
    /// <typeparam name="TEvent">事件的类型。</typeparam>
    public interface IEventHandler<in TEvent>
        where TEvent : IEvent
    {
        #region Methods
        /// <summary>
        /// 处理给定的事件。
        /// </summary>
        /// <param name="evnt">需要处理的事件。</param>
        void Handle(TEvent evnt);
        #endregion
    }
}
