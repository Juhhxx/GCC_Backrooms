using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float _reachDistance = 5;
    [SerializeField] private TextMeshProUGUI _tmp;
    [SerializeField] private bool _drawGizmos;
    private Transform _cameraTrans;
    void Start()
    {
        _cameraTrans = GetComponentInChildren<Camera>().gameObject.transform;
    }
    void Update()
    {
        CheckForObjectInReach();
    }
    private void CheckForObjectInReach()
    {
        Vector3 originPoint = _cameraTrans.position;
        Vector3 reachPoint = _cameraTrans.forward;

        RaycastHit objectHit;

        Debug.DrawLine(originPoint, originPoint + (reachPoint * _reachDistance), Color.blue);

        if (Physics.Raycast(originPoint, reachPoint, out objectHit, _reachDistance))
        {
            Interactive interactive = objectHit.collider.gameObject.GetComponent<Interactive>();

            if (interactive != null)
            {
                Debug.DrawLine(originPoint, objectHit.point, Color.red);
                Debug.Log("THERE IS AN OBJECT WITHIN REACH");
                TurnOnInteractText(interactive.DisplayText);
                if (Input.GetKeyDown(KeyCode.E))
                    interactive.Interact();
            }
            else
                TurnOffInteractText();
        }
    }
    private void TurnOnInteractText(string text)
    {
        _tmp.text = text;
        _tmp.gameObject.SetActive(true);
    }
    private void TurnOffInteractText() => _tmp.gameObject.SetActive(false);
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 1f, 0.3f);
        if (_drawGizmos)
            Gizmos.DrawSphere(transform.position + new Vector3(0f,1.65f,0.2f),_reachDistance);
    }
}
