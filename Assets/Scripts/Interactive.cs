using UnityEngine;

public class Interactive : MonoBehaviour
{
    [SerializeField] private string _displayText;
    public string DisplayText => _displayText;
    Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        _anim.SetTrigger("Interact");
    }
}
