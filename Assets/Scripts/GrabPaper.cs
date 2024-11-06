using UnityEngine;
using UnityEngine.AI;

public class GrabPaper : MonoBehaviour
{
    private GameObject _mainParent;
    private PageManager _pageManager;
    private EnemyMovement[] _enemyMovement;
    void Start()
    {
        _mainParent = GetComponentInParent<TAG_MAINPARENT>().gameObject;
        _pageManager = FindAnyObjectByType<PageManager>();
        _enemyMovement = FindObjectsByType<EnemyMovement>(0);
    }
    public void GrabPapers()
    {
        _pageManager.CollectedPage();
        Destroy(_mainParent);
        _enemyMovement[0].FollowSound(transform.position + transform.forward*2);
        _enemyMovement[1].FollowSound(transform.position + transform.forward*2);
    }
}
