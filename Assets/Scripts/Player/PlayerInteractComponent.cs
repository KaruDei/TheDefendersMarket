using UnityEngine;

public class PlayerInteractComponent : MonoBehaviour
{
    [SerializeField] private float _interactDistance;
    [SerializeField] private LayerMask _interactMask;

    [SerializeField] private GameObject _hintUI;

    private Camera _camera;

    private bool _canInteract = true;

    private IInteractable _interactableObject;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        CheckInteractableObjects();
    }

    public void CheckInteractableObjects()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hitInfo, _interactDistance, _interactMask))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable))
            {
                if (_interactableObject != interactable)
                    _interactableObject = interactable;
                
                if (_canInteract && !_hintUI.activeSelf) _hintUI.SetActive(true);
            }
            else
            {
                if (_interactableObject != null)
                    _interactableObject = null;
                if (_hintUI.activeSelf) _hintUI.SetActive(false);
            }
        }
        else
        {
            if (_hintUI.activeSelf) _hintUI.SetActive(false);
            if (_interactableObject != null)
                _interactableObject = null;
        }
    }

    public void Interaction()
    {
        if (_interactableObject != null && _canInteract)
            _interactableObject.Interact();
    }

    private void CanInteract(bool value)
    {
        _canInteract = value;
    }

    private void OnEnable()
    {
        EventBus.OnInteraction += Interaction;
    }

    private void OnDisable()
    {
        EventBus.OnInteraction -= Interaction;
    }
}
