using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameHub.Core.Event
{
    public class ActionNotification<T>
    {

        private List<Action<T>> _actions = new List<Action<T>>();

        public void AddListener(Action<T> callback)
        {
            _actions.Add(callback);
        }

        public void RemoveListener(Action<T> callback)
        {
            _actions.Remove(callback);
        }

        public void Notify(T eventType)
        {
            foreach (Action<T> action in _actions)
            {
                action(eventType);
            }
        }
    }
}