using UnityEngine;
using UnityEngine.AI;

public class Building : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Collider _collider;
    [SerializeField] private NavMeshObstacle _obstacle;

    public bool CanMove;
    public bool IsAvailable;

    public void SetAvailableMaterial()
    {
        if (_collider.isTrigger != true) _collider.isTrigger = true;
        if (_obstacle.enabled != false) _obstacle.enabled = false;

        if (IsAvailable)
            _renderer.material.color = new Color(0, 1, 0, 0.3f);
        else
            _renderer.material.color = new Color(1, 0, 0, 0.3f);
    }

    public void SetNormalMaterial()
    {
        if (_collider.isTrigger == true) _collider.isTrigger = false;
        if (_obstacle.enabled == false) _obstacle.enabled = true;
        _renderer.material.color = Color.white;
    }
}
