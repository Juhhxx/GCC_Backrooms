using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _walkRadius = 5f;
    private Transform _playerTrans;
    private Rigidbody rb;
    private NavMeshAgent agent;
    private Vector3 pointTarget;
    void Start()
    {
        _playerTrans = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = _speed;
        pointTarget = rb.position;
    }
    void Update()
    {
        UpdateRotation();
        // MoveObject(RandomNavmeshLocation(5f));
        WalkToPoint();
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
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }
    public void WalkToPoint()
    {
        if (rb.position == pointTarget)
            pointTarget = RandomNavmeshLocation(_walkRadius);

        MoveObject(pointTarget);
        Debug.Log($"Walking to ({pointTarget.z},{pointTarget.x})");
    }
}
