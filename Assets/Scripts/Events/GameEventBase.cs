using UnityEngine;

public abstract class GameEventBase : ScriptableObject
{
    [Header("Event Description")]
    [SerializeField, TextArea] private string _eventDescription;
}
