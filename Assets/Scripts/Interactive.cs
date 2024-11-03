using UnityEngine;

public class Interactive : MonoBehaviour
{
    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        anim.SetTrigger("Interact");
    }
}
