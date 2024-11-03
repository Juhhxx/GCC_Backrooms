using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{
    private Material buttonMaterial;
    private bool isOn;
    void Start()
    {
        buttonMaterial = GetComponent<MeshRenderer>().material;
    }
    void Update()
    {
        if (isOn)
            buttonMaterial.color = Color.green;
        else
            buttonMaterial.color = Color.red;
    }
    public void Switch()
    {
        isOn = !isOn;
    }
}
