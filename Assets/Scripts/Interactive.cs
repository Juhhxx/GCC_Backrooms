using UnityEngine;

public class Interactive : MonoBehaviour
{
    Animator _anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        _anim.SetTrigger("Interact");
    }
}
