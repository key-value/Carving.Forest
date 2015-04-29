﻿// Carving.DomainEvents IEventAggregator.cs
// writer sundy
// Last Update Time 2015-04-29-21:38
// Create Time 2015-04-29-21:38

using System;
using System.Collections.Generic;

namespace Carving.DomainEvents
{
    public interface IEventAggregator
    {
        void Subscribe<TEvent>(IEventHandler<TEvent> domainEventHandler)
            where TEvent : class, IEvent;
        void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> domainEventHandlers)
            where TEvent : class, IEvent;
        void Subscribe<TEvent>(params IEventHandler<TEvent>[] domainEventHandlers)
            where TEvent : class, IEvent;
        void Subscribe<TEvent>(Action<TEvent> domainEventHandlerFunc)
            where TEvent : class, IEvent;
        void Subscribe<TEvent>(IEnumerable<Func<TEvent, bool>> domainEventHandlerFuncs)
            where TEvent : class, IEvent;
        void Subscribe<TEvent>(params Func<TEvent, bool>[] domainEventHandlerFuncs)
            where TEvent : class, IEvent;
        void Unsubscribe<TEvent>(IEventHandler<TEvent> domainEventHandler)
            where TEvent : class, IEvent;
        void Unsubscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> domainEventHandlers)
            where TEvent : class, IEvent;
        void Unsubscribe<TEvent>(params IEventHandler<TEvent>[] domainEventHandlers)
            where TEvent : class, IEvent;
        void Unsubscribe<TEvent>(Action<TEvent> domainEventHandlerFunc)
            where TEvent : class, IEvent;
        void Unsubscribe<TEvent>(IEnumerable<Func<TEvent, bool>> domainEventHandlerFuncs)
            where TEvent : class, IEvent;
        void Unsubscribe<TEvent>(params Func<TEvent, bool>[] domainEventHandlerFuncs)
            where TEvent : class, IEvent;
        void UnsubscribeAll<TEvent>()
            where TEvent : class, IEvent;
        void UnsubscribeAll();
        IEnumerable<IEventHandler<TEvent>> GetSubscriptions<TEvent>()
            where TEvent : class, IEvent;
        void Publish<TEvent>(TEvent domainEvent)
            where TEvent : class, IEvent;
        void Publish<TEvent>(TEvent domainEvent, Action<TEvent, bool, Exception> callback, TimeSpan? timeout = null)
            where TEvent : class, IEvent;
    }
}
