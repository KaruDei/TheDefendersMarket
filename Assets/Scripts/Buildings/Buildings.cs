using UnityEngine;

public class Buildings : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    
    [SerializeField] private float _buildDistance;
    [SerializeField] private LayerMask _buildMask;

    [SerializeField] private Building _prefab;

    private Camera _camera;

    private Building _spawnObject;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void StartBuilding(Building prefab)
    {
        if (_spawnObject != null)
        {
            Destroy(_spawnObject.gameObject);
        }

        _spawnObject = Instantiate(prefab);
        _spawnObject.CanMove = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartBuilding(_prefab);
        }
        else if(Input.GetKeyDown(KeyCode.V))
        {
            ApplyBuilding();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            CancelBuilding();
        }

        //if (_spawnObject != null)
        //{
        //    RaycastHit hitInfo;
        //    if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hitInfo, _buildDistance, _buildMask))
        //    {
        //        _spawnObject.transform.position = hitInfo.point;
        //        _spawnObject.IsAvailable = true;
        //        _spawnObject.SetAvailableMaterial();
        //    }
        //    else
        //    {
        //        _spawnObject.IsAvailable = false;
        //        _spawnObject.SetAvailableMaterial();
        //    }
        //}

        if (_spawnObject != null)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hitInfo, _buildDistance, _buildMask))
            {
                _spawnObject.transform.position = hitInfo.point;

                bool hasCollisions = CheckCollisions(_spawnObject);

                if (!hasCollisions)
                {
                    _spawnObject.IsAvailable = true;
                    _spawnObject.SetAvailableMaterial();
                }
                else
                {
                    _spawnObject.IsAvailable = false;
                    _spawnObject.SetAvailableMaterial();
                }
            }
            else
            {
                _spawnObject.IsAvailable = false;
                _spawnObject.SetAvailableMaterial();
            }
        }
    }

    private bool CheckCollisions(Building building)
    {
        Collider buildingCollider = building.GetComponent<Collider>();
        if (buildingCollider == null)
        {
            Debug.LogError("Нет коллайдера!");
            return true;
        }

        Collider[] hitColliders = Physics.OverlapBox(buildingCollider.bounds.center, buildingCollider.bounds.extents, building.transform.rotation, ~_buildMask);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform.root != building.transform.root)
            {
                return true;
            }
        }

        return false;
    }

    private void ApplyBuilding()
    {
        if (_spawnObject == null || !_spawnObject.IsAvailable) return;

        _spawnObject.CanMove = false;
        _spawnObject.SetNormalMaterial();
        _spawnObject = null;
    }

    private void CancelBuilding()
    {
        if (_spawnObject != null)
            Destroy(_spawnObject.gameObject);
    }
}
