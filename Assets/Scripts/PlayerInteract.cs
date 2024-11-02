using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float reachDistance = 5;
    private Transform cameraTrans;
    void Start()
    {
        cameraTrans = GetComponentInChildren<Camera>().gameObject.transform;
    }
    void Update()
    {
        CheckForObjectInReach();
    }
    private void CheckForObjectInReach()
    {
        Vector3 originPoint = cameraTrans.position;
        Vector3 reachPoint = cameraTrans.forward;

        RaycastHit objectHit;

        Debug.DrawLine(originPoint, originPoint + (reachPoint * reachDistance), Color.blue);

        if (Physics.Raycast(originPoint, reachPoint, out objectHit, reachDistance))
        {
            if (objectHit.collider.gameObject.GetComponent<Interactive>() != null)
            {
                Debug.DrawLine(originPoint, objectHit.point, Color.red);
                Debug.Log("THERE IS AN OBJECT WITHIN REACH");
            }
        }

    }
}
