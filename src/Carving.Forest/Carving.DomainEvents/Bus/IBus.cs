﻿// Carving.DomainEvents IBus.cs
// writer sundy
// Last Update Time 2015-04-29-21:42
// Create Time 2015-04-29-21:42

using System;
using System.Collections.Generic;
using Carving.Domain.Core.Repositories;

namespace Carving.DomainEvents.Bus
{
    /// <summary>
    /// Represents the message bus.
    /// </summary>
    public interface IBus : IUnitOfWork, IDisposable
    {
        Guid ID { get; }
        /// <summary>
        /// Publishes the specified message to the bus.
        /// </summary>
        /// <param name="message">The message to be published.</param>
        void Publish<TMessage>(TMessage message)
            where TMessage : class, IEvent;
        /// <summary>
        /// Publishes a collection of messages to the bus.
        /// </summary>
        /// <param name="messages">The messages to be published.</param>
        void Publish<TMessage>(IEnumerable<TMessage> messages)
            where TMessage : class, IEvent;
        /// <summary>
        /// Clears the published messages waiting for commit.
        /// </summary>
        void Clear();
    }
}
