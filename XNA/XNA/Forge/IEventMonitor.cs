using Forge.Entities;
using System;

namespace Forge.XNA {
    /// <summary>
    /// Annotation required by types which implement IEventMonitor signifying that they should be
    /// automatically instantiated.
    /// </summary>
    /// <remarks>
    /// This attribute is not really necessary; instead, it is used because it greatly increases
    /// code-reading clarity.
    /// </remarks>
    public sealed class EventMonitorAutomaticInstantiationAttribute : Attribute {
    }

    /// <summary>
    /// A type that can monitor IEvents that are coming out of the game engine. These are
    /// automatically discovered and instantiated when a new GameEngine is created.
    /// </summary>
    public interface IEventMonitor {
        /// <summary>
        /// Initialize the event monitor.
        /// </summary>
        /// <param name="notifier">The event notifier to register the monitor with.</param>
        void Initialize(IEventNotifier notifier);
    }
}