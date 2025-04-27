using UnityEngine;
using UnityEngine.Events;

public class VoidGameEventListener : MonoBehaviour
{
    [Header("Event")]
    public VoidGameEvent GameEvent;

    [Header("Response Actions")]
    public UnityEvent Response;

    public void OnEventRaised() => Response?.Invoke();

    private void OnEnable() => GameEvent?.RegisterListener(this);

    private void OnDisable() => GameEvent?.UnregisterListener(this);
}
