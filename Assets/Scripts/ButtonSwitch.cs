using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{
    private Material _buttonMaterial;
    private bool _isOn;
    void Start()
    {
        _buttonMaterial = GetComponent<MeshRenderer>().material;
    }
    void Update()
    {
        if (_isOn)
            _buttonMaterial.color = Color.green;
        else
            _buttonMaterial.color = Color.red;
    }
    public void Switch()
    {
        _isOn = !_isOn;
    }
}
