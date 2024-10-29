using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 0.01f;
    private Transform _playerTrans;
    private Rigidbody rb;
    private NavMeshAgent agent;
    void Start()
    {
        _playerTrans = FindAnyObjectByType<PlayerMovement>().gameObject.transform;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        UpdateRotation();
        MoveObject(_playerTrans.position);
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
}
