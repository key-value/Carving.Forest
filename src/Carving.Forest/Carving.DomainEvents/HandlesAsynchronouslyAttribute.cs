// Carving.DomainEvents HandlesAsynchronouslyAttribute.cs
// writer sundy
// Last Update Time 2015-04-29-21:39
// Create Time 2015-04-29-21:39

using System;

namespace Carving.DomainEvents
{
    /// <summary>
    /// Represents that the event handlers applied with this attribute
    /// will handle the events in a asynchronous process.
    /// </summary>
    /// <remarks>This attribute is only applicable to the message handlers and will only
    /// be used by the message buses or message dispatchers. Applying this attribute to
    /// other types of classes will take no effect.</remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class HandlesAsynchronouslyAttribute : Attribute
    {

    }
}
