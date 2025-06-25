using UnityEngine;

public class OrderDesk : MonoBehaviour, IInteractable
{
    [SerializeField] private VoidGameEvent _onPlayerOrders;

    public void Interact()
    {
        _onPlayerOrders?.Raise();
    }
}
