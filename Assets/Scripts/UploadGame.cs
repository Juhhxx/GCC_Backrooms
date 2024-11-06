using TMPro;
using UnityEngine;

public class UploadGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tmp;
    private PageManager _pageManager;
    private Animator _anim;
    void Start()
    {
        _pageManager = FindAnyObjectByType<PageManager>();
    }
    public void UseComputer()
    {
        if (_pageManager.CollectedPages == _pageManager.TotalPages)
        {
            _anim.SetTrigger("Upload");
        }
        else
        {
            _tmp.text = "All pages must be collected!";
            _tmp.GetComponentInParent<Animator>().SetTrigger("Show");
            Debug.Log("All pages must be collected!");
        }
    }
}
