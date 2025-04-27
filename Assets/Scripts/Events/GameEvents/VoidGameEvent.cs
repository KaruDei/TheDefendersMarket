using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Event/Void")]
public class VoidGameEvent : GameEventBase
{
    public List<VoidGameEventListener> _listeners = new();

    public virtual void RegisterListener(VoidGameEventListener gameEventListener)
    {
        if (!_listeners.Contains(gameEventListener))
            _listeners.Add(gameEventListener);
    }

    public virtual void UnregisterListener(VoidGameEventListener gameEventListener)
    {
        if (_listeners.Contains(gameEventListener))
            _listeners.Remove(gameEventListener);
    }

    public virtual void Raise()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].OnEventRaised();
    }
}
