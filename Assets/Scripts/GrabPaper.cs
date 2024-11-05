using UnityEngine;

public class GrabPaper : MonoBehaviour
{
    private GameObject _mainParent;
    private PageManager _pageManager;
    void Start()
    {
        _mainParent = GetComponentInParent<TAG_MAINPARENT>().gameObject;
        _pageManager = FindAnyObjectByType<PageManager>();
    }
    public void GrabPapers()
    {
        _pageManager.CollectedPage();
        Destroy(_mainParent);
    }
}
