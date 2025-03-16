using UnityEngine;
using UnityEngine.AI;

public class TestAgent : MonoBehaviour
{
    public NavMeshAgent _agent;
    public Transform _target;

    public Transform _currentTarget;
    public NavMeshPath path;

    private void FixedUpdate()
    {
        GetTarget();
    }

    public void GetTarget()
    {
        if (NavMesh.CalculatePath(_agent.transform.position, _target.position, NavMesh.AllAreas, path))
        {
            Debug.Log("���� ������");
            if (_currentTarget != _target)
            {
                _currentTarget = _target;
                _agent.SetDestination(_currentTarget.position);
            }
        }
        else
        {
            Debug.Log("���� �� ������");
        }

        Debug.Log(path.status);
    }

    private void Start()
    {
        path = new();
    }

    //void Update()
    //{
    //    if (_target != null)
    //    {
    //        if (_currentTarget != _target)
    //        {
    //            _currentTarget = _target;
    //            _agent.SetDestination(_currentTarget.position);
    //        }

    //        if (!NavMesh.CalculatePath(_agent.transform.position, _target.position, NavMesh.AllAreas, path) || path.status != NavMeshPathStatus.PathComplete)
    //        {
    //            Debug.Log("���� ������������! ���� �����������...");
    //            FindAndDestroyBlockingObstacle();
    //        }
    //        else
    //        {
    //            Debug.Log("����");
    //        }
    //    }
    //}

    //void OnDrawGizmos()
    //{
    //    if (path != null && path.corners.Length > 1)
    //    {
    //        Gizmos.color = Color.red;
    //        for (int i = 0; i < path.corners.Length - 1; i++)
    //        {
    //            Gizmos.DrawLine(path.corners[i], path.corners[i + 1]);
    //        }
    //    }
    //}

    //void FindAndDestroyBlockingObstacle()
    //{
    //    Vector3 agentPos = _agent.transform.position;
    //    Vector3 targetPos = _target.position;
    //    Vector3 direction = (targetPos - agentPos).normalized;

    //    // ����� ��� ������� "Building" ����� � �������
    //    Collider[] obstacles = Physics.OverlapSphere(agentPos, 5f);

    //    GameObject bestObstacle = null;
    //    float bestScore = Mathf.Infinity;

    //    foreach (Collider col in obstacles)
    //    {
    //        if (col.CompareTag("Building"))
    //        {
    //            Vector3 obstaclePos = col.transform.position;

    //            // ���������, ��������� �� ����������� �� ����������� � ����
    //            float dot = Vector3.Dot(direction, (obstaclePos - agentPos).normalized);
    //            if (dot < 0.5f) continue; // ���� ������ �� � ����������� ����, ����������

    //            // ���������, ����������� �� ������ ���� (��� �������� ������ �� ������)
    //            if (!Physics.Raycast(agentPos, direction, out RaycastHit hit, 2.5f)) continue;
    //            if (hit.collider.gameObject != col.gameObject) continue;

    //            // ��� ����� ������ � ����� ����, ��� ���� ���������
    //            float distanceToLine = Vector3.Distance(obstaclePos, agentPos + direction * Vector3.Dot(obstaclePos - agentPos, direction));
    //            if (distanceToLine < bestScore)
    //            {
    //                bestScore = distanceToLine;
    //                bestObstacle = col.gameObject;
    //            }
    //        }
    //    }

    //    // ��������� ��������� ������ �� ����
    //    if (bestObstacle != null)
    //    {
    //        Debug.Log("��������� �����������: " + bestObstacle.name);
    //        Destroy(bestObstacle);
    //        TryRecalculatePath();
    //    }
    //}

    //void TryRecalculatePath()
    //{
    //    if (NavMesh.CalculatePath(_agent.transform.position, _target.position, NavMesh.AllAreas, path))
    //    {
    //        Debug.Log("���� ������ ����� ���������� �����������!");
    //        _agent.SetDestination(_currentTarget.position);
    //    }
    //    else
    //    {
    //        Debug.Log("���� ��� ��� ������������, ���������� ������...");
    //        FindAndDestroyBlockingObstacle();
    //    }
    //}
}
