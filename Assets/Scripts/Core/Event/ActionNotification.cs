using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameHub.Core.Event
{
    /// <summary>
    /// Class <c>ActionNotification</c> provides a lightweight event
    /// notification system using callbacks. When an event occurs,
    /// the event is broadcast to all registered subscribers. The
    /// <c>ActionNotification</c> class is generic and is uniquely
    /// tied to a single event type. If you need to manage multiple
    /// types of events, you should create a new instance for each.
    /// </summary>
    /// <example>
    /// ActionNotification<string> notification = new ActionNotification<string>();
    /// notification.AddListener((string value) => {
    ///     System.Console.WriteLine(value); 
    /// });
    /// notification.AddListener((string value) => {
    ///     System.Console.WriteLine(value);
    /// });
    /// notification.Notify("Hello");
    /// </example>
    public class ActionNotification<T>
    {
        /// <summary>
        /// Instance variable <c>_actions</c> stores all event listeners
        /// that will be called when an event is broadcast to its subscribers.
        /// </summary>
        private List<Action<T>> _actions = new List<Action<T>>();

        /// <summary>
        /// Method <c>AddListener</c> is used to register event subscribers.
        /// When an event occurs, the associated event type for this instance
        /// will be passed to the registered subscriber / callback.
        /// </summary>
        /// <param name="eventData">
        /// <c>callback</c> will be called when an event occurs and is broadcast
        /// to all listeners.
        /// </param>
        public void AddListener(Action<T> callback)
        {
            _actions.Add(callback);
        }

        /// <summary>
        /// Method <c>RemoveListener</c> is used to unregister event subscribers.
        /// </summary>
        /// <param name="eventData">
        /// <c>callback</c> that will be removed from the list of subscribers to be
        /// notified when an event occucrs.
        /// </param>
        public void RemoveListener(Action<T> callback)
        {
            _actions.Remove(callback);
        }

        /// <summary>
        /// Method <c>Notify</c> is used to notify event subscribers when a specific
        /// event type occurs. All subscribers must be registered before this method 
        /// called.
        /// </summary>
        /// <param name="eventType">
        /// <c>eventType</c> represents the unique event type associated with this
        /// instance that will be broadcast to all subscriber callbacks.
        /// </param>
        public void Notify(T eventType)
        {
            foreach (Action<T> action in _actions)
            {
                action(eventType);
            }
        }
    }
}