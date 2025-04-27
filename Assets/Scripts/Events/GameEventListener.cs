using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventListener<T> : MonoBehaviour
{
    [Header("Event")]
    public GameEvent<T> GameEvent;

    [Header("Response Actions")]
    public UnityEvent<T> Response;

    public void OnEventRaised(T value) => Response?.Invoke(value);

    private void OnEnable() => GameEvent?.RegisterListener(this);

    private void OnDisable() => GameEvent?.UnregisterListener(this);
}
