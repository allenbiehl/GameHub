using System.Collections.Generic;

namespace GameHub.Core.Event
{
    public class EventNotification<T>
    {
        private Dictionary<T, List<IEventListener<T>>> _eventTypes;

        public EventNotification()
        {
            _eventTypes = new Dictionary<T, List<IEventListener<T>>>();
        }

        public void AddListener( T eventType, IEventListener<T> listener )
        {
  
            List<IEventListener<T>> listeners;

            if (_eventTypes.ContainsKey(eventType)) 
            {
                listeners = _eventTypes[eventType];
            }
            else
            {
                listeners = new List<IEventListener<T>>();
                _eventTypes.Add(eventType, listeners);
            }
            listeners.Add(listener);
        }

        public void RemoveListener( T eventType, IEventListener<T> listener )
        {
            if (_eventTypes.ContainsKey(eventType))
            {
                _eventTypes[eventType].Remove(listener);
            }
        }

        public void Notify( T eventType )
        {
            if (_eventTypes.ContainsKey(eventType))
            {
                List<IEventListener<T>> listeners = _eventTypes[eventType];

                foreach (IEventListener<T> listener in listeners)
                {
                    listener.OnEvent(eventType);
                }
            }
        }
    }
}