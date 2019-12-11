using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsOvermind<EVENT_TYPE>
{
    public void AddListener<T>(Action<T> listener) where T : EVENT_TYPE
    {
        Type eventType = typeof(T);

        List<Delegate> delegates;
        if (!_delegatesMap.TryGetValue(eventType, out delegates))
        {
            delegates = new List<Delegate>();
            _delegatesMap.Add(eventType, delegates);
        }

        delegates.Add(listener);
    }

    public void RemoveListener<T>(Action<T> listener) where T : EVENT_TYPE
    {
        Type eventType = typeof(T);

        List<Delegate> delegates;
        if (_delegatesMap.TryGetValue(eventType, out delegates))
        {
            delegates.Remove(listener);
        }
    }

    public void RemoveListeners(object owner)
    {
        var enumerator = _delegatesMap.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var delegates = enumerator.Current.Value;
            var delegatesToRemove = new List<Delegate>();

            foreach (var d in delegates)
            {
                if (d.Target == owner)
                {
                    delegatesToRemove.Add(d);
                }
            }

            foreach (var d in delegatesToRemove)
            {
                delegates.Remove(d);
            }

            delegatesToRemove.Clear();
        }
    }

    public void Send<T>(T e) where T : EVENT_TYPE
    {
        Type eventType = typeof(T);

        List<Delegate> delegates;
        if (_delegatesMap.TryGetValue(eventType, out delegates))
        {
            foreach (var d in delegates)
            {
                var callback = d as Action<T>;
                if (callback != null)
                {
                    callback(e);
                } else
                {
                    var eventCallback = d as Action<EVENT_TYPE>;
                    if (eventCallback != null)
                    {
                        eventCallback(e);
                    }
                }
            }
        }
    }

    protected Dictionary<Type, List<Delegate>> _delegatesMap = new Dictionary<Type, List<Delegate>>();
}