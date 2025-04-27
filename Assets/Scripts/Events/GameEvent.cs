using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent<T> : GameEventBase
{
    [Header("Event Listeners")]
    public List<GameEventListener<T>> _listeners = new();

    public virtual void RegisterListener(GameEventListener<T> gameEventListener)
    {
        if (!_listeners.Contains(gameEventListener))
            _listeners.Add(gameEventListener);
    }

    public virtual void UnregisterListener(GameEventListener<T> gameEventListener)
    {
        if (_listeners.Contains(gameEventListener))
            _listeners.Remove(gameEventListener);
    }

    public virtual void Raise(T value)
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].OnEventRaised(value);
    }
}
