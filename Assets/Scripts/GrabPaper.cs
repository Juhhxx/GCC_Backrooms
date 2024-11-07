using UnityEngine;
using UnityEngine.AI;

public class GrabPaper : MonoBehaviour
{
    private GameObject _mainParent;
    private PageManager _pageManager;
    private EnemyMovement[] _enemyMovement;
    [SerializeField] AudioClip _audio;
    void Start()
    {
        _mainParent = GetComponentInParent<TAG_MAINPARENT>().gameObject;
        _pageManager = FindAnyObjectByType<PageManager>();
        _enemyMovement = FindObjectsByType<EnemyMovement>(0);
    }
    public void GrabPapers()
    {
        _pageManager.CollectedPage();
        _enemyMovement[0].FollowSound(transform.position + transform.forward*2);
        _enemyMovement[1].FollowSound(transform.position + transform.forward*2);
        AudioSource.PlayClipAtPoint(_audio,transform.position,1f);
        Destroy(_mainParent);
    }
}
