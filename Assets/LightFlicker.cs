using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] [Range (0,1)] private float _offChance;
    [SerializeField] private Material _lightOn;
    [SerializeField] private Material _lightOff;
    private MeshRenderer _meshRenderer;
    private Light _light;
    private bool _isOn = true;
    void Start()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _light = GetComponentInChildren<Light>();
    }
    void FixedUpdate()
    {
        float num = Random.Range(0f,1f);

        _isOn = num >= _offChance;

        UpdateLight();
    }
    private void UpdateLight()
    {
        _meshRenderer.material = _isOn ? _lightOn : _lightOff;
        _light.intensity = _isOn ? 1 : 0;
    }
}
