using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using NUnit.Framework.Constraints;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _normalSpeed = 2f;
    [SerializeField] private float _chaseSpeed = 5.5f;
    [SerializeField] private float _walkRadiusMin = 20f;
    [SerializeField] private float _walkRadiusMax = 50f;
    [SerializeField] private float _detectRadius = 10f;
    [SerializeField] private bool _drawGizmos = true;
    private Transform _playerTrans;
    private Rigidbody rb;
    private NavMeshAgent agent;
    private Vector3 pointTarget;
    private bool isChasing = false;
    void Start()
    {
        _playerTrans = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = _normalSpeed;
        pointTarget = rb.position;
        StartCoroutine(EnemyControl());
    }
    void Update()
    {
        UpdateRotation();

        if (isChasing)
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
    void MoveObject(Vector3 target)
    {
        agent.destination = target;
    }
    void UpdateRotation()
    {
        Vector3 targetPosition = new Vector3(_playerTrans.position.x,transform.position.y,_playerTrans.position.z);

        transform.LookAt(targetPosition);
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
        agent.speed = _normalSpeed;
        agent.stoppingDistance = 0f;

        if (rb.position == pointTarget)
            pointTarget = RandomNavmeshLocation(_walkRadiusMin,_walkRadiusMax);

        MoveObject(pointTarget);
        
        Debug.Log($"Walking to ({pointTarget.z},{pointTarget.x})");
    }
    private bool DetectPlayer()
    {
        if (Vector3.Distance(rb.position,_playerTrans.position) <= _detectRadius)
        {
            RaycastHit hit;
            
            if (Physics.Raycast(rb.position,_playerTrans.position - rb.position,out hit))
            {
                Debug.DrawLine(rb.position,_playerTrans.position,new Color(0f,0f,1f));
                Debug.DrawLine(rb.position,hit.collider.gameObject.transform.position,new Color(1f,0f,0f));

                Debug.Log($"{hit.collider.gameObject.name} WAS HIT");

                return hit.collider.gameObject.GetComponentInParent<PlayerMovement>() != null;
            }
        }
        return false;
    }
    private void ChasePlayer()
    {
        agent.speed = _chaseSpeed;
        agent.stoppingDistance = 2f;

        MoveObject(_playerTrans.position);
    }
    private IEnumerator EnemyControl()
    {
        float waitSeconds;

        while(true)
        {
            if (DetectPlayer())
            {
                isChasing = true;
                waitSeconds = 4f;
            }
            else
            {
                isChasing = false;
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
