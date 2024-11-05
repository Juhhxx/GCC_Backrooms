using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _normalSpeed = 2f;
    [SerializeField] private float _chaseSpeed = 5.5f;
    [SerializeField] private float _walkRadiusMin = 20f;
    [SerializeField] private float _walkRadiusMax = 50f;
    [SerializeField] private float _detectRadius = 10f;
    [SerializeField] private float _stopDistancePlayer = 2f;
    [SerializeField] private float _playerMemoryTime = 4f;
    [SerializeField] private bool _drawGizmos = true;
    private Transform _playerTrans;
    private NavMeshAgent _agent;
    private Vector3 _pointTarget;
    private bool _isChasing = false;

    // ENEMY IS GETTING A BUMP WHEN HE STARTS <= NEEDS FIXING
    void Start()
    {
        _playerTrans = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _normalSpeed;
        _pointTarget = transform.position;
        StartCoroutine(EnemyControl());
    }
    void Update()
    {
        UpdateRotation();
        if (_isChasing)
        {
            ChasePlayer();
            Debug.Log("CHASING PLAYER", gameObject);
        }
        else
        {
            WalkToPoint();
            Debug.Log("ROAMING AROUND", gameObject);
        }
    }
    void UpdateRotation()
    {
        Vector3 targetPosition = new Vector3(_playerTrans.position.x,transform.position.y,_playerTrans.position.z);

        transform.LookAt(targetPosition);
    }
    void MoveObject(Vector3 target)
    {
        _agent.destination = target;
    }
    private Vector3 RandomNavmeshLocation(float radiusMin, float radiusMax)
    {
        float radius = Random.Range(radiusMin,radiusMax);
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }
    private void WalkToPoint()
    {
        _agent.speed = _normalSpeed;
        _agent.stoppingDistance = 0f;

        if (transform.position == _pointTarget)
            _pointTarget = RandomNavmeshLocation(_walkRadiusMin,_walkRadiusMax);

        Vector3 correctedTarget = _pointTarget;
        correctedTarget.y = 0;

        MoveObject(correctedTarget);
        
        Debug.Log($"Walking to ({_pointTarget.z},{_pointTarget.x})");
    }
    private bool DetectPlayer()
    {
        if (Vector3.Distance(transform.position,_playerTrans.position) <= _detectRadius)
        {
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position,_playerTrans.position - transform.position,out hit))
            {
                Debug.DrawLine(transform.position,_playerTrans.position,Color.blue);
                Debug.DrawLine(transform.position,hit.point,Color.red);

                Debug.Log($"{hit.collider.gameObject.name} WAS HIT");
                return hit.collider.gameObject.GetComponentInParent<PlayerMovement>() != null;
            }
        }
        return false;
    }
    private void ChasePlayer()
    {
        _agent.speed = _chaseSpeed;
        _agent.stoppingDistance = _stopDistancePlayer;

        MoveObject(_playerTrans.position);
    }
    private IEnumerator EnemyControl()
    {
        float waitSeconds;

        while(true)
        {
            if (DetectPlayer())
            {
                _isChasing = true;
                waitSeconds = _playerMemoryTime;
            }
            else
            {
                _isChasing = false;
                waitSeconds = 0f;
            }

            yield return new WaitForSeconds(waitSeconds);
        }
    }
    public void DrawGizmoDisk(Transform t, float radius, Color color)
    {
        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.color = color;
        Gizmos.matrix = Matrix4x4.TRS(t.position, t.rotation, new Vector3(1f, 0.01f, 1f));
        Gizmos.DrawSphere(Vector3.zero, radius);
        Gizmos.matrix = oldMatrix;
    }
    void OnDrawGizmos()
    {
        if (_drawGizmos)
        {
            DrawGizmoDisk(transform, _walkRadiusMax,new Color(0f, 1f, 0f, 0.5f));
            DrawGizmoDisk(transform, _walkRadiusMin,new Color(1f, 0f, 0f, 0.5f));
            DrawGizmoDisk(transform, _detectRadius,new Color(0f, 0f, 1f, 0.5f));
        }
        
    }
}
